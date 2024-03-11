namespace FreshBox.Models
{
    /// <summary>
    /// 商品屬性表格類別
    /// </summary>
    public partial class ProductPropertys
    {
        [NotMapped]
        public string? PropertyName { get; set; }
    }
}

public class z_metaProductPropertys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "可選")]
    public bool IsSelect { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "屬性編號")]
    public string? PropertyNo { get; set; }
    [Display(Name = "屬性名稱")]
    public string? PropertyValue { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}