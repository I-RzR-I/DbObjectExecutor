// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 19:12
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-02-28 19:17
// ***********************************************************************
//  <copyright file="DbTypeConvertHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("DbObjectExecutor.Attribute")]

namespace DbObjectExecutor.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database type convert helper.
    /// </summary>
    /// =================================================================================================
    internal static class DbTypeConvertHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the type map.
        /// </summary>
        /// =================================================================================================
        internal static readonly Dictionary<Type, DbType> _typeMap = new()
        {
            [typeof(byte)] = DbType.Byte,
            [typeof(sbyte)] = DbType.SByte,
            [typeof(short)] = DbType.Int16,
            [typeof(ushort)] = DbType.UInt16,
            [typeof(int)] = DbType.Int32,
            [typeof(uint)] = DbType.UInt32,
            [typeof(long)] = DbType.Int64,
            [typeof(ulong)] = DbType.UInt64,
            [typeof(float)] = DbType.Single,
            [typeof(double)] = DbType.Double,
            [typeof(decimal)] = DbType.Decimal,
            [typeof(bool)] = DbType.Boolean,
            [typeof(string)] = DbType.String,
            [typeof(char)] = DbType.StringFixedLength,
            [typeof(Guid)] = DbType.Guid,
            [typeof(DateTime)] = DbType.DateTime,
            [typeof(DateTime)] = DbType.Date,
            [typeof(DateTime)] = DbType.DateTime2,
            [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
            [typeof(TimeSpan)] = DbType.Time,
            [typeof(byte[])] = DbType.Binary,
            [typeof(byte?)] = DbType.Byte,
            [typeof(sbyte?)] = DbType.SByte,
            [typeof(short?)] = DbType.Int16,
            [typeof(ushort?)] = DbType.UInt16,
            [typeof(int?)] = DbType.Int32,
            [typeof(uint?)] = DbType.UInt32,
            [typeof(long?)] = DbType.Int64,
            [typeof(ulong?)] = DbType.UInt64,
            [typeof(float?)] = DbType.Single,
            [typeof(double?)] = DbType.Double,
            [typeof(decimal?)] = DbType.Decimal,
            [typeof(bool?)] = DbType.Boolean,
            [typeof(char?)] = DbType.StringFixedLength,
            [typeof(Guid?)] = DbType.Guid,
            [typeof(DateTime?)] = DbType.DateTime,
            [typeof(DateTime)] = DbType.Date,
            [typeof(DateTime)] = DbType.DateTime2,
            [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
            [typeof(TimeSpan?)] = DbType.Time,
            [typeof(object)] = DbType.Object
            //[typeof(XDocument)] = DbType.Xml
        };

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts this object to a database type.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///     Thrown when the requested operation is not supported.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>
        ///     Object converted to a database type.
        /// </returns>
        /// =================================================================================================
        internal static DbType ConvertToDbType<T>()
        {
            var t = typeof(T);
            if (_typeMap.TryGetValue(t, out var dbType))
                return dbType;

            throw new NotSupportedException(string.Format(ResourceMessagesHelper.NotSupportedType, t.Name));
        }
    }
}