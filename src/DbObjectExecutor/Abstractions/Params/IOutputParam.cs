// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 16:53
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-02-28 16:53
// ***********************************************************************
//  <copyright file="IOutputParam.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace DbObjectExecutor.Abstractions.Params
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for output parameter.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// =================================================================================================
    public interface IOutputParam<out T>
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the parameter value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        /// =================================================================================================
        T Value { get; }
    }
}