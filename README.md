# ErikForwerk.MissingLinq

[![Current Repository Status](https://github.com/3rikF/missing-linq/actions/workflows/dotnet_build-test-pack-publish.yml/badge.svg)](https://github.com/3rikF/missing-linq/actions/workflows/dotnet_build-test-pack-publish.yml)
[![Codecov Test Coverage](https://codecov.io/gh/3rikF/missing-linq/graph/badge.svg?token=9IW6FHVRFF)](https://codecov.io/gh/3rikF/missing-linq)
[![NuGet](https://img.shields.io/nuget/v/ErikForwerk.MissingLinq?label=NuGet%20MissingLinq)](https://www.nuget.org/packages/ErikForwerk.MissingLinq/)
[![wakatime](https://wakatime.com/badge/user/ccce5eac-49f0-481f-998c-1183a3cd0b18/project/6290fc86-d37d-4303-87ea-499efdba7fb9.svg)](https://wakatime.com/badge/user/ccce5eac-49f0-481f-998c-1183a3cd0b18/project/6290fc86-d37d-4303-87ea-499efdba7fb9)

---

## What is MissingLinq?

**MissingLinq** is a lightweight .NET library that fills the gaps in LINQ by providing commonly needed extension methods that are not part of the standard `System.Linq` namespace.

The standard LINQ API covers a broad set of sequence operations, but experienced developers frequently find themselves writing the same small utility methods over and over again across projects. MissingLinq collects these recurring patterns into a single, well-tested, and NuGet-distributed library so you never have to reinvent them again.

---

## Goals

- **Complement LINQ** — not replace it. Every method in this library feels like a natural addition to what `System.Linq` already offers.
- **Zero friction** — drop the NuGet package in and the extension methods are immediately available on `IEnumerable<T>`, arrays, and other collection types.
- **Correctness first** — every method ships with a full suite of unit tests and benchmarks to ensure both behaviour and performance are well understood.
- **Modern .NET** — the library targets .NET 10 and embraces current language features such as nullable reference types, collection expressions, and tuple return types.

---

## Installation

```shell
dotnet add package ErikForwerk.MissingLinq
```

---

## Available Extensions

### `Split`

Splits a sequence into two groups in a single pass, based on a predicate — something LINQ's `Where` cannot do without enumerating the source twice.

| Overload | Description |
|---|---|
| `TItem[].Split(predicate, sortInPlace)` | Partitions an array into `(TrueItems, FalseItems)`. Supports an in-place mode to avoid an extra allocation. |
| `IEnumerable<TItem>.Split(predicate)` | Returns two lazily-evaluated sequences from any enumerable source. |

```csharp
int[] numbers = [1, 2, 3, 4, 5];

var (evens, odds) = numbers.Split(n => n % 2 == 0);
// evens → [2, 4]
// odds  → [1, 3, 5]
```

---

### `IsNullOrEmpty`

A null-safe emptiness check that mirrors the familiar `string.IsNullOrEmpty` pattern for collections.

| Overload | Description |
|---|---|
| `T[]?.IsNullOrEmpty()` | Returns `true` if the array is `null` or has a `Length` of 0. |
| `IList<T>?.IsNullOrEmpty()` | Returns `true` if the list is `null` or has a `Count` of 0. |
| `IEnumerable<T>?.IsNullOrEmpty()` | Returns `true` if the enumerable is `null` or contains no elements. |

```csharp
int[]? empty = null;
empty.IsNullOrEmpty(); // true

List<string> names = [];
names.IsNullOrEmpty(); // true
```

---

## Project Structure

```
ErikForwerk.MissingLinq/
├── Source/
│   └── ErikForwerk.MissingLinq/             # Library source
├── UnitTests/
│   └── ErikForwerk.MissingLinq.Tests/       # xUnit test suite
└── Benchmarks/
    └── ErikForwerk.MissingLinq.Benchmark/   # BenchmarkDotNet benchmarks
```

---

## Contributing

Contributions are welcome! If you regularly find yourself writing a LINQ-like helper that is absent from the standard library, feel free to open an issue or submit a pull request.

Please ensure all new methods are accompanied by:
- XML documentation comments
- Unit tests (using xUnit)
- A benchmark (using BenchmarkDotNet)

---

## License

This project is licensed under the [MIT License](LICENSE).
