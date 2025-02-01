using Microsoft.Extensions.Logging;

namespace AI_Assistant.Backend.Services
{
    public class LLMService
    {
        private readonly ILogger<LLMService> _logger;
        public LLMService(ILogger<LLMService> logger)
        {
            _logger = logger;
        }
        
        public async Task<string> HandleRemoteLLM(string provider, string input)
        {
            _logger.LogInformation("Calling remote LLM ({Provider}) for input: {Input}", provider, input);
            await Task.Delay(10); // Simulate remote API call
            var result = $"Remote {provider} response for input: {input}";
            _logger.LogInformation("Remote LLM response: {Result}", result);
            return result;
        }
    
        public async Task<string> HandleLocalLLM(string input)
        {
            _logger.LogInformation("Calling local LLM for input: {Input}", input);
            await Task.Delay(10); // Simulate local API call
            var result = $"Local response for input: {input}";
            _logger.LogInformation("Local LLM response: {Result}", result);
            return result;
        }
    }
}
