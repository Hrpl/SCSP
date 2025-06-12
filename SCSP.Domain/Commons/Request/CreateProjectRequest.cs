using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Commons.Request;

public class CreateProjectRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int StudentId { get; set; }
}
