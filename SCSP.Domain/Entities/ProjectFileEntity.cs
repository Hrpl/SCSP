using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Entities;

public class ProjectFileEntity : BaseEntity
{
    public int ProjectId { get; set; }
    public string File {  get; set; }
}
