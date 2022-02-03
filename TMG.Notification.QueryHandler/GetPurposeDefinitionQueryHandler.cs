using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TMG.Notification.Data;
using TMG.Notification.Query;
using TMG.Notification.Query.Model;

namespace TMG.Notification.QueryHandler
{
    public class GetPurposeDefinitionQueryHandler : IRequestHandler<GetPurposeDefinitionQuery, List<EmailPurposeResponseDto>>
    {
        private readonly EmailDbContext _emailDbContext;

        public GetPurposeDefinitionQueryHandler(EmailDbContext emailDbContext)
        {
            this._emailDbContext = emailDbContext;
        }

        public async Task<List<EmailPurposeResponseDto>> Handle(GetPurposeDefinitionQuery request, CancellationToken cancellationToken)
        {
            var query = _emailDbContext.EmailPurposes.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            var result = await query.Select(x=> new EmailPurposeResponseDto
            {
                Name = x.Name,
                Code = x.Code,
                Id = x.Id
            }).ToListAsync(cancellationToken);

            return result;
        }
    }
}