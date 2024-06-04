using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Core.Model;
using Ticketing.Core.Services;

namespace Ticketing.Core.Handler
{
    public class TicketBookingRequestHandler
    {
	    public TicketBookingRequestHandler(ITicketBookingService ticketBookingService)
	    {
	    }

	    public ServiceBookingResult BookService(TicketBookingRequest bookingRequest)
        {
	        if (bookingRequest is null)
	        {
		        throw new ArgumentNullException(nameof(bookingRequest));
	        }
            return new ServiceBookingResult()
            {
                Name = bookingRequest.Name,
                Family = bookingRequest.Family,
                Email = bookingRequest.Email
            };
        }
    }
}
