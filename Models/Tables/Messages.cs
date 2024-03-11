using System;
using System.Collections.Generic;

namespace FreshBox.Models;

public partial class Messages
{
    public int Id { get; set; }

    public bool IsRead { get; set; }

    public string? CodeNo { get; set; }

    public string? SenderNo { get; set; }

    public string? ReceiverNo { get; set; }

    public DateTime SendDate { get; set; }

    public TimeSpan SendTime { get; set; }

    public string ContactorName { get; set; } = null!;

    public string ContactorEmail { get; set; } = null!;

    public string HeaderText { get; set; } = null!;

    public string? MessageText { get; set; }

    public string? Remark { get; set; }
}
