// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-12 20:58
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-13 00:33
// ***********************************************************************
//  <copyright file="DbObjectExecutorRequestBuildHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Attribute.Attributes;
using DbObjectExecutor.Attribute.Models;
using DbObjectExecutor.Enums;
using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace DbObjectExecutor.Attribute.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object executor request build helper.
    /// </summary>
    /// =================================================================================================
    public static class DbObjectExecutorRequestBuildHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds data source procedure call.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is invalid.
        /// </exception>
        /// <param name="procedureRequest">The procedure request.</param>
        /// <param name="requestOrResultType">Type of the request or result.</param>
        /// <param name="procedureName">(Optional) Name of the procedure.</param>
        /// <param name="commandType">Default executor/command type.</param>
        /// <returns>
        ///     A DbObjectExecutorRequestCallDto.
        /// </returns>
        /// =================================================================================================
        public static DbObjectExecutorRequestCallDto BuildDataSourceProcedureCall(
            object procedureRequest, Type requestOrResultType,
            string procedureName = null, DbExecutorType commandType = DbExecutorType.Undefined)
        {
            var checkType = procedureRequest.IsNull()
                ? requestOrResultType
                : procedureRequest.GetType();

            var dbObjectName = GetDbObjectExecuteName(procedureName, checkType, commandType);
            if (dbObjectName.IsNull())
                throw new InvalidOperationException(ResourceMessagesHelper.DbObjectNameIsWithoutValue);

            var result = new DbObjectExecutorRequestCallDto
            {
                Name = dbObjectName!.Value!.Item1,
                CommandType = dbObjectName!.Value!.Item2
            };

            if (requestOrResultType.IsNotNull())
            {
                var paramList = new List<DbObjectExecutorRequestParamDto>();
                var requestProps = requestOrResultType.GetPropertyInfos();
                if (requestProps.IsNotNull())
                    foreach (var property in requestProps)
                    {
                        var attribute = property?.GetCustomAttribute<DbObjectParamAttribute>();
                        if (attribute.IsNotNull())
                        {
                            var propValue = property!.GetValue(procedureRequest, null);
                            if (attribute!.Direction == DbParamDirectionType.Input
                                && property.PropertyType.IsStringPropType()
                                && (propValue.IsNull() || propValue!.ToString().IsNullOrEmpty()))
                                propValue = attribute!.DefaultValue;

                            var paramItem = new DbObjectExecutorRequestParamDto
                            {
                                Direction = attribute.Direction,
                                Name = attribute.Name,
                                DbType = DbTypeConvertHelper._typeMap
                                    .FirstOrDefault(x => x.Key == property.PropertyType).Value,
                                Value = propValue,
                                Precision = attribute.Precision,
                                Scale = attribute.Scale,
                                Size = attribute.Size
                            };
                            paramList.Add(paramItem);
                        }
                    }

                result.Params = paramList;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets database object execute name.
        /// </summary>
        /// <param name="dbObjectName">Name of the FB object.</param>
        /// <param name="requestOrResultType">Type of the request or result.</param>
        /// <param name="commandType">(Optional) DB command type</param>
        /// <returns>
        ///     The database object execute name.
        /// </returns>
        /// =================================================================================================
        private static (string, DbExecutorType)? GetDbObjectExecuteName(string dbObjectName, Type requestOrResultType,
            DbExecutorType commandType = DbExecutorType.Undefined)
        {
            if (dbObjectName.IsNullOrEmpty().IsFalse() && commandType != DbExecutorType.Undefined)
                return (dbObjectName, commandType);

            if (requestOrResultType.IsNotNull())
            {
                var requestDbObject = DbObjectExecutorAttributeHelper.GetDbObjectNameAttribute(requestOrResultType);
                if (requestDbObject.IsNotNull())
                    return (requestDbObject.Name, requestDbObject.CommandType);
            }

            return null;
        }
    }
}