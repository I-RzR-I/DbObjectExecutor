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

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets milliseconds SQL server data type.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside the required range.
        /// </exception>
        /// <param name="paramDbType">The paramDbType to act on.</param>
        /// <returns>
        ///     The milliseconds SQL server data type.
        /// </returns>
        /// =================================================================================================
        internal static string GetMsSqlServerDataType(DbType paramDbType)
        {
            string dataType;
            switch (paramDbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    dataType = "VARCHAR";
                    break;
                case DbType.Object:
                case DbType.Binary:
                    dataType = "VARBINARY";
                    break;
                case DbType.Boolean:
                    dataType = "BIT";
                    break;
                case DbType.Byte:
                case DbType.SByte:
                    dataType = "TINYINT";
                    break;
                case DbType.Currency:
                    dataType = "MONEY";
                    break;
                case DbType.Date:
                case DbType.DateTime:
                    dataType = "DATETIME";
                    break;
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                    dataType = "DATETIME2";
                    break;
                case DbType.Decimal:
                    dataType = "DECIMAL";
                    break;
                case DbType.Double:
                    dataType = "FLOAT";
                    break;
                case DbType.Int16:
                case DbType.UInt16:
                    dataType = "SMALLINT";
                    break;
                case DbType.Int32:
                case DbType.UInt32:
                    dataType = "INT";
                    break;
                case DbType.Int64:
                case DbType.UInt64:
                    dataType = "BIGINT";
                    break;
                case DbType.Single:
                    dataType = "REAL";
                    break;
                case DbType.Guid:
                    dataType = "UNIQUEIDENTIFIER";
                    break;
                case DbType.String:
                    dataType = "NVARCHAR";
                    break;
                case DbType.StringFixedLength:
                    dataType = "NCHAR";
                    break;
                case DbType.Time:
                    dataType = "TIME";
                    break;
                case DbType.VarNumeric:
                    dataType = "NUMERIC";
                    break;
                case DbType.Xml:
                    dataType = "XML";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(paramDbType), paramDbType, null);
            }

            return dataType;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets postgre SQL server data type.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside the required range.
        /// </exception>
        /// <param name="paramDbType">The paramDbType to act on.</param>
        /// <returns>
        ///     The postgre SQL server data type.
        /// </returns>
        /// =================================================================================================
        internal static string GetPostgreSqlServerDataType(DbType paramDbType)
        {
            string dataType;
            switch (paramDbType)
            {
                case DbType.Object:
                case DbType.Binary:
                    dataType = "BYTEA";
                    break;
                case DbType.Boolean:
                    dataType = "BOOLEAN";
                    break;
                case DbType.Currency:
                    dataType = "MONEY";
                    break;
                case DbType.Date:
                case DbType.DateTime:
                    dataType = "DATETIME";
                    break;
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                    dataType = "TIMESTAMP";
                    break;
                case DbType.Decimal:
                case DbType.VarNumeric:
                    dataType = "NUMERIC";
                    break;
                case DbType.Double:
                    dataType = "FLOAT";
                    break;
                case DbType.Byte:
                case DbType.SByte:
                case DbType.Int16:
                case DbType.UInt16:
                    dataType = "SMALLINT";
                    break;
                case DbType.Int32:
                case DbType.UInt32:
                    dataType = "INTEGER";
                    break;
                case DbType.Int64:
                case DbType.UInt64:
                    dataType = "BIGINT";
                    break;
                case DbType.Single:
                    dataType = "REAL";
                    break;
                case DbType.Guid:
                    dataType = "UUID";
                    break;
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    dataType = "TEXT";
                    break;
                case DbType.Time:
                    dataType = "TIME";
                    break;
                case DbType.Xml:
                    dataType = "XML";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(paramDbType), paramDbType, null);
            }

            return dataType;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets oracle server data type.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside the required range.
        /// </exception>
        /// <param name="paramDbType">The paramDbType to act on.</param>
        /// <returns>
        ///     The oracle server data type.
        /// </returns>
        /// =================================================================================================
        internal static string GetOracleServerDataType(DbType paramDbType)
        {
            string dataType;
            switch (paramDbType)
            {
                case DbType.Object:
                case DbType.Binary:
                case DbType.Guid:
                    dataType = "RAW";
                    break;
                case DbType.Date:
                case DbType.DateTime:
                    dataType = "DATE";
                    break;
                case DbType.Time:
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                    dataType = "TIMESTAMP";
                    break;
                case DbType.Currency:
                case DbType.Decimal:
                case DbType.Single:
                case DbType.Double:
                    dataType = "FLOAT";
                    break;
                case DbType.VarNumeric:
                    dataType = "NUMERIC";
                    break;
                case DbType.Boolean:
                case DbType.Byte:
                case DbType.SByte:
                case DbType.Int16:
                case DbType.UInt16:
                case DbType.Int32:
                case DbType.UInt32:
                case DbType.Int64:
                case DbType.UInt64:
                    dataType = "INTEGER";
                    break;
                case DbType.String:
                case DbType.StringFixedLength:
                    dataType = "NCHAR";
                    break;
                case DbType.AnsiString:
                    dataType = "VARCHAR2";
                    break;
                case DbType.AnsiStringFixedLength:
                    dataType = "CHAR";
                    break;
                case DbType.Xml:
                    dataType = "CLOB";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(paramDbType), paramDbType, null);
            }

            return dataType;
        }
    }
}