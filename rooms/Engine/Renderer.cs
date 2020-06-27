using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using WebGLDotNET;
using System.Numerics;


namespace Engine
{
    public class Renderer : IDisposable
    {
        public WebGLBuffer indexBuffer;
        ShaderManager sm;
        Scene currentScene;

        float[] vertData; //data array of vertex positions
        float[] colData;  //data array of vertex colors
        ushort[] indiceData;
        float[] normalData;

        List<float> verts = new List<float>();
        List<ushort> inds = new List<ushort>();
        List<float> colors = new List<float>();
        List<float> normals = new List<float>();

        Matrix4x4 view;
        Matrix4x4 tempView;
        public Renderer()
        {
            Console.WriteLine($"Initializing {this}");
            CanvasHelper.gl.ClearColor(CanvasHelper.rCol, CanvasHelper.gCol, CanvasHelper.bCol, 1f);
            CanvasHelper.gl.Enable(WebGLRenderingContextBase.DEPTH_TEST);
            CanvasHelper.gl.Enable(WebGLRenderingContextBase.CULL_FACE);
            CanvasHelper.gl.CullFace(WebGLRenderingContextBase.BACK);

            CanvasHelper.gl.Disable(WebGLRenderingContextBase.DITHER);
            CanvasHelper.gl.Disable(WebGLRenderingContextBase.STENCIL_TEST);
            CanvasHelper.gl.Disable(WebGLRenderingContextBase.POLYGON_OFFSET_FILL);
            CanvasHelper.gl.Disable(WebGLRenderingContextBase.SAMPLE_ALPHA_TO_COVERAGE);
            CanvasHelper.gl.Disable(WebGLRenderingContextBase.SAMPLE_COVERAGE);
            CanvasHelper.gl.Disable(WebGLRenderingContextBase.SCISSOR_TEST);

            indexBuffer = CanvasHelper.gl.CreateBuffer();

            sm = new ShaderManager();

            currentScene = new TestScene();
            AssociateAttribs();
            Console.WriteLine($"Finished {this} initialization");
        }
        void AssociateAttribs()
        {
            verts.Clear();
            colors.Clear();
            inds.Clear();
            normals.Clear();

            int vertCount = 0;

            foreach (Mesh m in currentScene.objects)
            {
                verts.AddRange(m.GetVerts().ToList());
                inds.AddRange(m.GetIndices(vertCount).ToList());
                colors.AddRange(m.GetColorData().ToList());
                normals.AddRange(m.GetNormals().ToList());

                vertCount += m.VertCount;
            }

            vertData = verts.ToArray();
            colData = colors.ToArray();
            indiceData = inds.ToArray();
            normalData = normals.ToArray();

            if (sm.GetAttribute(sm.mShader, "vPos") != -1)
            {
                CanvasHelper.gl.BindBuffer(
                    WebGLRenderingContextBase.ARRAY_BUFFER,
                    sm.mShader.buffers["vPos"]);

                CanvasHelper.gl.BufferData(
                    WebGLRenderingContextBase.ARRAY_BUFFER,
                    vertData,
                    WebGLRenderingContextBase.STATIC_DRAW);

                CanvasHelper.gl.VertexAttribPointer(
                    (uint)sm.GetAttribute(sm.mShader, "vPos"),
                    3,
                    WebGLRenderingContextBase.FLOAT,
                    false,
                    3 * sizeof(float),
                    0);
            }

            if (sm.GetAttribute(sm.mShader, "vColor") != -1)
            {
                CanvasHelper.gl.BindBuffer(
                    WebGLRenderingContextBase.ARRAY_BUFFER,
                    sm.mShader.buffers["vColor"]);

                CanvasHelper.gl.BufferData(
                    WebGLRenderingContextBase.ARRAY_BUFFER,
                    colData,
                    WebGLRenderingContextBase.STATIC_DRAW);

                CanvasHelper.gl.VertexAttribPointer(
                    (uint)sm.GetAttribute(sm.mShader, "vColor"),
                    3,
                    WebGLRenderingContextBase.FLOAT,
                    false,
                    3 * sizeof(float),
                    0);
            }

            if (sm.GetAttribute(sm.mShader, "vNormal") != -1)
            {
                CanvasHelper.gl.BindBuffer(
                    WebGLRenderingContextBase.ARRAY_BUFFER,
                    sm.mShader.buffers["vNormal"]);

                CanvasHelper.gl.BufferData(
                    WebGLRenderingContextBase.ARRAY_BUFFER,
                    normalData,
                    WebGLRenderingContextBase.STATIC_DRAW);

                CanvasHelper.gl.VertexAttribPointer(
                    (uint)sm.GetAttribute(sm.mShader, "vNormal"),
                    3,
                    WebGLRenderingContextBase.FLOAT,
                    false,
                    3 * sizeof(float),
                    0);
            }

            CanvasHelper.gl.BindBuffer(
                WebGLRenderingContextBase.ELEMENT_ARRAY_BUFFER, indexBuffer);
            CanvasHelper.gl.BufferData(
                WebGLRenderingContextBase.ELEMENT_ARRAY_BUFFER,
                indiceData,
                WebGLRenderingContextBase.STATIC_DRAW);
        }
        public void Update(float dTime)
        {
            if (currentScene != null)
            {
                currentScene.Update(dTime);
            }

            foreach (Mesh m in currentScene.objects)
            {
                m.CalculateModelMatrix();

                m.ViewProjectionMatrix = currentScene.cam.GetViewMatrix() *
                    Matrix4x4.CreatePerspectiveFieldOfView(1.3f, CanvasHelper.canvasWidth / (float)CanvasHelper.canvasHeight, 0.1f, 50.0f);

                m.ModelViewProjectionMatrix = m.modelMatrix * m.ViewProjectionMatrix;
            }
            //view = currentScene.cam.GetViewMatrix();

            tempView = currentScene.cam.GetViewMatrix();
            System.Numerics.Matrix4x4.Invert(tempView, out view);

            Render();
        }

