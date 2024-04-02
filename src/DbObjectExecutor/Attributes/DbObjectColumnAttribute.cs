// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-20 17:51
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-20 18:24
// ***********************************************************************
//  <copyright file="DbObjectColumnAttribute.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace DbObjectExecutor.Attributes
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for database object column.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" />
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property)]
    public class DbObjectColumnAttribute : ColumnAttribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbObjectColumnAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// =================================================================================================
        public DbObjectColumnAttribute(string name) : base(name) { }
    }
}