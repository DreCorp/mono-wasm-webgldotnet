using System;
using WebGLDotNET;

namespace Engine
{
    public class ShaderManager
    {
        public ShaderProgram mShader;

        public string vs { get; set; }
        public string fs { get; set; }

        public ShaderManager()
        {
            Console.WriteLine($"Initializing {this}");
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
            program.prog = CanvasHelper.gl.CreateProgram();

            program.vs = CanvasHelper.gl.CreateShader(WebGLRenderingContextBase.VERTEX_SHADER);
            program.fs = CanvasHelper.gl.CreateShader(WebGLRenderingContextBase.FRAGMENT_SHADER);

            CanvasHelper.gl.ShaderSource(program.vs, _vs);
            CanvasHelper.gl.ShaderSource(program.fs, _fs);

            CanvasHelper.gl.CompileShader(program.vs);
            CanvasHelper.gl.CompileShader(program.fs);

            CanvasHelper.gl.AttachShader(program.prog, program.vs);
            CanvasHelper.gl.AttachShader(program.prog, program.fs);

            int attShaders =
                (int)CanvasHelper.gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.ATTACHED_SHADERS);
            Console.WriteLine($"{this}: Shaders attached: {attShaders}");

            bool vsComp = (bool)CanvasHelper.gl.GetShaderParameter(program.vs, WebGLRenderingContextBase.COMPILE_STATUS);
            bool fsComp = (bool)CanvasHelper.gl.GetShaderParameter(program.fs, WebGLRenderingContextBase.COMPILE_STATUS);

            Console.WriteLine($"{this}: Vertex shader compile status: {vsComp}");
            Console.WriteLine($"{this}: Fragment shader compile status: {fsComp}");

            LinkProgram(program);
        }

        private void LinkProgram(ShaderProgram program)
        {
            CanvasHelper.gl.LinkProgram(program.prog);
            CanvasHelper.gl.ValidateProgram(program.prog);

            bool linkStatus =
                (bool)CanvasHelper.gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.LINK_STATUS);
            bool validateStatus =
                (bool)CanvasHelper.gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.VALIDATE_STATUS);

            Console.WriteLine($"{this}: Shader program Link status: {linkStatus}");
            Console.WriteLine($"{this}: Shader program Validate status: {validateStatus}");

            int attribCount =
                (int)CanvasHelper.gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.ACTIVE_ATTRIBUTES);

            int uniformCount =
                (int)CanvasHelper.gl.GetProgramParameter(program.prog, WebGLRenderingContextBase.ACTIVE_UNIFORMS);

            Console.WriteLine($"Shader program has {attribCount} attributes, {uniformCount} uniforms.");


            for (uint i = 0; i < attribCount; i++)
            {
                WebGLActiveInfo ai = CanvasHelper.gl.GetActiveAttrib(program.prog, i);
                Console.WriteLine($"Attribute {i} Name: {ai.Name}, Size: {ai.Size}, Type: {ai.Type}");
            }

            for (uint i = 0; i < uniformCount; i++)
            {
                WebGLActiveInfo ai = CanvasHelper.gl.GetActiveUniform(program.prog, i);
                Console.WriteLine($"Uniform {i} Name: {ai.Name}, Size: {ai.Size}, Type: {ai.Type}");
            }

            GenAttribs(program, "vPos", typeof(AttributeInfo));
            GenAttribs(program, "vColor", typeof(AttributeInfo));
            GenAttribs(program, "vNormal", typeof(AttributeInfo));

            GenAttribs(program, "modelview", typeof(UniformInfo));
            GenAttribs(program, "view", typeof(UniformInfo));
            GenAttribs(program, "model", typeof(UniformInfo));

            GenAttribs(program, "lightDir", typeof(UniformInfo));
            GenAttribs(program, "lightColor", typeof(UniformInfo));
            GenAttribs(program, "lightAmbientIntens", typeof(UniformInfo));
            GenAttribs(program, "lightDiffuseIntens", typeof(UniformInfo));

            CanvasHelper.gl.UseProgram(program.prog);

            CanvasHelper.gl.DeleteShader(program.vs);
            CanvasHelper.gl.DeleteShader(program.fs);

            CanvasHelper.gl.DeleteProgram(program.prog);
        }

        void GenAttribs(ShaderProgram program, string _name, Type _type)
        {
            if (_type == typeof(AttributeInfo))
            {

                AttributeInfo info = new AttributeInfo();
                info.name = _name;
                int addr = CanvasHelper.gl.GetAttribLocation(program.prog, _name);
                info.address = (uint)addr;

                program.attribs.Add(info.name, info);
            }
            else if (_type == typeof(UniformInfo))
            {
                UniformInfo info = new UniformInfo();

                info.name = _name;
                info.address = CanvasHelper.gl.GetUniformLocation(program.prog, _name);

                program.uniforms.Add(info.name, info);
            }
            else
            {
                Console.WriteLine($"{this}.GenAttribs() - type is not supported");
                return;
            }

            program.buffers.Add(_name, CanvasHelper.gl.CreateBuffer());
        }

        public int GetAttribute(ShaderProgram program, string _name)
        {
            if (program.attribs.ContainsKey(_name)) return (int)program.attribs[_name].address;
            else return -1;
        }

        public WebGLUniformLocation GetUniform(ShaderProgram program, string _name)
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