        public void Render()
        {

            CanvasHelper.gl.Clear(WebGLRenderingContextBase.COLOR_BUFFER_BIT | WebGLRenderingContextBase.DEPTH_BUFFER_BIT);

            EnableVertexAttribArrays();

            int indiceat = 0;

            foreach (Mesh m in currentScene.objects)
            {
                CanvasHelper.gl.UniformMatrix4fv(
                    sm.GetUniform(sm.mShader, "modelview"),
                    false,
                    MathHelper.Mat4ToFloatArray(m.ModelViewProjectionMatrix));

                CanvasHelper.gl.UniformMatrix4fv(
                    sm.GetUniform(sm.mShader, "view"),
                    false,
                    MathHelper.Mat4ToFloatArray(view));


                CanvasHelper.gl.UniformMatrix4fv(
                    sm.GetUniform(sm.mShader, "model"),
                    false,
                    MathHelper.Mat4ToFloatArray(m.modelMatrix));


                CanvasHelper.gl.Uniform3fv(sm.GetUniform(sm.mShader, "lightDir"), CanvasHelper.light.direction);
                CanvasHelper.gl.Uniform3fv(sm.GetUniform(sm.mShader, "lightColor"), CanvasHelper.light.color);
                CanvasHelper.gl.Uniform1f(sm.GetUniform(sm.mShader, "lightAmbientIntens"), CanvasHelper.light.ambientIntensity);
                CanvasHelper.gl.Uniform1f(sm.GetUniform(sm.mShader, "lightDiffuseIntens"), CanvasHelper.light.diffuseIntensity);

                if (!CanvasHelper.drawLines)
                {
                    CanvasHelper.gl.DrawElements(
                        WebGLRenderingContextBase.TRIANGLES,
                        m.IndiceCount,
                        WebGLRenderingContextBase.UNSIGNED_SHORT,
                        (uint)(indiceat * sizeof(ushort)));
                }
                else
                {
                    CanvasHelper.gl.DrawElements(
                        WebGLRenderingContextBase.LINES,
                        m.IndiceCount,
                        WebGLRenderingContextBase.UNSIGNED_SHORT,
                        (uint)(indiceat * sizeof(ushort)));
                }

                indiceat += m.IndiceCount;
            }

            //DisableVertexAttribArrays();

            CanvasHelper.gl.Flush();
            CanvasHelper.gl.Finish();
        }

        void EnableVertexAttribArrays()
        {
            for (int i = 0; i < sm.mShader.attribs.Count; i++)
            {
                CanvasHelper.gl.EnableVertexAttribArray(sm.mShader.attribs.Values.ElementAt(i).address);
            }
        }
        void DisableVertexAttribArrays()
        {
            for (int i = 0; i < sm.mShader.attribs.Count; i++)
            {
                CanvasHelper.gl.DisableVertexAttribArray(sm.mShader.attribs.Values.ElementAt(i).address);
            }
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing of {this}...");
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
                CanvasHelper.gl.Dispose();

            }

            disposed = true;
        }
    }
}