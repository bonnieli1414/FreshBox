using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshBox.Models
{
    [ModelMetadataType(typeof(z_metaNews))]
    public partial class News
    {
    }
}

public class z_metaNews
{
    [Key]
    public int Id { get; set; } = 0;
    [Display(Name = "CodeNo")]
    public string? CodeNo { get; set; } = "";
    [Display(Name = "PublishDate")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime PublishDate { get; set; } = DateTime.Today;
    [Display(Name = "HeaderName")]
    public string? HeaderName { get; set; } = "";
    [Display(Name = "DetailText")]
    public string? DetailText { get; set; } = "";
    [Display(Name = "Remark")]
    public string? Remark { get; set; } = "";
}
