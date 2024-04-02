// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-20 18:44
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-20 21:14
// ***********************************************************************
//  <copyright file="DbObjectColumnAttributeHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Attributes;
using DbObjectExecutor.Extensions;
using System;
using System.Reflection;

#endregion

// ReSharper disable PossibleInvalidOperationException

namespace DbObjectExecutor.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object column attribute helper.
    /// </summary>
    /// =================================================================================================
    public static class DbObjectColumnAttributeHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets database object column name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="propertyInfo">Information describing the property.</param>
        /// <returns>
        ///     The database object column name.
        /// </returns>
        /// =================================================================================================
        public static string GetDbObjectColumnName(PropertyInfo propertyInfo)
        {
            if (propertyInfo.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(propertyInfo));

            var dbColumnName = propertyInfo?.GetCustomAttribute<DbObjectColumnAttribute>();

            return dbColumnName.IsNull() || (bool)dbColumnName?.Name?.IsNullOrEmpty()
                ? propertyInfo!.Name
                : dbColumnName.Name;
        }
    }
}