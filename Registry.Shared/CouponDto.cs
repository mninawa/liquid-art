namespace Registry.Shared;

public class CouponDto 
{ 
    // [IgnoreDataMember] public string? Id { get; set; }
    public int OperatorId { get; set; }
    public string CouponImg { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public DateTime ExpireDate { get; set; }
    public float MinAmt { get; set; }
    public float CouponVal { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; } = 1; // Default value
}