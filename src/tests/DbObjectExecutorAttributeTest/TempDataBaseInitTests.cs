// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorAttributeTest
//  Author           : RzR
//  Created On       : 2024-03-14 22:37
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-14 22:37
// ***********************************************************************
//  <copyright file="TempDataBaseInitTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;

#endregion

namespace DbObjectExecutorAttributeTest
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
        //[AssemblyCleanup]
        public static void Cleanup(TestContext context) 
            => DataBaseHelper.DropDataBase();
    }
}