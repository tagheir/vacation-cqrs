using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Mail;

namespace TMG.Notification.API.Model
{
  public class SendEmailRequestModel
  {
    public EmailAddress From { get; set; }
    public EmailAddress ReplyTo { get; set; }
    public List<EmailAddress> ToEmailIds { get; set; }
    public List<EmailAddress> CcEmailIds { get; set; }
    public List<EmailAddress> BccEmailIds { get; set; }
    public JObject DynamicContent { get; set; }
    public IEnumerable<Attachment> Attachments { get; set; }
    public Guid PurposeId { get; set; }
    public string LangCode { get; set; }
  }
}