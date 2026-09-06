using System.Net;
using NUnit.Framework;
using QA.Automation.Exercise.Api;
using QA.Automation.Exercise.Models;
using QA.Automation.Exercise.Support;

namespace QA.Automation.Exercise.Tests.Api;

[TestFixture]
[Category("API")]
public sealed class BookingApiTests
{
    private BookingApiClient _api = null!;

    [SetUp]
    public void SetUp() => _api = new BookingApiClient(TestSettings.ApiBaseUrl);

    [TearDown]
    public void TearDown() => _api.Dispose();

    [Test]
    public async Task CreateBooking_ThenRetrieveIt_ReturnsPersistedBookingDetails()
    {
        var booking = new Booking(
            FirstName: ($"QA-{Guid.NewGuid():N}")[..12],
            LastName: "Automation",
            TotalPrice: 250,
            DepositPaid: true,
            BookingDates: new BookingDates("2026-10-10", "2026-10-12"),
            AdditionalNeeds: "Breakfast");

        var createResult = await _api.CreateBookingAsync(booking);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(createResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(createResult.Body, Is.Not.Null);
            Assert.That(createResult.Body!.BookingId, Is.GreaterThan(0));
            Assert.That(createResult.Body.Booking, Is.EqualTo(booking));
        };

        var getResult = await _api.GetBookingAsync(createResult.Body!.BookingId);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(getResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(getResult.Body, Is.EqualTo(booking));
        };
    }

    [Test]
    public async Task GetBooking_WithNonExistentId_ReturnsNotFound()
    {
        const int nonExistentBookingId = 999999999;

        var result = await _api.GetBookingAsync(nonExistentBookingId);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(result.Body, Is.Null);
        };
    }
}
