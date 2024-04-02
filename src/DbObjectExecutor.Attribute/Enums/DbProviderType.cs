// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Attribute
//  Author           : RzR
//  Created On       : 2024-03-14 17:01
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-14 17:01
// ***********************************************************************
//  <copyright file="DbProviderType.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace DbObjectExecutor.Attribute.Enums
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Values that represent Database provider types.
    /// </summary>
    /// =================================================================================================
    public enum DbProviderType
    {
        /// <summary>
        ///     An enum constant representing the Milliseconds SQL option.
        /// </summary>
        MsSql,
        
        /// <summary>
        ///     An enum constant representing the sq lite option.
        /// </summary>
        SqLite,
        
        /// <summary>
        ///     An enum constant representing the postgre SQL option.
        /// </summary>
        PostgreSql,
        
        /// <summary>
        ///     An enum constant representing the oracle option.
        /// </summary>
        Oracle,
        
        /// <summary>
        ///     An enum constant representing my SQL option.
        /// </summary>
        MySql
    }
}