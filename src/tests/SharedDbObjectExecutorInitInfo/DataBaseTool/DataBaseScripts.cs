// ***********************************************************************
//  Assembly         : RzR.Shared.Entity.SharedDbObjectExecutorInitInfo
//  Author           : RzR
//  Created On       : 2024-03-14 22:34
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-03-14 22:36
// ***********************************************************************
//  <copyright file="DataBaseScripts.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbObjectExecutorAttributeTest")]
[assembly: InternalsVisibleTo("DbObjectExecutorTest")]

namespace SharedDbObjectExecutorInitInfo.DataBaseTool
{
    internal static class DataBaseScripts
    {
        internal const string DropDb = @"ALTER DATABASE [DbObjectExecutor] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [DbObjectExecutor]";

        internal static readonly string CreateDb = @$"IF EXISTS(SELECT * FROM sys.databases WHERE name = 'DbObjectExecutor')
                                                BEGIN {DropDb} END 
                                            CREATE DATABASE DbObjectExecutor;";
    }
}