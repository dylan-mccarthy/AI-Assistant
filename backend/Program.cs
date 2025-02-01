using AI_Assistant.Backend.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Register the LLM service
builder.Services.AddSingleton<LLMService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Removed weather API endpoint

// New endpoint to handle AI Assistant requests
app.MapPost("/assistant", async (HttpContext context, LLMService llmService) =>
{
    var logger = app.Logger;
    using var reader = new StreamReader(context.Request.Body);
    var input = await reader.ReadToEndAsync();
    logger.LogInformation("Received assistant request with input: {Input}", input);
    
    // Use top-level "LLMProvider" from configuration.
    var provider = app.Configuration["LLMProvider"]?.ToLowerInvariant();
    logger.LogInformation("Using LLM provider: {Provider}", provider);
    
    string llmResponse = provider switch
    {
        "openai" or "claude" => await llmService.HandleRemoteLLM(provider, input),
        "ollama"            => await llmService.HandleLocalLLM(input),
        _                   => "LLM provider not supported."
    };
    
    logger.LogInformation("Returning response: {Response}", llmResponse);
    return Results.Ok(new { response = llmResponse });
});

app.Run();

