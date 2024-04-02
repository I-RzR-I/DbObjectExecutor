// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-07 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-07 17:24
// ***********************************************************************
//  <copyright file="ReaderProperty.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace DbObjectExecutor.Mapper
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A reader property.
    /// </summary>
    /// =================================================================================================
    public struct ReaderProperty
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets or sets the property index.
        /// </summary>
        /// <value>
        ///     The property index.
        /// </value>
        /// =================================================================================================
        public int Idx { get; set; }

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
        ///     Gets or sets the setter.
        /// </summary>
        /// <value>
        ///     The setter.
        /// </value>
        /// =================================================================================================
        public Action<object, object> Setter { get; set; }
    }
}