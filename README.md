Tech Task solution is in the folder Broadridge
The project also contains the benchmark tests and unit tests.
The Benchmark tests is a list of beatles' song, it is too short to be meaningful but I have not had much time available to test "Moby Dick".

I evaluated different options using concurrent dictionary, caching is not the best, use simple addorupdate works, tryAdd and tryUpdate is faster.

I based my choice on 
1. Reliability and consistency of the results in test
2. the following benchmarks tests
| Method                     | Mean     | Error     | StdDev    | Median   |
|--------------------------- |---------:|----------:|----------:|---------:|
| UsedBenchmark              | 4.690 us | 0.0901 us | 0.2140 us | 4.678 us |
| LocalDictionariesBenchmark | 4.186 us | 0.0837 us | 0.1924 us | 4.164 us |
| WithNoCachingBenchmark     | 3.747 us | 0.0733 us | 0.1163 us | 3.776 us |
| WithTryAdd                 | 2.988 us | 0.0715 us | 0.1956 us | 2.925 us |
| WithCachingBenchmark       | 3.190 us | 0.0793 us | 0.2261 us | 3.122 us |

