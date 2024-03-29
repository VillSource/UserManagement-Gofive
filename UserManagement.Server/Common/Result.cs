namespace UserManagement.Server.Common;

public class Result
{
    public Exception? Error { get; set; }
    public bool IsSeccess
    {
        get
        {
            return Error is null;
        }
    }
    public string? Message { get; set; }
}
public class Result<T> : Result
{
    public T? Data { get; set; }
}
