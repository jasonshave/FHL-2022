using Azure.Communication.CallingServer;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;
using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using CallCenterDashboard.Repositories;
using CallCenterDashboard.Services;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using Microsoft.AspNetCore.Mvc;
using MudBlazor.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddEventHandlerServices(option => option.PropertyNameCaseInsensitive = true)
    .AddCallingServerEventHandling();
builder.Services.AddSingleton(new CallingServerClient(builder.Configuration["ACS:ConnectionString"]));
builder.Services.AddSingleton<IRepository<CallData>>(new InMemoryRepository<CallData>(new Dictionary<string, CallData>
{
    {"effc9413-e8ef-4e51-b51b-497e63a8bbd7", new CallData("4:+14255551212", "8:acs:eba32226-8a75-47dc-afa3-cbbe8e84bc95_00000012-e271-a97e-3ef0-8b3a0d005038", DateTimeOffset.UtcNow, "a582fed2-71a4-4eb8-b59c-769a8bf86db2", "effc9413-e8ef-4e51-b51b-497e63a8bbd7")}
}));
builder.Services.AddHostedService<CallingEventSubscriber>();

builder.Services.AddFluxor(x => x.ScanAssemblies(typeof(Program).Assembly));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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
