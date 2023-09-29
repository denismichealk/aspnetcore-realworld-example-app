using Conduit.Features.Favorites;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Threading;

namespace Conduit.MinimalApi
{
    public static class Favorites
    {
        public static RouteGroupBuilder RegisterFavoritesEndpoint(this RouteGroupBuilder app)
        {
            app.MapPost("articles/{slug}/favorite", [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)] async ([Validate] string slug,
                 CancellationToken cancellationToken,
               IMediator mediator) => await mediator.Send(new Add.Command(slug), cancellationToken));

            app.MapDelete("articles/{slug}/favorite", [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)] async ([Validate] string slug,
                CancellationToken cancellationToken, IMediator mediator) => await mediator.Send(new Delete.Command(slug), cancellationToken));

            return app;

        }
    }
}
