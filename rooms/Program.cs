using System;
using WebGLDotNET;
using WebAssembly;
using Engine;

class Program
{
    static JSObject window;
    static JSObject canvas;
    static ContentManager contentManager;
    static SceneManager sceneManager;
    static Renderer renderer;
    static KControls kcontrols;

    static void Main()
    {

        WebGLContextAttributes contextAttributes = new WebGLContextAttributes
        {
            Antialias = false,
            PremultipliedAlpha = false,
            Alpha = false,
            PreferLowPowerToHighPerformance = false
        };

        window = (JSObject)Runtime.GetGlobalObject("window");
        var width = (int)window.GetObjectProperty("innerWidth");
        var height = (int)window.GetObjectProperty("innerHeight");

        using (var document = (JSObject)Runtime.GetGlobalObject("document"))
        using (var body = (JSObject)document.GetObjectProperty("body"))
        {
            Console.WriteLine("Creating canvas");
            //canvas = (JSObject)document.Invoke("createElement", "canvas");            
            //body.Invoke("appendChild", canvas);
            canvas = (JSObject)document.Invoke("getElementById", "monocanvas");
            canvas.SetObjectProperty("width", width);
            canvas.SetObjectProperty("height", height);
        }

        WebGLRenderingContext gl = new WebGLRenderingContext(canvas, contextAttributes);

        contentManager = new ContentManager(gl);
        sceneManager = new SceneManager();
        renderer = new Renderer(gl, width, height, sceneManager.currentScene);
        renderer.ChangeViewPortSize(width, height);
        kcontrols = new KControls();

        window.Invoke("update_game");
    }
    void Update(JSObject e)
    {


        kcontrols.up = (bool)e.GetObjectProperty("up");
        kcontrols.down = (bool)e.GetObjectProperty("down");
        kcontrols.left = (bool)e.GetObjectProperty("left");
        kcontrols.right = (bool)e.GetObjectProperty("right");

        sceneManager.currentScene.Update(0.02f, kcontrols);
        renderer.Update(0.02f);

        e.Dispose();
    }

    void ResizeViewport(int w, int h)
    {
        renderer.ChangeViewPortSize(w, h);
    }

    void ChangeDrawPrimitive()
    {
        renderer.drawLines = renderer.drawLines ? false : true;
    }

    void AddCube()
    {
        //renderer.currentScene.AddMesh();
        //renderer.assocAttribs = true;
    }
}

public struct KControls
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;
}