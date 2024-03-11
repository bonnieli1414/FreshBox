public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 取得行事曆時間列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> CalendarMinuteList()
    {
        List<SelectListItem> listData = new List<SelectListItem>();
        string str_text = "";
        for (int i = 0; i < 60; i += 5)
        {
            str_text = i.ToString().PadLeft(2, '0');
            listData.Add(new SelectListItem() { Text = str_text, Value = str_text });
        }
        return listData;
    }
}