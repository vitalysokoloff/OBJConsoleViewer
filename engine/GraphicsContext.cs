namespace OrthoGraphEngine
{
    public class GraphicsContext
    {
        public char[] PixelBuffer { get; protected set;}
        public int Width { get; }
        public int Height { get; }
        public Matrix Projection {get; set;}
        public float Aspect { get; } // соотношение сторон консоли
        public float PixelAspect { get;}
        protected float _totalAspect;
        protected Point _basisDelta;

        public GraphicsContext(int width, int height)
        {
            Width = width;
            Height = height;
            PixelBuffer = new char[Width * Height];
            Projection = new Matrix(new float[,]{
                                    {1, 0, 0},
                                    {0, 1, 0},
                                    {0, 0, 0}
                                    }); // Плоскость x-y
            Aspect = (float)width / height;
            PixelAspect = 8.0f / 16.0f;  
            _totalAspect = Aspect * PixelAspect;
            _basisDelta = new Point((int)(Width / 2), (int)(Height / 2));        
        }

        public void Reset()
        {
            PixelBuffer = new char[Width * Height];
        }

        public void DrawLine(char texture, Vector3 va, Vector3 vb)
        {
            va = (Projection * va).ToVector3();
            vb = (Projection * vb).ToVector3(); 
            Vector3 kostyl = (Projection * new Vector3(1, 1, 1)).ToVector3();
 
            float vx = kostyl.X == 0? va.Y : va.X;
            float vy = kostyl.Z == 0? va.Y : va.Z;
            Point a = new Point((int)(vx * _totalAspect), (int)vy) + _basisDelta;

            vx = vb.X == 0? vb.Y : vb.X;
            vy = vb.Z == 0? vb.Y : vb.Z;
            Point b = new Point((int)(vx * _totalAspect), (int)vy) + _basisDelta; 


            //https://ru.wikipedia.org/wiki/Алгоритм_Брезенхэма
            //https://ru.wikibooks.org/wiki/Реализации_алгоритмов/Алгоритм_Брезенхэма

            int deltaX = Math.Abs(b.X - a.X);
            int deltaY = Math.Abs(b.Y - a.Y);
            int signX = Sign(b.X - a.X);
            int signY = Sign(b.Y - a.Y);
            int error = deltaX - deltaY;
            int coor = b.X + b.Y * Width;             
            int x = a.X, y = a.Y;

            if (x > Width && signX > 0) // Линия вышла за пределы вправо и продолжает движение вправо
                return;
            if (x < 0 && signX < 0) // Линия вышла за пределы влево и продолжает движение влево
                return;            
            
            while (x != b.X || y != b.Y )
            {
                coor = x + y * Width;
                if (coor < PixelBuffer.Length && coor > 0)
                {
                    if (x > 0 && x < Width)
                        PixelBuffer[coor] = texture;  // х находится в  границах
                }
                    
                int error2 = error * 2;
                
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x += signX;
                }
                
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y += signY;
                }
            }

            int Sign(int x)
            {
                return (x > 0) ? 1 : (x < 0) ? -1 : 0;
            }
        }

        public void DrawString(string text, Point position)
        {
            for (int i = 0; i < text.Length; i++)
                PixelBuffer[position.X + position.Y * Width + i] = text[i];
        }
    }
}