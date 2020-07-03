using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;

namespace Engine
{
    public class Scene
    {
        public List<Mesh> objects = new List<Mesh>();
        public Camera cam;
        public Light light;

        public Vector3 bgColor;

        //Vector2 lastMsPos;
        public Scene()
        {
            Console.WriteLine($"Initializing {this}");

            cam = new Camera();
            cam.position = new Vector3(0, 0, 5);

            light = new Light();
            light.direction = new float[] { 0.0f, 0.0f, 40.0f };
            light.ambientIntensity = 0.9f;
            light.diffuseIntensity = 0.9f;
            light.color = new float[] { 0.9f, 0.9f, 0.9f };

            bgColor = new Vector3(0.3f, 0.3f, 0.3f);
            //lastMsPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            Console.WriteLine($"Finished {this} initialization");
        }

        public virtual void Update(float e, KControls input) { }

        public virtual void AddMesh() { }



        /*        void ProcessInput()
                {
                    if (InputManager.currentKState.IsKeyDown(Key.W))
                    {
                        cam.Move(0f, 0.1f, 0f);
                    }
                    if (InputManager.currentKState.IsKeyDown(Key.S))
                    {
                        cam.Move(0f, -0.1f, 0f);
                    }
                    if (InputManager.currentKState.IsKeyDown(Key.A))
                    {
                        cam.Move(-0.1f, 0f, 0f);
                    }
                    if (InputManager.currentKState.IsKeyDown(Key.D))
                    {
                        cam.Move(0.1f, 0f, 0f);
                    }
                    if (InputManager.currentKState.IsKeyDown(Key.E))
                    {
                        cam.Move(0f, 0f, 0.1f);
                    }
                    if (InputManager.currentKState.IsKeyDown(Key.Q))
                    {
                        cam.Move(0f, 0f, -0.1f);
                    }
                    if (InputManager.KeyPressed(Key.Space))
                    {
                        GenerateModel("icosphere");
                        //GenerateModel("bb1");
                    }
                    if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        Vector2 delta = lastMsPos - new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                        lastMsPos += delta;

                        cam.AddRotation(delta.X, delta.Y);
                        lastMsPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                    }
                }
                */
    }
}