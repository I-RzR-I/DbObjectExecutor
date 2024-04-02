// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFrameworkNet6Test
//  Author           : RzR
//  Created On       : 2024-03-21 20:06
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-21 20:06
// ***********************************************************************
//  <copyright file="EntityFrameworkInit.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using Microsoft.EntityFrameworkCore;
using SharedDbObjectExecutorInitInfo.DataBaseTool;

namespace DbObjectExecutor.Imp.EntityFrameworkNet6Test;

[TestClass]
public class EntityFrameworkInit
{
    protected DbContext DbContext { get; private set; }

    [TestInitialize]
    public void Init()
    {
        var options = new DbContextOptionsBuilder<TestDbCtx>()
            .UseSqlServer(DataBaseHelper.ConnectionStringDefaultMsSql)
            .Options;
        var ctx = new TestDbCtx(options);

        DbContext = ctx;
    }
}