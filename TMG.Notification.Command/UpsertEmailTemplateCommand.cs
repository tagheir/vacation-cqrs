using MediatR;

using System;

namespace TMG.Notification.Command
{
  public class UpsertEmailTemplateCommand : IRequest<bool>
  {
    public Guid Id { get; init; }
    public Guid PurposeId { get; init; }
    public string LangCode { get; init; }
    public string SengGridTemplateId { get; init; }
  }
}
