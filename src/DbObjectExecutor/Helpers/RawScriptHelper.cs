// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2025-05-22 21:19
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-06-02 21:55
// ***********************************************************************
//  <copyright file="RawScriptHelper.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Extensions;
using DbObjectExecutor.Extensions.System.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

#endregion

namespace DbObjectExecutor.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A raw script helper.
    /// </summary>
    /// =================================================================================================
    internal static class RawScriptHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to the milliseconds SQL server procedure script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsMsSqlServerProcedureScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();
            script.AppendLine($"USE [{dbCommand.Connection!.Database}];");

            try
            {
                var outDeclarationParams = new StringBuilder();
                var spParams = new StringBuilder();
                var spOutSelectParams = new StringBuilder();

                script.AppendLine("DECLARE @return_value INT;");

                var paramCount = dbCommand.Parameters.Count;
                for (var paramIdx = 0; paramIdx < paramCount; paramIdx++)
                {
                    var isLastParam = paramIdx == paramCount - 1;
                    var tab = $"{(paramIdx == 0 ? "\t" : "\t,")}";
                    var param = dbCommand.Parameters[paramIdx];
                    var paramName = param.ParameterName.StartsWith("@") ? param.ParameterName : $"@{param.ParameterName}";
                    var paramDataType = DbTypeConvertHelper.GetMsSqlServerDataType(param.DbType);
                    if (param.DbType.IsTextType())
                        paramDataType = $"{paramDataType}({param.Size})";

                    //  Build declaration param
                    if (new List<ParameterDirection>
                        {
                            ParameterDirection.InputOutput,
                            ParameterDirection.Output
                        }.Contains(param.Direction))
                    {
                        var outParamDefaultValue = param.Direction == ParameterDirection.Output
                            ? "NULL"
                            : param.ParameterValueAsSql();

                        outDeclarationParams.AppendLine($"DECLARE {paramName} {paramDataType} = {outParamDefaultValue};");
                        if (isLastParam)
                            outDeclarationParams.AppendLine();

                        if (spOutSelectParams.Length == 0)
                            spOutSelectParams.Append("SELECT ");

                        spOutSelectParams.Append($"{paramName} AS N'{paramName}'");

                        switch (isLastParam)
                        {
                            case false:
                                spOutSelectParams.Append(", ");
                                break;
                            case true:
                                spOutSelectParams.Append(";");
                                spOutSelectParams.AppendLine();
                                break;
                        }
                    }

                    if (param.Direction == ParameterDirection.Input)
                        spParams.AppendLine($"{tab}{paramName} = {param.ParameterValueAsSql()}");

                    if (param.Direction == ParameterDirection.Output)
                        spParams.AppendLine($"{tab}{paramName} = {paramName} OUTPUT{(isLastParam ? ";" : "")}");

                    if (isLastParam)
                        spParams.AppendLine();
                }

                script.Append(outDeclarationParams);
                script.AppendLine($"EXEC @return_value = [{dbCommand.CommandText}]");
                script.Append(spParams);
                script.Append(spOutSelectParams);

                script.AppendLine("SELECT 'ReturnValue' = @return_value;");
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to the milliseconds SQL server function script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsMsSqlServerFunctionScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();
            script.AppendLine($"USE [{dbCommand.Connection!.Database}];");

            try
            {
                script.AppendLine("DECLARE @FuncResult INT;");
                script.AppendLine($"EXEC @FuncResult = [{dbCommand.CommandText}];");
                script.AppendLine("SELECT 'FunctionResult' = @FuncResult;");
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to a postgre SQL server procedure script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsPostgreSqlServerProcedureScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();

            try
            {
                script.AppendLine("DO $$");

                var outParams = new StringBuilder();
                var spParams = new StringBuilder();
                var paramCount = dbCommand.Parameters.Count;
                for (var paramIdx = 0; paramIdx < paramCount; paramIdx++)
                {
                    var isLastParam = paramIdx == paramCount - 1;
                    var param = dbCommand.Parameters[paramIdx];
                    var paramDataType = DbTypeConvertHelper.GetPostgreSqlServerDataType(param.DbType);
                    var paramValue = param.ParameterValueAsSql();
                    var paramValueWithType = $"{paramValue}::{paramDataType}";

                    //  Build declaration param
                    if (new List<ParameterDirection>
                        {
                            ParameterDirection.InputOutput,
                            ParameterDirection.Output
                        }.Contains(param.Direction))
                    {
                        outParams.AppendLine($"DECLARE {param.ParameterName} {paramDataType};");
                        spParams.Append(param.Value == null ? param.ParameterName : paramValueWithType);
                    }
                    else
                    {
                        spParams.Append(paramValueWithType);
                    }

                    if (isLastParam.IsFalse())
                        spParams.Append(",");
                }

                script.Append(outParams);

                script.AppendLine("BEGIN");
                script.Append($"CALL {dbCommand.CommandText}");
                script.Append("(");
                script.Append(spParams);
                script.Append(");");

                script.AppendLine("END $$;");
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to a postgre SQL server function script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsPostgreSqlServerFunctionScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();

            try
            {
                var spParams = new StringBuilder();

                var paramCount = dbCommand.Parameters.Count;
                for (var paramIdx = 0; paramIdx < paramCount; paramIdx++)
                {
                    var isLastParam = paramIdx == paramCount - 1;
                    var param = dbCommand.Parameters[paramIdx];
                    var paramDataType = DbTypeConvertHelper.GetPostgreSqlServerDataType(param.DbType);
                    var paramValue = param.ParameterValueAsSql();
                    var paramValueWithType = $"{paramValue}::{paramDataType}";

                    //  Build declaration param
                    spParams.Append(paramValueWithType);

                    if (isLastParam.IsFalse())
                        spParams.Append(",");
                }

                script.Append($"SELECT {dbCommand.CommandText}");
                script.Append("(");
                script.Append(spParams);
                script.Append(");");
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to an oracle server procedure script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsOracleServerProcedureScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();

            try
            {
                var outDeclarationParams = new StringBuilder();
                var spParams = new StringBuilder();
                var spOutSelectParams = new StringBuilder();

                var isAddedOutDeclareClause = false;
                var paramCount = dbCommand.Parameters.Count;

                const string cleanTab = "\t";
                for (var paramIdx = 0; paramIdx < paramCount; paramIdx++)
                {
                    var tab = $"{(paramIdx == 0 ? "\t" : "\t,")}";
                    var param = dbCommand.Parameters[paramIdx];
                    var paramName = param.ParameterName;
                    var paramDataType = DbTypeConvertHelper.GetOracleServerDataType(param.DbType);
                    if (param.DbType.IsTextType())
                        paramDataType = $"{paramDataType}({param.Size})";

                    //  Build declaration param
                    if (new List<ParameterDirection>
                        {
                            ParameterDirection.InputOutput,
                            ParameterDirection.Output
                        }.Contains(param.Direction))
                    {
                        var outParamDefaultValue = param.Direction == ParameterDirection.Output
                            ? "NULL"
                            : param.ParameterValueAsSql();

                        if (isAddedOutDeclareClause.IsFalse())
                        {
                            outDeclarationParams.AppendLine("DECLARE ");
                            isAddedOutDeclareClause = true;
                        }

                        outDeclarationParams.AppendLine($"{cleanTab}{paramName} {paramDataType} := {outParamDefaultValue};");

                        spOutSelectParams.AppendLine($"{cleanTab}DBMS_OUTPUT.PUT_LINE('{paramName}: ' || {paramName});");
                    }

                    if (param.Direction == ParameterDirection.Input)
                        spParams.AppendLine($"{cleanTab}{tab}{paramName} => {param.ParameterValueAsSql()}");

                    if (param.Direction == ParameterDirection.Output)
                        spParams.AppendLine($"{cleanTab}{tab}{paramName} => {paramName}");
                }

                script.Append(outDeclarationParams);
                script.AppendLine("BEGIN");
                script.Append($"{cleanTab}{dbCommand.CommandText}");
                script.Append("(");
                script.AppendLine();
                script.Append(spParams);
                script.Append($"{cleanTab});");
                script.AppendLine("\n");
                script.Append(spOutSelectParams);
                script.AppendLine("END;");
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to an oracle server function script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsOracleServerFunctionScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();
            try
            {
                var returnDeclarationParams = new StringBuilder();
                var spParams = new StringBuilder();

                var isAddedOutDeclareClause = false;
                var paramCount = dbCommand.Parameters.Count;

                const string cleanTab = "\t";
                var returnParamName = "";

                for (var paramIdx = 0; paramIdx < paramCount; paramIdx++)
                {
                    var isLastParam = paramIdx == paramCount - 1;
                    var param = dbCommand.Parameters[paramIdx];
                    var paramName = param.ParameterName;
                    var paramDataType = DbTypeConvertHelper.GetOracleServerDataType(param.DbType);
                    if (param.DbType.IsTextType())
                        paramDataType = $"{paramDataType}({param.Size})";

                    //  Build declaration param
                    if (new List<ParameterDirection> { ParameterDirection.ReturnValue }.Contains(param.Direction))
                    {
                        returnParamName = paramName;
                        var outParamDefaultValue = param.Direction == ParameterDirection.Output
                            ? "NULL"
                            : param.ParameterValueAsSql();

                        if (isAddedOutDeclareClause.IsFalse())
                        {
                            returnDeclarationParams.AppendLine("DECLARE ");
                            isAddedOutDeclareClause = true;
                        }

                        returnDeclarationParams.AppendLine($"{cleanTab}{paramName} {paramDataType} := {outParamDefaultValue};");
                    }

                    if (param.Direction == ParameterDirection.Input)
                        spParams.AppendLine($"{param.ParameterValueAsSql()}");

                    if (isLastParam.IsFalse())
                        spParams.Append(",");
                }

                script.Append(returnDeclarationParams);
                script.AppendLine("BEGIN");
                script.Append($"{cleanTab}{returnParamName} := {dbCommand.CommandText}");
                script.Append("(");
                script.AppendLine();
                script.Append(spParams);
                script.Append($"{cleanTab});");
                script.AppendLine();
                script.AppendLine("END;");
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to my SQL server procedure script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsMySqlServerProcedureScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();

            try
            {
                var outDeclarationParams = new StringBuilder();
                var spParams = new StringBuilder();
                var spOutSelectParams = new StringBuilder();

                var paramCount = dbCommand.Parameters.Count;
                for (var paramIdx = 0; paramIdx < paramCount; paramIdx++)
                {
                    var isLastParam = paramIdx == paramCount - 1;
                    var tab = $"{(paramIdx == 0 ? "\t" : "\t,")}";
                    var param = dbCommand.Parameters[paramIdx];
                    var paramName = param.ParameterName.StartsWith("@") ? param.ParameterName : $"@{param.ParameterName}";

                    //  Build declaration param
                    if (new List<ParameterDirection> { ParameterDirection.InputOutput, ParameterDirection.Output }.Contains(param.Direction))
                    {
                        var outParamDefaultValue = param.Direction == ParameterDirection.Output
                            ? "NULL"
                            : param.ParameterValueAsSql();

                        outDeclarationParams.AppendLine($"SET {paramName} = {outParamDefaultValue};");
                        if (isLastParam)
                            outDeclarationParams.AppendLine();

                        if (spOutSelectParams.Length == 0)
                            spOutSelectParams.Append("SELECT ");

                        spOutSelectParams.Append(paramName);

                        switch (isLastParam)
                        {
                            case false:
                                spOutSelectParams.Append(", ");
                                break;
                            case true:
                                spOutSelectParams.Append(";");
                                spOutSelectParams.AppendLine();
                                break;
                        }
                    }

                    if (param.Direction == ParameterDirection.Input)
                        spParams.AppendLine($"{tab}{param.ParameterValueAsSql()}");

                    if (new List<ParameterDirection>
                        {
                            ParameterDirection.InputOutput,
                            ParameterDirection.Output
                        }.Contains(param.Direction))
                        spParams.AppendLine($"{tab}{paramName}");
                }

                script.Append(outDeclarationParams);
                script.AppendLine($"CALL {dbCommand.CommandText}");
                script.Append("(");
                script.AppendLine();
                script.Append(spParams);
                script.Append(");");
                script.AppendLine("\n");
                script.Append(spOutSelectParams);
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Converts a dbCommand to my SQL server function script.
        /// </summary>
        /// <param name="dbCommand">[in,out] The database command.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        internal static string AsMySqlServerFunctionScript(ref DbCommand dbCommand)
        {
            var script = new StringBuilder();
            try
            {
                var returnDeclarationParams = new StringBuilder();
                var spParams = new StringBuilder();

                var isAddedOutDeclareClause = false;
                var paramCount = dbCommand.Parameters.Count;

                const string cleanTab = "\t";
                var returnParamName = "";

                for (var paramIdx = 0; paramIdx < paramCount; paramIdx++)
                {
                    var isLastParam = paramIdx == paramCount - 1;
                    var param = dbCommand.Parameters[paramIdx];
                    var paramName = param.ParameterName;
                    var paramDataType = DbTypeConvertHelper.GetOracleServerDataType(param.DbType);
                    if (param.DbType.IsTextType())
                        paramDataType = $"{paramDataType}({param.Size})";

                    //  Build declaration param
                    if (new List<ParameterDirection> { ParameterDirection.ReturnValue }.Contains(param.Direction))
                    {
                        returnParamName = paramName;
                        var outParamDefaultValue = param.Direction == ParameterDirection.Output
                            ? "NULL"
                            : param.ParameterValueAsSql();

                        if (isAddedOutDeclareClause.IsFalse())
                        {
                            returnDeclarationParams.AppendLine("SET ");
                            isAddedOutDeclareClause = true;
                        }

                        returnDeclarationParams.AppendLine($"{cleanTab}{paramName} {paramDataType} := {outParamDefaultValue};");
                    }

                    if (param.Direction == ParameterDirection.Input)
                        spParams.AppendLine($"{param.ParameterValueAsSql()}");

                    if (isLastParam.IsFalse())
                        spParams.Append(",");
                }

                script.Append(returnDeclarationParams);
                script.Append($"{returnParamName} := {dbCommand.CommandText}");
                script.Append("(");
                script.AppendLine();
                script.Append(spParams);
                script.Append($"{cleanTab});");
            }
            catch {/* ignored */}

            return script.ToString();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     An IDataParameter extension method that parameter value as SQL.
        /// </summary>
        /// <param name="sp">The sp to act on.</param>
        /// <returns>
        ///     A string.
        /// </returns>
        /// =================================================================================================
        private static string ParameterValueAsSql(this IDataParameter sp)
        {
            if (sp.DbType.IsNumberType())
                return sp.Value?.ToString();

            if (sp.DbType.IsTextType())
                return $"'{sp.Value?.ToString()?.Replace("'", "''")}'";

            return sp.Value?.ToString()?.Replace("'", "''");
        }
    }
}