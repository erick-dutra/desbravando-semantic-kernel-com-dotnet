using DesbravandoSKComDotNet.Ollama;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;

namespace ChatBot
{
    // Suppress the diagnostic warning SKEXP0070 as the method is for evaluation purposes only.
    // This suppression ensures the code can proceed without modification to the method itself.
    #pragma warning disable SKEXP0070
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = Kernel.CreateBuilder();

            builder.Services.AddScoped<IOllamaApiClient>(_ => new OllamaApiClient("http://localhost:11434"));
            builder.Services.AddScoped<IChatCompletionService, OllamaChatCompletionService>();

            var kernel = builder.Build();
            var chatService = kernel.GetRequiredService<IChatCompletionService>();
            var history = new ChatHistory();
            
            history.AddSystemMessage("Você é um assistente completo que me ajudará com minhas dúvidas.");

            while (true)
            {
                Console.Write("Você: ");
                var userMessage = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userMessage))
                {
                    break;
                }

                history.AddUserMessage(userMessage);

                var response = await chatService.GetChatMessageContentAsync(history);

                Console.WriteLine($"Robô: {response.Content}");

                history.AddMessage(response.Role, response.Content ?? string.Empty);
            }
        }

    }
}