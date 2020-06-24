using System;
using WebGLDotNET;
using WebAssembly;

using Utils;

class Program
{
    static JSObject window;
    static JSObject canvas;

    
    void Start()
    {
        Logger.Log("Starting App");
       
        window = (JSObject)Runtime.GetGlobalObject("window");
        var width = (int)window.GetObjectProperty("innerWidth");
        var height = (int)window.GetObjectProperty("innerHeight");
        string s = width + " " + height;
        Logger.Log($"Width: {width}, Height: {height}");

        
        using (var document = (JSObject)Runtime.GetGlobalObject("document"))
        using (var body = (JSObject)document.GetObjectProperty("body"))
        {
            canvas = (JSObject)document.Invoke("createElement", "canvas");
            body.Invoke("appendChild", canvas);
            canvas.SetObjectProperty("id", "mcanvas");
            canvas.SetObjectProperty("width", width);
            canvas.SetObjectProperty("height", height);
        }

        var gl = new WebGLRenderingContext(canvas);

        var verts = new float[]
        {
            0.0f,  0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f
        };

        var vertex_buffer = gl.CreateBuffer();
        gl.BindBuffer(WebGLRenderingContextBase.ARRAY_BUFFER, vertex_buffer);
        gl.BufferData(WebGLRenderingContextBase.ARRAY_BUFFER, verts, WebGLRenderingContextBase.STATIC_DRAW);
        gl.BindBuffer(WebGLRenderingContextBase.ARRAY_BUFFER, null);

        var inds = new ushort[]{
            0,1,2
        };

        var index_buffer = gl.CreateBuffer();
        gl.BindBuffer(WebGLRenderingContextBase.ELEMENT_ARRAY_BUFFER, index_buffer);
        gl.BufferData(WebGLRenderingContextBase.ELEMENT_ARRAY_BUFFER, inds, WebGLRenderingContextBase.STATIC_DRAW);
        gl.BindBuffer(WebGLRenderingContextBase.ELEMENT_ARRAY_BUFFER, null);

        var shader_program = gl.CreateProgram();
        var vShader = gl.CreateShader(WebGLRenderingContextBase.VERTEX_SHADER);
        gl.ShaderSource(vShader, @"
            attribute vec3 position;
            void main(){
                gl_Position = vec4(position, 1.0);
            }
        ");
        gl.CompileShader(vShader);

        var fShader = gl.CreateShader(WebGLRenderingContextBase.FRAGMENT_SHADER);
        gl.ShaderSource(fShader, @"
            void main(){
                gl_FragColor = vec4(0.0, 0.0, 1.0, 1.0);
            }
        ");
        gl.CompileShader(fShader);

        gl.AttachShader(shader_program, vShader);
        gl.AttachShader(shader_program, fShader);

        gl.LinkProgram(shader_program);

        var attribCount = (int)gl.GetProgramParameter(shader_program, WebGLRenderingContextBase.ACTIVE_ATTRIBUTES);
        var uniformCount = (int)gl.GetProgramParameter(shader_program, WebGLRenderingContextBase.ACTIVE_UNIFORMS);

        for (uint i =0; i< attribCount; i++){
            var info = (WebGLActiveInfo)gl.GetActiveAttrib(shader_program, i);
            string name = info.Name;
            //console.Invoke("log", name);
        }
        
        gl.UseProgram(shader_program);

        gl.BindBuffer(WebGLRenderingContextBase.ARRAY_BUFFER, vertex_buffer);
        gl.BindBuffer(WebGLRenderingContextBase.ELEMENT_ARRAY_BUFFER, index_buffer);

        var position_attrib = (uint)gl.GetAttribLocation(shader_program, "position");
        gl.VertexAttribPointer(position_attrib, 3, WebGLRenderingContextBase.FLOAT, false,0,0);
        gl.EnableVertexAttribArray(position_attrib);


        gl.Enable(WebGLRenderingContextBase.DEPTH_TEST);
        gl.Viewport(0,0, (int)canvas.GetObjectProperty("width"), (int)canvas.GetObjectProperty("height"));
    
        gl.ClearColor(0,0,0, 1);
        gl.Clear(WebGLRenderingContextBase.COLOR_BUFFER_BIT);

        gl.DrawElements(WebGLRenderingContextBase.TRIANGLES,inds.Length,WebGLRenderingContextBase.UNSIGNED_SHORT, 0);
    
    }

    void Resize(long w, long h) 
    {       
        //console.Invoke("log", $"{w} {h}");
    }
}

