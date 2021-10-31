using MediatR;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading;
using System.Threading.Tasks;

using TMG.Notification.Command;
using TMG.Notification.Data;
using TMG.Notification.Data.Model;

namespace TMG.Notification.CommandHandler
{
  public class UpsertEmailTemplateCommandHandler : IRequestHandler<UpsertEmailTemplateCommand,bool>
  {
    private readonly EmailDbContext _emailDbContext;

    public UpsertEmailTemplateCommandHandler(EmailDbContext emailDbContext)
    {
      this._emailDbContext = emailDbContext;
    }

    public async Task<bool> Handle(UpsertEmailTemplateCommand request, CancellationToken cancellationToken)
    {
      var emailTemplate = new EmailTemplate
      {
        Id = request.Id,
        LangCode=request.LangCode,
        PurposeId = request.PurposeId,
        SengGridTemplateId = request.SengGridTemplateId,
        ModifiedOn = DateTime.UtcNow
      };

      if (await _emailDbContext.EmailTemplates.AnyAsync(x => x.Id == request.Id, cancellationToken))
      {
        _emailDbContext.Update(emailTemplate);
      }
      else
      {
        emailTemplate.CreatedOn = DateTime.UtcNow;
        _emailDbContext.Add(emailTemplate);
      }

      await _emailDbContext.SaveChangesAsync(cancellationToken);

      return true;
    }
  }
}
