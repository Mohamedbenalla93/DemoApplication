using DemoApplication.Core.Application.Features.Author.Queries.GetAuthorsAsKeyValue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.v1
{
    
    public class AuthorController : BaseApiController
    {
        [HttpGet("get-authors-as-key-value")]
        public async Task<IActionResult> GetAuthorsAsKeyValue() =>
            Ok(await Mediator.Send(new GetAuthorAsKeyValueQuery()));
    }
}
