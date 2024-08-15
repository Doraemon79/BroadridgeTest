﻿using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using BenchmarkTests;

internal class Program
{
    private static void Main(string[] args)
    {
        var config = new ManualConfig()
  .WithOptions(ConfigOptions.DisableOptimizationsValidator)
  .AddValidator(JitOptimizationsValidator.DontFailOnError)
  .AddLogger(ConsoleLogger.Default)
  .AddColumnProvider(DefaultColumnProviders.Instance);

        var summary = BenchmarkRunner.Run<BenchmarkMethods>();
    }
}