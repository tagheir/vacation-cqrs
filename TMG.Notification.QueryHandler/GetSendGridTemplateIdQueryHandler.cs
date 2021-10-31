using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TMG.Notification.Data;
using TMG.Notification.Query;

namespace TMG.Notification.QueryHandler
{
  public class GetSendGridTemplateIdQueryHandler : IRequestHandler<GetSendGridTemplateIdQuery, string>
  {
    private readonly EmailDbContext _emailDbContext;

    public GetSendGridTemplateIdQueryHandler(EmailDbContext emailDbContext)
    {
      this._emailDbContext = emailDbContext;
    }

    public async Task<string> Handle(GetSendGridTemplateIdQuery request, CancellationToken cancellationToken) =>
      (await this._emailDbContext.EmailTemplates
        .Where(x => x.PurposeId == request.EmailPurposeId & x.LangCode == request.LangCode)
        .FirstOrDefaultAsync(cancellationToken))?.SengGridTemplateId;
  }
}