# FreshBox - .Net MVC專案

![專案封面圖](https://imgur.com/zMrFYiP.png)

## 專案開發工具

* 使用 Visual Studio Core 為開發工具
* 以 .NET MVC (.Net 8) C# 為設計架構
* 配合 MSSQL Server 資料庫
* 資料管理的程式以 Dapper 框架搭配

## 使用者角色區分

![使用者角色區分](https://imgur.com/7ArMJ5b.png)

## 程式架構

![程式架構](https://imgur.com/3bMkWmB.png)

## 功能

測試帳號密碼

**使用者角色：**

```bash
帳號： U004
密碼： super
```

**會員角色：**

```bash
帳號： U006
密碼： super
```

## 畫面

##### 前台

![商品加入購物車](https://imgur.com/2KoHrId.png)
![訂單查詢](https://imgur.com/Ae4JeF2.png)
![訂單明細](https://imgur.com/FKUe0Sd.png)

##### 後台

![會員登入](https://imgur.com/dmDtuc3.png)
![商品](https://imgur.com/DlABZGN.png)
![編輯圖片](https://imgur.com/nBSsifB.png)

## 安裝

1. [.Net 8.0以上](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. [SQL2022](https://go.microsoft.com/fwlink/p/?linkid=2215158&amp;clcid=0x404&amp;culture=zh-tw&amp;country=tw)
3. [SQL Server Management Studio（SSMS）](https://aka.ms/ssmsfullsetup)

### 取得專案

```bash
git clone https://github.com/bonnieli1414/FreshBox.git
```

### 附加資料庫

將專案內的SQL > FreshBox 的資料附加到SSMS

### 移動到專案內

```bash
cd FreshBox
```

### 修改連線字串資料

開啟專案內的appsettings.json 和 appsettings.Development.json檔案，
並將連線字串內的 1.伺服器名稱 2.帳號 3.密碼 修改為自己電腦SQL Server設定。

```bash
"ConnectionStrings": {
    "dbconn": "Server=自己電腦SQL Server的伺服器名稱;Database=FreshBox;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;User ID=自己電腦SQL Server的帳號;Password=自己電腦SQL Server的密碼;Integrated Security=False"
  }
```

### 安裝套件

```bash
libman restore
```

### 運行專案

```bash
dotnet watch
```

### 開啟專案

在瀏覽器網址列輸入以下即可看到畫面

```bash
http://localhost:5286/
```

## 資料夾說明

* AppCodes - 資料庫存取及應用程式的核心程式碼處理放置處
* Areas - 各個角色的控制器和畫面放置處
* Controllers - 控制器放置處
* Models - 模組放置處
* Views - 畫面放置處
* www - 靜態資源放置處
  * css - css 檔案放置處
  * images - 圖片放置處
  * js - javaScript放置處
  * lib - 前端套件放置處
  * vendor - 前端套件放置處
* appsettings.json - 連線字串設定
* appsettings.Development.json - 連線字串設定
