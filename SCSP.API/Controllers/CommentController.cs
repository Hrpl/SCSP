using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCSP.Domain.Commons.DTO;
using SCSP.Domain.Commons.Request;
using SCSP.Domain.Models;
using SCSP.Infrastructure.Services.Implementations;
using SCSP.Infrastructure.Services.Interfaces;
using Sprache;
using Swashbuckle.AspNetCore.Annotations;

namespace SCSP.Controllers;

[Route("comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    // GET: api/<ProjectsController>
    [HttpGet("{projectId}")]
    [SwaggerOperation(Summary = "Получение комментариев по id проекта")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> Get(int projectId)
    {
        try
        {
            var result = await _commentService.GetByProjectIdAsync(projectId);

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
    [SwaggerOperation(Summary = "Создание комментария. Нужен JWT")]
    public async Task<ActionResult> Post([FromBody] CreateCommentRequest request)
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

            var model = _mapper.Map<CommentModel>(request);

            model.UserId = Convert.ToInt32(userId);
            await _commentService.CreateAsync(model);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/<ProjectsController>/5
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удаление комментария")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _commentService.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
