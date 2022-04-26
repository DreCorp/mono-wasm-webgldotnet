using OpenToolkit.Mathematics;
using System;
namespace Engine
{
    public static class MathHelper
    {
        public const float E = (float)Math.E;
        public const float Log10E = 0.4342945f;
        public const float Log2E = 1.442695f;
        public const float Pi = (float)Math.PI;
        public const float PiOver2 = (float)(Math.PI / 2.0);
        public const float PiOver4 = (float)(Math.PI / 4.0);
        public const float TwoPi = (float)(Math.PI * 2.0);

        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            double amountSquared = amount * amount;
            double amountCubed = amountSquared * amount;
            return (float)(0.5 * (2.0 * value2 +
                (value3 - value1) * amount +
                (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

        public static float Clamp(float value, float min, float max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            value = (value < min) ? min : value;

            // There's no check to see if min > max.
            return value;
        }

        public static float Distance(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }

        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            double sCubed = s * s * s;
            double sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return (float)result;
        }


        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public static float Max(float value1, float value2)
        {
            return Math.Max(value1, value2);
        }

        public static float Min(float value1, float value2)
        {
            return Math.Min(value1, value2);
        }

        public static float SmoothStep(float value1, float value2, float amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
#if(USE_FARSEER)
            float result = SilverSpriteMathHelper.Clamp(amount, 0f, 1f);
            result = SilverSpriteMathHelper.Hermite(value1, 0f, value2, 0f, result);
#else
            float result = MathHelper.Clamp(amount, 0f, 1f);
            result = MathHelper.Hermite(value1, 0f, value2, 0f, result);
#endif
            return result;
        }

        public static float ToDegrees(float radians)
        {
            // This method uses double precission internally,
            // though it returns single float
            // Factor = 180 / pi
            return (float)(radians * 57.295779513082320876798154814105);
        }

        public static float ToRadians(float degrees)
        {
            // This method uses double precission internally,
            // though it returns single float
            // Factor = pi / 180
            return (float)(degrees * 0.017453292519943295769236907684886);
        }

        public static float[] Vec3ArrToFloatArr(Vector3[] _vec)
        {
            int l = (int)_vec.Length * 3;
            float[] f = new float[l];

            for (int i = 0; i < _vec.Length; i++)
            {
                f[i] = _vec[i].X;
                f[i + 1] = _vec[i].Y;
                f[i + 2] = _vec[i].Z;
            }

            return f;
        }

        public static float[] MultiplyMat4(float[] m1, float[] m2)
        {
            float[] nm = new float[16];

            nm[0] = m1[0] * m2[0] + m1[1] * m2[4] + m1[2] * m2[8] + m1[3] * m2[12];
            nm[1] = m1[0] * m2[1] + m1[1] * m2[5] + m1[2] * m2[9] + m1[3] * m2[13];
            nm[2] = m1[0] * m2[2] + m1[1] * m2[6] + m1[2] + m2[10] + m1[3] * m2[14];
            nm[3] = m1[0] * m2[3] + m2[1] * m2[7] + m1[2] + m2[11] + m1[3] * m2[15];


            return new float[] { };
        }

        static float[] m = new float[16];
        public static float[] Mat4ToFloatArray(Matrix4 mat)
        {
            m[0] = mat.M11;
            m[1] = mat.M12;
            m[2] = mat.M13;
            m[3] = mat.M14;

            m[4] = mat.M21;
            m[5] = mat.M22;
            m[6] = mat.M23;
            m[7] = mat.M24;

            m[8] = mat.M31;
            m[9] = mat.M32;
            m[10] = mat.M33;
            m[11] = mat.M34;

            m[12] = mat.M41;
            m[13] = mat.M42;
            m[14] = mat.M43;
            m[15] = mat.M44;

            return m;

            /*
            return new float[]
            {
                mat.M11, mat.M12, mat.M13, mat.M14,
                mat.M21, mat.M22, mat.M23, mat.M24,
                mat.M31, mat.M32, mat.M33, mat.M34,
                mat.M41, mat.M42, mat.M43, mat.M44,
            };
            */
        }

    }
}