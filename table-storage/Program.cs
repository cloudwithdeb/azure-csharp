using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Azure.Identity;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Define variables
string storageUri = "https://azfunctiondemos8865.table.core.windows.net";
string table_name = "Students";

var _service_client = new TableServiceClient(
    new Uri(storageUri),
    new DefaultAzureCredential()
);

var _table_client = new TableClient(
    new Uri(storageUri),
    tableName: table_name,
    new DefaultAzureCredential()
);

// Default route
app.MapGet("/", () => "Hello, Welcome to azure table storage with dotnet core 6");

//Create azure storage table
app.MapPost("/createtable", (string tablename) => {

    TableItem table =  _service_client.CreateTableIfNotExists(tablename);
    return $"Table created successfully with name {table.Name}. Thank you!";

});


// Insert Items into Entity
app.MapPost("/adduser", (UsersDataRequest user) => {

    string _PartitionKey = Guid.NewGuid().ToString();
    string _RowKey = Guid.NewGuid().ToString();

    var entity = new TableEntity(_PartitionKey, _RowKey)
    {
        { "username", user.username },
        { "age", user.age },
        { "amount", user.amount }
    };

    // Add entity
    _table_client.AddEntity(entity);

    return "User added successfully. Thank you!";

});

app.UseHttpsRedirection();
app.Run();

