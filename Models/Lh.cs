using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_final;

public partial class Lh
{
    [Key]
    public int IdLh { get; set; }

    public string? LhTieuDe { get; set; }

    public string? LhNoiDung { get; set; }
}
