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
            light.direction = new float[] { 0.0f, 1.0f, 40.0f };
            light.ambientIntensity = 0.9f;
            light.diffuseIntensity = 0.9f;
            light.color = new float[] { 0.9f, 0.9f, 0.9f };

            bgColor = new Vector3(0.3f, 0.3f, 0.3f);
            //lastMsPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            Console.WriteLine($"Finished {this} initialization");
        }

        public virtual void Update(float e, KControls input) { }

        public virtual void AddMesh() { }
    }
}