// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFrameworkNet5Test
//  Author           : RzR
//  Created On       : 2024-03-19 17:19
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-19 19:52
// ***********************************************************************
//  <copyright file="EntityFrameworkInit.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;

#endregion

namespace DbObjectExecutor.Imp.EntityFrameworkNet5Test
{
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
}