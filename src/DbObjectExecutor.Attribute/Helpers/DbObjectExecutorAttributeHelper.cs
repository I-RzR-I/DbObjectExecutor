// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-12 21:06
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-12 21:34
// ***********************************************************************
//  <copyright file="DbObjectExecutorAttributeHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Attribute.Attributes;
using DbObjectExecutor.Attributes;
using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using System;
using System.Linq.Expressions;
using System.Reflection;

#endregion

// ReSharper disable PossibleInvalidOperationException

namespace DbObjectExecutor.Attribute.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object executor attribute helper.
    /// </summary>
    /// =================================================================================================
    public static class DbObjectExecutorAttributeHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Get [DbObjectName] database object name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="objectType">Database object type.</param>
        /// <returns>
        ///     Return name of custom database object name.
        /// </returns>
        /// =================================================================================================
        public static string GetDbObjectName(Type objectType)
        {
            if (objectType.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(objectType));

            var attribute = objectType.GetCustomAttribute<DbObjectNameAttribute>(false);

            return attribute?.Name ?? string.Empty;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Get [DbObjectName] database object (Attribute)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="objectType">Database object type.</param>
        /// <returns>
        ///     Return custom database object detail (Attribute)
        /// </returns>
        /// =================================================================================================
        public static DbObjectNameAttribute GetDbObjectNameAttribute(Type objectType)
        {
            if (objectType.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(objectType));

            var attribute = objectType.GetCustomAttribute<DbObjectNameAttribute>(false);

            return attribute;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Get [DbObjectParam] property name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Property expression type.</typeparam>
        /// <param name="property">Property expression.</param>
        /// <returns>
        ///     Return name of custom database object param name.
        /// </returns>
        /// =================================================================================================
        public static string GetDbObjectParam<T>(Expression<Func<T>> property)
        {
            if (property.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(property));

            var lambda = property as LambdaExpression;
            var memberExpression = lambda.Body is UnaryExpression unaryExpression
                ? unaryExpression.Operand as MemberExpression
                : lambda.Body as MemberExpression;

            if (memberExpression.IsNull())
                return string.Empty;

            var propertyInfo = memberExpression!.Member as PropertyInfo;
            var dbColumnName = propertyInfo?.GetCustomAttribute<DbObjectParamAttribute>();

            return dbColumnName?.Name ?? string.Empty;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Get [DbObjectParamAttribute] || [DbObjectColumnAttribute] property name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="propertyInfo">Property info.</param>
        /// <returns>
        ///     Return name of custom database object column | param name.
        /// </returns>
        /// =================================================================================================
        public static string GetColumnParamName(PropertyInfo propertyInfo)
        {
            if (propertyInfo.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(propertyInfo));

            var dbColumnName = propertyInfo?.GetCustomAttribute<DbObjectColumnAttribute>();
            var dbPramName = propertyInfo?.GetCustomAttribute<DbObjectParamAttribute>();

            return (bool)dbColumnName?.Name?.IsNullOrEmpty()
                ? (bool)dbPramName?.Name?.IsNullOrEmpty() ? string.Empty : dbPramName.Name
                : dbColumnName.Name;
        }
    }
}