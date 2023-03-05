# Smusdi.RegexTools

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

> For now, there is no add schematic to do this.
> It is planned but not done.

- package installation
    ```
    npm install --save ngssm-regex-tools
    ```
- update *angular.json* to import package assets
    ```json
    "assets": [
        {
        "glob": "**/*",
        "input": "node_modules/ngssm-toolkit/assets",
        "output": "./assets/"
        },
        ...
    ],
    ```

- update *index.html* to load **.NET** assemblies
    ```html
    <head>
        ...
        <script type="module" src="./assets/smusdi/smusdi.regextools.js"></script>
    </head>
    ```

- update *styles.scss* to import package styles
    ```scss
    @import "ngssm-regex-tools/styles/ngssm-regex-tools.scss";
    ```
