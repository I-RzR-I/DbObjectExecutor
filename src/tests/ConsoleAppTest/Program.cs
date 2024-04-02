using DbObjectExecutor.Abstractions;
using DbObjectExecutor.Builders;
using DbObjectExecutor.Enums;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = File.ReadAllText("H:\\DataBases\\biz.connection.txt");

            NewIdWithSeparateTransaction(connection);

            Console.ReadKey();
        }

        private static void NewIdWithOwnTransaction(string connectionString)
        {
            var objConn = new SqlConnection(connectionString);
            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo("spNewTableId", objConn, DbExecutorType.Procedure).UseTransaction();
            sp.SetIn("TableName", "");
            sp.SetOut("NextId", out var outNextId, 0);

            sp.ExecuteNonQuery();

            sp.CommitTransaction()
                .Dispose();

            Console.WriteLine(outNextId.Value);
        }

        private static void NewIdWithSeparateTransaction(string connectionString)
        {
            var objConn = new SqlConnection(connectionString);
            objConn.Open();
            var trans = objConn.BeginTransaction();

            IDbObjectBuilder sp = new DbObjectBuilder();

            sp.SetInitInfo("spNewTableId", objConn, DbExecutorType.Procedure).UseTransaction(trans);
            sp.SetIn("TableName", "");
            sp.SetOut("NextId", out var outNextId, 0);

            sp.ExecuteNonQuery();

            sp.CommitTransaction()
                .Dispose();

            Console.WriteLine(outNextId.Value);
        }
    }
}
