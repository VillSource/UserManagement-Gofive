namespace UserManagement.Server.Models.Wrappers;

public class PaginationWrapper <T>
{
    public T? DataSource { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotolCount { get; set; }
}
