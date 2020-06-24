//using OpenTK;
using System.Numerics;

namespace Engine
{
    public class Cube : Mesh
    {
        float[] verts;
        float[] colorData;

        ushort[] inds;

        public Cube()
        {
            Setup(new Vector3(1f, 1f, 1f));
        }

        public Cube(Vector3 _color)
        {
            Setup(_color);
        }

        public void Setup(Vector3 _color)
        {
            //VertCount = 8;
            //ColorDataCount = 8;
            VertCount = 24;
            IndiceCount = 36;
            ColorDataCount = 24;

            TextureCoordCount = 24;

            verts = new float[]
            {
                -0.5f,-0.5f,-0.5f,
                0.5f, 0.5f,-0.5f,
                0.5f,-0.5f, -0.5f,
                -0.5f,0.5f, -0.5f,

                 //back
                0.5f,-0.5f,-0.5f,
                0.5f, 0.5f,-0.5f,
                0.5f, 0.5f,0.5f,
                0.5f,-0.5f,0.5f,

                //right
                -0.5f,-0.5f, 0.5f,
                0.5f, -0.5f, 0.5f,
                0.5f, 0.5f,0.5f,
                -0.5f,0.5f,0.5f,

                //top
                0.5f, 0.5f, -0.5f,
                -0.5f, 0.5f,-0.5f,
                0.5f, 0.5f,0.5f,
                -0.5f, 0.5f, 0.5f,

                //front
                -0.5f,-0.5f,-0.5f,
                -0.5f, 0.5f, 0.5f,
                -0.5f, 0.5f, -0.5f,
                -0.5f,-0.5f, 0.5f,

                //bottom
                -0.5f,-0.5f,-0.5f,
                0.5f,-0.5f, -0.5f,
                0.5f,-0.5f, 0.5f,
                -0.5f,-0.5f,0.5f
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

            //Matrix4x4.Invert(modelMatrix, out modelMatrix);
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
            /*
            short[] inds = new short[]
            {
                0,1,2,2,3,0,
                3,2,5,5,4,3,
                4,5,6,6,7,4,
                7,6,1,1,0,7,
                7,0,3,3,4,7,
                1,6,5,5,2,1,
            };*/
            inds = new ushort[]
            {
                0,1,2,0,3,1,

                4,5,6,4,6,7,

                8,9,10,8,10,11,

                13,14,12,13,15,14,

                16,17,18,16,19,17,

                20,21,22,20,22,23
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

                new Vector2(0f,0f),
                new Vector2(0f,1f),
                new Vector2(-1f,1f),
                new Vector2(-1f,0f),

                new Vector2(-1f,0f),
                new Vector2(0f,0f),
                new Vector2(0f,1f),
                new Vector2(-1f,1f),

                new Vector2(0f,0f),
                new Vector2(0f,1f),
                new Vector2(-1f,0f),
                new Vector2(-1f,1f),
                 //top
                new Vector2(0f,0f),
                new Vector2(1f,1f),
                new Vector2(0f,1f),
                new Vector2(1f,0f),
                 //bottom
                new Vector2(0f,0f),
                new Vector2(0f,1f),
                new Vector2(-1f,1f),
                new Vector2(-1f,0f),
            };
        }
    }
}