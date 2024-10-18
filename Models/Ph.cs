using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_final;

public partial class Ph
{
    [Key]
    public int ID_PH { get; set; }

    public int? IdUsers { get; set; }

    public string? NdPh { get; set; }

    public int? Hide { get; set; }
    public string? UsersName { get; set; }
    public string? img {  get; set; }

    public virtual User? IdUsersNavigation { get; set; }
}
