using System;
using System.Timers;
using WebGLDotNET;
using WebAssembly;
using Engine;
class Program
{
    static JSObject window;
    static JSObject canvas;
    static mEngine mengine;
    static KControls kcontrols;

    static void Main()
    {
        WebGLContextAttributes contextAttributes = new WebGLContextAttributes
        {
            Antialias = false,
            PremultipliedAlpha = false,
            Alpha = false,
            PreferLowPowerToHighPerformance = true
        };

        window = (JSObject)Runtime.GetGlobalObject("window");
        var width = (int)window.GetObjectProperty("innerWidth");
        var height = (int)window.GetObjectProperty("innerHeight");

        using (var document = (JSObject)Runtime.GetGlobalObject("document"))
        using (var body = (JSObject)document.GetObjectProperty("body"))
        {
            Console.WriteLine("Creating canvas");
            canvas = (JSObject)document.Invoke("createElement", "canvas");
            body.Invoke("appendChild", canvas);
            canvas.SetObjectProperty("width", width);
            canvas.SetObjectProperty("height", height);
        }

        CanvasHelper.canvasWidth = width;
        CanvasHelper.canvasHeight = height;

        CanvasHelper.light.direction = new float[] { 0.0f, 0.0f, 40.0f };
        CanvasHelper.light.ambientIntensity = 1f;
        CanvasHelper.light.diffuseIntensity = 1f;
        CanvasHelper.drawLines = false;

        CanvasHelper.gl = new WebGLRenderingContext(canvas, contextAttributes);
        CanvasHelper.light.color = new float[] { 0.9f, 0.9f, 0.9f };
        CanvasHelper.SetCanvasViewportSize(width, height);
        CanvasHelper.SetClearColor(0.3f, 0.3f, 0.3f);

        mengine = new mEngine();

        kcontrols = new KControls();

        window.Invoke("update_stuff");
    }
    static void Update(JSObject e)
    {
        mengine.Update(0.02f);
        if (e.GetObjectProperty("up") != null)
        {

            kcontrols.up = (bool)e.GetObjectProperty("up") ? true : false;
            kcontrols.down = (bool)e.GetObjectProperty("down") ? true : false;
            kcontrols.left = (bool)e.GetObjectProperty("left") ? true : false;
            kcontrols.right = (bool)e.GetObjectProperty("right") ? true : false;

        }

        //Console.WriteLine($"UP:{kcontrols.up}, DOWN: {kcontrols.down}, LEFT: {kcontrols.left}, RIGHT: {kcontrols.right}");
        e.Dispose();
    }

    void Resize(int w, int h)
    {
        CanvasHelper.SetCanvasViewportSize(w, h);
    }

    struct KControls
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }
}