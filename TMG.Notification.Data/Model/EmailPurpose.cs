using System;
using System.Collections.Generic;

namespace TMG.Notification.Data.Model
{
  public class EmailPurpose: BaseEntity
  {
    public string Name { get; set; }
    public string Code { get; set; } //Same code map in sendgrid
    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; }
  }
}
