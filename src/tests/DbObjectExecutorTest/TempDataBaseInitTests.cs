// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-03-04 21:50
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-04 21:50
// ***********************************************************************
//  <copyright file="TempDataBaseInitTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;

namespace DbObjectExecutorTest
{
    [TestClass]
    [DoNotParallelize]
    public static class TempDataBaseInitTests
    {
        [TestInitialize]
        //[AssemblyInitialize]
        public static void Init(TestContext context) 
            => DataBaseHelper.CreateDataBase();

        [TestCleanup]
        //[AssemblyCleanup()]
        public static void Cleanup(TestContext context) 
            => DataBaseHelper.DropDataBase();
    }
}