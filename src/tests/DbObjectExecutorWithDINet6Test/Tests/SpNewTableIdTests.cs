using DbObjectExecutor.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDbObjectExecutorInitInfo.DataBaseTool;
using System.Data.SqlClient;

namespace DbObjectExecutorWithDINet6Test.Tests
{
    [TestClass]
    public class SpNewTableIdTests : InitInfo
    {
        [TestMethod]
        public void SpNewTableId_Own_Transaction_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            
            _dbObjectBuilder.SetInitInfo(DataBaseObjectNames.spNewTableId, objConn, DbExecutorType.Procedure).UseTransaction();
            _dbObjectBuilder.SetIn("TableName", "TblX");
            _dbObjectBuilder.SetOut("NextId", out var outNextId, -1);

            _dbObjectBuilder.ExecuteNonQuery();

            _dbObjectBuilder.CommitTransaction()
                .Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > -1);
        }

        [TestMethod]
        public void SpNewTableId_Separate_Transaction_Success_Test()
        {
            var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
            objConn.Open();
            var trans = objConn.BeginTransaction();

            _dbObjectBuilder.SetInitInfo(DataBaseObjectNames.spNewTableId, objConn, DbExecutorType.Procedure).UseTransaction(trans);
            _dbObjectBuilder.SetIn("TableName", "TblX");
            _dbObjectBuilder.SetOut("NextId", out var outNextId, 0);

            _dbObjectBuilder.ExecuteNonQuery();

            _dbObjectBuilder.CommitTransaction()
                .Dispose();

            Assert.IsNotNull(outNextId.Value);
            Assert.IsTrue(outNextId.Value > -1);
        }
    }
}