public class UsersDatasets
{
    internal string? PartitionKey {get; set;}
    internal string? RowKey {get; set;}
    public string? username {get; set;}
    public int age {get; set;}
    public double? amount {get; set;}
    
    public UsersDatasets(string? _PartitionKey, string? _RowKey, string? _username, int _age, double? _amount)
    {
        PartitionKey=_PartitionKey;
        RowKey=_RowKey;
        username=_username;
        age=_age;
        amount=_amount;
    }
}

public class UsersDataRequest
{
    internal string? PartitionKey {get; set;}
    internal string? RowKey {get; set;}
    public string? username {get; set;}
    public int age {get; set;}
    public double? amount {get; set;}
    
}