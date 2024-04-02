// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-11 21:54
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-11 21:54
// ***********************************************************************
//  <copyright file="DbParamDirectionType.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace DbObjectExecutor.Enums
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Values that represent Database Parameter direction types.
    /// </summary>
    /// =================================================================================================
    public enum DbParamDirectionType
    {
        /// <summary>
        ///     An enum constant representing the input option.
        /// </summary>
        Input,
        /// <summary>
        ///     An enum constant representing the output option.
        /// </summary>
        Output,
        /// <summary>
        ///     An enum constant representing the input output option.
        /// </summary>
        InputOutput,

        /// <summary>
        ///     An enum constant representing the return option.
        /// </summary>
        Return
    }
}