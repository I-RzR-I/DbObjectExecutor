// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-04-07 23:54
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-07 23:54
// ***********************************************************************
//  <copyright file="PropertyMapDto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System.Reflection;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace DbObjectExecutor.Mapper.Models
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A property map data transfer object.
    /// </summary>
    /// =================================================================================================
    internal class PropertyMapDto
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets the name of the source.
        /// </summary>
        /// <value>
        ///     The name of the source.
        /// </value>
        /// =================================================================================================
        public string SourceName { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets the name of the attribute.
        /// </summary>
        /// <value>
        ///     The name of the attribute.
        /// </value>
        /// =================================================================================================
        public string AttributeName { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets a value indicating whether this object is in response.
        /// </summary>
        /// <value>
        ///     True if this object is in response, false if not.
        /// </value>
        /// =================================================================================================
        public bool IsInResponse { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets the property.
        /// </summary>
        /// <value>
        ///     The property.
        /// </value>
        /// =================================================================================================
        public PropertyInfo Property { get; set; }
    }
}