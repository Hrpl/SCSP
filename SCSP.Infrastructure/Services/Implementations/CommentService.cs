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

public class CommentService : ICommentService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "comments";

    public CommentService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<int> CreateAsync(CommentModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        return await _query.ExecuteAsync(query);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var query = _query.Query(TableName).Where("id", id).AsDelete();

        return await _query.ExecuteAsync(query);
    }

    public Task<IEnumerable<CommentDTO>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(CommentModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CommentDTO>> GetByProjectIdAsync(int projectId)
    {
        var query = _query.Query("comments as c").Where("c.project_id", projectId)
            .LeftJoin("users as u", "u.id", "c.user_id")
            .Select("c.id as Id",
            "c.project_id as ProjectId",
            "u.name as Name",
            "c.comment as Comment");

        var result = await _query.GetAsync<CommentDTO>(query);

        return result;
    }
}
