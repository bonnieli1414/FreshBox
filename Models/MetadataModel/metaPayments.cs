namespace FreshBox.Models
{
    /// <summary>
    /// 付款方式表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaPayments))]
    public partial class Payments
    {

    }
}

public class z_metaPayments
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "付款編號")]
    public string? PaymentNo { get; set; }
    [Display(Name = "付款名稱")]
    public string? PaymentName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}