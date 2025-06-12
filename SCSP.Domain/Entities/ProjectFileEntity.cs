using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SCSP.Domain.Entities;

public class ProjectFileEntity : BaseEntity
{
    public int ProjectId { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] FileData { get; set; }
    public long FileSize { get; set; }
    public DateTime? UploadDate { get; set; } = DateTime.UtcNow;
    public string? MetadataJson { get; set; }

    [Ignore] // если используете Dapper.FluentMap или аналоги
    public Dictionary<string, string>? Metadata
    {
        get => MetadataJson != null ? JsonSerializer.Deserialize<Dictionary<string, string>>(MetadataJson) : null;
        set => MetadataJson = value != null ? JsonSerializer.Serialize(value) : null;
    }
}
