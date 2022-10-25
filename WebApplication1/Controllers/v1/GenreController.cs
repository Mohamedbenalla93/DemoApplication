using DemoApplication.Core.Application.Features.Genre.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.v1
{
    
    public class GenreController : BaseApiController
    {
        [HttpGet("get-genres-as-key-value")]
        public async Task<IActionResult> GetGenresAsKeyValue() => Ok(await Mediator.Send(new GenreAsKeyValueQuery()));
    }
}
