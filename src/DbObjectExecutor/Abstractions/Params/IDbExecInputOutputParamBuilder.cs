// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-05 18:50
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-05 19:36
// ***********************************************************************
//  <copyright file="IDbExecInputOutputParamBuilder.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

// ReSharper disable MethodOverloadWithOptionalParameter

#endregion

namespace DbObjectExecutor.Abstractions.Params
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for database execute input output parameter builder.
    /// </summary>
    /// =================================================================================================
    public interface IDbExecInputOutputParamBuilder : IDisposable
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Sets input, output parameter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="outputParameter">[out] The output parameter.</param>
        /// <param name="defaultValue">(Optional) The default value.</param>
        /// <returns>
        ///     An IDbExecInputOutputParamBuilder.
        /// </returns>
        /// =================================================================================================
        IDbExecInputOutputParamBuilder SetInOut<T>(string name, T value, out IOutputParam<T> outputParameter, T defaultValue = default);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Sets input, output parameter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="outputParameter">[out] The output parameter.</param>
        /// <param name="defaultValue">(Optional) The default value.</param>
        /// <param name="size">(Optional) The size.</param>
        /// <param name="precision">(Optional) The precision.</param>
        /// <param name="scale">(Optional) The scale.</param>
        /// <returns>
        ///     An IDbExecInputOutputParamBuilder.
        /// </returns>
        /// =================================================================================================
        IDbExecInputOutputParamBuilder SetInOut<T>(string name, T value, out IOutputParam<T> outputParameter, T defaultValue = default, int size = 0, byte precision = 0, byte scale = 0);
    }
}