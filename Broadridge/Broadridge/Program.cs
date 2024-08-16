using Broadridge.Logic;
using Broadridge.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Please provide a file path as an argument.");
        string path = Console.ReadLine();


        if (!File.Exists(path))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        try
        {

            var host = CreateHostBuilder(args).Build();
            var FrequecyCalculatorService = host.Services.GetRequiredService<IFrequencyCalculator>();
            var HelperService = host.Services.GetRequiredService<IHelper>();
            string text = File.ReadAllText(path);
            var words = HelperService.SplitWords(text);

            var WordsByFrequency = FrequecyCalculatorService.WordFrequencyCalculator(words);
            HelperService.FlushOutput(WordsByFrequency);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
.ConfigureServices((hostContext, services) =>
{
    services.AddScoped<IFrequencyCalculator, FrequencyCalculator>();
    services.AddScoped<IHelper, Helper>();
});
}