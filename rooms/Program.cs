using System;
using System.Timers;
using WebGLDotNET;
using WebAssembly;
using Engine;

class Program
{
    static JSObject window;
    static JSObject canvas;
    static mEngine mEngine;
    static Timer aTimer;
    void Start()
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
        CanvasHelper.SetLightColorFromHex("#ffffff");
        CanvasHelper.SetCanvasViewportSize(width, height);
        CanvasHelper.SetClearColorFromHex("#000000");

        mEngine = new mEngine();

        aTimer = new Timer(1000 / 30);
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;

    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        mEngine.Update(0.03f);
    }

    void Resize(int w, int h)
    {
        CanvasHelper.SetCanvasViewportSize(w, h);
    }
}