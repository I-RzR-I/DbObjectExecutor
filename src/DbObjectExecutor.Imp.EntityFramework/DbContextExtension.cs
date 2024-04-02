// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFramework
//  Author           : RzR
//  Created On       : 2024-03-16 14:30
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-16 16:19
// ***********************************************************************
//  <copyright file="DbContextExtension.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Attribute.Builders;
using DbObjectExecutor.Attribute.Enums;
using DbObjectExecutor.Attribute.Models.Result;
using DbObjectExecutor.Builders;
using DbObjectExecutor.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

#endregion

namespace DbObjectExecutor.Imp.EntityFramework
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database context extension.
    /// </summary>
    /// =================================================================================================
    public static class DbContextExtension
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A DbContext extension method that loads database object.
        /// </summary>
        /// <param name="ctx">The ctx to act on.</param>
        /// <param name="nameOrQuery">The name or query.</param>
        /// <param name="commandType">(Optional) Type of the command.</param>
        /// <returns>
        ///     The database object.
        /// </returns>
        /// =================================================================================================
        public static IDbObjectBuilder LoadDbObject(this DbContext ctx, string nameOrQuery, DbExecutorType commandType = DbExecutorType.Query)
        {
            var connection = ctx.Database.GetDbConnection();
            var transaction = ctx.Database.CurrentTransaction?.GetDbTransaction();
            IDbObjectBuilder dbObject = new DbObjectBuilder();

            dbObject.SetInitInfo(nameOrQuery, connection, commandType);
            dbObject.UseTransaction(transaction);

            return dbObject;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A DbContext extension method that loads database object.
        /// </summary>
        /// <param name="ctx">The ctx to act on.</param>
        /// <param name="dbProviderType">Type of the database provider.</param>
        /// <param name="requestObjectInfo">Information describing the request object.</param>
        /// <param name="requestObjectInfoType">Type of the request object information.</param>
        /// <param name="dbObjectName">(Optional) Name of the database object.</param>
        /// <param name="commandType">(Optional) Type of the command.</param>
        /// <returns>
        ///     The database object.
        /// </returns>
        /// =================================================================================================
        public static BuildRequestResultDto LoadDbObject(this DbContext ctx, DbProviderType dbProviderType, object requestObjectInfo,
            Type requestObjectInfoType, string dbObjectName = null, DbExecutorType commandType = DbExecutorType.Undefined)
        {
            var connection = ctx.Database.GetDbConnection();
            var transaction = ctx.Database.CurrentTransaction?.GetDbTransaction();

            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(dbProviderType, connection, requestObjectInfo,
                requestObjectInfoType, dbObjectName, commandType, true);

            procedureRequest.DbObjectBuilder.UseTransaction(transaction);

            return procedureRequest;
        }
    }
}