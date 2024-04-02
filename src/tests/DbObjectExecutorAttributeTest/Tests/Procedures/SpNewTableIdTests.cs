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

using DbObjectExecutor.Attribute.Builders;
using DbObjectExecutor.Attribute.Enums;
using DbObjectExecutor.Attribute.Helpers;
using DbObjectExecutorAttributeTest.Models.Procedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using System.Data.SqlClient;
using System.Linq;

#endregion

namespace DbObjectExecutorAttributeTest.Tests.Procedures
{
    [TestClass]
    [DoNotParallelize]
    public class SpNewTableIdTests
    {
        [TestMethod]
        public void SpNewTableId_Own_Transaction_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            var request = new SpNewTableIdRequest() { TableName = "TblX" };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request, typeof(SpNewTableIdRequest));

            procedureRequest.DbObjectBuilder.ExecuteNonQuery();
            procedureRequest.DbObjectBuilder.Dispose();

            var outNextId = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutNextId));

            Assert.IsNotNull(outNextId);
            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue((int)outNextId.Value > 0);
        }

        [TestMethod]
        public void SpNewTableId_Separate_Transaction_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            objConn.Open();
            var trans = objConn.BeginTransaction();

            var request = new SpNewTableIdRequest() { TableName = "TblX" };
            var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request, typeof(SpNewTableIdRequest));
            procedureRequest.DbObjectBuilder.UseTransaction(trans);
            procedureRequest.DbObjectBuilder.ExecuteNonQuery();
            procedureRequest.DbObjectBuilder.CommitTransaction().Dispose();

            var outNextId = procedureRequest.OutParams
                .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutNextId));

            Assert.IsNotNull(outNextId);
            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue((int)outNextId.Value > 0);
        }
    }
}