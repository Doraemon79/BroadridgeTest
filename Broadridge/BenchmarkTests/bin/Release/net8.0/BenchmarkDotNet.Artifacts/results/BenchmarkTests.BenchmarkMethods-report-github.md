```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                     | Mean      | Error     | StdDev    | Median    |
|--------------------------- |----------:|----------:|----------:|----------:|
| LocalDictionariesBenchmark | 19.784 μs | 0.7012 μs | 1.9893 μs | 19.167 μs |
| WithNoCachingBenchmark     |  3.529 μs | 0.0371 μs | 0.0290 μs |  3.529 μs |
| WithTryAdd                 |  2.847 μs | 0.0534 μs | 0.1053 μs |  2.831 μs |
| WithCachingBenchmark       |  2.859 μs | 0.0532 μs | 0.0547 μs |  2.869 μs |
