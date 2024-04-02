// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 21:09
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-04 17:47
// ***********************************************************************
//  <copyright file="StoredProcedureBuilder.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Abstractions.Params;
using DbObjectExecutor.Enums;
using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable MethodOverloadWithOptionalParameter
// ReSharper disable PossibleIntendedRethrow
// 
#endregion

namespace DbObjectExecutor.Builders
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object builder. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="T:DbObjectExecutor.Abstractions.IDbObjectBuilder"/>
    ///
    /// ### <inheritdoc cref="IDbObjectBuilder"/>
    /// =================================================================================================
    public sealed class DbObjectBuilder : IDbObjectBuilder
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Name of the data base.
        /// </summary>
        /// =================================================================================================
        private string _dataBaseName;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) name of the return parameter.
        /// </summary>
        /// =================================================================================================
        private const string ReturnParamName = "_returnParam";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the default time out.
        /// </summary>
        /// =================================================================================================
        private const int _timeOut = 60;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     The database command.
        /// </summary>
        /// =================================================================================================
        private DbCommand _dbCommand;

        /// <inheritdoc/>
        public void Dispose() => _dbCommand.Dispose();

        /// <inheritdoc/>
        public IDbExecOutputParamBuilder Return<T>(out IOutputParam<T> returnParam)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            returnParam = _dbCommand.AddOutputParamInner(ReturnParamName, default(T), ParameterDirection.ReturnValue);

            return this;
        }

        /// <inheritdoc/>
        public IDbObjectCommon SetTimeout(TimeSpan timeout)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            _dbCommand.CommandTimeout = (int)timeout.TotalSeconds;

            return this;
        }

        /// <inheritdoc/>
        public IDbObjectCommon UseTransaction(DbTransaction transaction = null)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            OpenConnection();

            var trans = transaction.IsNull()
                ? _dbCommand.Connection.BeginTransaction(IsolationLevel.ReadUncommitted)
                : transaction;
            _dbCommand.Transaction = trans;

            return this;
        }

        /// <inheritdoc/>
        public IDbObjectCommon CommitTransaction()
        {
            if (_dbCommand.Connection.State.IsOpen())
                _dbCommand.Transaction.Commit();

            return this;
        }

        /// <inheritdoc/>
        public IDbObjectCommon RollBackTransaction()
        {
            if (_dbCommand.Connection.State.IsOpen())
                _dbCommand.Transaction.Rollback();

            return this;
        }

        /// <inheritdoc/>
        public IDbObjectCommon SetInitInfo(string nameOrQuery, DbConnection connection, DbExecutorType commandType = DbExecutorType.Query)
        {
            if (nameOrQuery.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(nameOrQuery));

            if (connection.IsNull())
                throw new ArgumentNullException(nameof(connection));

            if (commandType == DbExecutorType.Undefined)
                throw new NotSupportedException(nameof(commandType));

            var cmd = connection.CreateCommand();
            cmd.CommandType = commandType switch
            {
                DbExecutorType.Query => CommandType.Text,
                DbExecutorType.FunctionAsText => CommandType.Text,
                DbExecutorType.ProcedureAsText => CommandType.Text,
                DbExecutorType.FunctionAsProcedure => CommandType.StoredProcedure,
                DbExecutorType.Procedure => CommandType.StoredProcedure,
                _ => CommandType.Text
            };

            cmd.CommandText = nameOrQuery;
            cmd.CommandTimeout = _timeOut;

            _dbCommand = cmd;

            return this;
        }

        /// <inheritdoc/>
        public IDbObjectCommon UseDataBase(string databaseName)
        {
            _dataBaseName = databaseName;

            return this;
        }

        /// <inheritdoc/>
        public IDbExecInputParamBuilder SetIn(DbParameter parameter)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            if (parameter.IsNull())
                throw new ArgumentNullException(nameof(parameter));

            _dbCommand.Parameters.Add(parameter);

            return this;
        }

        /// <inheritdoc/>
        public IDbExecInputParamBuilder SetIn<T>(string name, T value)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            _dbCommand.AddInnerParam(name, value, ParameterDirection.Input);

            return this;
        }

        /// <inheritdoc/>
        public IDbExecOutputParamBuilder SetOut<T>(
            string name, out IOutputParam<T> outputParameter,
            T defaultValue = default)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            outputParameter = _dbCommand.AddOutputParamInner(name, default(T), ParameterDirection.Output, defaultValue);

            return this;
        }

        /// <inheritdoc/>
        public IDbExecOutputParamBuilder SetOut<T>(
            string name, out IOutputParam<T> outputParameter,
            T defaultValue = default, int size = 0, byte precision = 0, byte scale = 0)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            outputParameter = _dbCommand.AddOutputParamInner(name, default(T), ParameterDirection.Output, defaultValue, size, precision, scale);

            return this;
        }

        /// <inheritdoc/>
        public IDbExecInputOutputParamBuilder SetInOut<T>(
            string name, T value, out IOutputParam<T> outputParameter,
            T defaultValue = default)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            outputParameter = _dbCommand.AddOutputParamInner(name, value, ParameterDirection.InputOutput, defaultValue);

            return this;
        }

        /// <inheritdoc/>
        public IDbExecInputOutputParamBuilder SetInOut<T>(
            string name, T value, out IOutputParam<T> outputParameter,
            T defaultValue = default, int size = 0, byte precision = 0, byte scale = 0)
        {
            if (_dbCommand.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.DbCommandNotInitialized);

            outputParameter = _dbCommand.AddOutputParamInner(name, value, ParameterDirection.InputOutput, defaultValue, size, precision, scale);

            return this;
        }

        /// <inheritdoc/>
        public void Execute(Action<DbDataReader> action)
        {
            if (action.IsNull())
                throw new ArgumentNullException(nameof(action));

            var isOwnsConnection = false;
            try
            {
                isOwnsConnection = OpenConnection();
                using var reader = _dbCommand.ExecuteReader();
                action(reader);
            }
            finally
            {
                if (isOwnsConnection.IsTrue())
                    CloseConnection();

                Dispose();
            }
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(Func<DbDataReader, Task> function) => ExecuteAsync(function, CancellationToken.None);

        /// <inheritdoc/>
        public async Task ExecuteAsync(Func<DbDataReader, Task> function, CancellationToken cancellationToken)
        {
            if (function.IsNull())
                throw new ArgumentNullException(nameof(function));

            var isOwnsConnection = false;
            try
            {
                isOwnsConnection = await OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
                using var reader = await _dbCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    await function(reader).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    _dbCommand.Cancel();

                    throw exception;
                }
            }
            finally
            {
                if (isOwnsConnection.IsTrue())
                    CloseConnection();

                Dispose();
            }
        }

        /// <inheritdoc/>
        public int ExecuteNonQuery()
        {
            var isOwnsConnection = false;
            try
            {
                isOwnsConnection = OpenConnection();

                return _dbCommand.ExecuteNonQuery();
            }
            finally
            {
                if (isOwnsConnection.IsTrue())
                    CloseConnection();

                Dispose();
            }
        }

        /// <inheritdoc/>
        public Task<int> ExecuteNonQueryAsync() => ExecuteNonQueryAsync(CancellationToken.None);

        /// <inheritdoc/>
        public async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            var isOwnsConnection = false;
            try
            {
                isOwnsConnection = await OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                return await _dbCommand
                    .ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (isOwnsConnection.IsTrue())
                    CloseConnection();

                Dispose();
            }
        }

        /// <inheritdoc/>
        public void ExecuteScalar<T>(out T val)
        {
            var isOwnsConnection = false;
            try
            {
                isOwnsConnection = OpenConnection();

                val = _dbCommand.ExecuteScalar()
                    .DefaultIfDbNull<T>();
            }
            finally
            {
                if (isOwnsConnection.IsTrue())
                    CloseConnection();

                Dispose();
            }
        }

        /// <inheritdoc/>
        public Task ExecuteScalarAsync<T>(Action<T> action) => ExecuteScalarAsync(action, CancellationToken.None);

        /// <inheritdoc/>
        public async Task ExecuteScalarAsync<T>(Action<T> action, CancellationToken cancellationToken)
        {
            var isOwnsConnection = false;
            try
            {
                isOwnsConnection = await OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
                var scalar = await _dbCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                var val = scalar.DefaultIfDbNull<T>();

                action(val);
            }
            finally
            {
                if (isOwnsConnection.IsTrue())
                    CloseConnection();

                Dispose();
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Opens the connection.
        /// </summary>
        /// <returns>
        ///     True if it succeeds and it is inner own connection, false if not.
        /// </returns>
        /// =================================================================================================
        private bool OpenConnection()
        {
            if (_dbCommand.Connection.State.IsClose())
            {
                _dbCommand.Connection.Open();

                if (_dataBaseName.IsNullOrEmpty().IsFalse())
                    _dbCommand.Connection.ChangeDatabase(_dataBaseName);

                return true;
            }

            return false;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Opens connection asynchronous.
        /// </summary>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     True if it succeeds and it is inner own connection, false if not.
        /// </returns>
        /// =================================================================================================
        private async Task<bool> OpenConnectionAsync(CancellationToken cancellationToken)
        {
            if (_dbCommand.Connection.State.IsClose())
            {
                await _dbCommand.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                if (_dataBaseName.IsNullOrEmpty().IsFalse())
                    _dbCommand.Connection.ChangeDatabase(_dataBaseName);

                return true;
            }

            return false;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Closes the connection.
        /// </summary>
        /// =================================================================================================
        private void CloseConnection()
        {
            if (_dbCommand.Connection.State.IsClose().IsFalse())
                _dbCommand.Connection.Close();
        }
    }
}