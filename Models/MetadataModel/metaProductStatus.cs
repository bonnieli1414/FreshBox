namespace FreshBox.Models
{
    /// <summary>
    /// 商品狀態表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaProductStatus))]
    public partial class ProductStatus
    {
    }
}

public class z_metaProductStatus
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "狀態編號")]
    public bool StatusNo { get; set; }
    [Display(Name = "狀態名稱")]
    public string? StatusName { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}