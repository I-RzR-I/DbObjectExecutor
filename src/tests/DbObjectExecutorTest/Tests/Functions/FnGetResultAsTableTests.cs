// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-03-06 01:30
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-06 01:47
// ***********************************************************************
//  <copyright file="FnGetResultAsTableTests.cs" company="">
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
using DbObjectExecutor.Mapper.Extensions.DbDataReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using SharedDbObjectExecutorInitInfo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace DbObjectExecutorTest.Tests.Functions
{
    [TestClass]
    public class FnGetResultAsTableTests
    {
        [TestMethod]
        public void FnGetResultAsTable_Execute_As_CommandType_Text_Success_Test()
        {
            var result = new List<FnGetResultAsTableDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo("SELECT * FROM fnGetResultAsTable(1)", objConn, DbExecutorType.FunctionAsText);

            sp.Execute(reader =>
            {
                if (reader.HasRows)
                    while (reader.Read())
                        result.Add(new FnGetResultAsTableDto
                        {
                            Id = int.Parse(reader["Id"].ToString() ?? "-1"),
                            LocationId = int.Parse(reader["LocationId"].ToString() ?? "-1"),
                            IsVisible = reader["IsVisible"].ToString() == "1"
                        });
            });

            sp.Dispose();

            Assert.IsTrue(result.Count > 5);
        }

        [TestMethod]
        public void FnGetResultAsTable_Execute_As_CommandType_Text_Success_Test_2()
        {
            var result = new List<FnGetResultAsTableDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo("SELECT * FROM fnGetResultAsTable(1)", objConn, DbExecutorType.FunctionAsText);

            sp.Execute(reader => result = reader.ToList<FnGetResultAsTableDto>());

            sp.Dispose();

            Assert.IsTrue(result.Count > 5);
        }
    }
}