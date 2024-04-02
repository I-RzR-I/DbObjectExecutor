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

using DbObjectExecutor.Attribute.Builders;
using DbObjectExecutor.Attribute.Enums;
using DbObjectExecutor.Attribute.Helpers;
using DbObjectExecutor.Enums;
using DbObjectExecutor.Mapper.Extensions.DbDataReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DbObjectExecutorAttributeTest.Models.Procedures;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using SharedDbObjectExecutorInitInfo.Models;
using SharedDbObjectExecutorInitInfo.Models.Procedures.Result;
using System.Threading.Tasks;

#endregion

namespace DbObjectExecutorAttributeTest.Tests.Procedures
{
    [TestClass]
    public class SpGetRecordPagedTests
    {
        [TestMethod]
        public void SpGetRecordPaged_ManuallyReader_Success_Test()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 5 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged);
            
            procedureRequest.DbObjectBuilder.Execute(reader =>
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

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_MapReader_Success_Test()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 5 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged);

            procedureRequest.DbObjectBuilder.Execute(reader => result = reader.ToList<SpGetRecordPagedDto>());

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));

            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public async Task SpGetRecordPaged_MapReader_Success_Async_Test()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 5 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged);

            await procedureRequest.DbObjectBuilder.ExecuteAsync(async reader => result = await reader.ToListAsync<SpGetRecordPagedDto>());

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));

            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_ManuallyReader_Success_Test_2()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 5 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest));
            
            procedureRequest.DbObjectBuilder.Execute(reader =>
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

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_MapReader_Success_Test_2()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 5 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest));

            procedureRequest.DbObjectBuilder.Execute(reader => result = reader.ToList<SpGetRecordPagedDto>());

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_ManuallyReader_Success_Test_3()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 5 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged, DbExecutorType.Procedure);
            
            procedureRequest.DbObjectBuilder.Execute(reader =>
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

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_MapReader_Success_Test_3()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 5 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged, DbExecutorType.Procedure);

            procedureRequest.DbObjectBuilder.Execute(reader => result = reader.ToList<SpGetRecordPagedDto>());

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_AttributeMapReader_Success_Test()
        {
            var result = new List<SpGetRecordPagedCustomDto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 1 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged, DbExecutorType.Procedure);

            procedureRequest.DbObjectBuilder.Execute(reader => result = reader.ToList<SpGetRecordPagedCustomDto>());

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 1);

            var firstRecord = result[0];
            
            Assert.IsNotNull(firstRecord);
            Assert.IsNotNull(firstRecord.Id);
            Assert.IsNotNull(firstRecord.C);
            Assert.IsNotNull(firstRecord.N);
            Assert.IsNotNull(firstRecord.TF);
        }

        [TestMethod]
        public void SpGetRecordPaged_AttributeMapReader_Success_Test_2()
        {
            var result = new List<SpGetRecordPagedCustom2Dto>();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 1 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged, DbExecutorType.Procedure);

            procedureRequest.DbObjectBuilder.Execute(reader => result = reader.ToList<SpGetRecordPagedCustom2Dto>());

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            Assert.IsTrue(result.Count == 1);

            var firstRecord = result[0];
            
            Assert.IsNotNull(firstRecord);
            Assert.IsNotNull(firstRecord.Id);
            Assert.IsNotNull(firstRecord.C);
            Assert.IsNotNull(firstRecord.Name);
            Assert.IsNotNull(firstRecord.TF);
        }

        [TestMethod]
        public void SpGetRecordPaged_AttributeMapReader_Success_Test_3()
        {
            var result = new SpGetRecordPagedCustom2Dto();
            var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 1 };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
                typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged, DbExecutorType.Procedure);

            procedureRequest.DbObjectBuilder.Execute(reader => result = reader.FirstOrDefault<SpGetRecordPagedCustom2Dto>());

            procedureRequest.DbObjectBuilder.Dispose();

            var outRowCount = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
            
            Assert.IsNotNull(outRowCount);
            Assert.IsNotNull(outRowCount.Value);
            Assert.IsTrue((int)outRowCount.Value > 0);
            
            Assert.IsNotNull(result.Id);
            Assert.IsNotNull(result.C);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.TF);
        }
    }
}