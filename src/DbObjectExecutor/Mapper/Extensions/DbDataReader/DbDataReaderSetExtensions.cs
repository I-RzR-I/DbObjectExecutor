// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="DbDataReaderSetExtensions.cs" company="">
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
    ///     A database data reader extensions .ToSet().
    /// </content>
    /// =================================================================================================
    public static partial class DbDataReaderExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts a reader to a set.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     Reader as a HashSet&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static HashSet<T> ToSet<T>(this System.Data.Common.DbDataReader reader) where T : IComparable
        {
            var res = new HashSet<T>();

            while (reader.Read())
            {
                var val = (T)reader.GetValue(0);
                res.Add(val);
            }

            return res;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a set
        ///     asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///     The given data converted to a HashSet&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<HashSet<T>> ToSetAsync<T>(this System.Data.Common.DbDataReader reader) where T : IComparable
            => await ToSetAsync<T>(reader, CancellationToken.None);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A System.Data.Common.DbDataReader extension method that converts this object to a set
        ///     asynchronous.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="cancellationToken">A token that allows processing to be cancelled.</param>
        /// <returns>
        ///     The given data converted to a HashSet&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<HashSet<T>> ToSetAsync<T>(
            this System.Data.Common.DbDataReader reader,
            CancellationToken cancellationToken) where T : IComparable
        {
            var res = new HashSet<T>();

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var val = (T)reader.GetValue(0);
                res.Add(val);
            }

            return res;
        }
    }
}