```

BenchmarkDotNet v0.15.8, Linux Ubuntu 26.04.1 LTS (Resolute Raccoon)
Intel Core i7-3770 CPU 3.40GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.112
  [Host]     : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v2
  DefaultJob : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v2


```
| Method      | Mean     | Error    | StdDev   |
|------------ |---------:|---------:|---------:|
| Compiler_V1 | 41.173 s | 0.3848 s | 0.3600 s |
| Compiler_V2 |  6.874 s | 0.1160 s | 0.1028 s |
