using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Models;

public class ProjectFileModel
{
    [SqlKata.Column("project_id")]
    public int ProjectId { get; set; }
    [SqlKata.Column("file_name")]
    public string FileName { get; set; }
    [SqlKata.Column("content_type")]
    public string ContentType { get; set; }
    [SqlKata.Column("file_data")]
    public byte[] FileData { get; set; }
    [SqlKata.Column("file_size")]
    public long FileSize { get; set; }
    [SqlKata.Column("upload_date")]
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("metadata")]
    public string Metadata { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}
