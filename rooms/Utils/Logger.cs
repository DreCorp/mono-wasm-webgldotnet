using WebAssembly;

namespace Utils
{
    static class Logger
    {
        static JSObject console;
        static Logger()
        {
            console = Runtime.GetGlobalObject("console") as JSObject;
            Log("Initializing Logger()");
        }

        public static void Log(string msg)
        {
            console.Invoke("log", msg);
        }
    }
}