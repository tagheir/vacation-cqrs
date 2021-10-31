using System;

namespace TMG.Notification.API.Model
{
  public class CreateEmailTemplateRequestModel
  {
    public Guid PurposeId { get; set; }
    public string LangCode { get; set; }
    public string SendGridTemplateId { get; set; }
  }
}
