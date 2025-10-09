using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Events
{
    public record TourPurchaseRequested(long PurchaseId, long UserId, long TourId, long AuthorId);
}
