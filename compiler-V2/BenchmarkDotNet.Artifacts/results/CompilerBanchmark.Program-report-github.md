```

BenchmarkDotNet v0.15.8, Linux Ubuntu 26.04.1 LTS (Resolute Raccoon)
Intel Core i7-3770 CPU 3.40GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.112
  [Host]     : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v2
  DefaultJob : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v2


```
| Method      | Mean     | Error    | StdDev   |
|------------ |---------:|---------:|---------:|
| Compiler_V1 | 42.510 s | 0.4347 s | 0.3630 s |
| Compiler_V2 |  7.290 s | 0.0719 s | 0.0638 s |
