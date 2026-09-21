# Benchmark Results

## Overview

This benchmark compares the performance of two compiler implementations under the same runtime and workload conditions.

## Test Environment

- BenchmarkDotNet: v0.15.8
- Operating System: Linux Ubuntu 26.04.1 LTS (Resolute Raccoon)
- CPU: Intel Core i7-8700 CPU 3.20GHz (Max: 3.19GHz), Coffee Lake
- CPU topology: 1 CPU, 12 logical cores, 6 physical cores
- .NET SDK: 10.0.112
- Runtime: .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3
- Host: .NET 10.0.12, X64 RyuJIT x86-64-v3
- Job: DefaultJob

## Summary

The second compiler version shows a significant performance improvement over the first version in this environment.

| Method | Mean | Error | StdDev |
|---|---:|---:|---:|
| Compiler_V1 | 30.856 s | 0.2623 s | 0.2325 s |
| Compiler_V2 | 5.308 s | 0.0479 s | 0.0448 s |

## Relative Improvement

- Compiler_V2 is approximately 5.8x faster than Compiler_V1.
- Runtime reduction: about 82.8% lower execution time.

## Notes for Future Comparisons

This benchmark was executed on a single machine configuration. To keep later measurements comparable across different hardware, the following structure should be preserved for future reports:

1. Platform and OS details
2. CPU model and topology
3. .NET version and JIT settings
4. BenchmarkDotNet version
5. Same benchmark workload and execution settings
6. Result table with mean/error/stddev

Future test runs on other machines should be added in separate sections with a machine name and hardware summary, so results can be compared reliably over time and across systems.

## Machine Snapshot

- Machine name: Ubuntu 26.04.1 LTS
- CPU: Intel Core i7-8700
- Benchmark date: 2026-09-21
- Configuration: Single-machine baseline run
