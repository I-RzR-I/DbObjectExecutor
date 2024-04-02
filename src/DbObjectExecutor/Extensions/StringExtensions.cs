// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="StringExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using CodeSource;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbObjectExecutor.Attribute")]

namespace DbObjectExecutor.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A string extensions.
    /// </summary>
    /// =================================================================================================
    [CodeSource("https://tinyurl.com/StringExtensions", "RzR", 1D)]
    internal static class StringExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A string extension method that queries if a null or is empty.
        /// </summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>
        ///     True if the null or is empty, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNullOrEmpty(this string source)
            => string.IsNullOrEmpty(source);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A string extension method that query if 'source' is null empty or default.
        /// </summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>
        ///     True if null empty or default, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNullEmptyOrDefault(this string source)
            => source.IsNullOrEmpty() || source == default;
    }
}