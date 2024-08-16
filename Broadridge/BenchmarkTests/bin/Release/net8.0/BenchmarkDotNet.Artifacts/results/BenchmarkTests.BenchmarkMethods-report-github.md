```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]     : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI [AttachedDebugger]
  DefaultJob : .NET 8.0.5 (8.0.524.21615), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                     | Mean     | Error     | StdDev    | Median   |
|--------------------------- |---------:|----------:|----------:|---------:|
| UsedBenchmark              | 4.690 μs | 0.0901 μs | 0.2140 μs | 4.678 μs |
| LocalDictionariesBenchmark | 4.186 μs | 0.0837 μs | 0.1924 μs | 4.164 μs |
| WithNoCachingBenchmark     | 3.747 μs | 0.0733 μs | 0.1163 μs | 3.776 μs |
| WithTryAdd                 | 2.988 μs | 0.0715 μs | 0.1956 μs | 2.925 μs |
| WithCachingBenchmark       | 3.190 μs | 0.0793 μs | 0.2261 μs | 3.122 μs |
