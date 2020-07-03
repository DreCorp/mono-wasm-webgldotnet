#!/bin/bash
#"compile" command:
mcs /target:library \
-out:rooms.dll \
/noconfig \
/nostdlib \
/r:$WASM_SDK/wasm-bcl/wasm/mscorlib.dll \
/r:$WASM_SDK/wasm-bcl/wasm/System.dll \
/r:$WASM_SDK/wasm-bcl/wasm/System.Core.dll \
/r:$WASM_SDK/wasm-bcl/wasm/Facades/netstandard.dll \
/r:$WASM_SDK/wasm-bcl/wasm/System.Net.Http.dll \
/r:$WASM_SDK/framework/WebAssembly.Bindings.dll \
/r:WebGLDotNET.dll \
/r:OpenToolkit.Mathematics.dll \
Program.cs \
Engine/Camera.cs \
Engine/Color.cs \
Engine/ContentManager.cs \
Engine/Light.cs \
Engine/MathHelper.cs \
Engine/Renderer.cs \
Engine/Shaders/ShaderManager.cs \
Engine/Shaders/ShaderProgram.cs \
Engine/Shaders/ShaderStrings.cs \
Engine/Scenes/SceneManager.cs \
Engine/Scenes/Scene.cs \
Engine/Scenes/TestScene.cs \
Engine/Geometry/Cube.cs \
Engine/Geometry/Mesh.cs \
Engine/Geometry/Point.cs \
Engine/Geometry/Quad.cs \
Engine/Geometry/Sube.cs \
&& 
#"publish" command:
mono $WASM_SDK/packager.exe \
--copy=always \
--out=./publish \
--asset=./index.html \
--asset=./app.js \
--linker=true \
rooms.dll
#--asset=./Assets/brick_dark_0.png \