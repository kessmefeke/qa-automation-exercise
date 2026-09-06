using System.Text.Json.Serialization;

namespace QA.Automation.Exercise.Models;

public sealed record Booking(
    [property: JsonPropertyName("firstname")] string FirstName,
    [property: JsonPropertyName("lastname")] string LastName,
    [property: JsonPropertyName("totalprice")] int TotalPrice,
    [property: JsonPropertyName("depositpaid")] bool DepositPaid,
    [property: JsonPropertyName("bookingdates")] BookingDates BookingDates,
    [property: JsonPropertyName("additionalneeds")] string AdditionalNeeds);
