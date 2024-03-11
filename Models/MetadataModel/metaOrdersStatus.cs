namespace FreshBox.Models
{
    /// <summary>
    /// 訂單狀態表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaOrdersStatus))]
    public partial class OrdersStatus
    {

    }

}

public class z_metaOrdersStatus
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "已付款")]
    public bool IsPayed { get; set; }
    [Display(Name = "已結案")]
    public bool IsClosed { get; set; }
    [Display(Name = "狀態編號")]
    public string? StatusNo { get; set; }
    [Display(Name = "狀態名稱")]
    public string? StatusName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
