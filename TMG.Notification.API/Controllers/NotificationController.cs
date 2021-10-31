using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TMG.Notification.API.Model;
using TMG.Notification.Command;
using TMG.Notification.Query;

namespace TMG.Notification.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
      this._mediator = mediator;
    }

    [HttpPost("email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostEmailTemplate([FromBody] SendEmailRequestModel sendEmailRequestModel, CancellationToken cancellationToken)
    {
      var senGridTemplateId = await _mediator.Send(new GetSendGridTemplateIdQuery
      {
        LangCode = sendEmailRequestModel.LangCode,
        EmailPurposeId = sendEmailRequestModel.PurposeId
      }, cancellationToken);

      if (string.IsNullOrWhiteSpace(senGridTemplateId))
      {
        return new BadRequestObjectResult("sendGrid template not found");
      }

      var isSuccess = await _mediator.Send(new SendEmailCommand
      {
        CcEmailIds = sendEmailRequestModel.CcEmailIds,
        BccEmailIds = sendEmailRequestModel.BccEmailIds,
        Attachments = sendEmailRequestModel.Attachments,
        DynamicContent = sendEmailRequestModel.DynamicContent,
        From = sendEmailRequestModel.From,
        ReplyTo = sendEmailRequestModel.ReplyTo,
        TemplateId = senGridTemplateId,
        ToEmailIds = sendEmailRequestModel.ToEmailIds
      }, cancellationToken);

      if (isSuccess)
      {
        return new OkResult();
      }

      return new BadRequestObjectResult("Email sending fail");
    }
  }
}
