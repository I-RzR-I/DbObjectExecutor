// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFrameworkNet5Test
//  Author           : RzR
//  Created On       : 2024-03-19 18:39
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-19 18:45
// ***********************************************************************
//  <copyright file="TestDbCtx.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using Microsoft.EntityFrameworkCore;

#endregion

namespace DbObjectExecutor.Imp.EntityFrameworkNet5Test
{
    public class TestDbCtx : DbContext
    {
        public TestDbCtx(DbContextOptions<TestDbCtx> options)
            : base(options)
        {
        }
    }
}