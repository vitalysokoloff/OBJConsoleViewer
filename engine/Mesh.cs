namespace OrthoGraphEngine
{
    public class Mesh : Entity
    {
        public Mesh(Vector3[] verties, Point[] lines) : base(verties, lines){}

        public override void Draw(GraphicsContext context)
        {
            int q = 0;
            for (int i = 0; i < _lines.Length; i++)
            {
                context.DrawLine('.', _verties[_lines[i].X], _verties[_lines[i].Y]);
            }
        }
    }
}