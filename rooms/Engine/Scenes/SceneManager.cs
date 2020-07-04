using System;

namespace Engine
{
    public class SceneManager
    {
        public Scene currentScene;
        public SceneManager()
        {
            Console.WriteLine($"Initializing {this}");
            currentScene = new TestScene();
            Console.WriteLine($"Finished {this} initialization");
        }
    }
}