using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class OrderDetails
{
    /// <summary>
    /// 會員訂單明細檔
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 父階編號
    /// </summary>
    public string? ParentNo { get; set; }

    /// <summary>
    /// 廠商編號
    /// </summary>
    public string? VendorNo { get; set; }

    /// <summary>
    /// 商品分類編號
    /// </summary>
    public string? CategoryNo { get; set; }

    /// <summary>
    /// 商品編號
    /// </summary>
    public string? ProdNo { get; set; }

    /// <summary>
    /// 商品名稱
    /// </summary>
    public string? ProdName { get; set; }

    /// <summary>
    /// 商品規格
    /// </summary>
    public string? ProdSpec { get; set; }

    /// <summary>
    /// 商品單價
    /// </summary>
    public int OrderPrice { get; set; }

    /// <summary>
    /// 訂購數量
    /// </summary>
    public int OrderQty { get; set; }

    /// <summary>
    /// 合法金額
    /// </summary>
    public int OrderAmount { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
