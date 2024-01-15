namespace ProductApi.Models
{
    public class ProductSubGroup
    {
        public int Id { get; set; }
        public string SubGroupName { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
    }
}