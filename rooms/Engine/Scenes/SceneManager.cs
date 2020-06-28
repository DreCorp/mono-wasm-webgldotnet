using System;

namespace Engine
{
    public class SceneManager
    {

        public delegate void SceneChange(Scene newScene);
        public static event SceneChange OnSceneChanged;
        public Scene currentScene;
        public SceneManager()
        {
            Console.WriteLine($"Initializing {this}");
            currentScene = new TestScene();
            Console.WriteLine($"Finished {this} initialization");
        }

        public void ChangeScene()
        {

        }
    }
}