namespace OrthoGraphEngine
{
    public class Vector3
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }

        static public Vector3 VectorX 
        {
            get
            {
                return new Vector3(1, 0, 0);
            }
        }

        static public Vector3 VectorY 
        {
            get
            {
                return new Vector3(0, 1, 0);
            }
        }

        static public Vector3 VectorZ 
        {
            get
            {
                return new Vector3(0, 0, 1);
            }
        }

        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator + (Vector3 a, Vector3 b)
        {
            return new Vector3 { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };
        }

        public static Vector3 operator - (Vector3 a, Vector3 b)
        {
            return new Vector3 { X = a.X - b.X, Y = a.Y - b.Y, Z = a.Z - b.Z};
        }
    }
}