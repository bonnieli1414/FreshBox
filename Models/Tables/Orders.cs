using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class Orders
{
    /// <summary>
    /// 會員訂單表頭檔
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 訂單編號
    /// </summary>
    public string? SheetNo { get; set; }

    /// <summary>
    /// 訂單日期
    /// </summary>
    public DateTime SheetDate { get; set; }

    /// <summary>
    /// 訂單狀態代碼
    /// </summary>
    public string? StatusCode { get; set; }

    /// <summary>
    /// 已結單
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// 合法訂單
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// 會員編號
    /// </summary>
    public string? CustNo { get; set; }

    /// <summary>
    /// 會員姓名
    /// </summary>
    public string? CustName { get; set; }

    /// <summary>
    /// 付款方式編號
    /// </summary>
    public string? PaymentNo { get; set; }

    /// <summary>
    /// 出貨方式編號
    /// </summary>
    public string? ShippingNo { get; set; }

    /// <summary>
    /// 收件人姓名
    /// </summary>
    public string? ReceiverName { get; set; }

    /// <summary>
    /// 收件人電子信箱
    /// </summary>
    public string? ReceiverEmail { get; set; }

    /// <summary>
    /// 收件人地址
    /// </summary>
    public string? ReceiverAddress { get; set; }

    /// <summary>
    /// 訂單未稅金額
    /// </summary>
    public int OrderAmount { get; set; }

    /// <summary>
    /// 訂單稅額
    /// </summary>
    public int TaxAmount { get; set; }

    /// <summary>
    /// 訂單已稅金額
    /// </summary>
    public int TotalAmount { get; set; }

    /// <summary>
    /// 備註說明
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 唯一值編號
    /// </summary>
    public string? GuidNo { get; set; }
}
