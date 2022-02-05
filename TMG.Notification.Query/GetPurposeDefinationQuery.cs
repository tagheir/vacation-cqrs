using System.Collections.Generic;

using MediatR;

using TMG.Notification.Query.Model;

namespace TMG.Notification.Query
{
    public class GetPurposeDefinitionQuery : IRequest<List<EmailPurposeResponseDto>>
    {
        public string Name { get; set; }
    }
}