// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor
//  Author           : RzR
//  Created On       : 2024-03-20 20:33
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-20 20:48
// ***********************************************************************
//  <copyright file="ExpressionBuildHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Extensions;
using System;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace DbObjectExecutor.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     An expression build helper.
    /// </summary>
    /// =================================================================================================
    internal static class ExpressionBuildHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Builds property setter.
        /// </summary>
        /// <param name="property">The property.</param>
        /// =================================================================================================
        internal static Delegate BuildPropertySetter(PropertyInfo property)
        {
            if (property.IsNull())
                throw new ArgumentNullException(ResourceMessagesHelper.SpecifiedParameterIsNotSupplied, nameof(property));

            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");

            var instanceCast = property!.DeclaringType!.IsValueType
                ? Expression.Convert(instance, property.DeclaringType)
                : Expression.TypeAs(instance, property.DeclaringType);

            var valueCast = property.PropertyType.IsValueType
                ? Expression.Convert(value, property.PropertyType)
                : Expression.TypeAs(value, property.PropertyType);

            var setterCall = Expression.Call(instanceCast, property.GetSetMethod(), valueCast);

            return Expression.Lambda(setterCall, instance, value).Compile();
        }
    }
}