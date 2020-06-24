using System;
using System.Text;
using System.Numerics;
using System.Globalization;

namespace Engine
{
    public static class Utils
    {
        public static string StringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append('\n');
            }
            return builder.ToString();
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
    }

}