//using OpenTK;
using System.Numerics;

namespace Engine
{
    public class Quad : Mesh
    {
        float[] verts;
        float[] colorData;

        public Quad()
        {
            Setup(new Vector3(1f, 1f, 1f));
        }

        public Quad(Vector3 _color)
        {
            Setup(_color);
        }

        public void Setup(Vector3 _color)
        {
            VertCount = 4;
            IndiceCount = 6;
            ColorDataCount = 4;
            TextureCoordCount = 4;

            verts = new float[]
            {
                //CCW
                -0.5f, 0.5f, 0f,
                -0.5f,-0.5f, 0f,
                0.5f, -0.5f, 0f,
                0.5f, 0.5f,  0f,
            };

            color = _color;
        }
        /*
        public override void CalculateModelMatrix()
        {
            modelMatrix = Matrix4x4.CreateScale(Scale)
            * Matrix4x4.CreateRotationX(Rotation.X)
            * Matrix4x4.CreateRotationY(Rotation.Y)
            * Matrix4x4.CreateRotationZ(Rotation.Z)
            * Matrix4x4.CreateTranslation(Position);
        }
        */
        public override float[] GetColorData()
        {
            colorData = new float[ColorDataCount * 3];
            for (int i = 0; i < ColorDataCount; i++)
            {
                colorData[i * 3] = color.X;
                colorData[i * 3 + 1] = color.Y;
                colorData[i * 3 + 2] = color.Z;
            }
            return colorData;
        }
        public override ushort[] GetIndices(int offset = 0)
        {
            ushort[] inds = new ushort[]
            {
                //CCW
               0,1,2,2,3,0
            };

            if (offset != 0)
            {
                for (int i = 0; i < inds.Length; i++)
                {
                    inds[i] += (ushort)offset;
                }
            }

            return inds;
        }

        public override float[] GetVerts()
        {
            return verts;
        }

        public override Vector2[] GetTextureCoords()
        {
            return new Vector2[]
            {
                new Vector2(0f,0f),
                new Vector2(-1f,1f),
                new Vector2(-1f,0f),
                new Vector2(0f,1f),
            };
        }
    }
}