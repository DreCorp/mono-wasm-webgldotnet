using OpenToolkit.Mathematics;

namespace Engine
{
    public static class MathHelper
    {
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