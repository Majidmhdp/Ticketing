using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Core.Model;

namespace Ticketing.Core.Handler
{
    public class TicketBookingRequestHandler
    {
        public ServiceBookingResult BookService(TicketBookingRequest bookingRequest)
        {
            return new ServiceBookingResult()
            {
                Name = bookingRequest.Name,
                Family = bookingRequest.Family,
                Email = bookingRequest.Email
            };
        }
    }
}
