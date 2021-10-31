using System;

namespace TMG.Notification.API.Model
{
  public class UpdateEmailTemplateRequestModel : CreateEmailTemplateRequestModel
  {
    public Guid Id { get; set; }
  }
}