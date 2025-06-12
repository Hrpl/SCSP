using SCSP.Domain.Commons.DTO;
using SCSP.Domain.Commons.Request;
using SCSP.Domain.Models;

namespace SCSP.Infrastructure.Services.Interfaces;

public interface IProjectService
{
    public Task<int> CreateAsync(ProjectModel model);
    public Task<IEnumerable<ProjectDTO>> GetAsync(int userId, FilterRequest request);
    public int DeleteAsync(int id);
}
