// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-04-08 08:40
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-08 22:10
// ***********************************************************************
//  <copyright file="DbConnectionExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Attribute.Enums;
using System.Data.Common;
using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("DbObjectExecutor.Imp.EntityFramework")]

namespace DbObjectExecutor.Attribute.Extensions
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
        ///     A DbConnection extension method that gets database provider type.
        /// </summary>
        /// <param name="connection">The connection to act on.</param>
        /// <returns>
        ///     The database provider type.
        /// </returns>
        /// =================================================================================================
        internal static DbProviderType GetDbProviderType(this DbConnection connection)
            => connection.GetType().Name switch
            {
                "OracleConnection" => DbProviderType.Oracle,
                "MySqlConnection" => DbProviderType.MySql,
                "NpgsqlConnection" => DbProviderType.PostgreSql,
                "SqliteConnection" => DbProviderType.SqLite,
                "SqlServerConnection" => DbProviderType.MsSql,
                _ => DbProviderType.MsSql
            };
    }
}