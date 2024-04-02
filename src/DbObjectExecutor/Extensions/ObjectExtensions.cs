// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 21:52
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-01 15:33
// ***********************************************************************
//  <copyright file="ObjectExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using CodeSource;
using System;
using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("DbObjectExecutor.Attribute")]

namespace DbObjectExecutor.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     An object extensions.
    /// </summary>
    /// =================================================================================================
    [CodeSource("https://tinyurl.com/ObjectExtensions", "RzR", 1D)]
    internal static class ObjectExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     An object extension method that query if 'source' is null.
        /// </summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>
        ///     True if null, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNull(this object source)
            => source == null;

        /// <summary>
        ///     Is not null
        /// </summary>
        /// <param name="obj">Object to be checked</param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
            => obj != null;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     An object extension method that default if database null.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">The source to act on.</param>
        /// <returns>
        ///     A T source type.
        /// </returns>
        /// =================================================================================================
        internal static T DefaultIfDbNull<T>(this object source)
            => source == DBNull.Value ? default(T) : (T)source;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     An object extension method that query if 'source' is null or default.
        /// </summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>
        ///     True if null or default, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNullOrDefault(this object source)
            => source.IsNull() || source == default;
    }
}