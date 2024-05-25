using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Ticketing.Core.Handler;
using Ticketing.Core.Model;

namespace Ticketing.Core.Test
{
    public class TicketBookingRequestHandlerTest
	{
		[Fact]
		public void ShouldReturnTicketBookingResponseWithValues()
		{
			// Arrange
			var bookingRequest = new TicketBookingRequest
			{
				Name = "Test Name",
				Family = "Test Family",
				Email = "TestEmail"
			};

			var Handler = new TicketBookingRequestHandler();

			// Act
			ServiceBookingResult Result = Handler.BookService(bookingRequest);


			// Assert

			//Assert.NotNull(Result);
			Result.ShouldNotBeNull();

			//Assert.Equal(Result.Name, bookingRequest.Name);
			Result.Name.ShouldBe(bookingRequest.Name);

			//Assert.Equal(Result.Family, bookingRequest.Family);
			Result.Family.ShouldBe(bookingRequest.Family);

			//Assert.Equal(Result.Email, bookingRequest.Email);
			Result.Email.ShouldBe(bookingRequest.Email);
		}

	}

	
}
