// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-05 18:50
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-05 19:34
// ***********************************************************************
//  <copyright file="IDbExecReturnParamBuilder.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

#endregion

namespace DbObjectExecutor.Abstractions.Params
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for database execute return parameter builder.
    /// </summary>
    /// =================================================================================================
    public interface IDbExecReturnParamBuilder : IDisposable
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Returns the given return parameter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="returnParam">[out] The return parameter.</param>
        /// <returns>
        ///     An IDbExecOutputParamBuilder.
        /// </returns>
        /// =================================================================================================
        IDbExecOutputParamBuilder Return<T>(out IOutputParam<T> returnParam);
    }
}