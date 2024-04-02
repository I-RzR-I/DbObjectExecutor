# USING

From the beginning, it is mandatory to know that in core functionalities are defined parameters, data mapper, request builder, and implemented general execution.

Parameter definition:

* `Input` -> Set information about input parameters;
    
    * `SetIn(DbParameter parameter)`;
    * `SetIn<T>(string name, T value)`;
    
* `Output` -> Set information about output parameters;

    * `SetOut<T>(string name, out IOutputParam<T> outputParameter, T defaultValue = default)`;
    * `SetOut<T>(string name, out IOutputParam<T> outputParameter, T defaultValue = default, int size = 0, byte precision = 0, byte scale = 0)`;
    
* `InputOutput` -> Set information about input and output parameters;

    * `SetInOut<T>(string name, T value, out IOutputParam<T> outputParameter, T defaultValue = default)`;
    * `SetInOut<T>(string name, T value, out IOutputParam<T> outputParameter, T defaultValue = default, int size = 0, byte precision = 0, byte scale = 0)`;
    
* `Return` -> Set information about return parameters;
    
    * `Return<T>(out IOutputParam<T> returnParam)`;

---
Additional available methods that can help in execution:
* `SetInitInfo(string nameOrQuery, DbConnection connection, DbExecutorType commandType = DbExecutorType.Query)` -> Method that initializes all necessary objects and details for execution;
* `SetTimeout(TimeSpan timeout)` -> Set command timeout;
* `UseTransaction(DbTransaction transaction = null)` -> Set your own database transaction or initialize one;
* `CommitTransaction()` -> Commit current database transaction;
* `RollBackTransaction()` -> Rollback current database transaction;
* `UseDataBase(string databaseName)` -> Allow to change the database, different than is in the connection string;

---
Defined methods to execute the current request:
* `Execute(Action<DbDataReader> action)`;
* `ExecuteAsync(Func<DbDataReader, Task> function)`;
* `ExecuteAsync(Func<DbDataReader, Task> function, CancellationToken cancellationToken)`;
* `ExecuteNonQuery()`;
* `ExecuteNonQueryAsync()`;
* `ExecuteNonQueryAsync(CancellationToken cancellationToken)`;
* `ExecuteScalar<T>(out T val)`;
* `ExecuteScalarAsync<T>(Action<T> action)`;
* `ExecuteScalarAsync<T>(Action<T> action, CancellationToken cancellationToken)`;

Some additional functionalities are implemented in `DbObjectExecutor`: 
* An attribute that can decorate a property/field from the call as a result `DbObjectColumn`. This attribute will have an effect if you use `DbDataReader` extensions to get results. Some examples of how can be used can be found in test files.

In a few words `DbObjectExecutor.Attribute` is based on attribute data. The requests that will be executed are generated from all info that was included in property/field or class decoration.

Available attributes are: 
* `DbObjectName` -> represend name of the stored procedure, function, view;
* `DbObjectParam` -> represent information about request parameter.

To init execute request builder is avaiable method:
* `BuildRequest(DbProviderType dbProviderType, DbConnection connection, object requestObjectInfo, Type requestObjectInfoType, string dbObjectName = null, DbExecutorType commandType = DbExecutorType.Undefined, bool useMicrosoftSqlParam = false)`.

If we speak about `DbObjectExecutor.Imp.EntityFramework`, there are implemented 2 extensions for `DbContext` that will allow us to build and execute requests as in the previously mentioned modules.

---
Examples:
---

> There are only a few examples. More examples and more details you can be found in the project files (through test files).


`DbObjectExecutor`

```csharp
//  Get/create DB connection
var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);

//  Initialize database request builder
var sp = new DbObjectBuilder();

//  Set required info for pre-execution initialization
sp.SetInitInfo(DataBaseObjectNames.spNewTableId, objConn, DbExecutorType.Procedure).UseTransaction();
//  Set input param
sp.SetIn("TableName", "TblX");
//  Set output param
sp.SetOut("NextId", out var outNextId, -1);

//  Execute current request;
sp.ExecuteNonQuery();

//  Commit transaction
sp.CommitTransaction()
    .Dispose();
```
---
`DbObjectExecutor.Attribute`

### Example 1:

Define procedure request model with necessary attributes.
```csharp
[DbObjectName(DataBaseObjectNames.spNewTableId, DbExecutorType.Procedure)]
public class SpNewTableIdRequest
{
    [DbObjectParam("TableName")]
    public string TableName { get; set; }

    [DbObjectParam("NextId", DbParamDirectionType.Output)]
    public int OutNextId { get; }
}
```


```csharp
//  Get/create DB connection
var objConn = new SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);

//  Set request data
var request = new SpNewTableIdRequest() { TableName = "TblX" };

//  Build current request
var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request, typeof(SpNewTableIdRequest));

procedureRequest.DbObjectBuilder.ExecuteNonQuery();
procedureRequest.DbObjectBuilder.Dispose();

var outNextId = procedureRequest.OutParams
    .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutNextId));
```

### Example 2:

```csharp
public class SpGetRecordPagedCustom2Dto
{
    public int Id { get; set; }

    [DbObjectColumn("Code")]
    public string C { get; set; }

    public string Name { get; set; }

    [DbObjectColumn("IsActive")]
    public bool TF { get; set; }
}
```

```csharp
var result = new SpGetRecordPagedCustom2Dto();
var objConn = new System.Data.SqlClient.SqlConnection(DataBaseHelper.ConnectionStringDefaultMsSql);
var request = new SpGetRecordPagedRequest() { Skip = 0, Take = 1 };
var procedureRequest = DbObjectExecutorRequestBuilder.BuildRequest(DbProviderType.MsSql, objConn, request,
    typeof(SpGetRecordPagedRequest), DataBaseObjectNames.spGetRecordPaged, DbExecutorType.Procedure);

procedureRequest.DbObjectBuilder.Execute(reader => result = reader.FirstOrDefault<SpGetRecordPagedCustom2Dto>());

procedureRequest.DbObjectBuilder.Dispose();

var outRowCount = procedureRequest.OutParams
    .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutRowsCount));
```


---
`DbObjectExecutor.Imp.EntityFramework`

### Example 1:

```csharp
var sp = DbContext.LoadDbObject(DataBaseObjectNames.spNewTableId, DbExecutorType.Procedure);
sp.SetIn("TableName", "TblX");
sp.SetOut("NextId", out var outNextId, -1);

sp.ExecuteNonQuery();

sp.CommitTransaction()
    .Dispose();
```

### Example 2:
```csharp
var request = new SpNewTableIdRequest() { TableName = "TblX" };

var sp = DbContext.LoadDbObject(DbProviderType.MsSql, request, typeof(SpNewTableIdRequest), 
    DataBaseObjectNames.spNewTableId, DbExecutorType.Procedure);

sp.DbObjectBuilder.ExecuteNonQuery();
sp.DbObjectBuilder.Dispose();

var outNextId = sp.OutParams
    .FirstOrDefault(x => x.ParameterName == DbObjectExecutorAttributeHelper.GetDbObjectParam(() => request.OutNextId));
```

