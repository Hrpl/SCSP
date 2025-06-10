using SCSP.Domain.Commons.Request;
using SCSP.Domain.Commons.Response;
using SCSP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Infrastructure.Services.Interfaces;

public interface IAsyncRepository<TDTO, DModel> where TDTO : class where DModel : class
{
    public Task<IEnumerable<TDTO>> GetAsync();
    public Task<int> CreateAsync(DModel model);
    public Task<int> UpdateAsync(DModel model);
    public Task<int> DeleteAsync(int id);
}
