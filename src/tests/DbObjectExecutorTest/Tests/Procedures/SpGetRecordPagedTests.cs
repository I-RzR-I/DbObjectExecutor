// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-03-05 23:57
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-06 00:55
// ***********************************************************************
//  <copyright file="SpGetRecordPagedTests.cs" company="">
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

namespace DbObjectExecutorTest.Tests.Procedures
{
    [TestClass]
    public class SpGetRecordPagedTests
    {
        [TestMethod]
        public void SpGetRecordPaged_ManuallyReader_Success_Test()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.spGetRecordPaged, objConn, DbExecutorType.Procedure);
            sp.SetIn("Skip", "0");
            sp.SetIn("Take", "5");
            sp.SetIn("OrderBy", "Name");
            sp.SetIn("Where", "");
            sp.SetOut("RowsCount", out var outNextId, 0);

            sp.Execute(reader =>
            {
                if (reader.HasRows)
                    while (reader.Read())
                        result.Add(new SpGetRecordPagedDto
                        {
                            Id = int.Parse(reader["Id"].ToString() ?? "-1"),
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            IsActive = reader["IsActive"].ToString() == "1"
                        });
            });

            sp.Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_ManuallyReader_Success_Test_1()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.spGetRecordPaged, objConn, DbExecutorType.Procedure);
            sp.SetIn("Skip", "0");
            sp.SetIn("Take", "5");
            sp.SetIn("OrderBy", "Name");
            sp.SetIn("Where", "");
            sp.SetOut("RowsCount", out var outNextId, 0);

            sp.Execute(reader => result = reader.ToList<SpGetRecordPagedDto>());

            sp.Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }
    }
}