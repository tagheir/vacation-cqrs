using System;

using MediatR;

namespace TMG.Notification.Query
{
  public class GetSendGridTemplateIdQuery : IRequest<string>
  {
    public Guid EmailPurposeId { get; init; }
    public string LangCode { get; init; }
  }
}