namespace Domain.Filters;

public class AccountFilter : PaginationFilter
{
    public string? AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int OwnerId { get; set; }
    public string? Type { get; set; }
}
