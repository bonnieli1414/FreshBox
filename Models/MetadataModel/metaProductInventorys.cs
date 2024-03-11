namespace FreshBox.Models
{
    /// <summary>
    /// 商品各屬性庫存明細表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaProductInventorys))]
    public partial class ProductInventorys
    {
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string? ProdName { get; set; }
        [NotMapped]
        [Display(Name = "屬性名稱")]
        public string? PropertyName { get; set; }
    }
}

public class z_metaProductInventorys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "屬性編號")]
    public string? PropertyNo { get; set; }
    [Display(Name = "計算庫存")]
    public bool IsInventory { get; set; }
    [Display(Name = "庫存數量")]
    public int InventoryQty { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}
