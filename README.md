# ROOMS
 
## WASM and WebGL experiment using C# with [mono-wasm](https://www.mono-project.com/news/2018/01/16/mono-static-webassembly-compilation/ "mono-wasm") and [WebGLDotNET](https://github.com/WaveEngine/WebGL.NET "WebGLDotNET").



#### Disclaimer:

Current size of a compiled and "ready-to-serve" static page is 16mb+.

***

See this project running at [https://rwasm.netlify.app](https://rwasm.netlify.app).

`WASD` for horizontal controls,  
`Q` & `E` for vertical controls,  
`←` & `→` to rotate on horizontal axis,  
`P` to switch between `TRINGLES` and `LINES` drawing modes.


Please post any issues, suggestions, optimizations to issues tab.

***

This project initially started as a [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) app, you can see it running [here](https://dwasm.netlify.app).

***

### Resources used:

* [Run C# natively in browser](https://itnext.io/run-c-natively-in-the-browser-through-the-web-assembly-via-mono-wasm-60f3d55dd05a) guide by [0x414c49](https://github.com/0x414c49).

* [0x414c49's](https://github.com/0x414c49) basic mono-wasm [example](https://github.com/0x414c49/mono-wasm-example).

* Using `WebGLDotNET`: [Your first WebGL.NET app](https://geeks.ms/xamarinteam/2019/08/28/your-first-webgldotnet-app/) guide.

* OpenGL with C#: [Neo Kabuto's](https://neokabuto.blogspot.com/) [OpenTK tutorial](https://neokabuto.blogspot.com/p/tutorials.html).

* __Optional__ [Dungeon Crawl texture pack](https://opengameart.org/content/dungeon-crawl-32x32-tiles-supplemental) from [https://opengameart.org/](https://opengameart.org/). Read further for more info.

***

### To compile (on Linux):

Following [Run C# natively in browser](https://itnext.io/run-c-natively-in-the-browser-through-the-web-assembly-via-mono-wasm-60f3d55dd05a "Run C# natively in browser") tutorial by [0x414c49](https://github.com/0x414c49):

* Download and install Mono SDK for your OS.

* After installation make sure you can access both `mcs` and `mono` directly in your terminal.

* Get the latest successful build of `mono-wasm` [here](https://jenkins.mono-project.com/job/test-mono-mainline-wasm/label=ubuntu-1804-amd64/lastSuccessfulBuild/Azure/). Download and extract the `.zip` file designated with `sdks/wasm/mono-wasm-###########.zip`.

* Define a variable called `$WASM_SDK`. For example: `$WASM_SDK="/path/to_wasm_sdk/"`. Dont forget the trailing slash.

* Add compiled `WebGLDotNET.dll` and `OpenToolkit.Mathematics.dll` binaries to the `rooms` folder. At the moment you will need to compile them yourself. You can simply add relevant references to `.csproj`, then run `dotnet build`.

* Run `"compile"` command from `build.sh` in the `rooms` folder.

* To add any other binaries include binary file name before `Program.cs` in the `"compile"` command. For example add `r:/SomeBinary.dll` before `Program.cs`. Dont forget to include the actual binary file.

* The `-out:` in the `"compile"` command defines output of your assembly. Add any other C# files at the end of the command. For example `SomeCSharpFile.cs`.

* If compiles successfully, `rooms.dll` binary should be created.

* Publish the binary for web-assembly with `"publish"` command from the `build.sh`. This command runs `packager.exe`, a tool from Mono that publishes ready to serve web-assembly files.

* Add any other JS files with a `--asset=` prefix for it to be included in the published folder. For example `--asset=./some_js_file.js`.

* Alternatively run `build.sh` to compile and publish web-assembly files.

* __Optional__: include any assets in `Asset` folder as `ContentManager.cs` depends on them. `Asset` folder included contains some textures from [Dungeon Crawl texture pack](https://opengameart.org/content/dungeon-crawl-32x32-tiles-supplemental) from [https://opengameart.org/](https://opengameart.org/). Those are used with the `basic_texture` shader.

* Serve published folder as any other static webpage.