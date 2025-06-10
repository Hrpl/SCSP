using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Commons.DTO;

public class ProjectFileDTO
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string File { get; set; }
}
