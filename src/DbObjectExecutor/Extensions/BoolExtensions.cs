// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:29
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:29
// ***********************************************************************
//  <copyright file="BoolExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using CodeSource;

namespace DbObjectExecutor.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     An extensions.
    /// </summary>
    /// =================================================================================================
    [CodeSource("https://tinyurl.com/BoolExtensions", "RzR", 1D)]
    internal static class BoolExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Check if source value is equals with true.
        /// </summary>
        /// <param name="source">Source object to be checked.</param>
        /// <returns>
        ///     True if true, false if not.
        /// </returns>
        /// =================================================================================================
        public static bool IsTrue(this bool source)
            => source.IsNotNull() && source.Equals(true);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A bool extension method that query if 'source' is false.
        /// </summary>
        /// <param name="source">Source object to be checked.</param>
        /// <returns>
        ///     True if false, false if not.
        /// </returns>
        /// =================================================================================================
        public static bool IsFalse(this bool source)
            => source.IsNotNull() && source.Equals(false);
    }
}