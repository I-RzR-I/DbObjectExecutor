// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="DbDataReaderDictionaryExtensions.cs" company="">
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
    ///     A database data reader extensions .ToDictionary().
    /// </content>
    /// =================================================================================================
    public static partial class DbDataReaderExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a
        ///     dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="keyProjection">The key projection.</param>
        /// <returns>
        ///     The given data converted to a Dictionary&lt;TKey,TValue&gt;
        /// </returns>
        /// =================================================================================================
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this System.Data.Common.DbDataReader reader,
            Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, TValue>();
            var mapper = new ReaderMapper<TValue>(reader);

            mapper.Map(val =>
            {
                var key = keyProjection(val);
                res[key] = val;
            });

            return res;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a
        ///     dictionary asynchronous.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="keyProjection">The key projection.</param>
        /// <returns>
        ///     The given data converted to a Dictionary&lt;TKey,TValue&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(
            this System.Data.Common.DbDataReader reader,
            Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
            => await ToDictionaryAsync(reader, keyProjection, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a
        ///     dictionary asynchronous.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="keyProjection">The key projection.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     The given data converted to a Dictionary&lt;TKey,TValue&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(
            this System.Data.Common.DbDataReader reader,
            Func<TValue, TKey> keyProjection, CancellationToken cancellationToken) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, TValue>();
            var mapper = new ReaderMapper<TValue>(reader);

            await mapper.MapAsync(val =>
            {
                var key = keyProjection(val);
                res[key] = val;
            }, cancellationToken).ConfigureAwait(false);

            return res;
        }
    }
}