using System;

namespace TMG.Notification.Data.Model
{
  public class EmailTemplate: BaseEntity
  {
    public Guid PurposeId { get; set; }
    public string LangCode { get; set; }
    public string SengGridTemplateId { get; set; }
    public virtual EmailPurpose EmailPurpose { get; set; }
  }
}
