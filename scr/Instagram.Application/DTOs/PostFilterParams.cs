using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Application.DTOs;

public class PostFilterParams
{
    public string? Search { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int Skip { get; set; } = 0; 
    public int Take { get; set; } = 10; 
}
