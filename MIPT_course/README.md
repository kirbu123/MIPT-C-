# MIPT_course HW3

## Launch guide

Run all commands from the `hw3` branch.

Prerequisite:
- `.NET SDK 10.0` or newer installed (`dotnet --version`).

1. Open repository root:
   - `cd /home/bunin-kirill/Desktop/BOTAY/MIPT-C#`
2. Enter homework project directory:
   - `cd MIPT_course`
3. Build project:
   - `dotnet build`
4. Run project:
   - `dotnet run`
5. Optional full clean rebuild:
   - `dotnet clean && dotnet build && dotnet run`

## What is verified by run

- Generic `Repository<T>` works for both `Product` and `User`.
- `Add`, `GetById`, `GetAll`, `Count`, `Find`, and `Remove` are demonstrated.
- Duplicate add throws and is handled as `InvalidOperationException`.
- `CollectionUtils.Distinct` is tested on `List<int>` and `List<string>`.
- `CollectionUtils.GroupBy` groups words by length.
- `CollectionUtils.Merge` merges two dictionaries with conflict resolver.
- `CollectionUtils.MaxBy` returns max product by price and throws on empty list.

## Expected result

- `dotnet build` completes with 0 errors.
- `dotnet run` prints successful output for all scenarios listed above.
