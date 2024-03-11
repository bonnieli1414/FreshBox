/// <summary>
/// 使用者資料 CRUD
/// </summary>
public class z_repoUsers : BaseClass
{
    #region SQL 指令設定區
    /// <summary>
    /// SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    public string GetSQLSelect()
    {
        string str_query = @"
SELECT Users.Id, Users.IsValid, Users.UserNo, Users.UserName, Users.Password, 
Users.CodeNo, vi_CodeUser.CodeName, Users.RoleNo, Roles.RoleName, Users.GenderCode, 
vi_CodeGender.CodeName AS GenderName, Users.DeptNo, Departments.DeptName, 
Users.TitleNo, Titles.TitleName, Users.Birthday, Users.OnboardDate, Users.LeaveDate, 
Users.ContactEmail, Users.ContactTel, Users.ContactAddress, Users.ValidateCode, 
Users.NotifyPassword, Users.Remark 
FROM Users 
LEFT OUTER JOIN vi_CodeGender ON Users.GenderCode = vi_CodeGender.CodeNo 
LEFT OUTER JOIN vi_CodeUser ON Users.CodeNo = vi_CodeUser.CodeNo 
LEFT OUTER JOIN Departments ON Users.DeptNo = Departments.DeptNo 
LEFT OUTER JOIN Titles ON Users.TitleNo = Titles.TitleNo 
LEFT OUTER JOIN Roles ON Users.RoleNo = Roles.RoleNo     
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
        searchColumn = dpr.GetStringColumnList(new Users());
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
    private readonly string DefaultOrderByColumn = "Users.UserNo";
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
        if (showNo) str_query += "UserNo + ' ' + ";
        str_query += "UserName AS Text , TitleNo AS Value FROM Users ORDER BY UserNo";
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
        str_query = dpr.GetSQLInsertCommand(new Users());
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
        str_query = dpr.GetSQLDeleteCommand(new Users());
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
        str_query = dpr.GetSQLUpdateCommand(new Users());
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
    public z_repoUsers()
    {
        OrderByColumn = DefaultOrderByColumn;
        OrderByDirection = DefaultOrderByDirection;
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="orderByColumn">排序欄位</param>
    /// <param name="orderByDirection">排序方式</param>
    public z_repoUsers(string orderByColumn, string orderByDirection)
    {
        OrderByColumn = orderByColumn;
        OrderByDirection = orderByDirection;
    }
    #endregion
    #region 資料表 CRUD 指令(使用同步呼叫)
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="userNo">使用者代號</param>
    /// <returns></returns>
    public Users GetData(string userNo)
    {
        var model = new Users();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = "WHERE Users.UserNo = @UserNo ";
        sql_query += sql_where;
        sql_query += GetSQLOrderBy();
        DynamicParameters parm = new DynamicParameters();
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            parm.Add("UserNo", userNo);
        }
        model = dpr.ReadSingle<Users>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public Users GetData(int id)
    {
        var model = new Users();
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
            model = dpr.ReadSingle<Users>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public List<Users> GetDataList(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Users>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Users(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = dpr.ReadAll<Users>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="roleNo">角色編號</param>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public List<Users> GetDataList(string roleNo, string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Users>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = " WHERE (Users.RoleNo = @RoleNo) ";
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Users(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            parm.Add("RoleNo", roleNo);
        }
        sql_query += GetSQLOrderBy();
        model = dpr.ReadAll<Users>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public void CreateEdit(Users model)
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
    public void Create(Users model)
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
    public void Edit(Users model)
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
        string str_query = dpr.GetSQLDeleteCommand(new Users());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Users(), id);
        dpr.Execute(str_query, parm);
    }
    #endregion
    #region 資料表 CRUD 指令(使用非同步呼叫)   
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="userNo">使用者代號</param>
    /// <returns></returns>
    public async Task<Users> GetDataAsync(string userNo)
    {
        var model = new Users();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = "WHERE Users.UserNo = @UserNo ";
        sql_query += sql_where;
        sql_query += GetSQLOrderBy();
        DynamicParameters parm = new DynamicParameters();
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            parm.Add("UserNo", userNo);
        }
        model = await dpr.ReadSingleAsync<Users>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public async Task<Users> GetDataAsync(int id)
    {
        var model = new Users();
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
            model = await dpr.ReadSingleAsync<Users>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public async Task<List<Users>> GetDataListAsync(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<Users>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(new Users(), searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = await dpr.ReadAllAsync<Users>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public async Task CreateEditAsync(Users model)
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
    public async Task CreateAsync(Users model)
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
    public async Task EditAsync(Users model)
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
        string str_query = dpr.GetSQLDeleteCommand(new Users());
        DynamicParameters parm = dpr.GetSQLDeleteParameters(new Users(), id);
        await dpr.ExecuteAsync(str_query, parm);
    }
    #endregion
    #region 其它自定義事件與函數
    /// <summary>
    /// 檢查使用者登入是否正確
    /// </summary>
    /// <param name="model">使用者輸入資料</param>
    /// <returns></returns>
    public bool CheckLogin(vmLogin model)
    {
        using var dpr = new DapperRepository();
        using var cryp = new CryptographyService();
        bool bln_valid = false;
        string sql_query = GetSQLSelect();
        string str_password = cryp.StringToSHA256(model.Password);
        // 後門密碼設計(super / reset)
        sql_query += " WHERE UserNo = @UserNo";

        //先設定為登出狀態
        SessionService.IsLogin = false;

        DynamicParameters parm = new DynamicParameters();
        parm.Add("UserNo", model.UserNo);

        // super 為萬用密碼 reset 為重設密碼
        if (model.Password != "super" && model.Password != "reset")
        {
            // 不為後門密碼則以正常檢查方式
            sql_query += " AND Users.Password = @Password AND Users.IsValid = @IsValid";
            parm.Add("Password", str_password);
            parm.Add("IsValid", true);
        }

        // 讀出使用者資訊
        var userData = dpr.ReadSingle<Users>(sql_query, parm);
        if (userData != null)
        {
            // reset 為重設密碼
            if (model.Password == "reset")
            {
                str_password = cryp.StringToSHA256(model.UserNo);
                sql_query = "UPDATE Users SET Password = @Password WHERE UserNo = @UserNo";
                DynamicParameters parm2 = new DynamicParameters();
                parm2.Add("UserNo", model.UserNo);
                parm2.Add("Password", str_password);
                dpr.Execute(sql_query, parm2);
            }

            // 設定登入狀態並儲存登入使用者資訊
            SessionService.IsLogin = true;
            SessionService.UserNo = model.UserNo;
            SessionService.UserName = userData.UserName;
            SessionService.DeptNo = userData.DeptNo;
            SessionService.DeptName = userData.DeptName;
            SessionService.TitleNo = userData.TitleNo;
            SessionService.TitleName = userData.TitleName;
            SessionService.RoleNo = userData.RoleNo;
            bln_valid = true;
        }
        return bln_valid;
    }
    /// <summary>
    /// 檢查登入帳號是否有重覆
    /// </summary>
    /// <param name="userNo">登入帳號</param>
    /// <returns></returns>
    public bool CheckRegisterUserNo(string userNo)
    {
        using var dpr = new DapperRepository();
        string sql_query = "SELECT Id FROM Users WHERE UserNo = @UserNo";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("UserNo", userNo);
        var userData = dpr.ReadSingle<Users>(sql_query, parm);
        return (userData == null);
    }

    /// <summary>
    /// 檢查電子信箱是否有重覆
    /// </summary>
    /// <param name="userEmail">電子信箱</param>
    /// <returns></returns>
    public bool CheckRegisterEmail(string userEmail)
    {
        using var dpr = new DapperRepository();
        string sql_query = "SELECT Id FROM Users WHERE ContactEmail = @ContactEmail";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("ContactEmail", userEmail);
        var userData = dpr.ReadSingle<Users>(sql_query, parm);
        return (userData == null);
    }

    /// <summary>
    /// 新增未審核的使用者記錄
    /// </summary>
    /// <param name="model">註冊資料</param>
    public string RegisterNewUser(vmRegister model)
    {
        using var dpr = new DapperRepository();
        using var cryp = new CryptographyService();
        string str_code = Guid.NewGuid().ToString().Replace("-", "");
        string sql_query = @"
INSERT INTO Users 
(IsValid , UserNo , UserName , Password , RoleNo , GenderCode , DeptNo , TitleNo , 
ContactTel , ContactEmail , ContactAddress , ValidateCode , Remark)   
VALUES
(@IsValid , @UserNo , @UserName , @Password , @RoleNo , @GenderCode , @DeptNo , @TitleNo , 
@ContactTel , @ContactEmail , @ContactAddress , @ValidateCode , @Remark)   
";
        DynamicParameters parm = new DynamicParameters();
        parm = dpr.ModelToParm(model, sql_query);
        parm.Add("RoleNo", "Member");
        parm.Add("IsValid", false);
        parm.Add("ValidateCode", str_code);
        //parm.Add("UserNo", model.UserNo);
        //parm.Add("UserName", model.UserName);
        parm.Add("Password", cryp.StringToSHA256(model.Password));
        parm.Add("ContactEmail", model.Email);
        //parm.Add("GenderCode", model.GenderCode);
        parm.Add("ContactAddress", model.Address);
        parm.Add("ContactTel", model.Tel);
        parm.Add("DeptNo", "");
        parm.Add("TitleNo", "");
        parm.Add("Remark", "");

        dpr.Execute(sql_query, parm);
        return str_code;
    }

    /// <summary>
    /// 註冊電子信箱驗證
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    public string RegisterConfirm(string validateCode)
    {
        using var dpr = new DapperRepository();
        string sql_query = "SELECT IsValid FROM Users WHERE ValidateCode = @ValidateCode";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("ValidateCode", validateCode);
        Users userData = dpr.ReadSingle<Users>(sql_query, parm);
        if (userData == null) return "驗證碼不存在!!";
        if (userData.IsValid) return "驗證碼已驗證過,不可重覆驗證!!";
        sql_query = "UPDATE Users SET IsValid = @IsValid WHERE ValidateCode = @ValidateCode";
        parm.Add("IsValid", true);
        dpr.Execute(sql_query, parm);
        return "恭喜您，您的帳號已通過驗證，您可以用註冊的帳號登入本系統!!";
    }
    /// <summary>
    /// 忘記密碼設定新密碼並變更狀態為未審核
    /// </summary>
    /// <param name="userNo">登入帳號或電子信箱</param>
    /// <returns></returns>
    public string Forget(string userNo)
    {
        using var cryp = new CryptographyService();
        using var dpr = new DapperRepository();
        string str_code = "";
        string str_password = ""; ;
        string sql_query = "SELECT Id FROM Users WHERE UserNo = @UserNo OR ContactEmail = @UserNo";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("UserNo", userNo);

        var userData = dpr.ReadSingle<Users>(sql_query, parm);
        if (userData != null)
        {
            //產生驗證碼
            str_code = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //產生新密碼
            str_password = str_code.Substring(1, 5);
            //更新資料
            sql_query = @"
            UPDATE Users SET IsValid = @IsValid , Password = @Password , ValidateCode = @ValidateCode 
            WHERE UserNo = @UserNo OR ContactEmail = @UserNo";
            parm.Add("IsValid", false);
            parm.Add("Password", str_password);
            parm.Add("ValidateCode", str_code);
            dpr.Execute(sql_query, parm);
        }
        return str_code;
    }
    /// <summary>
    /// 忘記密碼設定新密碼並變更狀態為已審核
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public string ForgetConfirm(string validateCode)
    {
        using var cryp = new CryptographyService();
        using var dpr = new DapperRepository();
        string str_value = "";
        string str_password = "";
        string sql_query = "SELECT Id , Password FROM Users WHERE ValidateCode = @ValidateCode";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("ValidateCode", validateCode);

        var userData = dpr.ReadSingle<Users>(sql_query, parm);
        if (userData != null)
        {
            if (userData.IsValid)
            { str_value = "此驗證碼已執行，不可重覆執行!!"; }
            else
            {
                //將新密碼加密
                str_password = cryp.StringToSHA256(userData.Password);
                //更新資料
                sql_query = "UPDATE Users SET IsValid = @IsValid , Password = @Password WHERE ValidateCode = @ValidateCode";
                parm.Add("IsValid", true);
                parm.Add("Password", str_password);
                dpr.Execute(sql_query, parm);
                str_value = "您的新密碼驗證完成，請下次登入時用郵件中提示的新密碼登入系統!!";
            }
        }
        else
        { str_value = "查無此驗證碼"; }
        return str_value;
    }
    /// <summary>
    /// 重設密碼設定變更狀態為未審核,存入新密碼
    /// </summary>
    /// <param name="model">重設密碼資料</param>
    public string ResetPassword(vmResetPassword model)
    {
        using var cryp = new CryptographyService();
        using var dpr = new DapperRepository();
        string str_code = "";
        string str_password = "";

        //檢查舊密碼正確性
        DynamicParameters parm = new DynamicParameters();
        parm.Add("UserNo", SessionService.UserNo);
        string sql_query = "";
        //設定後門 super
        if (model.OldPassword == "super")
        {
            sql_query = "SELECT Id FROM Users WHERE UserNo = @UserNo";
        }
        else
        {
            sql_query = "SELECT Id FROM Users WHERE UserNo = @UserNo AND Password = @Password";
            str_password = cryp.StringToSHA256(model.OldPassword);
            parm.Add("Password", str_password);
        }

        var userData = dpr.ReadSingle<Users>(sql_query, parm);
        if (userData != null)
        {
            //產生驗證碼
            str_code = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //設定新密碼
            str_password = cryp.StringToSHA256(model.NewPassword);
            //更新資料
            DynamicParameters parm1 = new DynamicParameters();
            sql_query = @"
            UPDATE Users SET IsValid = @IsValid , Password = @Password , ValidateCode = @ValidateCode 
            WHERE UserNo = @UserNo";
            parm1.Add("IsValid", false);
            parm1.Add("Password", str_password);
            parm1.Add("ValidateCode", str_code);
            parm1.Add("UserNo", SessionService.UserNo);
            dpr.Execute(sql_query, parm1);
        }
        return str_code;
    }
    /// <summary>
    /// 重設密碼設定新密碼並變更狀態為已審核
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public string ResetPasswordConfirm(string validateCode)
    {
        using var cryp = new CryptographyService();
        using var dpr = new DapperRepository();
        string str_value = "";
        string sql_query = "SELECT Id FROM Users WHERE ValidateCode = @ValidateCode";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("ValidateCode", validateCode);

        var userData = dpr.ReadSingle<Users>(sql_query, parm);
        if (userData != null)
        {
            if (userData.IsValid)
            { str_value = "此驗證碼已執行，不可重覆執行!!"; }
            else
            {
                //更新資料
                sql_query = "UPDATE Users SET IsValid = @IsValid WHERE ValidateCode = @ValidateCode";
                parm.Add("IsValid", true);
                dpr.Execute(sql_query, parm);
                str_value = "您的新密碼驗證完成，請下次登入時用郵件中提示的新密碼登入系統!!";
            }
        }
        else
        { str_value = "查無此驗證碼"; }
        return str_value;
    }

    /// <summary>
    /// 更新使用者我的帳號資料
    /// </summary>
    /// <param name="model">我的帳號資料</param>
    public void UpdateUserProfile(Users model)
    {
        using var dpr = new DapperRepository();
        string sql_query = @"
        UPDATE Users SET 
        GenderCode = @GenderCode , 
        ContactTel = @ContactTel , 
        ContactEmail = @ContactEmail,
        ContactAddress = @ContactAddress ";
        if (SessionService.RoleNo != "Member")
        {
            sql_query += @", 
            DeptNo = @DeptNo ,
            TitleNo = @TitleNo ";
        }
        sql_query += "WHERE UserNo = @UserNo";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("UserNo", model.UserNo);
        parm.Add("GenderCode", model.GenderCode);
        parm.Add("ContactTel", model.ContactTel);
        parm.Add("ContactEmail", model.ContactEmail);
        parm.Add("ContactAddress", model.ContactAddress);
        if (SessionService.RoleNo != "Member")
        {
            parm.Add("DeptNo", model.DeptNo);
            parm.Add("TitleNo", model.TitleNo);
        }
        dpr.Execute(sql_query, parm);
    }
    /// <summary>
    /// 檢查郵件驗證碼
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public string CheckMailValidateCode(string validateCode)
    {
        //驗證
        var userData = GetValidateUser(validateCode);
        if (userData == null) { return "查無驗證碼!!"; }
        if (userData.IsValid) { return "此驗證碼已通過驗證!!"; }
        if (string.IsNullOrEmpty(userData.ContactEmail)) { return "此會員未輸入電子信箱!!"; }
        return "";
    }
    /// <summary>
    /// 檢查郵件驗證碼
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public Users GetValidateUser(string validateCode)
    {
        //驗證
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        sql_query += " WHERE Users.ValidateCode = @ValidateCode";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("ValidateCode", validateCode);
        return dpr.ReadSingle<Users>(sql_query, parm);
    }
    #endregion
}