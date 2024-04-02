// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFrameworkNet6Test
//  Author           : RzR
//  Created On       : 2024-03-21 20:05
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-21 20:05
// ***********************************************************************
//  <copyright file="TestDbCtx.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using Microsoft.EntityFrameworkCore;

namespace DbObjectExecutor.Imp.EntityFrameworkNet6Test;

public class TestDbCtx : DbContext
{
    public TestDbCtx(DbContextOptions<TestDbCtx> options)
        : base(options)
    {
    }
}