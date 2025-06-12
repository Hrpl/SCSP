using Microsoft.AspNetCore.Http;
using SCSP.Domain.Commons.DTO;
using FileInfo = SCSP.Infrastructure.Services.Implementations.FileInfo;

namespace SCSP.Infrastructure.Services.Interfaces;

public interface IProjectFileService
{
    public Task<IEnumerable<FileInfo>> GetFilesListAsync();
    public Task<ProjectFileDTO?> GetFileAsync(int fileId);
    public Task<int> UploadFileAsync(IFormFile file, int projectId, Dictionary<string, string>? metadata = null);
    public Task<bool> DeleteFileAsync(int fileId);
}
