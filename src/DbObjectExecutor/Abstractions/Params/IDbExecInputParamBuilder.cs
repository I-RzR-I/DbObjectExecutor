// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-05 18:50
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-05 19:35
// ***********************************************************************
//  <copyright file="IDbExecInputParamBuilder.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Data.Common;

#endregion

namespace DbObjectExecutor.Abstractions.Params
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for database execute input parameter builder.
    /// </summary>
    /// =================================================================================================
    public interface IDbExecInputParamBuilder : IDisposable
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Sets an input parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     An IDbExecInputParamBuilder.
        /// </returns>
        /// =================================================================================================
        IDbExecInputParamBuilder SetIn(DbParameter parameter);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Sets an input parameter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     An IDbExecInputParamBuilder.
        /// </returns>
        /// =================================================================================================
        IDbExecInputParamBuilder SetIn<T>(string name, T value);
    }
}