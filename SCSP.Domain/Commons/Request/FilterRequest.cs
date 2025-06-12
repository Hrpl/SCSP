using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Commons.Request;

public class FilterRequest
{
    public string? Search {  get; set; }
    public int? SortingOnName { get; set; }
    public int? SortingOnDate { get; set; }
}
