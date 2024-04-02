// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 16:40
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-05 19:13
// ***********************************************************************
//  <copyright file="IDbObjectBuilder.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions.Params;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable RedundantExtendsListEntry

#endregion

namespace DbObjectExecutor.Abstractions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for database object builder.
    /// </summary>
    /// =================================================================================================
    public interface IDbObjectBuilder : IDbObjectCommon,
        IDbExecInputParamBuilder,
        IDbExecOutputParamBuilder,
        IDbExecInputOutputParamBuilder,
        IDbExecReturnParamBuilder,
        IDisposable
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request as reader.
        /// </summary>
        /// <param name="action">Actions to do with the result sets.</param>
        /// =================================================================================================
        void Execute(Action<DbDataReader> action);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request as reader.
        /// </summary>
        /// <param name="function">Function to do with the result sets.</param>
        /// <returns>
        ///     A Task.
        /// </returns>
        /// =================================================================================================
        Task ExecuteAsync(Func<DbDataReader, Task> function);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request as reader.
        /// </summary>
        /// <exception cref="TaskCanceledException">
        ///     When <paramref name="cancellationToken" /> was cancelled.
        /// </exception>
        /// <param name="function">Function to do with the result sets.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        ///     A Task.
        /// </returns>
        /// =================================================================================================
        Task ExecuteAsync(Func<DbDataReader, Task> function, CancellationToken cancellationToken);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request as non query.
        /// </summary>
        /// <returns>
        ///     The number of rows affected.
        /// </returns>
        /// =================================================================================================
        int ExecuteNonQuery();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request as non query.
        /// </summary>
        /// <returns>
        ///     The number of rows affected.
        /// </returns>
        /// =================================================================================================
        Task<int> ExecuteNonQueryAsync();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request as non query.
        /// </summary>
        /// <exception cref="TaskCanceledException">
        ///     When <paramref name="cancellationToken" /> was cancelled.
        /// </exception>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        ///     The number of rows affected.
        /// </returns>
        /// =================================================================================================
        Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request and return the first column of the first row.
        /// </summary>
        /// <typeparam name="T">Type of the scalar value.</typeparam>
        /// <param name="val">[out] The first column of the first row in the result set.</param>
        /// =================================================================================================
        void ExecuteScalar<T>(out T val);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request and return the first column of the first row.
        /// </summary>
        /// <typeparam name="T">Type of the scalar value.</typeparam>
        /// <param name="action">
        ///     Action applied to the first column of the first row in the result set.
        /// </param>
        /// <returns>
        ///     A Task.
        /// </returns>
        /// =================================================================================================
        Task ExecuteScalarAsync<T>(Action<T> action);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute the request and return the first column of the first row.
        /// </summary>
        /// <exception cref="TaskCanceledException">
        ///     When <paramref name="cancellationToken" /> was cancelled.
        /// </exception>
        /// <typeparam name="T">Type of the scalar value.</typeparam>
        /// <param name="action">
        ///     Action applied to the first column of the first row in the result set.
        /// </param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        ///     A Task.
        /// </returns>
        /// =================================================================================================
        Task ExecuteScalarAsync<T>(Action<T> action, CancellationToken cancellationToken);
    }
}