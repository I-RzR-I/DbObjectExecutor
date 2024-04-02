// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-04-01 18:29
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-01 18:48
// ***********************************************************************
//  <copyright file="DbObjectParamExtensions.cs" company="">
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
    ///     A database object parameter extensions.
    /// </summary>
    /// =================================================================================================
    public static class DbObjectParamExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A string extension method that gets database object parameter.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="sourcePropertyName">The sourcePropertyName to act on.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns>
        ///     The database object parameter.
        /// </returns>
        /// =================================================================================================
        public static string GetDbObjectParam<T>(this string sourcePropertyName, T sourceType)
            where T : class
        {
            if (sourcePropertyName.IsNullEmptyOrDefault())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(sourcePropertyName)));

            var property = sourceType.GetType().GetPropertyInfos().FirstOrDefault(x => x.Name == sourcePropertyName);
            if (property.IsNull())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedResourceNotFound, sourcePropertyName));

            var dbParam = property?.GetCustomAttribute<DbObjectParamAttribute>();

            return dbParam?.Name ?? string.Empty;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A string extension method that gets database object parameter.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="sourcePropertyName">The sourcePropertyName to act on.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns>
        ///     The database object parameter.
        /// </returns>
        /// =================================================================================================
        public static string GetDbObjectParam(this string sourcePropertyName, Type sourceType)
        {
            if (sourcePropertyName.IsNullEmptyOrDefault())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(sourcePropertyName)));

            var property = sourceType.GetPropertyInfos().FirstOrDefault(x => x.Name == sourcePropertyName);
            if (property.IsNull())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedResourceNotFound, sourcePropertyName));

            var dbParam = property?.GetCustomAttribute<DbObjectParamAttribute>();

            return dbParam?.Name ?? string.Empty;
        }
    }
}