﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;
using RoutePlanning.Application.Locations.Queries.DeliveryInfo;
using RoutePlanning.Client.Web.Authorization;

namespace RoutePlanning.Client.Web.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize(nameof(TokenRequirement))]
[AllowAnonymous]
public sealed class RoutesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoutesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public Task<string> HelloWorld()
    {
        return Task.FromResult("Hello World!");
    }

    [HttpPost("[action]")]
    public async Task AddTwoWayConnection(CreateTwoWayConnectionCommand command)
    {
        await _mediator.Send(command);
    }

    [HttpGet("[action]")]
    public async Task<DeliveryInfo> GetDeliveryInfo(DeliveryInfoQuery query)
    {
        return await _mediator.Send(query);
    }
}
