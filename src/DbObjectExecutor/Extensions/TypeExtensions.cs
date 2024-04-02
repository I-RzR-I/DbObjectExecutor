// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-20 20:00
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-20 20:02
// ***********************************************************************
//  <copyright file="TypeExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("DbObjectExecutor.Attribute")]

namespace DbObjectExecutor.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A type extensions.
    /// </summary>
    /// =================================================================================================
    internal static class TypeExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that gets property by name.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        ///     The property by name.
        /// </returns>
        /// =================================================================================================
        internal static PropertyInfo GetPropertyByName(this Type sourceType, string name)
            => sourceType.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Get all properties.
        /// </summary>
        /// <param name="type">Entity type.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the property infos in this
        ///     collection.
        /// </returns>
        /// =================================================================================================
        internal static IEnumerable<PropertyInfo> GetPropertyInfos(this Type type)
        {
            if (type.IsNull())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(type)));

            try
            {
                return type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Type extension method that query if 'type' is string property type.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="type">Entity type.</param>
        /// <returns>
        ///     True if string property type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsStringPropType(this Type type)
        {
            if (type.IsNull())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(type)));

            return type == typeof(string);
        }
    }
}