using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class Photos
{
    public int Id { get; set; }

    public string? CodeNo { get; set; }

    public string? PhotoNo { get; set; }

    public string? ProdNo { get; set; }

    public string? FolderNo { get; set; }

    public string? FolderName { get; set; }

    public string? Remark { get; set; }
}
