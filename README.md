# ROOMS
 
### A small experiment using [mono-wasm](https://www.mono-project.com/news/2018/01/16/mono-static-webassembly-compilation/ "mono-wasm") and [WebGLDotNET](https://github.com/WaveEngine/WebGL.NET "WebGLDotNET").


As per [Run C# natively in browser](https://itnext.io/run-c-natively-in-the-browser-through-the-web-assembly-via-mono-wasm-60f3d55dd05a "Run C# natively in browser") tutorial by [Ali Bahraminezhad](https://github.com/0x414c49):



* Download and install Mono SDK for your OS.

* After installation make sure you can access both `mcs` and `mono` directly in your terminal.

* Get the latest successful build of mono wasm [here](https://jenkins.mono-project.com/job/test-mono-mainline-wasm/label=ubuntu-1804-amd64/lastSuccessfulBuild/Azure/). Download and extract the .zip file designated with `sdks/wasm/mono-wasm-###########.zip`.

* Define a variable called $WASM_SDK. For example: `$WASM_SDK="/path/to_wasm_sdk/"`. Dont forget the trailing slash.

* Download [Ali's](https://github.com/0x414c49) [example](https://github.com/0x414c49/mono-wasm-example) to get started.

* Run the following command: `mcs /target:library -out:Example.dll /noconfig /nostdlib /r:$WASM_SDK/wasm-bcl/wasm/mscorlib.dll /r:$WASM_SDK/wasm-bcl/wasm/System.dll /r:$WASM_SDK/wasm-bcl/wasm/System.Core.dll /r:$WASM_SDK/wasm-bcl/wasm/Facades/netstandard.dll /r:$WASM_SDK/wasm-bcl/wasm/System.Net.Http.dll /r:$WASM_SDK/framework/WebAssembly.Bindings.dll /r:$WASM_SDK/framework/WebAssembly.Bindings.dll Example.cs` in the example folder. Add any other binaries before `Example.cs`, for example add `r:/WebGLDotNET.dll` before `Example.cs`. The `-out:` defines output of your assembly. Add any other C# files at the end of the command.

* If compiles successfully, `Example.dll` binary should be created.

* Publish the binary for web-assembly with `mono $WASM_SDK/packager.exe --copy=always --out=./publish --asset=./index.html Example.dll`. This command runs `packager.exe`, a tool from Mono that publishes ready to serve web-assembly files. Add any other JS files with a `--asset=` prefix for it to be included in the published folder.

* Serve published folder as any other static/wasm webpage. 


