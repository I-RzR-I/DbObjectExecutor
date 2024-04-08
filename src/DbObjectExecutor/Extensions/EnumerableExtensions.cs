// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-04-07 22:30
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-08 22:22
// ***********************************************************************
//  <copyright file="EnumerableExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using System.Linq;

#endregion

namespace DbObjectExecutor.Extensions
{
    /// <summary>
    ///     Enumerable extensions
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        ///     Format list to list with item index
        /// </summary>
        /// <param name="self">Input list</param>
        /// <returns></returns>
        /// <typeparam name="T">List type</typeparam>
        /// <remarks></remarks>
        internal static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self?.Select((item, index) => (item, index)) ?? new List<(T, int)>();
    }
}