namespace Registry.Shared;

public class FacilityDto
{
    // [IgnoreDataMember] public string? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Img { get; set; } = string.Empty;
    public int Status { get; set; }
}