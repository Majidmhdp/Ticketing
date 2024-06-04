using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Moq;
using Shouldly;
using Ticketing.Core.Domain;
using Ticketing.Core.Enum;
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
		private List<Ticket> _availableTickets;

		public TicketBookingRequestHandlerTest()
		{
			// Arrange

			_request = new TicketBookingRequest
			{
				Name = "Test Name",
				Family = "Test Family",
				Email = "TestEmail",
				Date = DateTime.Now
			};

			_availableTickets = new EditableList<Ticket>()
			{
				new Ticket()
				{
					Id = 1
				}
			};

			_ticketBookingServiceMock = new Mock<ITicketBookingService>();

			_ticketBookingServiceMock
				.Setup(q => q.GetAvailableTickets(_request.Date))
				.Returns(_availableTickets);

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
				.Callback<TicketBooking>(booking =>
				{
					savedBooking = booking;
				});


			_handler.BookService(_request);

			_ticketBookingServiceMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once());

			savedBooking.ShouldNotBeNull();
			savedBooking.Name.ShouldBe(_request.Name);
			savedBooking.Family.ShouldBe(_request.Family);
			savedBooking.Email.ShouldBe(_request.Email);
			savedBooking.TicketId.ShouldBe(_availableTickets.First().Id);
		}

		[Fact]
		public void ShouldNotSaveTicketBookingRequestIfNoneAvailable()
		{
			_availableTickets.Clear();

			_handler.BookService(_request);

			_ticketBookingServiceMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Never());
		}

		[Theory]
		[InlineData(BookingResultFlag.Failure, false)]
		[InlineData(BookingResultFlag.Success, true)]
		public void ShouldReturnSuccessOrFailureFlagInResult(BookingResultFlag bookingSuccessFlag, bool isAvailable)
		{
			if (!isAvailable)
			{
				_availableTickets.Clear();
			}

			var result = _handler.BookService(_request);
			bookingSuccessFlag.ShouldBe(result.Flag);
		}

		[Theory]
		[InlineData(1, true)]
		[InlineData(null, false)]
		public void ShouldReturnTicketBookingInResult(int? ticketBookingId, bool isAvailable)
		{
			if (!isAvailable)
			{
				_availableTickets.Clear();
			}
			else
			{
				_ticketBookingServiceMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
					.Callback<TicketBooking>(booking =>
					{
						TicketBooking.Id = ticketBookingId.Value;
					});
			}

			var result = _handler.BookService(_request);
			result.TicketBookingId.ShouldBe(ticketBookingId);
		}
	}
}