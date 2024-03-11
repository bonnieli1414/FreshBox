namespace FreshBox.Models
{
    [ModelMetadataType(typeof(z_metaCalendars))]
    public partial class Calendars
    {
        [NotMapped]
        [Display(Name = "類別名稱")]
        public string CodeName { get; set; } = "";
        [NotMapped]
        [Display(Name = "開始日期")]
        public string EventStart { get { return StartDate.ToString("yyyy/MM/dd"); } }
        [NotMapped]
        [Display(Name = "結束日期")]
        public string EventEnd { get { return EndDate.ToString("yyyy/MM/dd"); } }
        [NotMapped]
        [Display(Name = "時始小時")]
        public string StartHour { get { return (string.IsNullOrEmpty(StartTime)) ? "00" : StartTime.Substring(0, 2); } }
        [NotMapped]
        [Display(Name = "時始分鐘")]
        public string StartMinute { get { return (string.IsNullOrEmpty(StartTime)) ? "00" : StartTime.Substring(3, 2); } }
        [NotMapped]
        [Display(Name = "時始小時")]
        public string EndHour { get { return (string.IsNullOrEmpty(EndTime)) ? "00" : EndTime.Substring(0, 2); } }
        [NotMapped]
        [Display(Name = "時始分鐘")]
        public string EndMinute { get { return (string.IsNullOrEmpty(EndTime)) ? "00" : EndTime.Substring(3, 2); } }
    }
}

public class z_metaCalendars
{
    [Key]
    public int Id { get; set; } = 0;
    [Display(Name = "對象類別")]
    public string TargetCode { get; set; } = "";
    [Display(Name = "對象代號")]
    public string TargetNo { get; set; } = "";
    [Display(Name = "類別")]
    public string CodeNo { get; set; } = "";
    [Display(Name = "主旨")]
    public string SubjectName { get; set; } = "";
    [Display(Name = "開始日期")]
    public System.DateTime StartDate { get; set; } = DateTime.Now;
    [Display(Name = "開始時間")]
    public string StartTime { get; set; } = "00";
    [Display(Name = "結束日期")]
    public System.DateTime EndDate { get; set; } = DateTime.Now;
    [Display(Name = "結束時間")]
    public string EndTime { get; set; } = "23";
    [Display(Name = "行程顏色")]
    public string ColorName { get; set; } = "";
    [Display(Name = "全天行程")]
    public bool IsFullday { get; set; } = false;
    [Display(Name = "地點名稱")]
    public string PlaceName { get; set; } = "";
    [Display(Name = "連絡人")]
    public string ContactName { get; set; } = "";
    [Display(Name = "連絡電話")]
    public string ContactTel { get; set; } = "";
    [Display(Name = "地點地址")]
    public string PlaceAddress { get; set; } = "";
    [Display(Name = "會議室號")]
    public string RoomNo { get; set; } = "";
    [Display(Name = "攜帶物品")]
    public string ResourceText { get; set; } = "";
    [Display(Name = "詳細內容")]
    public string Description { get; set; } = "";
    [Display(Name = "備註")]
    public string Remark { get; set; } = "";
}