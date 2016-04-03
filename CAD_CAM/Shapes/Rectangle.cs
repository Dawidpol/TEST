using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Shapes
{
    public class Rectangle : Shape
    {
        public Vertex Vertex_1;
        public Vertex Vertex_2;
        public Vertex Vertex_3;
        public Vertex Vertex_4;

        public Color Color;

        public Rectangle(Vertex vertex_1, Vertex vertex_2, Vertex vertex_3, Vertex vertex_4, OpenGL openGL) : base (openGL)
        {
            Vertex_1 = vertex_1;
            Vertex_2 = vertex_2;
            Vertex_3 = vertex_3;
            Vertex_4 = vertex_4;

            Color = Color.Black;
            SortByX();
        }

        public Rectangle(Vertex vertex_1, Vertex vertex_2, Vertex vertex_3, Vertex vertex_4, Color color, OpenGL openGL) : base(openGL)
        {
            Vertex_1 = vertex_1;
            Vertex_2 = vertex_2;
            Vertex_3 = vertex_3;
            Vertex_4 = vertex_4;

            Color = color;
            SortByX();
        }

        private void SortByX()
        {
            List<Vertex> vertexList = new List<Vertex>();
            vertexList.Add( Vertex_1 );
            vertexList.Add( Vertex_2 );
            vertexList.Add( Vertex_3 );
            vertexList.Add( Vertex_4 );

            vertexList.Sort( (vert1, vert2) =>
            {
                return vert1.X.CompareTo(vert2.X);
            });

            Vertex_1 = vertexList[0];
            Vertex_2 = vertexList[1];
            Vertex_3 = vertexList[2];
            Vertex_4 = vertexList[3];
        }

        public override void Draw()
        {
            OpenGL.Begin(OpenGL.GL_TRIANGLES);
            OpenGL.Color(Color.R, Color.G, Color.B, Color.A);

            OpenGL.Vertex(Vertex_1.X, Vertex_1.Y, Vertex_1.Z);
            OpenGL.Vertex(Vertex_2.X, Vertex_2.Y, Vertex_2.Z);
            OpenGL.Vertex(Vertex_3.X, Vertex_3.Y, Vertex_3.Z);

            OpenGL.Vertex(Vertex_2.X, Vertex_2.Y, Vertex_2.Z);
            OpenGL.Vertex(Vertex_3.X, Vertex_3.Y, Vertex_3.Z);
            OpenGL.Vertex(Vertex_4.X, Vertex_4.Y, Vertex_4.Z);
        }
    }
}
