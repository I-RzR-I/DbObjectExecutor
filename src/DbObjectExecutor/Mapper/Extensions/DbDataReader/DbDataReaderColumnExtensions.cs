// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="DbDataReaderColumnExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace DbObjectExecutor.Mapper.Extensions.DbDataReader
{
    /// -------------------------------------------------------------------------------------------------
    /// <content>
    ///     A database data reader extensions .Column().
    /// </content>
    /// =================================================================================================
    public static partial class DbDataReaderExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that columns.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static List<T> Column<T>(this System.Data.Common.DbDataReader reader) where T : IComparable
            => Column<T>(reader, 0);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that columns.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static List<T> Column<T>(this System.Data.Common.DbDataReader reader, string columnName) where T : IComparable
        {
            var ordinal = columnName.IsNullOrEmpty() ? 0 : reader.GetOrdinal(columnName);

            return Column<T>(reader, ordinal);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that columns.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static List<T> Column<T>(this System.Data.Common.DbDataReader reader, int ordinal) where T : IComparable
        {
            var res = new List<T>();
            while (reader.Read())
            {
                var value = reader.IsDBNull(ordinal)
                    ? default(T)
                    : (T)reader.GetValue(ordinal);
                res.Add(value);
            }

            return res;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that column asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ColumnAsync<T>(this System.Data.Common.DbDataReader reader) where T : IComparable
            => await ColumnAsync<T>(reader, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that column asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ColumnAsync<T>(this System.Data.Common.DbDataReader reader, CancellationToken cancellationToken)
            where T : IComparable
            => await ColumnAsync<T>(reader, 0, cancellationToken);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that column asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ColumnAsync<T>(this System.Data.Common.DbDataReader reader, string columnName)
            where T : IComparable
            => await ColumnAsync<T>(reader, columnName, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that column asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ColumnAsync<T>(
            this System.Data.Common.DbDataReader reader, string columnName,
            CancellationToken cancellationToken) where T : IComparable
        {
            var ordinal = columnName.IsNullOrEmpty() ? 0 : reader.GetOrdinal(columnName);

            return await ColumnAsync<T>(reader, ordinal, cancellationToken);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that column asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ColumnAsync<T>(this System.Data.Common.DbDataReader reader, int ordinal) where T : IComparable
            => await ColumnAsync<T>(reader, ordinal, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that column asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="ordinal">The ordinal.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     A List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ColumnAsync<T>(
            this System.Data.Common.DbDataReader reader, int ordinal,
            CancellationToken cancellationToken) where T : IComparable
        {
            var res = new List<T>();
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var value = await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false)
                    ? default(T)
                    : (T)reader.GetValue(ordinal);
                res.Add(value);
            }

            return res;
        }
    }
}