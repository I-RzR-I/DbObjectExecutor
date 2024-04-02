// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorAttributeTest
//  Author           : RzR
//  Created On       : 2024-03-20 18:26
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-20 18:26
// ***********************************************************************
//  <copyright file="SpGetRecordPagedCustom2Dto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable InconsistentNaming
// 

using DbObjectExecutor.Attributes;

namespace SharedDbObjectExecutorInitInfo.Models.Procedures.Result
{
    public class SpGetRecordPagedCustom2Dto
    {
        public int Id { get; set; }

        [DbObjectColumn("Code")]
        public string C { get; set; }

        public string Name { get; set; }

        [DbObjectColumn("IsActive")]
        public bool TF { get; set; }
    }
}