using System;
using System.Collections.Generic;
using System.Numerics;

namespace Engine
{
    public class Scene
    {
        public List<Mesh> objects = new List<Mesh>();
        public Camera cam;

        //Vector2 lastMsPos;
        public Scene()
        {
            Console.WriteLine($"Initializing {this}");
            cam = new Camera();
            cam.position = new Vector3(0, 0, 5);
            //lastMsPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            Console.WriteLine($"Finished {this} initialization");
        }

        public virtual void Update(float e)
        {

        }



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