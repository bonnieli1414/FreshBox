using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class ProductStatus
{
    /// <summary>
    /// 商品狀態明細檔
    /// </summary>
    public int Id { get; set; }

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
