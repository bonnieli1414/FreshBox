namespace FreshBox.Models
{
    /// <summary>
    /// 購物車資料表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaCarts))]
    public partial class Carts
    {

    }
}

public class z_metaCarts
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "批次代號")]
    public string? LotNo { get; set; }
    [Display(Name = "會員編號")]
    public string? MemberNo { get; set; }
    [Display(Name = "廠商編號")]
    public string? VendorNo { get; set; }
    [Display(Name = "商品分類代號")]
    public string? CategoryNo { get; set; }
    [Display(Name = "商品分類")]
    public string? CategoryName { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "商品名稱")]
    public string? ProdName { get; set; }
    [Display(Name = "商品規格")]
    public string? ProdSpec { get; set; }
    [Display(Name = "訂購數量")]
    public int OrderQty { get; set; }
    [Display(Name = "商品單價")]
    public int OrderPrice { get; set; }
    [Display(Name = "訂單金額")]
    public int OrderAmount { get; set; }
    [Display(Name = "建檔時間")]
    public DateTime CreateTime { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}
