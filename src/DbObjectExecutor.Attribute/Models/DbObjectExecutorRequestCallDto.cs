// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-12 22:42
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-13 00:21
// ***********************************************************************
//  <copyright file="DbObjectExecutorRequestCallDto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Enums;
using System.Collections.Generic;

// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace DbObjectExecutor.Attribute.Models
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object executor request call data transfer object.
    /// </summary>
    /// =================================================================================================
    public class DbObjectExecutorRequestCallDto
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// =================================================================================================
        public string Name { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets the type of the command.
        /// </summary>
        /// <value>
        ///     The type of the command.
        /// </value>
        /// =================================================================================================
        public DbExecutorType CommandType { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets options for controlling the operation.
        /// </summary>
        /// <value>
        ///     The parameters.
        /// </value>
        /// =================================================================================================
        public IEnumerable<DbObjectExecutorRequestParamDto> Params { get; set; }
    }
}