using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class OrdersStatus
{
    /// <summary>
    /// 訂單狀態主檔
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 已付款
    /// </summary>
    public bool IsPayed { get; set; }

    /// <summary>
    /// 已結單
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// 狀態編號
    /// </summary>
    public string? StatusNo { get; set; }

    /// <summary>
    /// 狀態名稱
    /// </summary>
    public string? StatusName { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
