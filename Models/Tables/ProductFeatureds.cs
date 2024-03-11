using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class ProductFeatureds
{
    /// <summary>
    /// 商品特色明細檔
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 商品編號
    /// </summary>
    public string? ProdNo { get; set; }

    /// <summary>
    /// 排序編號
    /// </summary>
    public string? SortNo { get; set; }

    /// <summary>
    /// 特色說明項目
    /// </summary>
    public string? FeaturedName { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
