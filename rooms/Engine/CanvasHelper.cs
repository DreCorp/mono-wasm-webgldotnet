using System;
using System.Numerics;
using System.Globalization;
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
            Vector3 col = FloatRGBFromHex(_hex);
            rCol = col.X;
            gCol = col.Y;
            bCol = col.Z;

            gl.ClearColor(rCol, gCol, bCol, 1f);
        }

        public static void SetLightColorFromHex(string _hex)
        {
            Vector3 col = FloatRGBFromHex(_hex);
            light.color = new float[] { col.X, col.Y, col.Z };
        }
        public static Vector3 FloatRGBFromHex(string _hex)
        {
            Vector3 col = new Vector3();
            string colorcode = _hex.TrimStart('#');
            try
            {
                //Console.WriteLine($"SetClearColorFromHex({_hex}) ");
                if (colorcode.Length == 6)
                {
                    int r = int.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber);
                    int g = int.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber);
                    int b = int.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber);
                    col.X = (float)(1.0f / 255) * r;
                    col.Y = (float)(1.0f / 255) * g;
                    col.Z = (float)(1.0f / 255) * b;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CanvaseHelper.SetClearColorFromHex() EXCEPTIONS : {ex}");
            }

            return col;
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
