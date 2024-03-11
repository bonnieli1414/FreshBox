using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshBox.Models
{
    [ModelMetadataType(typeof(z_metaCompanys))]
    public partial class Companys
    {
        [NotMapped]
        [Display(Name = "公司類別")]
        public string? CodeName { get; set; } = "";
    }
}

public class z_metaCompanys
{
    [Key]
    public int Id { get; set; } = 0;
    [Display(Name = "IsDefault")]
    public bool IsDefault { get; set; } = false;
    [Display(Name = "IsEnabled")]
    public bool IsEnabled { get; set; } = false;
    [Display(Name = "CodeNo")]
    public string? CodeNo { get; set; } = "";
    [Display(Name = "CompNo")]
    public string? CompNo { get; set; } = "";
    [Display(Name = "CompName")]
    public string? CompName { get; set; } = "";
    [Display(Name = "ShortName")]
    public string? ShortName { get; set; } = "";
    [Display(Name = "EngName")]
    public string? EngName { get; set; } = "";
    [Display(Name = "EngShortName")]
    public string? EngShortName { get; set; } = "";
    [Display(Name = "RegisterDate")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime RegisterDate { get; set; } = DateTime.Today;
    [Display(Name = "BossName")]
    public string? BossName { get; set; } = "";
    [Display(Name = "ContactName")]
    public string? ContactName { get; set; } = "";
    [Display(Name = "CompTel")]
    public string? CompTel { get; set; } = "";
    [Display(Name = "ContactTel")]
    public string? ContactTel { get; set; } = "";
    [Display(Name = "CompFax")]
    public string? CompFax { get; set; } = "";
    [Display(Name = "CompID")]
    public string? CompID { get; set; } = "";
    [Display(Name = "ContactEmail")]
    public string? ContactEmail { get; set; } = "";
    [Display(Name = "CompAddress")]
    public string? CompAddress { get; set; } = "";
    [Display(Name = "CompUrl")]
    public string? CompUrl { get; set; } = "";
    [Display(Name = "TwitterUrl")]
    public string? TwitterUrl { get; set; } = "";
    [Display(Name = "FacebookUrl")]
    public string? FacebookUrl { get; set; } = "";
    [Display(Name = "InstagramUrl")]
    public string? InstagramUrl { get; set; } = "";
    [Display(Name = "SkypeUrl")]
    public string? SkypeUrl { get; set; } = "";
    [Display(Name = "LinkedinUrl")]
    public string? LinkedinUrl { get; set; } = "";
    [Display(Name = "Latitude")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N1}")]
    public decimal Latitude { get; set; } = 0;
    [Display(Name = "Longitude")]
    public decimal Longitude { get; set; } = 0;
    [Display(Name = "AboutusText")]
    public string? AboutusText { get; set; } = "";
    [Display(Name = "SupportText")]
    public string? SupportText { get; set; } = "";
    [Display(Name = "ReturnText")]
    public string? ReturnText { get; set; } = "";
    [Display(Name = "ShippingText")]
    public string? ShippingText { get; set; } = "";
    [Display(Name = "PaymentText")]
    public string? PaymentText { get; set; } = "";
    [Display(Name = "Remark")]
    public string? Remark { get; set; } = "";
}
