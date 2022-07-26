using Azure.Communication.CallingServer;
using Azure.Communication.Identity;
using Azure.Communication.PhoneNumbers;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;
using CallingDashboard.Interfaces;
using CallingDashboard.Models;
using CallingDashboard.Repositories;
using CallingDashboard.Services;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler;
using Microsoft.AspNetCore.Mvc;
using MudBlazor.Services;
using System.Text.Json;
using Azure.Communication.JobRouter;
using JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;
using JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddEventHandlerServices()
    .AddCallAutomationEventHandling()
    .AddJobRouterEventHandling();

// ACS SDKs
builder.Services.AddSingleton(new CallAutomationClient(builder.Configuration["ACS:ConnectionString"]));
builder.Services.AddSingleton(new CommunicationIdentityClient(builder.Configuration["ACS:ConnectionString"]));
builder.Services.AddSingleton(new PhoneNumbersClient(builder.Configuration["ACS:ConnectionString"]));
builder.Services.AddSingleton(new RouterAdministrationClient(builder.Configuration["ACS:ConnectionString"]));
builder.Services.AddSingleton(new RouterClient(builder.Configuration["ACS:ConnectionString"]));

builder.Services.AddSingleton<IRepository<CallData>, InMemoryRepository<CallData>>();
builder.Services.AddSingleton<IRepository<EventLogData>, InMemoryRepository<EventLogData>>();
builder.Services.AddSingleton<IRepository<MediaData>, InMemoryRepository<MediaData>>();
builder.Services.AddSingleton<IApplicationSettingsService, ApplicationSettingsService>();

builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddSingleton<IMediaDataService, MediaDataService>();

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
    [FromServices] IEventPublisher<Calling> publisher) =>
{
    foreach (EventGridEvent eventGridEvent in eventGridEvents)
    {
        // Handle system events
        if (eventGridEvent.TryGetSystemEventData(out object eventData))
        {
            // Handle the subscription validation event
            if (eventData is SubscriptionValidationEventData subscriptionValidationEventData)
            {
                var responseData = new SubscriptionValidationResponse
                {
                    ValidationResponse = subscriptionValidationEventData.ValidationCode
                };
                return Results.Ok(responseData);
            }
        }

        publisher.Publish(eventGridEvent);
    }

    return Results.Ok();
}).Produces(StatusCodes.Status200OK);

app.MapPost("/api/jobRouter", (
    [FromBody] EventGridEvent[] eventGridEvents,
    [FromServices] IEventPublisher<Router> publisher) =>
{
    foreach (EventGridEvent eventGridEvent in eventGridEvents)
    {
        publisher.Publish(eventGridEvent);
    }
}).Produces(StatusCodes.Status200OK);

app.MapPost("/api/calls/{contextId}", async (
    HttpRequest request,
    [FromServices] ILogger<Program> logger,
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
        logger.LogInformation($"Publishing {@event.Type}");
        publisher.Publish(@event, contextId);
    }

    return Results.Ok();
}).Produces(StatusCodes.Status200OK);

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
