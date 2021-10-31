using System;

namespace TMG.Notification.API.Model
{
  public class UpdateEmailPurposeRequestModel : CreateEmailPurposeRequestModel
  {
    public Guid Id { get; set; }
  }
}
