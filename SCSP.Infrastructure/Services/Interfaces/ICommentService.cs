using SCSP.Domain.Commons.DTO;
using SCSP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Infrastructure.Services.Interfaces;

public interface ICommentService : IAsyncRepository<CommentDTO, CommentModel>
{
    public Task<IEnumerable<CommentDTO>> GetByProjectIdAsync(int projectId);
}
