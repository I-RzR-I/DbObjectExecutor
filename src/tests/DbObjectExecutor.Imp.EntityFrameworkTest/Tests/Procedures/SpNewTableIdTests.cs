// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.DbObjectExecutor.Imp.EntityFrameworkNet5Test
//  Author           : RzR
//  Created On       : 2024-03-19 17:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-19 19:19
// ***********************************************************************
//  <copyright file="SpNewTableIdTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DbObjectExecutor.Attribute.Enums;
using DbObjectExecutor.Attribute.Extensions;
using DbObjectExecutor.Attribute.Helpers;
using DbObjectExecutor.Enums;
using DbObjectExecutor.Imp.EntityFramework;
using DbObjectExecutor.Imp.EntityFrameworkNet5Test.Models.Procedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using System.Linq;

#endregion

namespace DbObjectExecutor.Imp.EntityFrameworkNet5Test.Tests.Procedures
{
    [TestClass]
    [DoNotParallelize]
    public class SpNewTableIdTests : EntityFrameworkInit
    {
        [TestMethod]
        public void SpNewTableId_Own_Transaction_Success_Test()
        {
            var sp = DbContext.LoadDbObject(DataBaseObjectNames.spNewTableId, DbExecutorType.Procedure);
            sp.SetIn("TableName", "");
            sp.SetOut("NextId", out var outNextId, -1);

            sp.ExecuteNonQuery();

            sp.CommitTransaction()
                .Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > -1);
        }

        [TestMethod]
        public void SpNewTableId_Own_Transaction_Success_DbContext_Test()
        {
            var request = new SpNewTableIdRequest() { TableName = "TblX" };

            var sp = DbContext.LoadDbObject(DbProviderType.MsSql, request, typeof(SpNewTableIdRequest), 
                DataBaseObjectNames.spNewTableId, DbExecutorType.Procedure);

            sp.DbObjectBuilder.ExecuteNonQuery();
            sp.DbObjectBuilder.Dispose();

            //var outNextId = sp.OutParams
            //    .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutNextId));
            //var outNextId = sp.OutParams
            //        .FirstOrDefault(x => x.ParameterName == nameof(request.OutNextId).GetDbObjectParam(request));
            var outNextId = sp.OutParams
                    .FirstOrDefault(x => x.ParameterName == nameof(request.OutNextId).GetDbObjectParam(request.GetType()));

            Assert.IsNotNull(outNextId);
            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue((int)outNextId.Value > 0);
        }
    }
}