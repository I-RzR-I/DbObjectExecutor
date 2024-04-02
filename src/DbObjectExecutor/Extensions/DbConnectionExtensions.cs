// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-29 18:15
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-02-29 18:21
// ***********************************************************************
//  <copyright file="DbConnectionExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Data;

#endregion

namespace DbObjectExecutor.Extensions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database connection extensions.
    /// </summary>
    /// =================================================================================================
    internal static class DbConnectionExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A ConnectionState extension method that query if 'connection' is open.
        /// </summary>
        /// <param name="connection">The connection to act on.</param>
        /// <returns>
        ///     True if open, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsOpen(this ConnectionState connection)
            => connection == ConnectionState.Open;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A ConnectionState extension method that query if 'connection' is close.
        /// </summary>
        /// <param name="connection">The connection to act on.</param>
        /// <returns>
        ///     True if close, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsClose(this ConnectionState connection)
            => connection == ConnectionState.Closed;
    }
}