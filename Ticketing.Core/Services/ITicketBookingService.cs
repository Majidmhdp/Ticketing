using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Core.Domain;

namespace Ticketing.Core.Services
{
	public interface ITicketBookingService
	{
		void Save(TicketBooking ticketBooking);
	}
}
