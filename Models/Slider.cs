﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Web_final.Models;

public partial class Slider
{
    public int IdSlide { get; set; }

    public string? Title { get; set; }

    public string? Img { get; set; }

    public int? Order { get; set; }

    public string? Link { get; set; }

    public int? Hide { get; set; }
}
