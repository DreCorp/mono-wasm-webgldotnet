using WebGLDotNET;
using WebAssembly.Host;
using WebAssembly;
using System;

namespace Engine
{
    public class ContentManager
    {
        string[] textureNames = new string[] {
            "Assets/brick_dark_0.png",
            "Assets/brick_brown_0.png",
            "Assets/brick_brown-vines_1.png",
            "Assets/beehives_0.png",
            "Assets/brick_gray_0.png",
            "Assets/catacombs_0.png",
            "Assets/church_0.png",
            "Assets/cobalt_rock_1.png",
            "Assets/cobalt_stone_1.png",
            "Assets/crystal_wall_0.png",
            "Assets/emerald_1.png",
            "Assets/hell_1.png",
            "Assets/hive_0.png",
            "Assets/lab-metal_0.png",
            "Assets/lab-rock_0.png",
            "Assets/lab-stone_0.png",
            "Assets/lair_0_new.png"
        };
        public static WebGLTexture[] textures;
        Action<JSObject> onLoad;

        WebGLRenderingContext gl;
        public ContentManager(WebGLRenderingContext _gl)
        {
            Console.WriteLine($"Initializing {this}");
            gl = _gl;
            textures = new WebGLTexture[textureNames.Length];
            LoadTextures(_gl);

            onLoad = new Action<JSObject>(loadEvent =>
            {
                loadEvent.Dispose();
                Runtime.FreeObject(onLoad);
            });

            Console.WriteLine($"Finished {this} initialization");
        }

        void LoadTextures(WebGLRenderingContext gl)
        {

            for (int i = 0; i < textureNames.Length; i++)
            {
                try
                {
                    textures[i] = LoadTexture(gl, textureNames[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        WebGLTexture LoadTexture(WebGLRenderingContext gl, string path)
        {
            WebGLTexture t = gl.CreateTexture();

            var image = new HostObject("Image");
            onLoad = new Action<JSObject>(loadEvent =>
            {
                gl.BindTexture(WebGLRenderingContextBase.TEXTURE_2D, t);


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

                //loadEvent.Dispose();
                //Runtime.FreeObject(onLoad);

                //image.SetObjectProperty("onload", null);
                //image.Dispose();
            });
            image.SetObjectProperty("onload", onLoad);
            image.SetObjectProperty("src", path);

            return t;
        }
    }
}
