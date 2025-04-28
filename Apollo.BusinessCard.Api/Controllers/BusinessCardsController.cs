using Apollo.BusinessCard.Application.Common.DTOs;
using Apollo.BusinessCard.Application.Common.Shared;
using Apollo.BusinessCard.Application.Features.BusinessCard.Commands;
using Apollo.BusinessCard.Application.Features.BusinessCard.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apollo.BusinessCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardsController : ControllerBase
    {
        private ISender _mediator = null!;
        private ISender MediatR => _mediator ?? HttpContext.RequestServices.GetRequiredService<ISender>();

        [HttpGet]
        [Route("paged")]
        public async Task<Result<PaginatedList<BusinessCardBrief>>> GetAllBusinessCards([FromQuery] AllBusinessCardsPagedQuery query)
        {
            return await MediatR.Send(query);
        }

        [HttpPost]
        public async Task<Result<Unit>> CreateBusinessCard([FromBody] CreateBusinessCardCommand command)
        {
            return await MediatR.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Result<bool>> DeleteBusinessCard(int id)
        {
            var command = new DeleteBusinessCardCommand(id);
            return await MediatR.Send(command);
        }

        [HttpGet]
        [Route("statistics")]
        public async Task<Result<BusinessCardsStatisticsBrief>> GetStatistics([FromQuery] BusinessCardsStatisticsQuery query)
        {
            return await MediatR.Send(query);
        }
    }
}
