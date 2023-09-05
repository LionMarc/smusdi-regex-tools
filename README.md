# Smusdi.RegexTools

[![license](https://img.shields.io/badge/License-MIT-purple.svg)](LICENSE)
![build workflow](https://github.com/LionMarc/smusdi-regex-tools/actions/workflows/build.yml/badge.svg)
[![NuGet stable version](https://badgen.net/nuget/v/Smusdi.RegexTools)](https://nuget.org/packages/Smusdi.RegexTools)

Some C# utilities to process regular expressions.

The main purpose is to allow compiling the code in wasm to be used in an *angular* application as well as in a *C#* service.

To build the wasm:

```
 dotnet publish src/Smusdi.Wasm/Smusdi.Wasm.csproj --configuration Release
```

> to build with multithread:
>  - set to true the *WasmEnableThreads* property in *Smusdi.Wasm*
>  - export a method that returns a *Task*
 
## Installation of the angular package ngssm-regex-tools

The angular package has been moved to https://github.com/LionMarc/ngssm-regex-tools.
