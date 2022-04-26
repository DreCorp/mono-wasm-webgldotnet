using WebGLDotNET;
using System.Collections.Generic;
using System;

namespace Engine
{
    public class ShaderProgram
    {
        public WebGLProgram prog;
        public WebGLShader vs;
        public WebGLShader fs;
        public Dictionary<string, WebGLBuffer> buffers = new Dictionary<string, WebGLBuffer>();
        public Dictionary<string, AttributeInfo> attribs = new Dictionary<string, AttributeInfo>();
        public Dictionary<string, UniformInfo> uniforms = new Dictionary<string, UniformInfo>();

        public ShaderProgram()
        {
            Console.WriteLine($"Initializing {this}");
        }
    }
}