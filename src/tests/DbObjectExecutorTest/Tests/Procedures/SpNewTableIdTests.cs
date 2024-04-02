// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-03-04 22:32
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-06 00:55
// ***********************************************************************
//  <copyright file="SpNewTableIdTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Builders;
using DbObjectExecutor.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using System.Data.SqlClient;

#endregion

namespace DbObjectExecutorTest.Tests.Procedures
{
    [TestClass]
    [DoNotParallelize]
    public class SpNewTableIdTests
    {
        [TestMethod]
        public void SpNewTableId_Own_Transaction_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.spNewTableId, objConn, DbExecutorType.Procedure).UseTransaction();
            sp.SetIn("TableName", "");
            sp.SetOut("NextId", out var outNextId, -1);

            sp.ExecuteNonQuery();

            sp.CommitTransaction()
                .Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > -1);
        }

        [TestMethod]
        public void SpNewTableId_Separate_Transaction_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            objConn.Open();
            var trans = objConn.BeginTransaction();

            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.spNewTableId, objConn, DbExecutorType.Procedure).UseTransaction(trans);
            sp.SetIn("TableName", "");
            sp.SetOut("NextId", out var outNextId, 0);

            sp.ExecuteNonQuery();

            sp.CommitTransaction()
                .Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > -1);
        }
    }
}