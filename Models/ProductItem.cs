// Models/ProductItem.cs
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        // public string GroupName { get; set; }
        // public string SubGroupName { get; set; }
        public string? NameJp { get; set; }
        public string? NameKr { get; set; }
        public int AvailableUnit { get; set; }
        public bool SoftDelete { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public int ProductSubGroupId { get; set; }
        public ProductSubGroup ProductSubGroup { get; set; }
    }
}
