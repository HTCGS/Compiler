```

BenchmarkDotNet v0.15.8, Linux Ubuntu 26.04.1 LTS (Resolute Raccoon)
Intel Core i7-8700 CPU 3.20GHz (Max: 3.19GHz) (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.112
  [Host]     : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3


```
| Method      | Mean      | Error     | StdDev    |
|------------ |----------:|----------:|----------:|
| Compiler_V1 | 26.395 ms | 0.3853 ms | 0.3604 ms |
| Compiler_V2 |  2.101 ms | 0.0259 ms | 0.0242 ms |
