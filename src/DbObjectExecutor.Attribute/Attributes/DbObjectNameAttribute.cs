// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-01 15:25
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-11 22:10
// ***********************************************************************
//  <copyright file="DbObjectNameAttribute.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Enums;
using DbObjectExecutor.Extensions;
using DbObjectExecutor.Helpers;
using System;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace DbObjectExecutor.Attribute.Attributes
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Attribute for database object.
    /// </summary>
    /// <seealso cref="T:Attribute"/>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Class)]
    public class DbObjectNameAttribute : System.Attribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the database object name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// =================================================================================================
        public string Name { get; private set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the type of the command.
        /// </summary>
        /// <value>
        ///     The type of the command.
        /// </value>
        /// =================================================================================================
        public DbExecutorType CommandType { get; private set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbObjectNameAttribute"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="name">The database object name.</param>
        /// <param name="commandType">(Optional) Type of the command.</param>
        /// =================================================================================================
        public DbObjectNameAttribute(string name, DbExecutorType commandType = DbExecutorType.Undefined)
        {
            if (name.IsNullOrEmpty())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(name)));

            Name = name;
            CommandType = commandType;
        }
    }
}