// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2025-06-02 22:03
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-06-02 22:03
// ***********************************************************************
//  <copyright file="DbTypeExtensions.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using System.Data;

#endregion

namespace DbObjectExecutor.Extensions.System.Data
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database type extensions.
    /// </summary>
    /// =================================================================================================
    internal static class DbTypeExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A DbType extension method that query if 'paramDbType' is text type.
        /// </summary>
        /// <param name="paramDbType">The paramDbType to act on.</param>
        /// <returns>
        ///     True if text type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsTextType(this DbType paramDbType)
            => new List<DbType>
            {
                DbType.StringFixedLength,
                DbType.String,
                DbType.AnsiString, 
                DbType.AnsiStringFixedLength
            }.Contains(paramDbType);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A DbType extension method that query if 'paramDbType' is number type.
        /// </summary>
        /// <param name="paramDbType">The paramDbType to act on.</param>
        /// <returns>
        ///     True if number type, false if not.
        /// </returns>
        /// =================================================================================================
        internal static bool IsNumberType(this DbType paramDbType)
            => new List<DbType>
            {
                DbType.Int16,
                DbType.Int32,
                DbType.Int64,
                DbType.UInt16,
                DbType.UInt32,
                DbType.UInt64,
                DbType.VarNumeric
            }.Contains(paramDbType);
    }
}