using System.Collections.Generic;

using MediatR;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Mail;

namespace TMG.Notification.Command
{
  public class SendEmailCommand : IRequest<bool>
  {
    public EmailAddress From { get; init; }
    public EmailAddress ReplyTo { get; init; }
    public List<EmailAddress> ToEmailIds { get; init; }
    public List<EmailAddress> CcEmailIds { get; init; }
    public List<EmailAddress> BccEmailIds { get; init; }
    public JObject DynamicContent { get; init; }
    public string HtmlContent { get; init; }
    public string TemplateId { get; init; }
    public string Subject { get; init; }
    public IEnumerable<Attachment> Attachments { get; init; }
  }
}