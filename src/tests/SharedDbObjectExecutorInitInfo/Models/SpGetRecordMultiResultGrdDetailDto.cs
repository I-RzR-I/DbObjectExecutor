﻿// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.SharedDbObjectExecutorInitInfo
//  Author           : RzR
//  Created On       : 2024-03-14 22:22
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-14 22:36
// ***********************************************************************
//  <copyright file="SpGetRecordMultiResultGrdDetailDto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace SharedDbObjectExecutorInitInfo.Models
{
    public class SpGetRecordMultiResultGrdDetailDto
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        public string Country { get; set; }
    }
}