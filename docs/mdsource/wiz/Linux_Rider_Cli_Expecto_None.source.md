# Getting Started Wizard

[Home](/docs/wiz/readme.md) > [Linux](Linux.md) > [JetBrains Rider](Linux_Rider.md) > [Prefer CLI](Linux_Rider_Cli.md) > [Expecto](Linux_Rider_Cli_Expecto.md) > No build server

## Add NuGet packages

Add the following packages to the test project:

```
dotnet add package YoloDev.Expecto.TestSdk
dotnet add package Expecto
dotnet add package Verify.Expecto
```

## Implicit Usings

include: implicit-usings


## Conventions


### Source Control Includes/Excludes

include: include-exclude


### Text file settings

include: text-file-settings


### Conventions check

Conventions can be checked by calling `VerifyChecks.Run()` in a test

snippet: VerifyChecksExpecto


## Rider Plugin

Install the [Rider Plugin](https://plugins.jetbrains.com/plugin/17240-verify-support)

Provides a mechanism for contextually accepting or rejecting snapshot changes inside the Rider test runner.

This is optional, but recommended.

include: rider-resharper-orphaned-process


## Treat "return value of pure method is not used" as error

include: pure
## DiffPlex

The text comparison behavior of Verify is pluggable. The default behaviour, on failure, is to output both the received
and the verified contents as part of the exception. This can be noisy when verifying large strings.

[Verify.DiffPlex](https://github.com/VerifyTests/Verify.DiffPlex) changes the text compare result to highlighting text differences inline.

This is optional, but recommended.

### Add the NuGet

```
dotnet add package Verify.DiffPlex
```

### Enable

```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifyDiffPlex.Initialize();
```

## Verify.Terminal

[Verify.Terminal](https://github.com/VerifyTests/Verify.Terminal) is a dotnet tool for managing snapshots from the command line.

This is optional.

### Install the tool

```
dotnet tool install -g verify.tool
```


## Sample Test

snippet: SampleTestExpecto

## Diff Tool

Verify supports many [Diff Tools](https://github.com/VerifyTests/DiffEngine/blob/main/docs/diff-tool.md#supported-tools) for comparing received to verified.
While IDEs are supported, due to their MDI nature, using a different Diff Tool is recommended.

Tools supported by Linux:

 * [BeyondCompare](https://www.scootersoftware.com)
 * [P4Merge](https://www.perforce.com/products/helix-core-apps/merge-diff-tool-p4merge)
 * [Rider](https://www.jetbrains.com/rider/)
 * [Neovim](https://neovim.io/)

