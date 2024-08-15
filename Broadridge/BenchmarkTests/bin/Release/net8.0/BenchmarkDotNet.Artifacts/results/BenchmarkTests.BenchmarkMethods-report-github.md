```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method               | Mean     | Error     | StdDev    |
|--------------------- |---------:|----------:|----------:|
| WithCachingBenchmark | 7.247 ms | 0.1432 ms | 0.2229 ms |
| NoCachingBenchmark   | 6.498 ms | 0.3346 ms | 0.9653 ms |
