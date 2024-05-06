namespace Domain.Filters;

public class CustomerFilter : PaginationFilter
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}
