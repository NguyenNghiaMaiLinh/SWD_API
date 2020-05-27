namespace DemoApp.Core.Data.Enity
{
    public partial class Product : BaseEntity
    {

            public string Code { get; set; }
            public string Name { get; set; }
            public int? Status { get; set; }
            public decimal? Price { get; set; }
            public bool? IsDelete { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
        
    }
}
