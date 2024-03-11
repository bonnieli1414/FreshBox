using System;
using System.Collections.Generic;

namespace FreshBox.Models
{
    [ModelMetadataType(typeof(z_metaPhotos))]
    public partial class Photos
    {
        [NotMapped]
        [Display(Name = "商品名稱")]
        public string ProdName { get; set; }
    }
}

public class z_metaPhotos
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "圖片類別")]
    public string? CodeNo { get; set; }
    [Display(Name = "圖片編號")]
    public string? PhotoNo { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "資料夾編號")]
    public string? FolderNo { get; set; }
    [Display(Name = "資料夾名稱")]
    public string? FolderName { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}
