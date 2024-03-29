namespace UserManagement.Server.Models.Wrappers;

public class ResponseWrapper <T>
{
    public ResponseWrapperStatus? Status { get; set; }
    public T? Data { get; set; }
}

public class ResponseWrapperStatus
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
