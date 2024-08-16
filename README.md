Tech Task solution is in the folder Broadridge
The project also contains the benchmark tests and unit tests.
The Benchmark tests is a list of beatles' song, it is too short to be meaningful but I have not had much time available to test "Moby Dick".

I evaluated different options using concurrent dictionary, caching is not the best, use simple addorupdate works, tryAdd and tryUpdate is faster.

I based my choice on 
1. Reliability and consistency of the results in test
2. the following benchmarks tests
| Method                     | Mean     | Error     | StdDev    |
|--------------------------- |---------:|----------:|----------:|
| UsedBenchmark              | 4.679 us | 0.0927 us | 0.2053 us |
| LocalDictionariesBenchmark | 8.984 us | 0.1781 us | 0.3515 us |
| WithNoCachingBenchmark     | 3.710 us | 0.0737 us | 0.1125 us |
| WithTryAdd                 | 2.885 us | 0.0568 us | 0.0739 us |
| WithCachingBenchmark       | 2.806 us | 0.0523 us | 0.0537 us |

