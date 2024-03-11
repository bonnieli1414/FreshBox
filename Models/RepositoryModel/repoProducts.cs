/// <summary>
/// 使用者資料 CRUD
/// </summary>
public class z_repoProducts : BaseClass
{
    public string ErrorMessage { get; set; }
    #region SQL 指令設定區
    /// <summary>
    /// SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLSelect()
    {
        string str_query = @"
SELECT Products.Id,  Products.IsEnabled, Products.ProdNo, Products.ProdName, Products.CategoryNo, Categorys.CategoryName, Products.InventoryQty, Products.StatusNo, ProductStatus.StatusName, Products.SpecificationText, Products.ContentText, Products.CostPrice, Products.MarketPrice, Products.SalePrice, Products.DiscountPrice, Products.Remark, 
CASE WHEN Products.DiscountPrice <> 0 
THEN Products.DiscountPrice ELSE Products.SalePrice END AS MinPrice ,
'~/images/product/' + Products.ProdNo + '/' + Products.ProdNo + '.jpg' AS ProdImage 
FROM Products LEFT OUTER JOIN ProductStatus ON Products.StatusNo = ProductStatus.StatusNo 
LEFT OUTER JOIN Categorys ON Products.CategoryNo = Categorys.CategoryNo ";
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
        searchColumn = dpr.GetStringColumnList(new Products());
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
    private readonly string DefaultOrderByColumn = "Products.ProdNo";
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
        if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
        if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
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
        str_query += "ProdName AS Text , ProdNo AS Value FROM Products ORDER BY ProdNo";
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
        str_query = dpr.GetSQLInsertCommand(new Products());
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
        str_query = dpr.GetSQLDeleteCommand(new Products());
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
        str_query = dpr.GetSQLUpdateCommand(new Products());
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
    public z_repoProducts()
    {
        OrderByColumn = DefaultOrderByColumn;
        OrderByDirection = DefaultOrderByDirection;
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="orderByColumn">排序欄位</param>
    /// <param name="orderByDirection">排序方式</param>
    public z_repoProducts(string orderByColumn, string orderByDirection)
    {
        OrderByColumn = orderByColumn;
        OrderByDirection = orderByDirection;
    }
    #endregion
    #region 資料表 CRUD 指令(使用同步呼叫)
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="prodNo">商品代號</param>
    /// <returns></returns>
    public Products GetData(string prodNo)
    {
        var model = new Products();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = "WHERE Products.ProdNo = @ProdNo ";
        sql_query += sql_where;
        sql_query += GetSQLOrderBy();
        DynamicParameters parm = new DynamicParameters();
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            parm.Add("ProdNo", prodNo);
        }
        model = dpr.ReadSingle<Products>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public Products GetData(int id)
    {
        var model = new Products();
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
            model = dpr.ReadSingle<Products>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public List<Products> GetDataList(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Products>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Products(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = dpr.ReadAll<Products>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="sortNo">排序方式</param>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public List<Products> GetDataList(string sortNo, string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Products>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Products(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        if (sortNo == "High") sql_query += " ORDER BY MinPrice DESC;";
        else if (sortNo == "Low") sql_query += " ORDER BY MinPrice ASC";
        else sql_query += " ORDER BY Products.ProdNo ASC";

        model = dpr.ReadAll<Products>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="categoryNo">Category No</param>
    /// <param name="sortNo">排序方式</param>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public List<Products> GetDataList(string categoryNo, string sortNo, string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Products>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = " WHERE Products.CategoryNo = @CategoryNo ";
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Products(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            parm.Add("CategoryNo", categoryNo);
        }
        if (sortNo == "High") sql_query += " ORDER BY MinPrice DESC;";
        else if (sortNo == "Low") sql_query += " ORDER BY MinPrice ASC";
        else sql_query += " ORDER BY Products.ProdNo ASC";

        model = dpr.ReadAll<Products>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void CreateEdit(Products model)
    {
        // 先檢查是否已存在相同的 PhotoNo
        // var existingPhoto = GetData(model.ProdNo);
        // if (existingPhoto != null && existingPhoto.ProdNo == model.ProdNo)
        // {
        //     // 如果有，則回覆商品編號重複
        //     ErrorMessage = "商品編號重複";
        // }
        if (model.Id == 0)
            Create(model);
        else
            Edit(model);
    }
    /// <summary>
    /// 新增資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void Create(Products model)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLInsertCommand(model);
        DynamicParameters parm = dpr.GetSQLInsertParameters(model);
        dpr.Execute(str_query, parm);

        // 儲存商品後，獲取新增商品的編號
        string prodNo = model.ProdNo;

        // 取得目前專案資料夾目錄路徑
        string FileNameOnServer = Directory.GetCurrentDirectory();
        // 專案目錄路徑
        string WebFolder = Path.Combine(FileNameOnServer, "wwwroot");

        // 複製應用程式的 logo 圖片作為商品的預設圖片
        string sourceFilePath = Path.Combine(WebFolder, "images", "app", "AppLogo.jpg");
        // 使用商品的編號作為圖片檔名
        string destFileName = $"{prodNo}.jpg";
        // 設定圖片存放的路徑
        string destFilePath = Path.Combine(WebFolder, "images", "product\\" + prodNo, destFileName);
        string folderPath = Path.Combine(FileNameOnServer, "product\\" + prodNo);
        // 檢查資料夾是否存在，如果不存在則建立
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine("資料夾已成功建立！");
        }
        // 複製 logo 圖片到商品圖片的路徑
        System.IO.File.Copy(sourceFilePath, destFilePath, true);
        var photoDatas = new Photos();
        photoDatas.CodeNo = model.CategoryNo;
        photoDatas.PhotoNo = model.ProdNo;
        photoDatas.ProdNo = model.ProdNo;
        photoDatas.FolderNo = model.CategoryNo;
        using var photos = new z_repoPhotos();
        photos.CreateEdit(photoDatas);

    }
    /// <summary>
    /// 更新資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void Edit(Products model)
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
        string str_query = dpr.GetSQLDeleteCommand(new Products());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Products(), id);
        dpr.Execute(str_query, parm);
    }
    #endregion
    #region 資料表 CRUD 指令(使用非同步呼叫)   
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="prodNo">使用者代號</param>
    /// <returns></returns>
    public async Task<Products> GetDataAsync(string prodNo)
    {
        var model = new Products();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = "WHERE Products.ProdNo = @ProdNo ";
        sql_query += sql_where;
        sql_query += GetSQLOrderBy();
        DynamicParameters parm = new DynamicParameters();
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            parm.Add("ProdNo", prodNo);
        }
        model = await dpr.ReadSingleAsync<Products>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public async Task<Products> GetDataAsync(int id)
    {
        var model = new Products();
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
            model = await dpr.ReadSingleAsync<Products>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public async Task<List<Products>> GetDataListAsync(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Products>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Products(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Where Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = await dpr.ReadAllAsync<Products>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public async Task CreateEditAsync(Products model)
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
    public async Task CreateAsync(Products model)
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
    public async Task EditAsync(Products model)
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
    public async Task DeleteAsync(int id = 0)
    {
        using var dpr = new DapperRepository();
        string str_query = dpr.GetSQLDeleteCommand(new Products());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Products(), id);
        await dpr.ExecuteAsync(str_query, parm);
    }
    #endregion
    #region 其它自定義事件與函數
    /// <summary>
    /// 取得指定分類商品資料(同步呼叫)
    /// </summary>
    /// <param name="categoryNo">分類代號</param>
    /// <returns></returns>
    public List<Products> GetCategoryDataList(string categoryNo = "")
    {
        List<string> searchColumns = GetSearchColumns();
        var model = new List<Products>();
        using var dpr = new DapperRepository();
        using var cate = new z_repoCategorys();
        DynamicParameters parm = new DynamicParameters();
        string sql_query = GetSQLSelect();
        if (!string.IsNullOrEmpty(SessionService.SearchText))
        {
            sql_query += $" WHERE (Products.ProdNo LIKE '%{SessionService.SearchText}%' ";
            sql_query += $" OR Products.ProdName LIKE '%{SessionService.SearchText}%') ";
            sql_query += " AND ";
        }
        else
        {
            if (!string.IsNullOrEmpty(categoryNo) && categoryNo != "All")
            {
                var data = cate.GetData(categoryNo);
                if (string.IsNullOrEmpty(data.ParentNo))
                    sql_query += " WHERE Categorys.ParentNo = @CategoryNo AND ";
                else
                    sql_query += " WHERE Products.CategoryNo = @CategoryNo AND ";
                parm.Add("CategoryNo", categoryNo);
            }
            else
            {
                sql_query += " WHERE ";
            }
        }
        sql_query += " Products.IsEnabled = @IsEnabled ";
        sql_query += GetSortOrderBy();
        parm.Add("IsEnabled", true);
        model = dpr.ReadAll<Products>(sql_query, parm);
        return model;
    }

    private string GetSortOrderBy()
    {
        if (string.IsNullOrEmpty(SessionService.SortNo)) SessionService.SortNo = "D";
        if (SessionService.SortNo == "H") return " ORDER BY MinPrice DESC";
        if (SessionService.SortNo == "L") return " ORDER BY MinPrice ASC";
        if (SessionService.SortNo == "D") return " ORDER BY Products.ProdNo ASC";
        return " ORDER BY Products.ProdNo";
    }
    #endregion
}