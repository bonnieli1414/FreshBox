public class vmContact
{
    [Display(Name = "聯絡姓名")]
    [Required(ErrorMessage = "請填寫聯絡姓名")]
    public string ContactorName { get; set; } = "";
    [Display(Name = "電子信箱")]
    [Required(ErrorMessage = "請填寫電子信箱")]
    [EmailAddress(ErrorMessage = "EmailAddressErrorMessage")]
    public string ContactorEmail { get; set; } = "";
    [Display(Name = "主旨")]
    [Required(ErrorMessage = "請填寫主旨")]
    public string HeaderText { get; set; } = "";
    [Display(Name = "聯絡訊息")]
    [Required(ErrorMessage = "請填寫聯絡訊息")]
    public string MessageText { get; set; } = "";
}