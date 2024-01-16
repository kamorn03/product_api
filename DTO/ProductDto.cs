namespace ProductApi.Dtos
{
    public class ProductStatusUpdateDto
    {
        public bool IsActive { get; set; }
    }


    public class ProducSearchDto
    {
        public string? name { get; set; }
        public int? groupId { get; set; }
        public int? subGroupId { get; set; }
        public bool? isActive { get; set; }
    }

}
