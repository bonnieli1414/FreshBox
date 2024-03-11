/// <summary>
/// Json 訊息傳遞的 Model
/// </summary>
public class dmJsonMessage
{
    /// <summary>
    /// 執行狀態,true=成功 false=失敗
    /// </summary>
    public bool Mode { get; set; } = false;
    /// <summary>
    /// 訊息文字
    /// </summary>
    public string Message { get; set; } = "";
}