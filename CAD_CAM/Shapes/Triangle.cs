using SharpGL;

namespace Models.Shapes
{
    public class Triangle : Shape
    {
        public uint Name;

        public Vertex Vertex_1;
        public Vertex Vertex_2;
        public Vertex Vertex_3;

        public Color Color;

        public Triangle(Vertex vertex_1, Vertex vertex_2, Vertex vertex_3, OpenGL openGL) : base (openGL)
        {
            Vertex_1 = vertex_1;
            Vertex_2 = vertex_2;
            Vertex_3 = vertex_3;

            Color = Color.Black;
        }

        public Triangle(Vertex vertex_1, Vertex vertex_2, Vertex vertex_3, Color color, OpenGL openGL) : base(openGL)
        {
            Vertex_1 = vertex_1;
            Vertex_2 = vertex_2;
            Vertex_3 = vertex_3;

            Color = color;
        }

        public override void Draw()
        {
            OpenGL.LoadName(Name);
            OpenGL.Begin(OpenGL.GL_TRIANGLES);
            OpenGL.Color(Color.R, Color.G, Color.B, Color.A);

            OpenGL.Vertex(Vertex_1.X, Vertex_1.Y, Vertex_1.Z);
            OpenGL.Vertex(Vertex_2.X, Vertex_2.Y, Vertex_2.Z);
            OpenGL.Vertex(Vertex_3.X, Vertex_3.Y, Vertex_3.Z);
            OpenGL.End();
        }
    }
}
