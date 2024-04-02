// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-03-06 00:41
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-06 00:55
// ***********************************************************************
//  <copyright file="SpGetRecordMultiResultTests.cs" company="">
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
    public class SpGetRecordMultiResultTests
    {
        [DataRow(-1, 0)]
        [DataRow(0, 0)]
        [DataRow(1, 2)]
        [DataRow(2, 1)]
        [TestMethod]
        public void SpGetRecordMultiResult_ManuallyReader_Success_Test(int id, int resultByIdCount)
        {
            var result = new List<SpGetRecordMultiResultGrdDto>();
            var resultGrd = new List<SpGetRecordMultiResultGrdDetailDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.spGetRecordMultiResult, objConn, DbExecutorType.Procedure);
            sp.SetIn("Id", id);

            sp.Execute(reader =>
            {
                if (reader.HasRows)
                    while (reader.Read())
                        result.Add(new SpGetRecordMultiResultGrdDto
                        {
                            Id = int.Parse(reader["Id"].ToString() ?? "-1"),
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            IsActive = reader["IsActive"].ToString() == "1"
                        });

                reader.NextResult();

                if (reader.HasRows)
                    while (reader.Read())
                        resultGrd.Add(new SpGetRecordMultiResultGrdDetailDto
                        {
                            Id = int.Parse(reader["Id"].ToString() ?? "-1"),
                            ParentId = int.Parse(reader["ParentId"].ToString() ?? "-1"),
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            IsActive = reader["IsActive"].ToString() == "1",
                            Address = reader["Address"].ToString(),
                            Location = reader["Location"].ToString(),
                            Country = reader["Country"].ToString()
                        });
            });

            sp.Dispose();

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(resultGrd.Count == resultByIdCount);
        }

        [DataRow(-1, 0)]
        [DataRow(0, 0)]
        [DataRow(1, 2)]
        [DataRow(2, 1)]
        [TestMethod]
        public void SpGetRecordMultiResult_ManuallyReader_Success_Test_1(int id, int resultByIdCount)
        {
            var result = new List<SpGetRecordMultiResultGrdDto>();
            var resultGrd = new List<SpGetRecordMultiResultGrdDetailDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo(DataBaseObjectNames.spGetRecordMultiResult, objConn, DbExecutorType.Procedure);
            sp.SetIn("Id", id);

            sp.Execute(reader =>
            {
                result = reader.ToList<SpGetRecordMultiResultGrdDto>();

                reader.NextResult();

                resultGrd = reader.ToList<SpGetRecordMultiResultGrdDetailDto>();
            });

            sp.Dispose();

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(resultGrd.Count == resultByIdCount);
        }
    }
}