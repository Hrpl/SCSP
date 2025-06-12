using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Commons.Request;

public class CreateCommentRequest
{
    public int ProjectId { get; set; }
    public string Comment { get; set; }
}
