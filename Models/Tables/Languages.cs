using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class Languages
{
    public int Id { get; set; }

    public string? LangNo { get; set; }

    public string? LangName { get; set; }

    public string? Remark { get; set; }
}
