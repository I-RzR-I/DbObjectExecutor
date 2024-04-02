// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFrameworkNet6Test
//  Author           : RzR
//  Created On       : 2024-03-21 20:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-21 20:18
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

namespace DbObjectExecutor.Imp.EntityFrameworkNet6Test.Models;

[DbObjectName(DataBaseObjectNames.spNewTableId, DbExecutorType.Procedure)]
public class SpNewTableIdRequest
{
    [DbObjectParam("TableName")]
    public string TableName { get; set; }

    [DbObjectParam("NextId", DbParamDirectionType.Output)]
    public int OutNextId { get; }
}