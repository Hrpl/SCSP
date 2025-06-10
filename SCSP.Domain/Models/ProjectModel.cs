using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Models;

public class ProjectModel
{
    [SqlKata.Column("name")]
    public string Name { get; set; }
    [SqlKata.Column("description")]
    public string Description { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}
