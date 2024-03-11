namespace FreshBox.Models
{
    [ModelMetadataType(typeof(z_metaCodeDatas))]
    public partial class CodeDatas
    {
    }
}

public class z_metaCodeDatas
{
    [Key]
    public int Id { get; set; } = 0;
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; } = true;
    [Display(Name = "類別代號")]
    [Required(ErrorMessage = "類別代號不可空白!!")]
    public string BaseNo { get; set; } = "";
    [Display(Name = "父階代號")]
    public string ParentNo { get; set; } = "";
    [Display(Name = "排序")]
    public string SortNo { get; set; } = "";
    [Display(Name = "代號")]
    public string CodeNo { get; set; } = "";
    [Display(Name = "名稱")]
    public string CodeName { get; set; } = "";
    [Display(Name = "資料值")]
    public string CodeValue { get; set; } = "";
    [Display(Name = "備註")]
    public string Remark { get; set; } = "";
}