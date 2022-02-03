using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TMG.Notification.API.Model;
using TMG.Notification.Command;
using TMG.Notification.Query;
using TMG.Notification.Query.Model;

namespace TMG.Notification.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost("purpose")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostEmailPurpose([FromBody] CreateEmailPurposeRequestModel createEmailPurposeRequest, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var isSuccess = await _mediator.Send(new UpsertEmailPurposeCommand
            {
                Code = createEmailPurposeRequest.Code,
                Id = id,
                Name = createEmailPurposeRequest.Name
            }, cancellationToken);

            if (isSuccess)
            {
                return new OkObjectResult(id);
            }

            return new BadRequestResult();
        }

        [HttpPut("purpose")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEmailPurpose([FromBody] UpdateEmailPurposeRequestModel updateEmailPurposeRequest, CancellationToken cancellationToken)
        {
            var isSuccess = await _mediator.Send(new UpsertEmailPurposeCommand
            {
                Code = updateEmailPurposeRequest.Code,
                Id = updateEmailPurposeRequest.Id,
                Name = updateEmailPurposeRequest.Name
            }, cancellationToken);
            if (isSuccess)
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }

        [HttpGet("purpose")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmailPurposeResponseDto>))]
        public async Task<IActionResult> GetEmailPurpose([FromQuery] string name, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPurposeDefinitionQuery
            {
                Name = name
            }, cancellationToken);

            return new OkObjectResult(result);
        }

        [HttpPost("template")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostEmailTemplate([FromBody] CreateEmailTemplateRequestModel createEmailTemplateRequest, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var isSuccess = await _mediator.Send(new UpsertEmailTemplateCommand
            {
                Id = id,
                LangCode = createEmailTemplateRequest.LangCode,
                PurposeId = createEmailTemplateRequest.PurposeId,
                SengGridTemplateId = createEmailTemplateRequest.SendGridTemplateId
            }, cancellationToken);

            if (isSuccess)
            {
                return new OkObjectResult(id);
            }

            return new BadRequestResult();
        }

        [HttpPut("template")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEmailTemplate([FromBody] UpdateEmailTemplateRequestModel updateEmailPurposeRequest, CancellationToken cancellationToken)
        {
            var isSuccess = await _mediator.Send(new UpsertEmailTemplateCommand
            {
                Id = updateEmailPurposeRequest.Id,
                LangCode = updateEmailPurposeRequest.LangCode,
                PurposeId = updateEmailPurposeRequest.PurposeId,
                SengGridTemplateId = updateEmailPurposeRequest.SendGridTemplateId
            }, cancellationToken);

            if (isSuccess)
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
