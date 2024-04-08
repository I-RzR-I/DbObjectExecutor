// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorAttributeTest
//  Author           : RzR
//  Created On       : 2024-04-05 19:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-05 19:04
// ***********************************************************************
//  <copyright file="Locations.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace SharedDbObjectExecutorInitInfo.Models.Tables
{
    public class Locations
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}