using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Core.Enum;

namespace Ticketing.Core.Model
{
    public class ServiceBookingResult : ServiceBookingBase
	{
        public BookingResultFlag Flag { get; set; }

        public int? TicketBookingId {get; set; }
      
    }
}
