namespace FreshBox.Models
{
    /// <summary>
    /// 角色主檔表格類別
    /// </summary>
    [ModelMetadataType(typeof(z_metaQuestions))]
    public partial class Questions
    {
    }
}

public partial class z_metaQuestions
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序編號")]
    public string? SortNo { get; set; }
    [Display(Name = "問題題目")]
    public string? QuestionText { get; set; }
    [Display(Name = "問題解答")]
    public string? AnswerText { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}