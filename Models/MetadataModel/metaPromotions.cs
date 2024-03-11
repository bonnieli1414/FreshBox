namespace FreshBox.Models
{
    /// <summary>
    /// 促銷商品明細表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaPromotions))]
    public partial class Promotions
    {
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProdName { get; set; }
    }
}

public class z_metaPromotions
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序編號")]
    public string? SortNo { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "開始時間")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
    public DateTime StartTime { get; set; }
    [Display(Name = "結束時間")]
    public DateTime EndTime { get; set; }
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
    [Display(Name = "促銷價格")]
    public int SalePrice { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}