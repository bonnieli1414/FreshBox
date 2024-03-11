/// <summary>
/// 使用者資料 CRUD
/// </summary>
public class z_repoCarts : BaseClass
{
    #region SQL 指令設定區
    /// <summary>
    /// SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLSelect()
    {
        string str_query = @"
SELECT Id, LotNo, MemberNo, VendorNo, CategoryNo, CategoryName, 
ProdNo, ProdName, ProdSpec, OrderQty, OrderPrice, 
OrderAmount, CreateTime, Remark 
FROM Carts 
";
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
        searchColumn = dpr.GetStringColumnList(new Carts());
        return searchColumn;
    }
    /// <summary>
    /// SQL 查詢條件式指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLWhere()
    {
        string str_query = "";
        return str_query;
    }
    /// <summary>
    /// 預設 SQL 排序指令
    /// </summary>
    private readonly string DefaultOrderByColumn = "Carts.ProdNo";
    /// <summary>
    /// 預設 SQL 排序方式指令
    /// </summary>
    private readonly string DefaultOrderByDirection = "ASC";
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
        if (showNo) str_query += "ProdNo + ' ' + ";
        str_query += "ProdName AS Text , ProdNo AS Value FROM Carts ORDER BY ProdNo";
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
        str_query = dpr.GetSQLInsertCommand(new Carts());
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
        str_query = dpr.GetSQLDeleteCommand(new Carts());
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
        str_query = dpr.GetSQLUpdateCommand(new Carts());
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
    public z_repoCarts()
    {
        OrderByColumn = DefaultOrderByColumn;
        OrderByDirection = DefaultOrderByDirection;
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="orderByColumn">排序欄位</param>
    /// <param name="orderByDirection">排序方式</param>
    public z_repoCarts(string orderByColumn, string orderByDirection)
    {
        OrderByColumn = orderByColumn;
        OrderByDirection = orderByDirection;
    }
    #endregion
    #region 資料表 CRUD 指令(使用同步呼叫)
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public Carts GetData(int id)
    {
        var model = new Carts();
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
                //自定義的 Where Parm 參數
                //parm.Add("參數名稱", "參數值");
            }
            model = dpr.ReadSingle<Carts>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <returns></returns>
    public List<Carts> GetDataList()
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Carts>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = " WHERE LotNo = @LotNo ";
        parm.Add("LotNo", CartService.LotNo);
        sql_query += sql_where;
        sql_query += GetSQLOrderBy();
        model = dpr.ReadAll<Carts>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void CreateEdit(Carts model)
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
    public void Create(Carts model)
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
    public void Edit(Carts model)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLUpdateCommand(model);
        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 刪除資料(同步呼叫)
    /// </summary>
    /// <param name="id">id</param>
    public void Delete(int id = 0)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLDeleteCommand(new Carts());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Carts(), id);
        dpr.Execute(str_query, parm);
    }
    #endregion
    #region 資料表 CRUD 指令(使用非同步呼叫)
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="userNo">使用者代號</param>
    /// <returns></returns>
    public async Task<Carts> GetDataAsync(string userNo)
    {
        var model = new Carts();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = "WHERE Carts.MemberNo = @MemberNo ";
        sql_query += sql_where;
        sql_query += GetSQLOrderBy();
        DynamicParameters parm = new DynamicParameters();
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            parm.Add("MemberNo", userNo);
        }
        model = await dpr.ReadSingleAsync<Carts>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public async Task<Carts> GetDataAsync(int id)
    {
        var model = new Carts();
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
                //自定義的 Where Parm 參數
                //parm.Add("參數名稱", "參數值");
            }
            model = await dpr.ReadSingleAsync<Carts>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public async Task<List<Carts>> GetDataListAsync(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Carts>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Carts(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = await dpr.ReadAllAsync<Carts>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public async Task CreateEditAsync(Carts model)
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
    public async Task CreateAsync(Carts model)
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
    public async Task EditAsync(Carts model)
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
        string str_query = dpr.GetSQLDeleteCommand(new Carts());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Carts(), id);
        await dpr.ExecuteAsync(str_query, parm);
    }
    #endregion
    #region 其它自定義事件與函數
    /// <summary>
    /// 遊客購物車合併至會員購物車
    /// </summary>
    public void MergeCart()
    {
        //取得遊客購物車明細
        string str_query = "";
        using var dpr = new DapperRepository();
        str_query = GetSQLSelect();
        str_query += " WHERE LotNo = @LotNo";
        DynamicParameters parm1 = new DynamicParameters();
        parm1.Add("LotNo", CartService.LotNo);
        var data = dpr.ReadAll<Carts>(str_query, parm1);

        //更新購物車批號
        CartService.NewLotNo();

        //將新批號寫入會員購物車
        str_query = "UPDATE Carts SET LotNo = @LotNo WHERE MemberNo = @MemberNo";
        DynamicParameters parm2 = new DynamicParameters();
        parm2.Add("LotNo", CartService.LotNo);
        parm2.Add("MemberNo", SessionService.UserNo);
        dpr.Execute(str_query, parm2);

        //將遊客購物車合併至會員購物車
        foreach (var item in data)
        {
            AddCart(item.ProdNo, item.ProdSpec, item.OrderQty);
        }
    }
    /// <summary>
    /// 加入商品至目前批號購物車中
    /// </summary>
    /// <param name="prodNo">商品編號</param>
    /// <param name="prodSpec">商品規格(空白可自抓)</param>
    /// <param name="qty">數量</param>
    public void AddCart(string prodNo, string prodSpec, int qty)
    {
        string str_query = "";
        using var dpr = new DapperRepository();
        using var prod = new z_repoProducts();
        var prodData = prod.GetData(prodNo);
        if (string.IsNullOrEmpty(prodSpec))
        {
            using (var prodProp = new z_repoProductPropertys())
            {
                prodSpec = prodProp.GetProductSpec(prodNo);
            }
        }
        int int_price = (prodData.DiscountPrice != 0) ? prodData.DiscountPrice : prodData.SalePrice;
        int int_qty = qty;
        int int_amount = int_qty * int_price;
        str_query = GetSQLSelect();
        str_query += " WHERE LotNo = @LotNo AND ProdNo = @ProdNo AND ProdSpec = @ProdSpec";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("LotNo", CartService.LotNo);
        parm.Add("ProdNo", prodNo);
        parm.Add("ProdSpec", prodSpec);
        var data = dpr.ReadSingle<Carts>(str_query, parm);
        if (data == null || data.Id == 0)
        {
            str_query = @"
INSERT INTO Carts
(LotNo,MemberNo,VendorNo,CategoryNo,CategoryName
,ProdNo,ProdName,ProdSpec,OrderQty,OrderPrice
,OrderAmount,CreateTime,Remark)
VALUES 
(@LotNo,@MemberNo,@VendorNo,@CategoryNo,@CategoryName
,@ProdNo,@ProdName,@ProdSpec,@OrderQty,@OrderPrice
,@OrderAmount,@CreateTime,@Remark)
";
            parm.Add("MemberNo", SessionService.UserNo);
            parm.Add("VendorNo", "");
            parm.Add("CategoryNo", prodData.CategoryNo);
            parm.Add("CategoryName", prodData.CategoryName);
            parm.Add("ProdName", prodData.ProdName);
            parm.Add("OrderQty", int_qty);
            parm.Add("OrderPrice", int_price);
            parm.Add("OrderAmount", int_amount);
            parm.Add("CreateTime", DateTime.Now);
            parm.Add("Remark", "");
        }
        else
        {
            int_qty += data.OrderQty;
            int_amount = int_qty * int_price;
            str_query = @"
UPDATE Carts SET OrderQty = @OrderQty , OrderAmount = @OrderAmount , CreateTime = @CreateTime 
WHERE LotNo = @LotNo AND ProdNo = @ProdNo AND ProdSpec = @ProdSpec
";
            parm.Add("OrderQty", int_qty);
            parm.Add("OrderAmount", int_amount);
            parm.Add("CreateTime", DateTime.Now);
        }
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 更新購物車
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="qty">數量</param>
    public void UpdateCart(int id, int qty)
    {
        int int_qty = qty;
        int int_price = 0;
        int int_amount = 0;
        string str_query = "";
        using var dpr = new DapperRepository();
        DynamicParameters parm = new DynamicParameters();
        str_query = GetSQLSelect();
        str_query += " WHERE Id = @Id";
        parm.Add("Id", id);
        var data = dpr.ReadSingle<Carts>(str_query, parm);
        if (data != null)
        {
            int_qty = data.OrderQty;
            int_price = data.OrderPrice;
            int_amount = int_qty * int_price;

            str_query = @"
UPDATE Carts SET OrderQty = @OrderQty , OrderAmount = @OrderAmount , CreateTime = @CreateTime 
WHERE Id = @Id
";
            parm.Add("OrderQty", qty);
            parm.Add("OrderAmount", int_amount);
            parm.Add("CreateTime", DateTime.Now);
            dpr.Execute(str_query, parm);
        }
    }
    /// <summary>
    /// 異動購物車商品數量
    /// </summary>
    /// <param name="ProdNo"></param>
    /// <param name="Qty"></param>
    /// <returns></returns>
    public string UpdateQty(string ProdNo, int Qty)
    {
        string str_message = "";
        using var dpr = new DapperRepository();
        DynamicParameters parm = new DynamicParameters();
        string str_query = "UPDATE [dbo].[Carts]";
        // 最後一定要加上空格，避免跟WHERE語法連在一起造成錯誤
        str_query += "SET [OrderQty] = @OrderQty ,[OrderAmount] = OrderPrice*@OrderQty ";
        str_query += "WHERE [LotNo] = @LotNo AND [ProdNo] = @ProdNo";
        parm.Add("LotNo", CartService.LotNo);
        parm.Add("ProdNo", ProdNo);
        parm.Add("OrderQty", Qty);
        dpr.Execute(str_query, parm);
        // 寫入資料庫成功會回傳空值
        str_message = dpr.ErrorMessage;
        return str_message;
    }
    /// <summary>
    /// 刪除購物車
    /// </summary>
    /// <param name="id">Id</param>
    public void DeleteCart(int id)
    {
        string str_query = "";
        using var dpr = new DapperRepository();
        DynamicParameters parm = new DynamicParameters();
        str_query = "DELETE FROM Carts WHERE Id = @Id";
        parm.Add("Id", id);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 刪除目前批號購物車
    /// </summary>
    public void DeleteCart()
    {
        string str_query = "";
        using var dpr = new DapperRepository();
        DynamicParameters parm = new DynamicParameters();
        str_query = "DELETE FROM Carts WHERE LotNo = @LotNo";
        parm.Add("LotNo", CartService.LotNo);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 取得目前批號購物車筆數
    /// </summary>
    /// <returns></returns>
    public int GetCartCount()
    {
        List<Carts> data = new List<Carts>();
        data = GetDataList();
        return data.Count();
    }
    /// <summary>
    /// 取得目前批號購物車合計
    /// </summary>
    /// <returns></returns>
    public int GetCartTotal()
    {
        List<Carts> data = new List<Carts>();
        data = GetDataList();
        return data.Sum(m => m.OrderQty * m.OrderPrice);
    }
    #endregion
}