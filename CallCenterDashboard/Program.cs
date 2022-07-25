using System.Text.Json;
using Azure.Communication.CallingServer;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;
using CallCenterDashboard.Data;
using JasonShave.Azure.Communication.Service.EventHandler;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using Microsoft.AspNetCore.Mvc;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddEventHandlerServices(option => option.PropertyNameCaseInsensitive = true)
    .AddCallingServerEventHandling();

builder.Services.AddSingleton(new CallingServerClient(builder.Configuration["ACS:ConnectionString"]));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapPost("/api/incomingCall", (
    [FromBody] EventGridEvent[] eventGridEvents,
    [FromServices] IEventPublisher<Calling> publisher,
    [FromServices] CallingServerClient callingServerClient) =>
{
    foreach (EventGridEvent eventGridEvent in eventGridEvents)
    {
        // Handle system events
        if (eventGridEvent.TryGetSystemEventData(out object eventData))
        {
            // Handle the subscription validation event
            if (eventData is SubscriptionValidationEventData subscriptionValidationEventData)
            {
                var responseData = new SubscriptionValidationResponse()
                {
                    ValidationResponse = subscriptionValidationEventData.ValidationCode
                };
                return Results.Ok(responseData);
            }
        }

        publisher.Publish(eventGridEvent.Data.ToString(), eventGridEvent.EventType);
    }

    return Results.Ok();
}).Produces(StatusCodes.Status200OK);

app.MapPost("/api/calls/{contextId}", async (
    HttpRequest request,
    [FromRoute] string contextId,
    [FromServices] IEventPublisher<Calling> publisher) =>
{
    // get body from HttpRequest
    string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

    if (string.IsNullOrEmpty(requestBody)) return Results.Ok();

    // deserialize into an array of CloudEvent types
    CloudEvent[] cloudEvents = JsonSerializer.Deserialize<CloudEvent[]>(requestBody);

    foreach (var @event in cloudEvents)
    {
        publisher.Publish(@event.Data.ToString(), @event.Type, contextId);
    }

    return Results.Ok();
}).Produces(StatusCodes.Status200OK);

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
