using System;
using System.Numerics;

namespace Engine
{
    public class TestScene : Scene
    {
        public TestScene() : base()
        {
            this.cam.position = new Vector3(0, 0, 10);
            GenerateTestCubes(50);
            //GenerateTestQuads(20);
        }

        public override void Update(float e)
        {
            foreach (Mesh m in objects)
            {
                m.Rotation.Y += e * 2;
                m.Rotation.Z += e * 2;
            }
        }

        private void GenerateTestQuads(int _amount)
        {
            for (int i = 0; i < _amount; i++)
            {
                GenerateTestQuad();
            }
        }

        private void GenerateTestQuad()
        {
            Random r = new Random();
            int span = 3;

            Quad q = new Quad();
            q.color = new Vector3(r.Next(0, 2), r.Next(0, 2), r.Next(0, 2));
            q.Position = new Vector3(r.Next(-span, span), r.Next(-span, span), r.Next(-span, span));
            q.Rotation = new Vector3((float)r.Next(0, 6), (float)r.Next(0, 6), (float)r.Next(0, 6));
            q.Scale = Vector3.One;
            q.CalculateNormals();
            objects.Add(q);
        }

        private void GenerateTestSubes(int _amount)
        {

            for (int i = 0; i < _amount; i++)
            {
                GenerateTestSube();
            }
        }

        private void GenerateTestSube()
        {
            Random r = new Random();
            int span = 3;


            Sube s = new Sube();
            s.color = new Vector3(r.Next(0, 2), r.Next(0, 2), r.Next(0, 2));
            s.Position = new Vector3(r.Next(-span, span), r.Next(-span, span), r.Next(-span, span));
            s.Rotation = new Vector3((float)r.Next(0, 6), (float)r.Next(0, 6), (float)r.Next(0, 6));
            s.Scale = Vector3.One;
            s.CalculateNormals();
            objects.Add(s);
        }

        private void GenerateTestCube()
        {
            Random r = new Random();
            int span = 5;

            Cube c = new Cube();
            c.color = new Vector3(r.Next(0, 2), r.Next(0, 2), r.Next(0, 2));
            c.Position = new Vector3(r.Next(-span, span), r.Next(-span, span), r.Next(-span, span));
            c.Rotation = new Vector3((float)r.Next(0, 6), (float)r.Next(0, 6), (float)r.Next(0, 6));
            c.Scale = Vector3.One;
            c.CalculateNormals();
            objects.Add(c);
        }

        private void GenerateTestQuads()
        {
            Quad c = new Quad(new Vector3(1, 0, 0));
            c.Position = new Vector3(0, 0, 0);
            c.Rotation = Vector3.Zero;
            c.Scale = Vector3.One;
            c.CalculateNormals();

            Quad c1 = new Quad(new Vector3(0, 1, 0));
            c1.Position = new Vector3(-2f, 0, 0);
            c1.Rotation = Vector3.Zero;
            c1.Scale = Vector3.One;
            c1.CalculateNormals();

            Quad c2 = new Quad(new Vector3(0, 0, 1));
            c2.Position = new Vector3(2f, 0, 0);
            c2.Rotation = Vector3.Zero;
            c2.Scale = Vector3.One;
            c2.CalculateNormals();

            Quad c3 = new Quad(new Vector3(1, 1, 0));
            c3.Position = new Vector3(0, 2f, 0);
            c3.Rotation = Vector3.Zero;
            c3.Scale = Vector3.One;
            c3.CalculateNormals();

            Quad c4 = new Quad(new Vector3(1, 0, 1));
            c4.Position = new Vector3(0, -2f, 0);
            c4.Rotation = Vector3.Zero;
            c4.Scale = Vector3.One;
            c4.CalculateNormals();

            objects.Add(c);
            objects.Add(c1);
            objects.Add(c2);
            objects.Add(c3);
            objects.Add(c4);
        }

        private void GenerateTestCubes(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GenerateTestCube();
            }
        }

        private void GenerateTestCubes()
        {
            Cube c = new Cube();
            c.color = new Vector3(1, 0, 0);
            c.Position = new Vector3(0, 0, 0);
            c.Rotation = Vector3.Zero;
            c.Scale = Vector3.One;
            c.CalculateNormals();

            Cube c1 = new Cube();
            c1.color = new Vector3(0, 1, 0);
            c1.Position = new Vector3(-2f, 0, 0);
            c1.Rotation = Vector3.Zero;
            c1.Scale = Vector3.One;
            c1.CalculateNormals();

            Cube c2 = new Cube();
            c2.color = new Vector3(0, 0, 1);
            c2.Position = new Vector3(2f, 0, 0);
            c2.Rotation = Vector3.Zero;
            c2.Scale = Vector3.One;
            c2.CalculateNormals();

            Cube c3 = new Cube(new Vector3(1, 1, 0));
            c3.Position = new Vector3(0, 2f, 0);
            c3.Rotation = Vector3.Zero;
            c3.Scale = Vector3.One;
            c3.CalculateNormals();

            Cube c4 = new Cube();
            c4.color = new Vector3(1, 0, 1);
            c4.Position = new Vector3(0, -2f, 0);
            c4.Rotation = Vector3.Zero;
            c4.Scale = Vector3.One;
            c4.CalculateNormals();

            objects.Add(c);
            objects.Add(c1);
            objects.Add(c2);
            objects.Add(c3);
            objects.Add(c4);
        }

        void GenerateCube()
        {
            Random rand = new Random();

            Cube c = new Cube(new Vector3((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble()))
            {
                Position = new Vector3((float)rand.Next(-10, 10), (float)rand.Next(-10, 10), (float)rand.Next(-10, 10)),
                Rotation = new Vector3((float)rand.Next(0, 6), (float)rand.Next(0, 6), (float)rand.Next(0, 6)),
                Scale = Vector3.One * ((float)rand.NextDouble() + 0.2f)
            };

            objects.Add(c);
        }

    }
}