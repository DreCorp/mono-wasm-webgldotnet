using OpenToolkit.Mathematics;

namespace Engine
{
    public class Sube : Mesh
    {
        float[] verts;
        float[] colorData;

        ushort[] inds;

        public Sube()
        {
            Setup(new Vector3(1f, 1f, 1f));
        }

        public Sube(Vector3 _color)
        {
            Setup(_color);
        }

        void Setup(Vector3 _color)
        {
            VertCount = 8;
            IndiceCount = 36;
            ColorDataCount = 8;

            verts = new float[]{
                -0.5f, 0.5f, 0.5f,
                -0.5f,-0.5f, 0.5f,
                0.5f, -0.5f, 0.5f,
                0.5f, 0.5f, 0.5f,

                0.5f, 0.5f, -0.5f,
                0.5f,-0.5f, -0.5f,
                -0.5f,-0.5f,-0.5f,
                -0.5f,0.5f,-0.5f,
            };

            color = _color;
        }

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
            inds = new ushort[]
            {
                0,1,2,2,3,0,
                3,2,5,5,4,3,
                4,5,6,6,7,4,
                7,6,1,1,0,7,
                7,0,3,3,4,7,
                1,6,5,5,2,1,
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
