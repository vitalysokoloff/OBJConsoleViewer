namespace OBJViewer
{
    public static class Program
    {   
        public static void Main(string[] args)
        {
            string path;
            if (args.Length == 0)
                path = "test.obj";
            else
                path = args[0];
            App app = new App(path);
            app.Start();
        }
    }
}