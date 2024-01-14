// Models/ProductItem.cs
public class ProductItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string GroupName { get; set; }
    public string SubGroupName { get; set; }
    public string? NameJp { get; set; }
    public string? NameKr { get; set; }
    public int AvailableUnit { get; set; }
    public bool SoftDelete { get; set; }
}
