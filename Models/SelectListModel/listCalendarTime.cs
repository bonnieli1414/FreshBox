public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 取得行事曆時間列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> CalendarTimeList()
    {
        List<SelectListItem> listTime = new List<SelectListItem>();
        string str_text = "";
        string str_value = "";
        for (int i = 0; i < 23; i++)
        {
            str_text = i.ToString().PadLeft(2, '0');
            str_text += ":00";
            str_value = str_text + ":00";
            listTime.Add(new SelectListItem() { Text = str_text, Value = str_value, Selected = (i == 8) });
            str_text = i.ToString().PadLeft(2, '0');
            str_text += ":30";
            str_value = str_text + ":00";
            listTime.Add(new SelectListItem() { Text = str_text, Value = str_value, Selected = false });
        }
        return listTime;
    }
}