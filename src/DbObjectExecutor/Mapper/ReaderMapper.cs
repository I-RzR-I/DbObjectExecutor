// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="ReaderMapper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using DbObjectExecutor.Mapper.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable StaticMemberInGenericType

#endregion

namespace DbObjectExecutor.Mapper
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A reader mapper.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// =================================================================================================
    internal class ReaderMapper<T> where T : class, new()
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the properties cache.
        /// </summary>
        /// =================================================================================================
        private static readonly ConcurrentDictionary<int, ReaderProperty[]> PropertiesCache = new();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the properties.
        /// </summary>
        /// =================================================================================================
        private readonly ReaderProperty[] _properties;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the reader.
        /// </summary>
        /// =================================================================================================
        private readonly DbDataReader _reader;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReaderMapper{T}" /> class.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// =================================================================================================
        internal ReaderMapper(DbDataReader reader)
        {
            _reader = reader;
            _properties = MapColumnsToProperties();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Maps the given action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// =================================================================================================
        internal void Map(Action<T> action)
        {
            while (_reader.Read())
            {
                var row = MapNextRow();
                action(row);
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Map asynchronous.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>
        ///     A Task.
        /// </returns>
        /// =================================================================================================
        internal Task MapAsync(Action<T> action) => MapAsync(action, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Map asynchronous.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A Task.
        /// </returns>
        /// =================================================================================================
        internal async Task MapAsync(Action<T> action, CancellationToken cancellationToken)
        {
            while (await _reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var row = await MapNextRowAsync(cancellationToken).ConfigureAwait(false);
                action(row);
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Map next row.
        /// </summary>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        internal T MapNextRow()
        {
            var row = new T();
            for (var i = 0; i < _properties.Length; ++i)
            {
                var value = _reader.IsDBNull(_properties[i].Idx) ? null : _reader.GetValue(_properties[i].Idx);
                _properties[i].Setter(row, value);
            }

            return row;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Map next row asynchronous.
        /// </summary>
        /// <returns>
        ///     The map next row.
        /// </returns>
        /// =================================================================================================
        internal Task<T> MapNextRowAsync() => MapNextRowAsync(CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Map next row asynchronous.
        /// </summary>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     The map next row.
        /// </returns>
        /// =================================================================================================
        internal async Task<T> MapNextRowAsync(CancellationToken cancellationToken)
        {
            var row = new T();
            for (var i = 0; i < _properties.Length; ++i)
            {
                var value = await _reader.IsDBNullAsync(_properties[i].Idx, cancellationToken).ConfigureAwait(false)
                    ? null
                    : _reader.GetValue(_properties[i].Idx);
                _properties[i].Setter(row, value);
            }

            return row;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Calculates the property key.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <returns>
        ///     The calculated property key.
        /// </returns>
        /// =================================================================================================
        private static int ComputePropertyKey(IEnumerable<string> columns)
        {
            unchecked
            {
                var hashCode = 17;
                foreach (var column in columns)
                    hashCode = (hashCode * 31) + column.GetHashCode();

                return hashCode;
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Map columns to properties.
        /// </summary>
        /// <returns>
        ///     A ReaderProperty[].
        /// </returns>
        /// =================================================================================================
        private ReaderProperty[] MapColumnsToProperties()
        {
            var sourceType = typeof(T);
            var columns = new string[_reader.FieldCount];

            var sourcePropertyInfo = new List<PropertyMapDto>(sourceType.GetPropertyInfos().WithIndex()
                .Select(x => new PropertyMapDto()
                {
                    SourceName = x.item.Name,
                    Property = x.item,
                    AttributeName = DbObjectColumnAttributeHelper.GetDbObjectColumnName(x.item),
                    IsInResponse = false
                }));


            for (var i = 0; i < _reader.FieldCount; i++)
            {
                var columnName = _reader.GetName(i);
                columns[i] = columnName;
                var sourceProp = sourcePropertyInfo.FirstOrDefault(x => x.AttributeName == columnName);
                if (sourceProp.IsNotNull())
                {
                    sourceProp!.IsInResponse = true;
                }
            }

            //  Create result property list hash
            var propKey = ComputePropertyKey(columns);
            if (PropertiesCache.TryGetValue(propKey, out var propValue))
                return propValue;

            var properties = new ReaderProperty[columns.Length];
            for (var i = 0; i < columns.Length; i++)
            {
                var property = sourcePropertyInfo.FirstOrDefault(x => x.AttributeName == columns[i].Replace("_", "") 
                                                                      && x.IsInResponse.IsTrue());
                if (property.IsNotNull())
                {
                    var setter = (Action<object, object>)ExpressionBuildHelper.BuildPropertySetter(property!.Property);

                    properties[i] = new ReaderProperty { Idx = i, Setter = setter, Name = property.SourceName };
                }
            }

            PropertiesCache[propKey] = properties;

            return properties;
        }
    }
}