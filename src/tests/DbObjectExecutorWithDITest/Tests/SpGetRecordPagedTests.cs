#region U S A G E S

using DbObjectExecutor.Enums;
using DbObjectExecutor.Mapper.Extensions.DbDataReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using SharedDbObjectExecutorInitInfo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace DbObjectExecutorWithDITest.Tests
{
    [TestClass]
    public class SpGetRecordPagedTests : InitInfo
    {
        [TestMethod]
        public void SpGetRecordPaged_ManuallyReader_Success_Test()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);

            _dbObjectBuilder.SetInitInfo(DataBaseObjectNames.spGetRecordPaged, objConn, DbExecutorType.Procedure);
            _dbObjectBuilder.SetIn("Skip", "0");
            _dbObjectBuilder.SetIn("Take", "5");
            _dbObjectBuilder.SetIn("OrderBy", "Name");
            _dbObjectBuilder.SetIn("Where", "");
            _dbObjectBuilder.SetOut("RowsCount", out var outNextId, 0);

            _dbObjectBuilder.Execute(reader =>
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

            _dbObjectBuilder.Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void SpGetRecordPaged_ManuallyReader_Success_Test_1()
        {
            var result = new List<SpGetRecordPagedDto>();
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            
            _dbObjectBuilder.SetInitInfo(DataBaseObjectNames.spGetRecordPaged, objConn, DbExecutorType.Procedure);
            _dbObjectBuilder.SetIn("Skip", "0");
            _dbObjectBuilder.SetIn("Take", "5");
            _dbObjectBuilder.SetIn("OrderBy", "Name");
            _dbObjectBuilder.SetIn("Where", "");
            _dbObjectBuilder.SetOut("RowsCount", out var outNextId, 0);

            _dbObjectBuilder.Execute(reader => result = reader.ToList<SpGetRecordPagedDto>());

            _dbObjectBuilder.Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > 0);
            Assert.IsTrue(result.Count == 5);
        }
    }
}