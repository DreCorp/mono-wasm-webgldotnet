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
Engine/Light.cs \
Engine/MathHelper.cs \
Engine/Renderer.cs \
Engine/ShaderManager.cs \
Engine/ShaderProgram.cs \
Engine/ShaderStrings.cs \
Engine/Utils.cs \
Engine/Scenes/SceneManager.cs \
Engine/Scenes/Scene.cs \
Engine/Scenes/TestScene.cs \
Engine/Shapes/Cube.cs \
Engine/Shapes/Mesh.cs \
Engine/Shapes/Quad.cs \
Engine/Shapes/Sube.cs \
&& 
#"publish" command:
mono $WASM_SDK/packager.exe \
--copy=always \
--out=./publish \
--asset=./index.html \
--asset=./m_helper.js \
rooms.dll