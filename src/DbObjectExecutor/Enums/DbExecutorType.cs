// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-05 09:24
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-05 09:24
// ***********************************************************************
//  <copyright file="DbExecutorType.cs" company="">
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
    ///     Values that represent Database executor types.
    /// </summary>
    /// =================================================================================================
    public enum DbExecutorType
    {
        /// <summary>
        ///     An enum constant representing the undefined option.
        /// </summary>
        Undefined,

        /// <summary>
        ///     An enum constant representing the query option.
        /// </summary>
        Query,

        /// <summary>
        ///     An enum constant representing the function as text option.
        /// </summary>
        FunctionAsText,

        /// <summary>
        ///     An enum constant representing the function as procedure option.
        /// </summary>
        FunctionAsProcedure,

        /// <summary>
        ///     An enum constant representing the procedure as text option.
        /// </summary>
        ProcedureAsText,

        /// <summary>
        ///     An enum constant representing the procedure option.
        /// </summary>
        Procedure
    }
}