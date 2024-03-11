using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class Products
{
    /// <summary>
    /// 商品資料主檔
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 啟用
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// 計算庫存(0=不計算，1=計算)
    /// </summary>
    public bool IsInventory { get; set; }

    /// <summary>
    /// 顯示圖片
    /// </summary>
    public bool IsShowPhoto { get; set; }

    /// <summary>
    /// 商品編號
    /// </summary>
    public string? ProdNo { get; set; }

    /// <summary>
    /// 商品名稱
    /// </summary>
    public string? ProdName { get; set; }

    /// <summary>
    /// 廠牌名稱
    /// </summary>
    public string? BrandName { get; set; }

    /// <summary>
    /// 廠牌系列名稱
    /// </summary>
    public string? BrandSeriesName { get; set; }

    public string? BarcodeNo { get; set; }

    /// <summary>
    /// 商品狀態
    /// </summary>
    public string? StatusNo { get; set; }

    /// <summary>
    /// 商品編號
    /// </summary>
    public string? VendorNo { get; set; }

    /// <summary>
    /// 商品分類代號
    /// </summary>
    public string? CategoryNo { get; set; }

    /// <summary>
    /// 成本價格
    /// </summary>
    public int CostPrice { get; set; }

    /// <summary>
    /// 市場價格
    /// </summary>
    public int MarketPrice { get; set; }

    /// <summary>
    /// 銷售價格
    /// </summary>
    public int SalePrice { get; set; }

    /// <summary>
    /// 折扣價格
    /// </summary>
    public int DiscountPrice { get; set; }

    /// <summary>
    /// 庫存量
    /// </summary>
    public int InventoryQty { get; set; }

    /// <summary>
    /// 商品內容說明
    /// </summary>
    public string? ContentText { get; set; }

    /// <summary>
    /// 商品規格說明
    /// </summary>
    public string? SpecificationText { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
