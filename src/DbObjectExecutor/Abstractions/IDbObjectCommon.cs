// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 17:52
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-05 19:39
// ***********************************************************************
//  <copyright file="IDbObjectCommon.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Enums;
using System;
using System.Data.Common;

#endregion

namespace DbObjectExecutor.Abstractions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for database object common.
    /// </summary>
    /// =================================================================================================
    public interface IDbObjectCommon : IDisposable
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Sets a timeout.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        ///     An IDbObjectCommon.
        /// </returns>
        /// =================================================================================================
        IDbObjectCommon SetTimeout(TimeSpan timeout);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Use transaction.
        /// </summary>
        /// <param name="transaction">(Optional) The transaction.</param>
        /// <returns>
        ///     An IDbObjectCommon.
        /// </returns>
        /// =================================================================================================
        IDbObjectCommon UseTransaction(DbTransaction transaction = null);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Commits a transaction.
        /// </summary>
        /// <returns>
        ///     An IDbObjectCommon.
        /// </returns>
        /// =================================================================================================
        IDbObjectCommon CommitTransaction();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Rolls back a transaction.
        /// </summary>
        /// <returns>
        ///     An IDbObjectCommon.
        /// </returns>
        /// =================================================================================================
        IDbObjectCommon RollBackTransaction();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Sets initialize information.
        /// </summary>
        /// <param name="nameOrQuery">The name or query.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="commandType">(Optional) Type of the command.</param>
        /// <returns>
        ///     An IDbObjectCommon.
        /// </returns>
        /// =================================================================================================
        IDbObjectCommon SetInitInfo(string nameOrQuery, DbConnection connection, DbExecutorType commandType = DbExecutorType.Query);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Use data base. Change database used in current request.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>
        ///     An IDbObjectCommon.
        /// </returns>
        /// =================================================================================================
        IDbObjectCommon UseDataBase(string databaseName);
    }
}