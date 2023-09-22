namespace OrthoGraphEngine
{
    public class KeysController
    {
        public delegate void Handler(ConsoleKey key);
        public event Handler KeyEvent;

        public void Listen()
        {            
            KeyEvent?.Invoke(Console.ReadKey(true).Key);
            Listen();
        }
    }
}