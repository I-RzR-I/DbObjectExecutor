// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-12 22:52
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-12 22:52
// ***********************************************************************
//  <copyright file="DbObjectExecutorRequestParamDto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Enums;
using System.Data;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8632

#endregion U S A G E S


namespace DbObjectExecutor.Attribute.Models
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database object executor request parameter data transfer object.
    /// </summary>
    /// =================================================================================================
    public class DbObjectExecutorRequestParamDto
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Property/parameter name used by db object.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// =================================================================================================
        public string Name { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     DB object parameter value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        /// =================================================================================================
        public object? Value { get; set; } = null;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     DB object parameter database type.
        /// </summary>
        /// <value>
        ///     The type of the database.
        /// </value>
        /// =================================================================================================
        public DbType? DbType { get; set; } = null;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     DB object parameter direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        /// =================================================================================================
        public DbParamDirectionType? Direction { get; set; } = null;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     DB object parameter size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        /// =================================================================================================
        public int? Size { get; set; } = null;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     DB object parameter precision.
        /// </summary>
        /// <value>
        ///     The precision.
        /// </value>
        /// =================================================================================================
        public byte? Precision { get; set; } = null;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     DB object parameter scale.
        /// </summary>
        /// <value>
        ///     The scale.
        /// </value>
        /// =================================================================================================
        public byte? Scale { get; set; } = null;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbObjectExecutorRequestParamDto" /> class.
        /// </summary>
        /// <remarks>
        ///     Default constructor.
        /// </remarks>
        /// =================================================================================================
        public DbObjectExecutorRequestParamDto()
        {
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbObjectExecutorRequestParamDto" /> class.
        /// </summary>
        /// <remarks>
        ///     Constructor for input parameters only.
        /// </remarks>
        /// <param name="name">Required. Property/parameter name used by db object.</param>
        /// <param name="value">Required. DB object parameter value.</param>
        /// <param name="dbType">Required. DB object parameter database type.</param>
        /// <param name="size">
        ///     (Optional) Optional. DB object parameter size. The default value is null.
        /// </param>
        /// <param name="precision">
        ///     (Optional) Optional. DB object parameter precision. The default value is null.
        /// </param>
        /// <param name="scale">
        ///     (Optional) Optional. DB object parameter scale. The default value is null.
        /// </param>
        /// =================================================================================================
        public DbObjectExecutorRequestParamDto(
            string name, object value, DbType dbType,
            int? size = null, byte? precision = null, byte? scale = null)
        {
            Name = name;
            Value = value;
            DbType = dbType;
            Size = size;
            Precision = precision;
            Scale = scale;
            Direction = DbParamDirectionType.Input;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbObjectExecutorRequestParamDto" /> class.
        /// </summary>
        /// <param name="name">Required. Property/parameter name used by db object.</param>
        /// <param name="value">
        ///     (Optional) Optional. DB object parameter value. The default value is null.
        /// </param>
        /// <param name="dbType">
        ///     (Optional) Optional. DB object parameter database type. The default value is null.
        /// </param>
        /// <param name="direction">
        ///     (Optional) Optional. DB object parameter direction. The default value is null.
        /// </param>
        /// <param name="size">
        ///     (Optional) Optional. DB object parameter size. The default value is null.
        /// </param>
        /// <param name="precision">
        ///     (Optional) Optional. DB object parameter precision. The default value is null.
        /// </param>
        /// <param name="scale">
        ///     (Optional) Optional. DB object parameter scale. The default value is null.
        /// </param>
        ///// <param name="mutatedCodeParamName">
        /////     (Optional) Optional. Property name user in code. The default value is null.
        ///// </param>
        ///// =================================================================================================
        public DbObjectExecutorRequestParamDto(
            string name, object? value = null, DbType? dbType = null, DbParamDirectionType? direction = null,
            int? size = null, byte? precision = null, byte? scale = null/*, string mutatedCodeParamName = null*/)
        {
            Name = name;
            Value = value;
            DbType = dbType;
            Direction = direction;
            Size = size;
            Precision = precision;
            Scale = scale;
        }
    }
}