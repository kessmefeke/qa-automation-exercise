using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using QA.Automation.Exercise.Models;

namespace QA.Automation.Exercise.Api;

public sealed class BookingApiClient : IDisposable
{
    private readonly HttpClient _client;
    private readonly bool _disposeClient;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public BookingApiClient(string baseUrl, HttpClient? client = null)
    {
        _disposeClient = client is null;
        _client = client ?? new HttpClient();
        _client.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");
        _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
    }

    public async Task<(HttpStatusCode StatusCode, CreateBookingResponse? Body)> CreateBookingAsync(Booking booking)
    {
        using var response = await _client.PostAsJsonAsync("booking", booking, JsonOptions);
        var body = await response.Content.ReadFromJsonAsync<CreateBookingResponse>(JsonOptions);
        return (response.StatusCode, body);
    }

    public async Task<(HttpStatusCode StatusCode, Booking? Body)> GetBookingAsync(int bookingId)
    {
        using var response = await _client.GetAsync($"booking/{bookingId}");

        if (!response.IsSuccessStatusCode)
            return (response.StatusCode, null);

        var body = await response.Content.ReadFromJsonAsync<Booking>(JsonOptions);
        return (response.StatusCode, body);
    }

    public void Dispose()
    {
        if (_disposeClient)
            _client.Dispose();
    }
}
