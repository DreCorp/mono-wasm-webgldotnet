using System;
using System.Numerics;


namespace Engine
{
    public class Camera
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 orientation = new Vector3((float)Math.PI, 0f, 0f);
        public float moveSpeed = 0.2f;
        public float mouseSens = 0.0125f;

        public Matrix4x4 GetViewMatrix()
        {
            Vector3 lookAt = new Vector3()
            {
                X = (float)(Math.Sin((float)orientation.X) * Math.Cos((float)orientation.Y)),
                Y = (float)Math.Sin((float)orientation.Y),
                Z = (float)(Math.Cos((float)orientation.X) * Math.Cos((float)orientation.Y))
            };

            return Matrix4x4.CreateLookAt(position, position + lookAt, Vector3.UnitY);
        }

        public void Move(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin((float)orientation.X), 0, (float)Math.Cos((float)orientation.X));

            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset = Vector3.Normalize(offset);
            offset = Vector3.Multiply(offset, moveSpeed);

            position += offset;
        }

        public void AddRotation(float x, float y)
        {
            x = x * mouseSens;
            y = y * mouseSens;

            orientation.X = (orientation.X + x) % ((float)Math.PI * 2.0f);
            orientation.Y = Math.Max(Math.Min(orientation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
    }
}