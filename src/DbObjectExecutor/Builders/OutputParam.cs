// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 19:59
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-02-28 20:22
// ***********************************************************************
//  <copyright file="OutputParam.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions.Params;
using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using System;
using System.Data.Common;

#endregion

namespace DbObjectExecutor.Builders
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     An output parameter.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <seealso cref="T:DbObjectExecutor.Abstractions.Params.IOutputParam{T}"/>
    ///
    /// ### <inheritdoc cref="IOutputParam{T}"/>
    /// =================================================================================================
    internal class OutputParam<T> : IOutputParam<T>
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the database parameter.
        /// </summary>
        /// =================================================================================================
        private readonly DbParameter _dbParam;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default value.
        /// </summary>
        /// =================================================================================================
        private readonly T _defaultValue;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="OutputParam{T}" /> class.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="defaultValue">(Optional) The default value.</param>
        /// =================================================================================================
        public OutputParam(DbParameter param, T defaultValue = default)
        {
            _dbParam = param;
            _defaultValue = defaultValue;
        }

        /// <inheritdoc/>
        public T Value
        {
            get
            {
                if (_dbParam.Value is DBNull)
                    return default(T).IsNull()
                        ? _defaultValue//default
                        : throw new InvalidOperationException(string.Format(ResourceMessagesHelper.UnAssignableNullToNotNullable, _dbParam.ParameterName));

                var nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(T));
                if (nullableUnderlyingType.IsNotNull())
                    return (T)Convert.ChangeType(_dbParam.Value, nullableUnderlyingType!);

                return (T)Convert.ChangeType(_dbParam.Value, typeof(T));
            }
        }

        /// <inheritdoc/>
        public override string ToString() => _dbParam.Value.ToString();
    }
}