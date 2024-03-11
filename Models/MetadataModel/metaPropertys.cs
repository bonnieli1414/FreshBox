namespace FreshBox.Models
{
    /// <summary>
    /// 商品屬性主檔表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaPropertys))]
    public partial class Propertys
    {

    }
}

public partial class z_metaPropertys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "可選取")]
    public bool IsSelect { get; set; }
    [Display(Name = "屬性編號")]
    public string? PropertyNo { get; set; }
    [Display(Name = "屬性名稱")]
    public string? PropertyName { get; set; }
    [Display(Name = "屬性值")]
    public string? PropertyValue { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}