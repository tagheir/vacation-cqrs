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
  public class UpsertEmailPurposeCommandHandler : IRequestHandler<UpsertEmailPurposeCommand, bool>
  {
    private readonly EmailDbContext _emailDbContext;

    public UpsertEmailPurposeCommandHandler(EmailDbContext emailDbContext)
    {
      this._emailDbContext = emailDbContext;
    }
    public async Task<bool> Handle(UpsertEmailPurposeCommand request, CancellationToken cancellationToken)
    {
      var emailPurpose = new EmailPurpose
      {
        Id = request.Id,
        Code = request.Code,
        Name = request.Name,
        ModifiedOn = DateTime.UtcNow
      };

      if (await _emailDbContext.EmailPurposes.AnyAsync(x => x.Id == request.Id, cancellationToken))
      {
        _emailDbContext.Update(emailPurpose);
      }
      else
      {
        emailPurpose.CreatedOn = DateTime.UtcNow;
        _emailDbContext.Add(emailPurpose);
      }

      await _emailDbContext.SaveChangesAsync(cancellationToken);
      return true;
    }
  }
}
