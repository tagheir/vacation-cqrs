using MediatR;

using System;

namespace TMG.Notification.Command
{
  public class UpsertEmailPurposeCommand : IRequest<bool>
  {
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Code { get; init; }
  }
}
