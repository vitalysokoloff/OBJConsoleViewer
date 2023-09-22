using  OrthoGraphEngine;

namespace OBJViewer
{
    public class App
    {
        Graphics graphics;
        Entity entity;
        Matrix curProjection;

        float scale = 1;

        public App(string path)
        {
            curProjection = new Matrix(new float[,]{
                                    {1, 0, 0},
                                    {0, 1, 0},
                                    {0, 0, 0}
                                    });
            graphics = new Graphics(120, 30);
            graphics.Context.Projection = curProjection * 1;
            entity = OBJ.Read(path);
        }
        
        public void Start()
        {            
            Thread drawing = new Thread(new ThreadStart(Draw));
            Thread updating = new Thread(new ThreadStart(Update)); 
            KeysController controller = new KeysController();
            controller.KeyEvent += KeyControl;
            Thread listening = new Thread(new ThreadStart(controller.Listen));

            drawing.Start();
            updating.Start();
            listening.Start();
        }

        public void Draw()
        {                      
            graphics.Begin();
            entity.Draw(graphics.Context);
            graphics.Context.DrawString("OBJ Viewer | [Q/W/E] - Front/Left/Top projections, [A/S/D] - X/Y/Z rotate, [-/+] - scale", new Point(4, 28));
            Thread.Sleep(32);
            graphics.End();
            
            Draw();
        }

        public void Update()
        {                           
            Thread.Sleep(32);
            Update();
        }

        public void KeyControl(ConsoleKey key)
        {
            if (key == ConsoleKey.Q) // x-y
            {
                curProjection = new Matrix(new float[,]{
                                    {1, 0, 0},
                                    {0, 1, 0},
                                    {0, 0, 0}
                                    });
                graphics.Context.Projection = curProjection;
            }

            if (key == ConsoleKey.W) // y-z
            {
                curProjection = new Matrix(new float[,]{
                                    {0, 0, 0},
                                    {0, 1, 0},
                                    {0, 0, 1}
                                    });
                graphics.Context.Projection = curProjection;
            }

            if (key == ConsoleKey.E) // x-z
            {
                curProjection = new Matrix(new float[,]{
                                    {1, 0, 0},
                                    {0, 0, 0},
                                    {0, 0, 1}
                                    });
                graphics.Context.Projection = curProjection ;
            }

            if (key == ConsoleKey.A) // x rotate
            {
                entity.Rotate(Vector3.VectorX, 0.05f);
            }

            if (key == ConsoleKey.S) // y rotate
            {
                entity.Rotate(Vector3.VectorY, 0.05f);
            }

            if (key == ConsoleKey.D) // z rotate
            {
                entity.Rotate(Vector3.VectorZ, 0.05f);
            }

            if (key == ConsoleKey.OemPlus) // + scale
            {
                scale += 0.1f;
                graphics.Context.Projection = curProjection * scale;
            }

            if (key == ConsoleKey.OemMinus) // - scale
            {
                scale -= 0.1f;
                graphics.Context.Projection = curProjection * scale;
            }
        }

        public static float CalculateAngle(float sum)
        {
            if (sum == 6.283f)
                sum = 0;

            if (sum > 6.283f)
                sum = CalculateAngle(sum - 6.283f);
            if (sum < 0f)
                sum = CalculateAngle(6.283f + sum);

            return sum;
        }
    }
}