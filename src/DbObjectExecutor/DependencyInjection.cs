// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-04-01 21:26
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-01 21:53
// ***********************************************************************
//  <copyright file="DependencyInjection.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#if NETSTANDARD2_1_OR_GREATER || NET

#region U S A G E S

using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Builders;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace DbObjectExecutor
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A dependency injection.
    /// </summary>
    /// =================================================================================================
    public static class DependencyInjection
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     An IServiceCollection extension method that registers the database object builder
        ///     described by serviceCollection.
        /// </summary>
        /// <param name="serviceCollection">The serviceCollection to act on.</param>
        /// =================================================================================================
        public static void RegisterDbObjectBuilder(this IServiceCollection serviceCollection) 
            => serviceCollection.AddTransient<IDbObjectBuilder, DbObjectBuilder>();
    }
}

#endif