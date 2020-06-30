using WebGLDotNET;
using WebAssembly.Host;
using WebAssembly;
using System;

namespace Engine
{
    public class ContentManager
    {
        public WebGLTexture texture1;

        const string AssetPath = "opentksquare.png";
        Action<JSObject> onLoad;

        public void LoadImage(WebGLRenderingContext gl)
        {
            texture1 = gl.CreateTexture();

            var image = new HostObject("Image");
            onLoad = new Action<JSObject>(loadEvent =>
            {
                gl.BindTexture(WebGLRenderingContextBase.TEXTURE_2D, texture1);


                gl.TexImage2D(
                    WebGLRenderingContextBase.TEXTURE_2D,
                    0,
                    WebGLRenderingContextBase.RGB,

                    WebGLRenderingContextBase.RGB,
                    WebGLRenderingContextBase.UNSIGNED_BYTE,
                    image);

                gl.GenerateMipmap(WebGLRenderingContextBase.TEXTURE_2D);


                gl.TexParameteri(
                    WebGLRenderingContextBase.TEXTURE_2D,
                    WebGLRenderingContextBase.TEXTURE_WRAP_S,
                    (int)WebGLRenderingContextBase.CLAMP_TO_EDGE);
                gl.TexParameteri(
                    WebGLRenderingContextBase.TEXTURE_2D,
                    WebGLRenderingContextBase.TEXTURE_WRAP_T,
                    (int)WebGLRenderingContextBase.CLAMP_TO_EDGE);
                gl.TexParameteri(
                    WebGLRenderingContextBase.TEXTURE_2D,
                    WebGLRenderingContextBase.TEXTURE_MIN_FILTER,
                    (int)WebGLRenderingContextBase.NEAREST);
                gl.TexParameteri(
                    WebGLRenderingContextBase.TEXTURE_2D,
                    WebGLRenderingContextBase.TEXTURE_MAG_FILTER,
                    (int)WebGLRenderingContextBase.NEAREST);

                gl.TexImage2D(
                    WebGLRenderingContextBase.TEXTURE_2D,
                    0,
                    WebGLRenderingContextBase.RGB,
                    WebGLRenderingContextBase.RGB,
                    WebGLRenderingContextBase.UNSIGNED_BYTE,
                    image);

                loadEvent.Dispose();
                Runtime.FreeObject(onLoad);

                image.SetObjectProperty("onload", null);
                image.Dispose();
            });
            image.SetObjectProperty("onload", onLoad);
            image.SetObjectProperty("src", AssetPath);

            //gl.BindTexture(WebGL2RenderingContextBase.TEXTURE_2D, texture1);
        }
    }
}
