// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-04-05 19:11
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-05 19:12
// ***********************************************************************
//  <copyright file="CustomLocationDto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using DbObjectExecutor.Attributes;

namespace DbObjectExecutorTest.Models
{
    public class CustomLocationDto
    {
        [DbObjectColumn("Id")]
        public int Id { get; set; }

        [DbObjectColumn("Code")]
        public string C { get; set; }

        [DbObjectColumn("Name")]
        public string N { get; set; }

        [DbObjectColumn("Description")]
        public string D { get; set; }

        [DbObjectColumn("IsActive")]
        public bool IsActive { get; set; }
    }
}