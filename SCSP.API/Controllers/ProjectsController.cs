using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCSP.Domain.Commons.DTO;
using SCSP.Domain.Commons.Request;
using SCSP.Domain.Models;
using SCSP.Infrastructure.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCSP.Controllers;

[Route("projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    // GET: api/<ProjectsController>
    [HttpGet]
    [Authorize]
    [SwaggerOperation(Summary = "Получение проектов пользователя. Нужен JWT")]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> Get(FilterRequest request)
    {
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Unauthorized",
                    Detail = "Invalid user ID in token."
                });
            }

            var result = await _projectService.GetAsync(Convert.ToInt32(userId), request);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST api/<ProjectsController>
    [HttpPost]
    [Authorize]
    [SwaggerOperation(Summary = "Создание проекта. Нужен JWT")]
    public async Task<ActionResult> Post([FromBody] CreateProjectRequest request)
    {
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Unauthorized",
                    Detail = "Invalid user ID in token."
                });
            }

            var model = _mapper.Map<ProjectModel>(request);

            model.TeacherId = Convert.ToInt32(userId);

            await _projectService.CreateAsync(model);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/<ProjectsController>/5
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удаление проекта")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            _projectService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
