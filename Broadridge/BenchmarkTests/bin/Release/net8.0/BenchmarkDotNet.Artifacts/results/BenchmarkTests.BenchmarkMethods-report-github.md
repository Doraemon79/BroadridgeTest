```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                     | Mean     | Error     | StdDev    |
|--------------------------- |---------:|----------:|----------:|
| UsedBenchmark              | 4.679 μs | 0.0927 μs | 0.2053 μs |
| LocalDictionariesBenchmark | 8.984 μs | 0.1781 μs | 0.3515 μs |
| WithNoCachingBenchmark     | 3.710 μs | 0.0737 μs | 0.1125 μs |
| WithTryAdd                 | 2.885 μs | 0.0568 μs | 0.0739 μs |
| WithCachingBenchmark       | 2.806 μs | 0.0523 μs | 0.0537 μs |
