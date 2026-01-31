namespace AssociationEntities.CustomModels
{
    public partial class BlogImageModel
    {
        public int ImageId { get; set; }

        public int BlogId { get; set; }

        public int TenantId { get; set; }

        public string? ImageUrl { get; set; }

        public string? Text { get; set; }

        public int Position { get; set; }

        public string? Udf { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }

}
