# ROOMS
 
## A small experiment using [mono-wasm](https://www.mono-project.com/news/2018/01/16/mono-static-webassembly-compilation/ "mono-wasm") and [WebGLDotNET](https://github.com/WaveEngine/WebGL.NET "WebGLDotNET").

### Resources used:

* [Run C# natively in browser](https://itnext.io/run-c-natively-in-the-browser-through-the-web-assembly-via-mono-wasm-60f3d55dd05a) guide by [Ali Bahraminezhad](https://github.com/0x414c49).

* [Ali's](https://github.com/0x414c49) basic mono-wasm [example](https://github.com/0x414c49/mono-wasm-example).

* Using `WebGLDotNET`: [Your first WebGL.NET app](https://geeks.ms/xamarinteam/2019/08/28/your-first-webgldotnet-app/) guide.

* OpenGL with C#: [Neo Kabuto's](https://neokabuto.blogspot.com/) [OpenTK tutorial](https://neokabuto.blogspot.com/p/tutorials.html).

***

### To compile:

Following [Run C# natively in browser](https://itnext.io/run-c-natively-in-the-browser-through-the-web-assembly-via-mono-wasm-60f3d55dd05a "Run C# natively in browser") tutorial by [Ali Bahraminezhad](https://github.com/0x414c49):

* Download and install Mono SDK for your OS.

* After installation make sure you can access both `mcs` and `mono` directly in your terminal.

* Get the latest successful build of mono wasm [here](https://jenkins.mono-project.com/job/test-mono-mainline-wasm/label=ubuntu-1804-amd64/lastSuccessfulBuild/Azure/). Download and extract the .zip file designated with `sdks/wasm/mono-wasm-###########.zip`.

* Define a variable called `$WASM_SDK`. For example: `$WASM_SDK="/path/to_wasm_sdk/"`. Dont forget the trailing slash.

* Run the following command: `mcs /target:library -out:rooms.dll /noconfig /nostdlib /r:$WASM_SDK/wasm-bcl/wasm/mscorlib.dll /r:$WASM_SDK/wasm-bcl/wasm/System.dll /r:$WASM_SDK/wasm-bcl/wasm/System.Core.dll /r:$WASM_SDK/wasm-bcl/wasm/Facades/netstandard.dll /r:$WASM_SDK/wasm-bcl/wasm/System.Net.Http.dll /r:$WASM_SDK/framework/WebAssembly.Bindings.dll /r:$WASM_SDK/framework/WebAssembly.Bindings.dll /r:WebGLDotNET.dll /r:System.Numerics.Vectors.dll Program.cs Engine/Camera.cs Engine/CanvasHelper.cs Engine/Light.cs Engine/MathHelper.cs Engine/mEngine.cs Engine/Scene.cs Engine/TestScene.cs Engine/ShaderManager.cs Engine/ShaderProgram.cs Engine/ShaderStrings.cs Engine/Shapes/Cube.cs Engine/Shapes/Mesh.cs Engine/Shapes/Quad.cs Engine/Shapes/Sube.cs` in the `rooms` folder.

* To add any other binaries include binary file name before `Program.cs`. For example add `r:/SomeBinary.dll` before `Program.cs`. Dont forget to include the actual binary file.

* The `-out:` defines output of your assembly. Add any other C# files at the end of the command. For example `SomeCSharpFile.cs`.

* If compiles successfully, `rooms.dll` binary should be created.

* Publish the binary for web-assembly with `mono $WASM_SDK/packager.exe --copy=always --out=./publish --asset=./index.html --asset=./m_helper.js rooms.dll`. This command runs `packager.exe`, a tool from Mono that publishes ready to serve web-assembly files. 

* Add any other JS files with a `--asset=` prefix for it to be included in the published folder. For example `--asset=./some_js_file.js`.

* Alternatively run `build.sh` to compile and publish web-assembly files.

* Serve published folder as any other static/wasm webpage. 


