# Smusdi.RegexTools

Some C# utilities to process regular expressions.

The main purpose is to allow compiling the code in wasm to be used in an *angular* application as well as in a *C#* service.

To build the wasm:

```
 dotnet publish src/Smusdi.Wasm/Smusdi.Wasm.csproj --configuration Release
```