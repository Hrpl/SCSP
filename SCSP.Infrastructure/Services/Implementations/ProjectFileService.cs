using Microsoft.AspNetCore.Http;
using SCSP.Domain.Commons.DTO;
using SCSP.Domain.Models;
using SCSP.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Infrastructure.Services.Implementations;

public class ProjectFileService : IProjectFileService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "project_files";

    public ProjectFileService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    // Загрузка файла в базу данных
    public async Task<int> UploadFileAsync(IFormFile file, int projectId, Dictionary<string, string>? metadata = null)
    {

        await using var stream = file.OpenReadStream();
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);

        var fileData = new ProjectFileModel
        {
            ProjectId = projectId,
            FileName = file.FileName,
            ContentType = file.ContentType,
            FileData = memoryStream.ToArray(),
            FileSize = file.Length,
            Metadata = metadata != null ? System.Text.Json.JsonSerializer.Serialize(metadata) : null
        };

        await _query.Query(TableName).InsertAsync(fileData);

        return 1;
    }

    // Получение файла из базы данных
    public async Task<ProjectFileDTO?> GetFileAsync(int fileId)
    {
        var result = await _query.Query(TableName)
            .Select("id as Id", 
            "file_name as FileName", 
            "content_type as ContentType", 
            "file_data as FileData", 
            "file_size as FileSize", 
            "upload_date as UploadDate", 
            "metadata as Metatada",
            "project_id as ProjectId")
            .Where("id", fileId)
            .FirstOrDefaultAsync<ProjectFileDTO>();

        if (result == null)
            return null;

        return result;
    }

    // Удаление файла
    public async Task<bool> DeleteFileAsync(int fileId)
    {
        var affected = await _query.Query(TableName)
            .Where("id", fileId)
            .DeleteAsync();

        return affected > 0;
    }

    // Получение списка файлов (без самих данных файлов)
    public async Task<IEnumerable<FileInfo>> GetFilesListAsync(int projectId)
    {
        return await _query.Query(TableName)
            .Where("project_id", projectId)
            .Select("id as Id", 
            "file_name as FileName", 
            "content_type as ContentType", 
            "file_size as FileSize", 
            "upload_date as UploadDate")
            .GetAsync<FileInfo>();
    }
}
public class FileInfo
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    public DateTime UploadDate { get; set; }
}