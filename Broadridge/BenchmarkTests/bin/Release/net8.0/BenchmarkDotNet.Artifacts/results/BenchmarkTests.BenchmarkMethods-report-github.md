```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                 | Mean     | Error     | StdDev    |
|----------------------- |---------:|----------:|----------:|
| WithNoCachingBenchmark | 4.238 μs | 0.0981 μs | 0.2815 μs |
| WithTryAdd             | 2.938 μs | 0.0577 μs | 0.0965 μs |
| WithCachingBenchmark   | 2.965 μs | 0.0583 μs | 0.0648 μs |
