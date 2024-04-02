// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-03-04 22:47
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-06 00:55
// ***********************************************************************
//  <copyright file="FnGetDbTypeTests.cs" company="">
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
    [DoNotParallelize]
    public class FnGetDbTypeTests
    {
        [TestMethod]
        public void FnGetDbType_Execute_As_CommandType_Text_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();
            var data = string.Empty;
            sp.SetInitInfo($"SELECT dbo.{DataBaseObjectNames.fnGetDbType}()", objConn, DbExecutorType.FunctionAsText);

            sp.Execute(reader =>
            {
                if (reader.HasRows)
                    while (reader.Read())
                        data = reader[0].ToString();
            });

            sp.Dispose();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 1);
            Assert.IsTrue(data.Equals("HeadOffice"));
        }

        [TestMethod]
        public void FnGetDbType_Execute_As_CommandType_Text_Success_Test_1()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo($"SELECT dbo.{DataBaseObjectNames.fnGetDbType}()", objConn, DbExecutorType.FunctionAsText);

            sp.ExecuteScalar(out string data);

            sp.Dispose();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 1);
            Assert.IsTrue(data.Equals("HeadOffice"));
        }

        [TestMethod]
        public void FnGetDbType_Execute_As_CommandType_Procedure_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.fnGetDbType, objConn, DbExecutorType.FunctionAsProcedure);
            sp.Return(out IOutputParam<string> stringName);
            sp.ExecuteNonQuery();

            sp.Dispose();

            Assert.IsNotNull(stringName);
            Assert.IsNotNull(stringName.Value);
            Assert.IsTrue(stringName.Value.Length > 1);
            Assert.IsTrue(stringName.Value.Equals("HeadOffice"));
        }
    }
}