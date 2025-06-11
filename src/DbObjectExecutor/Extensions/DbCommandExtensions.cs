// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-04 15:02
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-04 15:05
// ***********************************************************************
//  <copyright file="DbCommandExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Builders;
using DbObjectExecutor.Enums;
using DbObjectExecutor.Helpers;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

#endregion

namespace DbObjectExecutor.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database command extensions.
    /// </summary>
    /// =================================================================================================
    internal static class DbCommandExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A DbCommand extension method that adds an inner parameter.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="dbCommand">The dbCommand to act on.</param>
        /// <param name="name">The name.</param>
        /// <param name="val">The value.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="defaultValue">(Optional) The default value.</param>
        /// <param name="size">(Optional) The size.</param>
        /// <param name="precision">(Optional) The precision.</param>
        /// <param name="scale">(Optional) The scale.</param>
        /// <returns>
        ///     A DbParameter.
        /// </returns>
        /// =================================================================================================
        internal static DbParameter AddInnerParam<T>(
            this DbCommand dbCommand, string name, T val,
            ParameterDirection direction, T defaultValue = default, int size = 0, byte precision = 0, byte scale = 0)
        {
            if (name.IsNull())
                throw new ArgumentNullException(nameof(name));

            var param = dbCommand.CreateParameter();
            param.ParameterName = name;
            param.Value = (object)val ?? DBNull.Value;
            param.Direction = direction;
            param.DbType = DbTypeConvertHelper.ConvertToDbType<T>();
            param.Size = size;
            param.Precision = precision;
            param.Scale = scale;

            dbCommand.Parameters.Add(param);

            return param;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A DbCommand extension method that adds an output parameter inner.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="dbCommand">The dbCommand to act on.</param>
        /// <param name="name">The name.</param>
        /// <param name="val">The value.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="defaultValue">(Optional) The default value.</param>
        /// <param name="size">(Optional) The size.</param>
        /// <param name="precision">(Optional) The precision.</param>
        /// <param name="scale">(Optional) The scale.</param>
        /// <returns>
        ///     An OutputParam&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        internal static OutputParam<T> AddOutputParamInner<T>(
            this DbCommand dbCommand, string name, T val,
            ParameterDirection direction, T defaultValue = default, int size = 0, byte precision = 0, byte scale = 0)
        {
            var param = dbCommand.AddInnerParam(name, val, direction, defaultValue, size, precision, scale);

            return new OutputParam<T>(param, defaultValue);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A DbCommand extension method that converts this object to a raw SQL.
        /// </summary>
        /// <param name="dbCommand">The dbCommand to act on.</param>
        /// <param name="executorType"></param>
        /// <param name="dbProviderId">
        ///     Identifier for the database provider.
        ///     Is type of DbObjectExecutor.Attribute.Enums.DbProviderType.
        /// </param>
        /// <remarks>
        /// </remarks>
        /// <returns>
        ///     A database command as raw string.
        /// </returns>
        /// =================================================================================================
        internal static string AsRawSql(this DbCommand dbCommand, DbExecutorType executorType, int dbProviderId)
        {
            var sb = new StringBuilder();

            try
            {
                switch (executorType)
                {
                    case DbExecutorType.FunctionAsProcedure:
                        switch (dbProviderId)
                        {
                            case 0:
                                sb.Append(RawScriptHelper.AsMsSqlServerFunctionScript(ref dbCommand));
                                break;
                            case 2:
                                sb.Append(RawScriptHelper.AsPostgreSqlServerFunctionScript(ref dbCommand));
                                break;
                            case 3:
                                sb.Append(RawScriptHelper.AsOracleServerFunctionScript(ref dbCommand));
                                break;
                            case 4:
                                sb.Append(RawScriptHelper.AsMySqlServerFunctionScript(ref dbCommand));
                                break;
                        }
                        break;
                    case DbExecutorType.Procedure:
                        switch (dbProviderId)
                        {
                            case 0:
                                sb.Append(RawScriptHelper.AsMsSqlServerProcedureScript(ref dbCommand));
                                break;
                            case 2:
                                sb.Append(RawScriptHelper.AsPostgreSqlServerProcedureScript(ref dbCommand));
                                break;
                            case 3:
                                sb.Append(RawScriptHelper.AsOracleServerProcedureScript(ref dbCommand));
                                break;
                            case 4:
                                sb.Append(RawScriptHelper.AsMySqlServerProcedureScript(ref dbCommand));
                                break;
                        }
                        break;
                    default:
                        sb.AppendLine(dbCommand.CommandText);
                        break;
                }
            }
            catch {/* ignored */}

            return sb.ToString();
        }
    }
}