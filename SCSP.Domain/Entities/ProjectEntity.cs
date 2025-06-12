using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Entities;

public class ProjectEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int TeacherId { get; set; }
    public int StudentId { get; set; }
}
