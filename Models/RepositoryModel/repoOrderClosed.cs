/// <summary>
/// 訂單資料 CRUD 程式
/// </summary>
public class z_repoOrderClosed : BaseClass
{
    #region SQL 指令設定區
    /// <summary>
    /// SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLSelect()
    {
        string str_query = @"
SELECT Orders.Id,Orders.SheetNo,Orders.SheetDate,
OrdersStatus.StatusName,Orders.IsClosed,Orders.IsValid,
Orders.CustNo,Orders.CustName,Payments.PaymentName,
Shippings.ShippingName,Orders.ReceiverName,Orders.ReceiverEmail,Orders.ReceiverAddress,Orders.OrderAmount,Orders.TaxAmount,Orders.TotalAmount,Orders.Remark,Orders.GuidNo";
        str_query += @" FROM dbo.Orders ";
        str_query += @" 
 LEFT OUTER JOIN dbo.OrdersStatus ON Orders.StatusCode = OrdersStatus.StatusNo 
 LEFT OUTER JOIN dbo.Payments ON Orders.PaymentNo = Payments.PaymentNo 
 LEFT OUTER JOIN dbo.Shippings ON Orders.ShippingNo = Shippings.ShippingNo ";
        return str_query;
    }
    /// <summary>
    /// 取得模擬搜尋的欄位集合
    /// </summary>
    /// <returns></returns>
    public List<string> GetSearchColumns()
    {
        List<string> searchColumn;
        //由系統自動取得文字欄位的集合
        using var dpr = new DapperRepository();
        searchColumn = dpr.GetStringColumnList(new Orders());
        return searchColumn;
    }
    /// <summary>
    /// SQL 查詢條件式指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLWhere()
    {
        string str_query = " WHERE Orders.IsClosed = 1 ";
        return str_query;
    }
    /// <summary>
    /// 預設 SQL 排序指令
    /// </summary>
    private readonly string DefaultOrderByColumn = "Orders.SheetNo";
    /// <summary>
    /// 預設 SQL 排序方式指令
    /// </summary>
    private readonly string DefaultOrderByDirection = "DESC";
    /// <summary>
    /// SQL 查詢排序指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLOrderBy()
    {
        string str_query = $" ORDER BY {OrderByColumn}";
        if (!string.IsNullOrEmpty(OrderByDirection)) str_query += $" {OrderByDirection}";
        return str_query;
    }
    /// <summary>
    /// 取得下拉式選單資料集
    /// </summary>
    /// <param name="showNo">是否顯示編號</param>
    /// <returns></returns>
    public List<SelectListItem> GetDropDownList(bool showNo = true)
    {
        using var dpr = new DapperRepository();
        string str_query = "SELECT ";
        if (showNo) str_query += "SheetNo + ' ' + ";
        str_query += "CustName AS Text , SheetNo AS Value FROM Orders ORDER BY SheetNo DESC";
        var model = dpr.ReadAll<SelectListItem>(str_query);
        return model;
    }
    /// <summary>
    /// SQL 新增指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLInsert()
    {
        string str_query = "";
        //自動由表格 Class 產生 Insert 查詢指令
        using var dpr = new DapperRepository();
        str_query = dpr.GetSQLInsertCommand(new Orders());
        return str_query;
    }
    /// <summary>
    /// SQL 刪除指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLDelete()
    {
        string str_query = "";
        //自動由表格 Class 產生 Delete 查詢指令
        using var dpr = new DapperRepository();
        str_query = dpr.GetSQLDeleteCommand(new Orders());
        return str_query;
    }
    /// <summary>
    /// SQL 修改指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLUpdate()
    {
        string str_query = "";
        //自動由表格 Class 產生 Update 查詢指令
        using var dpr = new DapperRepository();
        str_query = dpr.GetSQLUpdateCommand(new Orders());
        return str_query;
    }
    #endregion
    #region 物件建構子
    /// <summary>
    /// OrderBy 排序指令
    /// </summary>
    /// <value></value>
    public string OrderByColumn { get; set; }
    /// <summary>
    /// OrderBy 排序方式
    /// </summary>
    public string OrderByDirection { get; set; }
    /// <summary>
    /// 建構子
    /// </summary>
    public z_repoOrderClosed()
    {
        OrderByColumn = DefaultOrderByColumn;
        OrderByDirection = DefaultOrderByDirection;
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="orderByColumn">排序欄位</param>
    /// <param name="orderByDirection">排序方式</param>
    public z_repoOrderClosed(string orderByColumn, string orderByDirection)
    {
        if (!string.IsNullOrEmpty(orderByColumn))
            OrderByColumn = orderByColumn;
        else
            OrderByColumn = DefaultOrderByColumn;
        if (!string.IsNullOrEmpty(orderByDirection))
            OrderByDirection = orderByDirection;
        else
            OrderByDirection = DefaultOrderByDirection;
    }
    #endregion
    #region 資料表 CRUD 指令(使用同步呼叫)
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="sheetNo">訂單編號</param>
    /// <returns></returns>
    public Orders GetData(string sheetNo)
    {
        var model = new Orders();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        sql_query += " WHERE Orders.SheetNo = @SheetNo AND Orders.IsClosed = 1";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("SheetNo", sheetNo);
        model = dpr.ReadSingle<Orders>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public Orders GetData(int id)
    {
        var model = new Orders();
        if (id == 0)
        {
            //新增預設值
        }
        else
        {
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = GetSQLWhere();
            sql_query += dpr.GetSQLSelectWhereById(model, sql_where);
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                //parm.Add("參數名稱", "參數值");
            }
            model = dpr.ReadSingle<Orders>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public List<Orders> GetDataList(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Orders>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Orders(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = dpr.ReadAll<Orders>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void CreateEdit(Orders model)
    {
        if (model.Id == 0)
            Create(model);
        else
            Edit(model);
    }
    /// <summary>
    /// 新增資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void Create(Orders model)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLInsertCommand(model);
        DynamicParameters parm = dpr.GetSQLInsertParameters(model);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 更新資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void Edit(Orders model)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLUpdateCommand(model);
        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 刪除資料(同步呼叫)
    /// </summary>
    /// <param name="id">Id</param>
    public void Delete(int id = 0)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLDeleteCommand(new Orders());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Orders(), id);
        dpr.Execute(str_query, parm);
    }
    #endregion
    #region 資料表 CRUD 指令(使用非同步呼叫)
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public async Task<Orders> GetDataAsync(int id)
    {
        var model = new Orders();
        if (id == 0)
        {
            //新增預設值
        }
        else
        {
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = GetSQLWhere();
            sql_query += dpr.GetSQLSelectWhereById(model, sql_where);
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                //parm.Add("參數名稱", "參數值");
            }
            model = await dpr.ReadSingleAsync<Orders>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public async Task<List<Orders>> GetDataListAsync(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Orders>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Orders(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = await dpr.ReadAllAsync<Orders>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public async Task CreateEditAsync(Orders model)
    {
        if (model.Id == 0)
            await CreateAsync(model);
        else
            await EditAsync(model);
    }
    /// <summary>
    /// 新增資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public async Task CreateAsync(Orders model)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLInsertCommand(model);
        DynamicParameters parm = dpr.GetSQLInsertParameters(model);
        await dpr.ExecuteAsync(str_query, parm);
    }
    /// <summary>
    /// 更新資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public async Task EditAsync(Orders model)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLUpdateCommand(model);
        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);
        await dpr.ExecuteAsync(str_query, parm);
    }
    /// <summary>
    /// 刪除資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns></returns>
    public async Task DeleteAsync(int id = 0)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLDeleteCommand(new Orders());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Orders(), id);
        await dpr.ExecuteAsync(str_query, parm);
    }
    #endregion
    #region 其它自定義事件與函數
    /// <summary>
    /// 結帳付款產生訂單表頭
    /// </summary>
    /// <param name="model">付款資訊</param>
    /// <returns>訂單編號</returns>
    public string CreateNewOrder(vmOrders model)
    {
        string str_order_no = "";
        int int_amount = 0;
        int int_tax = 0;
        int int_total = 0;
        string str_guid = Guid.NewGuid().ToString().Replace("-", "");
        using var dpr = new DapperRepository();
        //計算購物車中的合計金額
        using var cart = new z_repoCarts();
        var CartList = cart.GetDataList();
        int_amount = CartList.Sum(m => m.OrderQty * m.OrderPrice);
        double dbl_amount = Convert.ToDouble(int_amount);
        if (dbl_amount != 0) int_tax = Convert.ToInt32(Math.Round((dbl_amount * 5 / 100), 0));
        int_total = int_amount + int_tax;
        //新增訂單表頭
        string str_query = @"
INSERT INTO Orders
(SheetNo,SheetDate,StatusCode,IsClosed,IsValid
,CustNo,CustName,PaymentNo,ShippingNo,ReceiverName
,ReceiverEmail,ReceiverAddress,OrderAmount
,TaxAmount,TotalAmount,Remark,GuidNo) 
VALUES 
(@SheetNo,@SheetDate,@StatusCode,@IsClosed,@IsValid
,@CustNo,@CustName,@PaymentNo,@ShippingNo,@ReceiverName
,@ReceiverEmail,@ReceiverAddress,@OrderAmount
,@TaxAmount,@TotalAmount,@Remark,@GuidNo) 
";
        DateTime nowTime = DateTime.Now;
        string nowTimeStr = nowTime.ToString("yyyy-MM-dd HH:mm:ss");

        DynamicParameters parm2 = new DynamicParameters();
        parm2.Add("SheetNo", "");
        parm2.Add("SheetDate", nowTimeStr);
        parm2.Add("StatusCode", "NP");
        parm2.Add("IsClosed", false);
        parm2.Add("IsValid", true);
        parm2.Add("CustNo", SessionService.UserNo);
        parm2.Add("CustName", SessionService.UserName);
        parm2.Add("PaymentNo", model.PaymentNo);
        parm2.Add("ShippingNo", model.ShippingNo);
        if (model.IsDiffenceMember == "on")
        {
            parm2.Add("ReceiverName", model.ReceiveName);
            parm2.Add("ReceiverEmail", model.ReceiveEmail);
            parm2.Add("ReceiverAddress", model.ReceiveAddress);
        }
        else
        {
            parm2.Add("ReceiverName", model.MemberName);
            parm2.Add("ReceiverEmail", model.MemberEmail);
            parm2.Add("ReceiverAddress", model.MemberAddress);
        }
        parm2.Add("OrderAmount", int_amount);
        parm2.Add("TaxAmount", int_tax);
        parm2.Add("TotalAmount", int_total);
        parm2.Add("Remark", model.Remark);
        parm2.Add("GuidNo", str_guid);
        dpr.Execute(str_query, parm2);
        //取得訂單Id , 更新訂單編號
        str_query = GetSQLSelect();
        str_query += " WHERE GuidNo = @GuidNo";
        DynamicParameters parm3 = new DynamicParameters();
        parm3.Add("GuidNo", str_guid);
        var data = dpr.ReadSingle<Orders>(str_query, parm3);
        if (data != null)
        {
            int int_id = data.Id;
            str_order_no = int_id.ToString().PadLeft(8, '0');
            str_query = "UPDATE Orders SET SheetNo = @SheetNo WHERE Id = @Id";
            DynamicParameters parm4 = new DynamicParameters();
            parm4.Add("Id", int_id);
            parm4.Add("SheetNo", str_order_no);
            dpr.Execute(str_query, parm4);
        }
        return str_order_no;
    }
    /// <summary>
    /// 取得會員訂單
    /// </summary>
    /// <returns></returns>
    public List<Orders> GetOrderList()
    {
        var model = new List<Orders>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        sql_query += " WHERE CustNo = @CustNo";
        sql_query += " ORDER BY SheetNo DESC";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("CustNo", SessionService.UserNo);
        model = dpr.ReadAll<Orders>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取消訂單
    /// </summary>
    /// <param name="sheetNo">訂單編號</param>
    public void CancelOrder(string sheetNo)
    {
        using var dpr = new DapperRepository();
        string sql_query = "UPDATE Orders SET StatusCode = @StatusCode WHERE SheetNo = @SheetNo";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("SheetNo", sheetNo);
        parm.Add("StatusCode", "CC");
        dpr.Execute(sql_query, parm);
    }
    #endregion
}