// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-01 15:25
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-11 22:02
// ***********************************************************************
//  <copyright file="DbObjectParamAttribute.cs" company="">
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
    ///     Attribute for database object parameter.
    /// </summary>
    /// =================================================================================================
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DbObjectParamAttribute : System.Attribute
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the data base parameter name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// =================================================================================================
        public string Name { get; private set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the data base parameter direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        /// =================================================================================================
        public DbParamDirectionType Direction { get; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the data base parameter size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        /// =================================================================================================
        public int? Size { get; private set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the data base parameter precision.
        /// </summary>
        /// <value>
        ///     The precision.
        /// </value>
        /// =================================================================================================
        public byte? Precision { get; private set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the data base parameter scale.
        /// </summary>
        /// <value>
        ///     The scale.
        /// </value>
        /// =================================================================================================
        public byte? Scale { get; private set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the data base parameter default value.
        /// </summary>
        /// <value>
        ///     The default value.
        /// </value>
        /// =================================================================================================
        public object DefaultValue { get; private set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbObjectParamAttribute" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="direction">(Optional) The direction.</param>
        /// <param name="defaultValue">(Optional) The default value.</param>
        /// <param name="size">(Optional) The size.</param>
        /// <param name="precision">(Optional) The precision.</param>
        /// <param name="scale">(Optional) The scale.</param>
        /// =================================================================================================
        public DbObjectParamAttribute(
            string paramName, DbParamDirectionType direction = DbParamDirectionType.Input,
            object defaultValue = default, int size = Int32.MinValue, byte precision = Byte.MinValue, byte scale = Byte.MinValue)
        {
            if (paramName.IsNullOrEmpty())
                throw new ArgumentNullException(string.Format(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(paramName)));

            Name = paramName;
            Direction = direction;
            DefaultValue = defaultValue;
            Scale = scale == Byte.MinValue ? null : scale;
            Size = size == Int32.MinValue ? null : size;
            Precision = precision == Byte.MinValue ? null : precision;
        }
    }
}