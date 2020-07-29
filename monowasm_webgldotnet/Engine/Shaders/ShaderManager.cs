using System;
using WebGLDotNET;

namespace Engine
{
    public class ShaderManager
    {
        WebGLRenderingContext gl;
        public ShaderProgram mShader;

        public string vs { get; set; }
        public string fs { get; set; }

        public ShaderManager(WebGLRenderingContext _gl)
        {
            Console.WriteLine($"Initializing {this}");
            gl = _gl;
            LoadShader();
            Console.WriteLine($"Finished {this} initialization");
        }

        void LoadShader()
        {
            vs = ShaderStrings.directional_vertex_shader;
            fs = ShaderStrings.directional_fragment_shader;
            mShader = new ShaderProgram();
            InitProgram(mShader, vs, fs);
        }

        private void InitProgram(ShaderProgram program, string _vs, string _fs)
        {
            program.prog = gl.CreateProgram();

            program.vs = gl.CreateShader(WebGLRenderingContextBase.VERTEX_SHADER);
            program.fs = gl.CreateShader(WebGLRenderingContextBase.FRAGMENT_SHADER);

            gl.ShaderSource(program.vs, _vs);
            gl.ShaderSource(program.fs, _fs);

            gl.CompileShader(program.vs);
            gl.CompileShader(program.fs);

            gl.AttachShader(program.prog, program.vs);
            gl.AttachShader(program.prog, program.fs);

            int attShaders =
                (int)gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.ATTACHED_SHADERS);
            Console.WriteLine($"{this}: Shaders attached: {attShaders}");

            bool vsComp = (bool)gl.GetShaderParameter(program.vs, WebGLRenderingContextBase.COMPILE_STATUS);
            bool fsComp = (bool)gl.GetShaderParameter(program.fs, WebGLRenderingContextBase.COMPILE_STATUS);

            Console.WriteLine($"{this}: Vertex shader compile status: {vsComp}");
            Console.WriteLine($"{this}: Fragment shader compile status: {fsComp}");

            LinkProgram(program);
        }

        private void LinkProgram(ShaderProgram program)
        {
            gl.LinkProgram(program.prog);
            gl.ValidateProgram(program.prog);

            bool linkStatus =
                (bool)gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.LINK_STATUS);
            bool validateStatus =
                (bool)gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.VALIDATE_STATUS);

            Console.WriteLine($"{this}: Shader program Link status: {linkStatus}");
            Console.WriteLine($"{this}: Shader program Validate status: {validateStatus}");

            int attribCount =
                (int)gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.ACTIVE_ATTRIBUTES);

            int uniformCount =
                (int)gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.ACTIVE_UNIFORMS);

            Console.WriteLine($"Shader program has {attribCount} attributes, {uniformCount} uniforms.");


            for (uint i = 0; i < attribCount; i++)
            {
                WebGLActiveInfo ai = gl.GetActiveAttrib(program.prog, i);
                Console.WriteLine($"Attribute {i} Name: {ai.Name}, Size: {ai.Size}, Type: {ai.Type}");
                GenerateAttribute(program, ai.Name);
            }

            for (uint i = 0; i < uniformCount; i++)
            {
                WebGLActiveInfo ai = gl.GetActiveUniform(program.prog, i);
                Console.WriteLine($"Uniform {i} Name: {ai.Name}, Size: {ai.Size}, Type: {ai.Type}");
                GenerateUniform(program, ai.Name);
            }

            gl.UseProgram(program.prog);

            gl.DeleteShader(program.vs);
            gl.DeleteShader(program.fs);

            gl.DeleteProgram(program.prog);
        }

        void GenerateAttribute(ShaderProgram program, string _name)
        {
            AttributeInfo info = new AttributeInfo();
            info.name = _name;
            int addr = gl.GetAttribLocation(program.prog, _name);
            info.address = (uint)addr;

            program.attribs.Add(info.name, info);

            program.buffers.Add(_name, gl.CreateBuffer());
        }

        void GenerateUniform(ShaderProgram program, string _name)
        {
            UniformInfo info = new UniformInfo();

            info.name = _name;
            info.address = gl.GetUniformLocation(program.prog, _name);

            program.uniforms.Add(info.name, info);

            program.buffers.Add(_name, gl.CreateBuffer());
        }

        public int GetAttributeLocation(ShaderProgram program, string _name)
        {
            if (program.attribs.ContainsKey(_name)) return (int)program.attribs[_name].address;
            else return -1;
        }

        public WebGLUniformLocation GetUniformLocation(ShaderProgram program, string _name)
        {
            if (program.uniforms.ContainsKey(_name)) return program.uniforms[_name].address;
            else return null;
        }
    }

    public class AttributeInfo
    {
        public string name = "";
        public uint address = 0;
    }

    public class UniformInfo
    {
        public string name = "";
        public WebGLUniformLocation address;
    }
}