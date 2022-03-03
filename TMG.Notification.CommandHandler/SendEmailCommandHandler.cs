using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SendGrid;
using SendGrid.Helpers.Mail;

using TMG.Notification.Command;

namespace TMG.Notification.CommandHandler
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly ILogger<SendEmailCommandHandler> _logger;

        public SendEmailCommandHandler(ISendGridClient sendGridClient, ILogger<SendEmailCommandHandler> logger)
        {
            this._sendGridClient = sendGridClient;
            this._logger = logger;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                  $"Started {nameof(SendEmailCommandHandler)} | {nameof(request.TemplateId)} : {request.TemplateId}");
                var msg = new SendGridMessage()
                {
                    From = request.From,
                    ReplyTo = request.ReplyTo,
                };
                msg.AddTos(request.ToEmailIds);

                if (!string.IsNullOrWhiteSpace(request.HtmlContent))
                {
                    var dynamicHtml = new
                    {
                        htmlContent = request.HtmlContent
                    };
                    msg.SetTemplateData(dynamicHtml);
                }
                else
                {
                    msg.SetTemplateData(request.DynamicContent);
                }

                if (!string.IsNullOrWhiteSpace(request.Subject))
                {
                    msg.SetSubject(request.Subject);
                }

                msg.SetTemplateId(request.TemplateId);

                if (request.Attachments?.Any() ?? false)
                {
                    msg.AddAttachments(request.Attachments);
                }

                if (request.CcEmailIds?.Any() ?? false)
                {
                    msg.AddCcs(request.CcEmailIds);
                }

                if (request.BccEmailIds?.Any() ?? false)
                {
                    msg.AddBccs(request.BccEmailIds);
                }

                var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
                var responseString = await response.Body.ReadAsStringAsync();


                _logger.LogInformation(
                  $"Completed {nameof(SendEmailCommandHandler)} | {nameof(request.TemplateId)} : {request.TemplateId} | {nameof(response.StatusCode)} : {response.StatusCode} | {nameof(responseString)} : {responseString}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(
                  $"Completed {nameof(SendEmailCommandHandler)} | {nameof(request.TemplateId)} : {request.TemplateId} | {nameof(ex.Message)} : {ex.Message} | {nameof(ex.StackTrace)} : {ex.StackTrace}");

                return false;
            }
        }
    }
}