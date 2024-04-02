// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.SharedDbObjectExecutorInitInfo
//  Author           : RzR
//  Created On       : 2024-03-14 22:34
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-14 22:36
// ***********************************************************************
//  <copyright file="DataBaseHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("DbObjectExecutorAttributeTest")]
[assembly: InternalsVisibleTo("DbObjectExecutorTest")]
[assembly: InternalsVisibleTo("DbObjectExecutor.Imp.EntityFramework5Test")]
[assembly: InternalsVisibleTo("DbObjectExecutor.Imp.EntityFramework6Test")]
[assembly: InternalsVisibleTo("DbObjectExecutorWithDITest")]

namespace SharedDbObjectExecutorInitInfo.DataBaseTool
{
    internal static class DataBaseHelper
    {
        private const string connectionStringMasterMsSql =
            "TrustServerCertificate=True;Server=localhost;Database=master;MultipleActiveResultSets=true;Trusted_Connection=false;User ID=sa;Password=ZaPassWord-1-9-2-1";

        internal const string ConnectionStringDefaultMsSql =
            "TrustServerCertificate=True;Server=localhost;Database=DbObjectExecutor;MultipleActiveResultSets=true;Trusted_Connection=false;User ID=sa;Password=ZaPassWord-1-9-2-1";

        internal static void ExecuteScript(string script, string connectionString)
        {
            using var conn = new System.Data.SqlClient.SqlConnection(connectionString);
            conn.Open();
            using (var cmd = new System.Data.SqlClient.SqlCommand(script, conn))
            {
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        private static void ExecuteBatchScript(string script, string connectionString)
        {
            using var conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
            var server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
        }

        internal static void CreateDataBase()
        {
            TryDropDataBase();

            ExecuteScript(DataBaseScripts.CreateDb, connectionStringMasterMsSql);

            //Run base functions
            ExecuteBatchScript(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Scripts/Base/Functions/fnCheckIfExistFunction.sql"), ConnectionStringDefaultMsSql);
            ExecuteBatchScript(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Scripts/Base/Functions/fnCheckIfExistProcedure.sql"), ConnectionStringDefaultMsSql);

            CreateFunctions();
            CreateStoredProcedures();
        }

        private static void CreateStoredProcedures()
        {
            foreach (var file in Directory.EnumerateFiles($"{Directory.GetCurrentDirectory()}/Scripts/Procedures", "*.sql"))
                ExecuteBatchScript(File.ReadAllText(file), ConnectionStringDefaultMsSql);
        }

        private static void CreateFunctions()
        {
            foreach (var file in Directory.EnumerateFiles($"{Directory.GetCurrentDirectory()}/Scripts/Functions", "*.sql"))
                ExecuteBatchScript(File.ReadAllText(file), ConnectionStringDefaultMsSql);
        }

        private static void TryDropDataBase()
        {
            try
            {
                DropDataBase();
            }
            catch
            {
            }
        }

        internal static void DropDataBase() => ExecuteScript(DataBaseScripts.DropDb, connectionStringMasterMsSql);
    }
}