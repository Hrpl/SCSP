using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Entities;

public class CommentEntity : BaseEntity
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
}
