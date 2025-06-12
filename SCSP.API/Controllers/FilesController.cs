using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCSP.Infrastructure.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SCSP.Controllers;

[Route("files")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IProjectFileService _fileUploadService;

    public FilesController(IProjectFileService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    [HttpPost("upload")]
    [SwaggerOperation(Summary = "Загрузка файла в проект")]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] int projectId)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        try
        {
            var fileId = await _fileUploadService.UploadFileAsync(file, projectId);
            return Ok(new { FileId = fileId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("download/{fileId}")]
    [SwaggerOperation(Summary = "Скачивание файла по Id")]
    public async Task<IActionResult> DownloadFile(int fileId)
    {
        var file = await _fileUploadService.GetFileAsync(fileId);

        if (file == null)
            return NotFound();

        return File(file.FileData, file.ContentType, file.FileName);
    }

    [HttpGet("list")]
    [SwaggerOperation(Summary = "Получение списка файлов проекта")]
    public async Task<IActionResult> GetFilesList()
    {
        var files = await _fileUploadService.GetFilesListAsync();
        return Ok(files);
    }

    [HttpDelete("{fileId}")]
    [SwaggerOperation(Summary = "Удаление файла по его id")]
    public async Task<IActionResult> DeleteFile(int fileId)
    {
        var result = await _fileUploadService.DeleteFileAsync(fileId);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
