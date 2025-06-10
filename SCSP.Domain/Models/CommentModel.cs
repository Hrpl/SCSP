using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Models;

public class CommentModel
{
    [SqlKata.Column("project_id")]
    public int ProjectId { get; set; }
    [SqlKata.Column("user_id")]
    public int UserId { get; set; }
    [SqlKata.Column("comment")]
    public string Comment { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}
