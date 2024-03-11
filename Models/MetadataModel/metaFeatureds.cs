namespace FreshBox.Models
{
    /// <summary>
    /// 精選商品主檔表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaFeatureds))]
    public partial class Featureds
    {
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProdName { get; set; }
    }
}

public class z_metaFeatureds
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "排序編號")]
    public string? SortNo { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}