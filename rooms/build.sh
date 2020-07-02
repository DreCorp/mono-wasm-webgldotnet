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
Engine/Utils.cs \
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
--asset=./m_helper.js \
--asset=./Assets/brick_dark_0.png \
--asset=./Assets/brick_brown_0.png \
--asset=./Assets/brick_brown-vines_1.png \
--asset=./Assets/beehives_0.png \
--asset=./Assets/brick_gray_0.png \
--asset=./Assets/catacombs_0.png \
--asset=./Assets/church_0.png \
--asset=./Assets/cobalt_rock_1.png \
--asset=./Assets/cobalt_stone_1.png \
--asset=./Assets/crystal_wall_0.png \
--asset=./Assets/emerald_1.png \
--asset=./Assets/hell_1.png \
--asset=./Assets/hive_0.png \
--asset=./Assets/lab-metal_0.png \
--asset=./Assets/lab-rock_0.png \
--asset=./Assets/lab-stone_0.png \
--asset=./Assets/lair_0_new.png \
rooms.dll