namespace FreshBox.Models
{
    [ModelMetadataType(typeof(z_metaMessages))]
    public partial class Messages
    {

    }
}
public class z_metaMessages
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "是否已讀")]
    public bool IsRead { get; set; }
    [Display(Name = "類別")]
    public string? CodeNo { get; set; }
    [Display(Name = "寄送編號")]
    public string? SenderNo { get; set; }
    [Display(Name = "接收編號")]
    public string? ReceiverNo { get; set; }
    [Display(Name = "日期")]
    public DateTime SendDate { get; set; }
    [Display(Name = "時間")]
    public TimeSpan SendTime { get; set; }
    [Display(Name = "聯絡姓名")]
    public string ContactorName { get; set; } = "";
    [Display(Name = "電子信箱")]
    public string ContactorEmail { get; set; } = "";
    [Display(Name = "主旨")]
    public string? HeaderText { get; set; }
    [Display(Name = "內容")]
    public string? MessageText { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}