using System.Text.Json.Serialization;

namespace QA.Automation.Exercise.Models;

public sealed record BookingDates(
    [property: JsonPropertyName("checkin")] string CheckIn,
    [property: JsonPropertyName("checkout")] string CheckOut);
