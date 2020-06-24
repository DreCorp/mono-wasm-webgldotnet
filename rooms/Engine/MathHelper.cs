//using OpenTK;
using System.Collections.Generic;
using System;
using System.Numerics;

namespace Engine
{
    public static class MathHelper
    {
        public static float[] Vec3ArrToFloatArr(Vector3[] _vec)
        {
            int l = (int)_vec.Length *3;
            float[] f = new float[l];
            
            for(int i = 0; i<_vec.Length; i++)
            {
                f[i] = _vec[i].X;
                f[i+1] = _vec[i].Y;
                f[i+2] = _vec[i].Z;

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

            
            return new float[]{};
        }

        public static float[] Mat4ToFloatArray(Matrix4x4 mat)
        {
            return new float[]
            {
                mat.M11, mat.M12, mat.M13, mat.M14,
                mat.M21, mat.M22, mat.M23, mat.M24,
                mat.M31, mat.M32, mat.M33, mat.M34,
                mat.M41, mat.M42, mat.M43, mat.M44,
            };
        }

        public static float[] Mat4IdToFloatArray()
        {
            Matrix4x4 mat = Matrix4x4.Identity;
            return Mat4ToFloatArray(mat);
        }
    }
}