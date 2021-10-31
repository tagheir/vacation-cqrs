using System;

namespace TMG.Notification.Data.Model
{
 public abstract class BaseEntity
  {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
  }
}
