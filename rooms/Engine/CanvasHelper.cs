using System;
using System.Numerics;
using WebGLDotNET;

namespace Engine
{
    public static class CanvasHelper
    {
        public static WebGLRenderingContext gl;
        public static long canvasWidth = 100;
        public static long canvasHeight = 100;

        public static float rCol = 0f;
        public static float gCol = 0f;
        public static float bCol = 0f;
        public static bool drawLines = false;

        public static Light light = new Light();

        public static void SetCanvasViewportSize(long _w, long _h)
        {
            canvasWidth = _w;
            canvasHeight = _h;

            gl.Viewport(0, 0, (int)canvasWidth, (int)canvasHeight);
        }

        public static void SetClearColor(float _r, float _g, float _b)
        {
            rCol = _r;
            gCol = _g;
            bCol = _b;
        }
        public static void SetClearColorFromHex(string _hex)
        {
            Vector3 col = Utils.FloatRGBFromHex(_hex);
            rCol = col.X;
            gCol = col.Y;
            bCol = col.Z;

            gl.ClearColor(rCol, gCol, bCol, 1f);
        }

        public static void SetLightColorFromHex(string _hex)
        {
            Vector3 col = Utils.FloatRGBFromHex(_hex);
            light.color = new float[] { col.X, col.Y, col.Z };
        }

        public static void ChangePrimitive(string _p)
        {
            switch (_p)
            {
                case "Lines":
                    drawLines = true;
                    break;

                case "Triangles":
                    drawLines = false;
                    break;

                default:
                    Console.WriteLine("CanvasHelper.ChangePrimitive() incorrect input");
                    break;
            }
        }
    }
}
