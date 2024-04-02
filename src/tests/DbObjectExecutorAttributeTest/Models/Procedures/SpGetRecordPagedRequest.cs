// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorAttributeTest
//  Author           : RzR
//  Created On       : 2024-03-15 19:32
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-15 19:32
// ***********************************************************************
//  <copyright file="SpGetRecordPagedRequest.cs" company="">
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

namespace DbObjectExecutorAttributeTest.Models.Procedures
{
    [DbObjectName(DataBaseObjectNames.spGetRecordPaged, DbExecutorType.Procedure)]
    public class SpGetRecordPagedRequest
    {
        [DbObjectParam("Skip", DbParamDirectionType.Input, 0)]
        public int Skip { get; set; }

        [DbObjectParam("Take", DbParamDirectionType.Input, 10)]
        public int Take { get; set; }

        [DbObjectParam("OrderBy", defaultValue: "Name")]
        public string OrderBy { get; set; }

        [DbObjectParam("Where", defaultValue: " (1 = 1) ")]
        public string Where { get; set; }

        [DbObjectParam("RowsCount", DbParamDirectionType.Output)]
        public int OutRowsCount { get; }
    }
}