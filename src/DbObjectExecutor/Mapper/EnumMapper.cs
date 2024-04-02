// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-14 18:45
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-14 18:59
// ***********************************************************************
//  <copyright file="EnumMapper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Enums;
using System.Data;
using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("DbObjectExecutor.Attribute")]

namespace DbObjectExecutor.Mapper
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     An enum mapper.
    /// </summary>
    /// =================================================================================================
    internal static class EnumMapper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Map to parameter direction.
        /// </summary>
        /// <param name="paramDirection">The parameter direction.</param>
        /// <returns>
        ///     A ParameterDirection.
        /// </returns>
        /// =================================================================================================
        internal static ParameterDirection MapToParameterDirection(DbParamDirectionType? paramDirection)
            => paramDirection switch
            {
                DbParamDirectionType.Input => ParameterDirection.Input,
                DbParamDirectionType.InputOutput => ParameterDirection.InputOutput,
                DbParamDirectionType.Output => ParameterDirection.Output,
                DbParamDirectionType.Return => ParameterDirection.ReturnValue,
                _ => ParameterDirection.Input
            };
    }
}