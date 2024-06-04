using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Ticketing.Core.Domain;
using Ticketing.Core.Handler;
using Ticketing.Core.Model;
using Ticketing.Core.Services;

namespace Ticketing.Core.Test
{
	public class TicketBookingRequestHandlerTest
	{
		private readonly TicketBookingRequestHandler _handler;
		private readonly TicketBookingRequest _request;
		private readonly Mock<ITicketBookingService> _ticketBookingServiceMock;

		public TicketBookingRequestHandlerTest()
		{
			// Arrange
			
			_request = new TicketBookingRequest
			{
				Name = "Test Name",
				Family = "Test Family",
				Email = "TestEmail"
			};

			_ticketBookingServiceMock = new Mock<ITicketBookingService>();

			_handler = new TicketBookingRequestHandler(_ticketBookingServiceMock.Object);
		}

		[Fact]
		public void ShouldReturnTicketBookingResponseWithValues()
		{
			// Act
			ServiceBookingResult Result = _handler.BookService(_request);


			// Assert

			//Assert.NotNull(Result);
			Result.ShouldNotBeNull();

			//Assert.Equal(Result.Name, bookingRequest.Name);
			Result.Name.ShouldBe(_request.Name);

			//Assert.Equal(Result.Family, bookingRequest.Family);
			Result.Family.ShouldBe(_request.Family);

			//Assert.Equal(Result.Email, bookingRequest.Email);
			Result.Email.ShouldBe(_request.Email);
		}

		[Fact]
		public void ShouldTrowExceptionForNullRequest()
		{
			//Assert.Throws<ArgumentNullException>(() => _handler.BookService(null));

			var exception = Should.Throw<ArgumentNullException>(() => _handler.BookService(null));
			exception.ParamName.ShouldBe("bookingRequest");
		}

		[Fact]
		public void ShouldSaveTicketBookingRequest()
		{
			TicketBooking savedBooking = null;

			_ticketBookingServiceMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
				.Callback<TicketBooking>(booking => { savedBooking = booking; });


			_handler.BookService(_request);

			_ticketBookingServiceMock.Verify(x=> x.Save(It.IsAny<TicketBooking>()), Times.Once());

			savedBooking.ShouldNotBeNull();
			savedBooking.Name.ShouldBe(_request.Name);
			savedBooking.Family.ShouldBe(_request.Family);
			savedBooking.Email.ShouldBe(_request.Email);
		}
	}
}