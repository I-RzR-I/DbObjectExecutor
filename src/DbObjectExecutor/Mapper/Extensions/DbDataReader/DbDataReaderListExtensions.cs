// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="DbDataReaderListExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace DbObjectExecutor.Mapper.Extensions.DbDataReader
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database data reader list extensions .ToList().
    /// </summary>
    /// =================================================================================================
    public static partial class DbDataReaderExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts a reader to a list.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     Reader as a List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static List<T> ToList<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
        {
            var res = new List<T>();
            var mapper = new ReaderMapper<T>(reader);
            mapper.Map(row => res.Add(row));

            return res;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a list
        ///     asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     The given data converted to a List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ToListAsync<T>(this System.Data.Common.DbDataReader reader) where T : class, new()
            => await ToListAsync<T>(reader, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a list
        ///     asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     The given data converted to a List&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<List<T>> ToListAsync<T>(
            this System.Data.Common.DbDataReader reader,
            CancellationToken cancellationToken) where T : class, new()
        {
            var res = new List<T>();
            var mapper = new ReaderMapper<T>(reader);
            await mapper.MapAsync(row => res.Add(row), cancellationToken).ConfigureAwait(false);

            return res;
        }
    }
}