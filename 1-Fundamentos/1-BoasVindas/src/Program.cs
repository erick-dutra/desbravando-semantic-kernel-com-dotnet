using DesbravandoSKComDotNet.Ollama;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;

namespace BoasVindas
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
            var prompt = "Você pode me responder com uma mensagem simples de boas-vindas?";

            Console.WriteLine($"Usuário: {prompt}");

            var resultado = await kernel.InvokePromptAsync(prompt);

            Console.WriteLine($"IA: {resultado.GetValue<string>()}");
            Console.ReadLine();
        }
    }
}