// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-02-28 19:14
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-02-28 19:14
// ***********************************************************************
//  <copyright file="ResourceMessagesHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace DbObjectExecutor.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A resource messages helper.
    /// </summary>
    /// =================================================================================================
    internal static class ResourceMessagesHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) type of the not supported.
        /// </summary>
        /// =================================================================================================
        internal const string NotSupportedType = "Type not supported : {0}.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the un assignable null to not nullable.
        /// </summary>
        /// =================================================================================================
        internal const string UnAssignableNullToNotNullable = "{0} is null and can't be assigned to a non-nullable type.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the database command not initialized.
        /// </summary>
        /// =================================================================================================
        internal const string DbCommandNotInitialized = "The databse command must be initilized. Use `SetInitInfo()` method.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the no element error.
        /// </summary>
        /// =================================================================================================
        internal const string NoElementError = "Sequence contains no element";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the more than one element error.
        /// </summary>
        /// =================================================================================================
        internal const string MoreThanOneElementError = "Sequence contains more than one element";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the specified parameter is not supplied.
        /// </summary>
        /// =================================================================================================
        internal const string SpecifiedParameterIsNotSupplied = "The specified parameter {0} is not supplied.";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the database object name is without value.
        /// </summary>
        /// =================================================================================================
        internal const string DbObjectNameIsWithoutValue = "Database object name must be specified!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) an error occurred and result is null.
        /// </summary>
        /// =================================================================================================
        internal const string AnErrorOccurredAndResultIsNull = "An error occurred while trying to execute the request and the result is null!";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the specified resource not found.
        /// </summary>
        /// =================================================================================================
        internal const string SpecifiedResourceNotFound = "The specified resource {0} not found!";
    }
}