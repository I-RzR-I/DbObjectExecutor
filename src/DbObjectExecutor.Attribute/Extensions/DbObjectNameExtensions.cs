// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-04-01 18:30
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-01 18:48
// ***********************************************************************
//  <copyright file="DbObjectNameExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Attribute.Attributes;
using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using System;
using System.Linq;
using System.Reflection;

#endregion

namespace DbObjectExecutor.Attribute.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object name extensions.
    /// </summary>
    /// =================================================================================================
    public static class DbObjectNameExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A string extension method that gets database object name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="sourcePropertyName">The sourcePropertyName to act on.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns>
        ///     The database object name.
        /// </returns>
        /// =================================================================================================
        public static string GetDbObjectName<T>(this string sourcePropertyName, T sourceType)
            where T : class
        {
            if (sourcePropertyName.IsNullEmptyOrDefault())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(sourcePropertyName)));

            var property = sourceType.GetType().GetPropertyInfos().FirstOrDefault(x => x.Name == sourcePropertyName);
            if (property.IsNull())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedResourceNotFound, sourcePropertyName));

            var dbObjName = property?.GetCustomAttribute<DbObjectNameAttribute>();

            return dbObjName?.Name ?? string.Empty;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A string extension method that gets database object name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="sourcePropertyName">The sourcePropertyName to act on.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns>
        ///     The database object name.
        /// </returns>
        /// =================================================================================================
        public static string GetDbObjectName(this string sourcePropertyName, Type sourceType)
        {
            if (sourcePropertyName.IsNullEmptyOrDefault())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(sourcePropertyName)));

            var property = sourceType.GetPropertyInfos().FirstOrDefault(x => x.Name == sourcePropertyName);
            if (property.IsNull())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedResourceNotFound, sourcePropertyName));

            var dbObjName = property?.GetCustomAttribute<DbObjectNameAttribute>();

            return dbObjName?.Name ?? string.Empty;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that gets database object name attribute.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="objectType">The objectType to act on.</param>
        /// <returns>
        ///     The database object name attribute.
        /// </returns>
        /// =================================================================================================
        public static DbObjectNameAttribute GetDbObjectNameAttribute(this Type objectType)
        {
            if (objectType.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(objectType));

            var attribute = objectType.GetCustomAttribute<DbObjectNameAttribute>(false);

            return attribute;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that gets database object name attribute.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="sourceObject">The sourceObject to act on.</param>
        /// <returns>
        ///     The database object name attribute.
        /// </returns>
        /// =================================================================================================
        public static DbObjectNameAttribute GetDbObjectNameAttribute<T>(this T sourceObject) where T : class
        {
            if (sourceObject.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(sourceObject));

            var attribute = sourceObject.GetType().GetCustomAttribute<DbObjectNameAttribute>(false);

            return attribute;
        }
    }
}