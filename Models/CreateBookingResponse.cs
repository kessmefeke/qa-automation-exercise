using System.Text.Json.Serialization;

namespace QA.Automation.Exercise.Models;

public sealed record CreateBookingResponse(
    [property: JsonPropertyName("bookingid")] int BookingId,
    [property: JsonPropertyName("booking")] Booking Booking);
