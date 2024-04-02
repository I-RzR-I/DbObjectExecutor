// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFrameworkNet5Test
//  Author           : RzR
//  Created On       : 2024-03-15 16:34
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-15 16:34
// ***********************************************************************
//  <copyright file="SpNewTableIdRequest.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using DbObjectExecutor.Attribute.Attributes;
using DbObjectExecutor.Enums;
using SharedDbObjectExecutorInitInfo.DataBaseTool;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace DbObjectExecutor.Imp.EntityFrameworkNet5Test.Models.Procedures
{
    [DbObjectName(DataBaseObjectNames.spNewTableId, DbExecutorType.Procedure)]
    public class SpNewTableIdRequest
    {
        [DbObjectParam("TableName")]
        public string TableName { get; set; }

        [DbObjectParam("NextId", DbParamDirectionType.Output)]
        public int OutNextId { get; }
    }
}