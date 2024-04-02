// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-14 20:40
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-19 20:06
// ***********************************************************************
//  <copyright file="BuildRequestResultDto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions;
using System.Collections.Generic;
using System.Data.Common;

#endregion

namespace DbObjectExecutor.Attribute.Models.Result
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A build request result data transfer object.
    /// </summary>
    /// =================================================================================================
    public class BuildRequestResultDto
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets the database object builder.
        /// </summary>
        /// <value>
        ///     The database object builder.
        /// </value>
        /// =================================================================================================
        public IDbObjectBuilder DbObjectBuilder { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets options for controlling the out.
        /// </summary>
        /// <value>
        ///     Options that control the out.
        /// </value>
        /// =================================================================================================
        public IEnumerable<DbParameter> OutParams { get; set; }
    }
}