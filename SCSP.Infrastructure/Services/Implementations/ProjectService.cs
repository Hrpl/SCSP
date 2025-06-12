using SCSP.Domain.Commons.DTO;
using SCSP.Domain.Commons.Request;
using SCSP.Domain.Models;
using SCSP.Infrastructure.Services.Interfaces;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Infrastructure.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "projects";

    public ProjectService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<int> CreateAsync(ProjectModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        return await _query.ExecuteAsync(query);
    }

    public async Task<IEnumerable<ProjectDTO>> GetAsync(int userId, FilterRequest request)
    {
        var query = _query.Query(TableName)
            .Where("teacher_id", userId)
            .OrWhere("student_id", userId)
            .When(request.SortingOnName == 0, q => q.OrderByDesc("name"), q => q.OrderBy("name"))
            .When(request.SortingOnDate == 0, q => q.OrderByDesc("created_at"), q => q.OrderBy("created_at"))
            .Select("id as Id",
            "name as Name",
            "description as Description",
            "teacher_id as TeacherId",
            "student_id as StudentId");

        if (!string.IsNullOrEmpty(request.Search)) 
        {
            query.WhereRaw($"LOWER(name) LIKE LOWER(?)", $"%{request.Search}%");
        }


        var result = await _query.GetAsync<ProjectDTO>(query);

        return result;    
    }

    public int DeleteAsync(int id)
    {
        return _query.Query(TableName).Where("id", id).Delete();
    }
}
