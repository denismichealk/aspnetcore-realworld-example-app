using Conduit.Features.Comments;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading;

namespace Conduit.MinimalApi
{
    public static class Comments
    {
        public static RouteGroupBuilder RegisterCommentEndpoints(this RouteGroupBuilder app)
        {
            app.MapPost("articles/{slug}/comments", [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)] async (string slug,
                [Validate][FromBody] Create.Model model, CancellationToken cancellationToken,
               IMediator mediator) => await mediator.Send(new Create.Command(model, slug), cancellationToken));

            app.MapGet("articles/{slug}/comments", async (string slug,
               [FromBody] Create.Model model, CancellationToken cancellationToken,
              IMediator mediator) => await mediator.Send(new List.Query(slug), cancellationToken));

            app.MapDelete("articles/{slug}/comments/{id}", [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)] async ([Validate] string slug,
                int id, CancellationToken cancellationToken, IMediator mediator) => await mediator.Send(new Delete.Command(slug, id), cancellationToken));

            return app;

        }
    }
}

