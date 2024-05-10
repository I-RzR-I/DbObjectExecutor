// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorWithDITest
//  Author           : RzR
//  Created On       : 2024-04-01 22:08
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-01 22:08
// ***********************************************************************
//  <copyright file="InitInfo.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using DbObjectExecutor;
using DbObjectExecutor.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbObjectExecutorWithDINet6Test
{
    [TestClass]
    public class InitInfo
    {
        protected IDbObjectBuilder _dbObjectBuilder;

        [TestInitialize]
        public void Init()
        {
            var sp = new ServiceCollection();
            sp.RegisterDbObjectBuilder();

            var serviceProvider = sp.BuildServiceProvider();
            _dbObjectBuilder = serviceProvider.GetRequiredService<IDbObjectBuilder>();
        }
    }
}