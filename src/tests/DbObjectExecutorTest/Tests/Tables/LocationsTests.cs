// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutorTest
//  Author           : RzR
//  Created On       : 2024-04-05 18:42
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-04-05 18:42
// ***********************************************************************
//  <copyright file="LocationsTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Builders;
using DbObjectExecutor.Mapper.Extensions.DbDataReader;
using DbObjectExecutorTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DbObjectExecutorTest.Tests.Tables
{
    [TestClass]
    [DoNotParallelize]
    public class LocationsTests
    {
        [TestMethod]
        public void ReadTableData_Test()
        {
            var resultData = new List<dynamic>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo($"SELECT * FROM {DataBaseObjectNames.Locations}", objConn);

            sp.Execute(reader =>
            {
                if (reader.HasRows)
                    while (reader.Read())
                        resultData.Add(new
                        {
                            Id = int.Parse(reader["Id"].ToString() ?? "-1"),
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            IsActive = reader["IsActive"].ToString() == "1"
                        });
            });

            sp.Dispose();

            Assert.IsTrue(resultData.Count != 0);
            var first = resultData.FirstOrDefault();
            
            Assert.IsNotNull(first);
            Assert.IsTrue(resultData.Count == 5);
        }

        [TestMethod]
        public void ReadTableData_Model_Test()
        {
            var resultData = new List<SharedDbObjectExecutorInitInfo.Models.Tables.Locations>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo($"SELECT * FROM {DataBaseObjectNames.Locations}", objConn);

            sp.Execute(reader => resultData = reader.ToList<SharedDbObjectExecutorInitInfo.Models.Tables.Locations>());

            sp.Dispose();

            Assert.IsTrue(resultData.Count != 0);
            var first = resultData.FirstOrDefault();
            
            Assert.IsNotNull(first);
            Assert.IsTrue(resultData.Count == 5);
        }

        [TestMethod]
        public void ReadTableData_CustomModel_Test()
        {
            var resultData = new List<CustomLocationDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo($"SELECT * FROM {DataBaseObjectNames.Locations}", objConn);

            sp.Execute(reader => resultData = reader.ToList<CustomLocationDto>());

            sp.Dispose();

            Assert.IsTrue(resultData.Count != 0);
            var first = resultData.FirstOrDefault();
            
            Assert.IsNotNull(first);

            Assert.IsNotNull(first.Id);
            Assert.IsNotNull(first.N);
            Assert.IsTrue(resultData.Count == 5);
        }

        [TestMethod]
        public void ReadTableData_CustomModel_SomeColumn_Test()
        {
            var resultData = new List<CustomLocationDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo($"SELECT Id,Name FROM {DataBaseObjectNames.Locations}", objConn);

            sp.Execute(reader => resultData = reader.ToList<CustomLocationDto>());

            sp.Dispose();

            Assert.IsTrue(resultData.Count != 0);
            var first = resultData.FirstOrDefault();
            
            Assert.IsNotNull(first);

            Assert.IsNotNull(first.Id);
            Assert.IsNull(first.C);
            Assert.IsNotNull(first.N);
            Assert.IsTrue(resultData.Count == 5);
        }
    }
}