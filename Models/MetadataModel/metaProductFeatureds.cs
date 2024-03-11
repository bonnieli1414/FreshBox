namespace FreshBox.Models
{
    /// <summary>
    /// 購物車資料表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaProductFeatureds))]
    public partial class ProductFeatureds
    {
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProdName { get; set; }
    }
}

public class z_metaProductFeatureds
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "排序編號")]
    public string? SortNo { get; set; }
    [Display(Name = "特色說明項目")]
    public string? FeaturedName { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}