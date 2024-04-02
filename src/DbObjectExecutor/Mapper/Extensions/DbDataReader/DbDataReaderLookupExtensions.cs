// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="DbDataReaderLookupExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace DbObjectExecutor.Mapper.Extensions.DbDataReader
{
    /// -------------------------------------------------------------------------------------------------
    /// <content>
    ///     A database data reader extensions .ToLookup().
    /// </content>
    /// =================================================================================================
    public static partial class DbDataReaderExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a
        ///     lookup.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="keyProjection">The key projection.</param>
        /// <returns>
        ///     The given data converted to a Dictionary&lt;TKey,List&lt;TValue&gt;&gt;
        /// </returns>
        /// =================================================================================================
        public static Dictionary<TKey, List<TValue>> ToLookup<TKey, TValue>(
            this System.Data.Common.DbDataReader reader,
            Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, List<TValue>>();
            var mapper = new ReaderMapper<TValue>(reader);

            mapper.Map(val =>
            {
                var key = keyProjection(val);

                if (res.ContainsKey(key))
                    res[key].Add(val);
                else
                    res[key] = new List<TValue> { val };
            });

            return res;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a lookup
        ///     asynchronous.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="keyProjection">The key projection.</param>
        /// <returns>
        ///     The given data converted to a Dictionary&lt;TKey,List&lt;TValue&gt;&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Dictionary<TKey, List<TValue>>> ToLookupAsync<TKey, TValue>(
            this System.Data.Common.DbDataReader reader,
            Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
            => await ToLookupAsync(reader, keyProjection, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a lookup
        ///     asynchronous.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="keyProjection">The key projection.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     The given data converted to a Dictionary&lt;TKey,List&lt;TValue&gt;&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Dictionary<TKey, List<TValue>>> ToLookupAsync<TKey, TValue>(
            this System.Data.Common.DbDataReader reader,
            Func<TValue, TKey> keyProjection, CancellationToken cancellationToken) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, List<TValue>>();
            var mapper = new ReaderMapper<TValue>(reader);

            await mapper.MapAsync(val =>
            {
                var key = keyProjection(val);

                if (res.ContainsKey(key))
                    res[key].Add(val);
                else
                    res[key] = new List<TValue> { val };
            }, cancellationToken).ConfigureAwait(false);

            return res;
        }
    }
}