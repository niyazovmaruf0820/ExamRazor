namespace Domain.Entities;

public class Account : BaseEntity
{
    public string? AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int OwnerId { get; set; }
    public string? Type { get; set; }
    
    public Customer? Owner { get; set; }
}
