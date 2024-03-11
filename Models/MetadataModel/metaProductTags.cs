namespace FreshBox.Models
{
    /// <summary>
    /// 商品標籤明細表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaProductTags))]
    public partial class ProductTags
    {
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProdName { get; set; }
    }
}

public class z_metaProductTags
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "商品編號")]
    public bool ProdNo { get; set; }
    [Display(Name = "標籤名稱")]
    public string? TagName { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}
