// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="DbDataReaderOneOrDefaultExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable RedundantJumpStatement

#endregion

namespace DbObjectExecutor.Mapper.Extensions.DbDataReader
{
    /// -------------------------------------------------------------------------------------------------
    /// <content>
    ///     A database data reader extensions.
    /// </content>
    /// =================================================================================================
    public static partial class DbDataReaderExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that firsts the given reader.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static T First<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => One<T>(reader, false, false);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that first or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static T FirstOrDefault<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => One<T>(reader, true, false);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that singles the given reader.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static T Single<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => One<T>(reader, false, true);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that single or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static T SingleOrDefault<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => One<T>(reader, true, true);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that first asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> FirstAsync<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => await FirstAsync<T>(reader, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that first asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> FirstAsync<T>(this System.Data.Common.DbDataReader reader, CancellationToken cancellationToken) where T : class, new()
            => await OneAsync<T>(reader, false, false, cancellationToken);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that first or default asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> FirstOrDefaultAsync<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => await FirstOrDefaultAsync<T>(reader, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that first or default asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> FirstOrDefaultAsync<T>(
            this System.Data.Common.DbDataReader reader,
            CancellationToken cancellationToken) where T : class, new()
            => await OneAsync<T>(reader, true, false, cancellationToken);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that single asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> SingleAsync<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => await SingleAsync<T>(reader, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that single asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> SingleAsync<T>(
            this System.Data.Common.DbDataReader reader,
            CancellationToken cancellationToken) where T : class, new()
            => await OneAsync<T>(reader, false, true, cancellationToken);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that single or default asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> SingleOrDefaultAsync<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => await SingleOrDefaultAsync<T>(reader, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that single or default
        ///     asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        public static async Task<T> SingleOrDefaultAsync<T>(
            this System.Data.Common.DbDataReader reader,
            CancellationToken cancellationToken) where T : class, new()
            => await OneAsync<T>(reader, true, true, cancellationToken);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Ones.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is invalid.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="useDefault">True to use default.</param>
        /// <param name="throwIfMultiple">True to throw if multiple.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        private static T One<T>(System.Data.Common.DbDataReader reader, bool useDefault, bool throwIfMultiple) where T : class, new()
        {
            if (reader.Read())
            {
                var mapper = new ReaderMapper<T>(reader);
                var row = mapper.MapNextRow();

                if (throwIfMultiple && reader.Read())
                    throw new InvalidOperationException(ResourceMessagesHelper.MoreThanOneElementError);

                return row;
            }

            if (useDefault)
                return default;

            throw new InvalidOperationException(ResourceMessagesHelper.NoElementError);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     One asynchronous.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is invalid.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="useDefault">True to use default.</param>
        /// <param name="throwIfMultiple">True to throw if multiple.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A T.
        /// </returns>
        /// =================================================================================================
        private static async Task<T> OneAsync<T>(
            System.Data.Common.DbDataReader reader, bool useDefault,
            bool throwIfMultiple, CancellationToken cancellationToken) where T : class, new()
        {
            if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var mapper = new ReaderMapper<T>(reader);
                var row = await mapper.MapNextRowAsync(cancellationToken).ConfigureAwait(false);

                if (throwIfMultiple && await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                    throw new InvalidOperationException(ResourceMessagesHelper.MoreThanOneElementError);

                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                    continue;

                return row;
            }

            if (useDefault)
                return default;

            throw new InvalidOperationException(ResourceMessagesHelper.NoElementError);
        }
    }
}