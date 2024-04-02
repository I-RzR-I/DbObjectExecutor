// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-13 22:45
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-19 20:29
// ***********************************************************************
//  <copyright file="DbObjectExecutorRequestBuilder.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Attribute.Enums;
using DbObjectExecutor.Attribute.Helpers;
using DbObjectExecutor.Attribute.Models;
using DbObjectExecutor.Attribute.Models.Result;
using DbObjectExecutor.Builders;
using DbObjectExecutor.Enums;
using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using DbObjectExecutor.Mapper;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

#endregion

namespace DbObjectExecutor.Attribute.Builders
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object executor request builder.
    /// </summary>
    /// =================================================================================================
    public static class DbObjectExecutorRequestBuilder
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds a request.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have unsupported or illegal values.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside the required range.
        /// </exception>
        /// <param name="dbProviderType">Type of the database provider.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="requestObjectInfo">Information describing the request object.</param>
        /// <param name="requestObjectInfoType">Type of the request object information.</param>
        /// <param name="dbObjectName">(Optional) Name of the database object.</param>
        /// <param name="commandType">Default command executor type.</param>
        /// <param name="useMicrosoftSqlParam">Use 'Microsoft.Data.SqlClient' reference to build SQL parameter.</param>
        /// <returns>
        ///     An IDbObjectBuilder.
        /// </returns>
        /// =================================================================================================
        public static BuildRequestResultDto BuildRequest(
            DbProviderType dbProviderType, DbConnection connection, object requestObjectInfo,
            Type requestObjectInfoType, string dbObjectName = null, DbExecutorType commandType = DbExecutorType.Undefined, bool useMicrosoftSqlParam = false)
        {
            var rawRequestInfo = DbObjectExecutorRequestBuildHelper.BuildDataSourceProcedureCall(requestObjectInfo,
                requestObjectInfoType, dbObjectName, commandType);
            if (rawRequestInfo.IsNull())
                throw new ArgumentException(ResourceMessagesHelper.AnErrorOccurredAndResultIsNull);

            IDbObjectBuilder builder = new DbObjectBuilder();
            var outParams = new List<DbParameter>();

            builder.SetInitInfo(rawRequestInfo.Name, connection, rawRequestInfo.CommandType);
            foreach (var param in rawRequestInfo.Params)
                switch (dbProviderType)
                {
                    case DbProviderType.MsSql:
                        {
                            if (useMicrosoftSqlParam.IsTrue())
                            {
                                var dbParam = BuildMicrosoftSqlParameterInfo(param);
                                if (dbParam.IsNotNull())
                                    builder.SetIn(dbParam);
                                outParams.Add(dbParam);

                            }
                            else
                            {
                                var dbParam = BuildSqlParameterInfo(param);
                                if (dbParam.IsNotNull())
                                    builder.SetIn(dbParam);
                                outParams.Add(dbParam);
                            }
                        }
                        break;
                    case DbProviderType.SqLite:
                        var dbSqlLiteParam = BuildSqlLiteParameterInfo(param);
                        if (dbSqlLiteParam.IsNotNull())
                            builder.SetIn(dbSqlLiteParam);
                        outParams.Add(dbSqlLiteParam);
                        break;
                    case DbProviderType.MySql:
                        var dbMySqlParam = BuildMySqlParameterInfo(param);
                        if (dbMySqlParam.IsNotNull())
                            builder.SetIn(dbMySqlParam);
                        outParams.Add(dbMySqlParam);
                        break;
                    case DbProviderType.PostgreSql:
                        var dbNpgSqlParam = BuildNpgSqlParameterInfo(param);
                        if (dbNpgSqlParam.IsNotNull())
                            builder.SetIn(dbNpgSqlParam);
                        outParams.Add(dbNpgSqlParam);
                        break;
                    case DbProviderType.Oracle:
                        var dbOracleParam = BuildOracleParameterInfo(param);
                        if (dbOracleParam.IsNotNull())
                            builder.SetIn(dbOracleParam);
                        outParams.Add(dbOracleParam);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(dbProviderType), dbProviderType,
                            string.Format(ResourceMessagesHelper.NotSupportedType, dbProviderType));
                }

            return new BuildRequestResultDto { DbObjectBuilder = builder, OutParams = outParams };
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds (Microsoft.Data.SqlClient) SQL parameter information.
        /// </summary>
        /// <param name="paramInfo">Information describing the parameter.</param>
        /// <returns>
        ///     A SqlParameter.
        /// </returns>
        /// =================================================================================================
        private static Microsoft.Data.SqlClient.SqlParameter BuildMicrosoftSqlParameterInfo(DbObjectExecutorRequestParamDto paramInfo)
        {
            var param = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = paramInfo.Name,
                Value = paramInfo.Value ?? DBNull.Value,
                Direction = EnumMapper.MapToParameterDirection(paramInfo.Direction),
                DbType = (DbType)paramInfo.DbType!,
                Size = paramInfo.Size ?? 0,
                Precision = paramInfo.Precision ?? 0,
                Scale = paramInfo.Scale ?? 0
            };

            return param;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds SQL parameter information.
        /// </summary>
        /// <param name="paramInfo">Information describing the parameter.</param>
        /// <returns>
        ///     A SqlParameter.
        /// </returns>
        /// =================================================================================================
        private static System.Data.SqlClient.SqlParameter BuildSqlParameterInfo(DbObjectExecutorRequestParamDto paramInfo)
        {
            var param = new System.Data.SqlClient.SqlParameter
            {
                ParameterName = paramInfo.Name,
                Value = paramInfo.Value ?? DBNull.Value,
                Direction = EnumMapper.MapToParameterDirection(paramInfo.Direction),
                DbType = (DbType)paramInfo.DbType!,
                Size = paramInfo.Size ?? 0,
                Precision = paramInfo.Precision ?? 0,
                Scale = paramInfo.Scale ?? 0
            };

            return param;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds my SQL parameter information.
        /// </summary>
        /// <param name="paramInfo">Information describing the parameter.</param>
        /// <returns>
        ///     A MySqlParameter.
        /// </returns>
        /// =================================================================================================
        private static MySqlParameter BuildMySqlParameterInfo(DbObjectExecutorRequestParamDto paramInfo)
        {
            var param = new MySqlParameter
            {
                ParameterName = paramInfo.Name,
                Value = paramInfo.Value ?? DBNull.Value,
                Direction = EnumMapper.MapToParameterDirection(paramInfo.Direction),
                DbType = (DbType)paramInfo.DbType!,
                Size = paramInfo.Size ?? 0,
                Precision = paramInfo.Precision ?? 0,
                Scale = paramInfo.Scale ?? 0
            };

            return param;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds npg SQL parameter information.
        /// </summary>
        /// <param name="paramInfo">Information describing the parameter.</param>
        /// <returns>
        ///     A NpgsqlParameter.
        /// </returns>
        /// =================================================================================================
        private static NpgsqlParameter BuildNpgSqlParameterInfo(DbObjectExecutorRequestParamDto paramInfo)
        {
            var param = new NpgsqlParameter
            {
                ParameterName = paramInfo.Name,
                Value = paramInfo.Value ?? DBNull.Value,
                Direction = EnumMapper.MapToParameterDirection(paramInfo.Direction),
                DbType = (DbType)paramInfo.DbType!,
                Size = paramInfo.Size ?? 0,
                Precision = paramInfo.Precision ?? 0,
                Scale = paramInfo.Scale ?? 0
            };

            return param;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds oracle parameter information.
        /// </summary>
        /// <param name="paramInfo">Information describing the parameter.</param>
        /// <returns>
        ///     An OracleParameter.
        /// </returns>
        /// =================================================================================================
        private static OracleParameter BuildOracleParameterInfo(DbObjectExecutorRequestParamDto paramInfo)
        {
            var param = new OracleParameter
            {
                ParameterName = paramInfo.Name,
                Value = paramInfo.Value ?? DBNull.Value,
                Direction = EnumMapper.MapToParameterDirection(paramInfo.Direction),
                DbType = (DbType)paramInfo.DbType!,
                Size = paramInfo.Size ?? 0,
                Precision = paramInfo.Precision ?? 0,
                Scale = paramInfo.Scale ?? 0
            };

            return param;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds SQL lite parameter information.
        /// </summary>
        /// <param name="paramInfo">Information describing the parameter.</param>
        /// <returns>
        ///     A SqliteParameter.
        /// </returns>
        /// =================================================================================================
        private static SqliteParameter BuildSqlLiteParameterInfo(DbObjectExecutorRequestParamDto paramInfo)
        {
            var param = new SqliteParameter
            {
                ParameterName = paramInfo.Name,
                Value = paramInfo.Value ?? DBNull.Value,
                Direction = EnumMapper.MapToParameterDirection(paramInfo.Direction),
                DbType = (DbType)paramInfo.DbType!,
                Size = paramInfo.Size ?? 0,
                Precision = paramInfo.Precision ?? 0,
                Scale = paramInfo.Scale ?? 0
            };

            return param;
        }
    }
}