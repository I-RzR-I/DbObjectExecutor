// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-03-04 22:57
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-06 00:55
// ***********************************************************************
//  <copyright file="FnGetHostLocationIdTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Abstractions.Params;
using DbObjectExecutor.Builders;
using DbObjectExecutor.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using System.Data.SqlClient;

#endregion

namespace DbObjectExecutorTest.Tests.Functions
{
    [TestClass]
    public class FnGetHostLocationIdTests
    {
        [TestMethod]
        public void FnGetHostLocationId_Execute_As_CommandType_Procedure_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.fnGetHostLocationId, objConn, DbExecutorType.FunctionAsProcedure);
            sp.Return(out IOutputParam<int> intCode);
            sp.ExecuteNonQuery();

            sp.Dispose();

            Assert.IsNotNull(intCode);
            Assert.IsNotNull(intCode.Value);
            Assert.IsTrue(intCode.Value >= 1);
        }

        [TestMethod]
        public void FnGetHostLocationId_Execute_As_CommandType_Procedure_Success_Test_1()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(@$"
                            DECLARE @result INT;
                            EXEC @result = {DataBaseObjectNames.fnGetHostLocationId};
                            SELECT @result
                            ", objConn, DbExecutorType.FunctionAsText);

            sp.ExecuteScalar(out int intCode);

            sp.Dispose();

            Assert.IsNotNull(intCode);
            Assert.IsTrue(intCode >= 1);
        }
    }
}