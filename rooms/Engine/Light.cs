using System;

namespace Engine
{
    public class Light
    {
        public float[] color;
        public float diffuseIntensity = 0.1f;
        public float ambientIntensity = 0.1f;
        public float[] direction;

        public Light()
        {
            Console.WriteLine($"Initializing {this}");
        }

        public Light(float[] _dir, float[] _color, float _diffuse = 0f, float _ambient = 0f)
        {
            Console.WriteLine($"Initializing {this}");
            direction = _dir;
            color = _color;
            diffuseIntensity = _diffuse;
            ambientIntensity = _ambient;
            Console.WriteLine($"Finished {this} initialization");
        }
    }
}