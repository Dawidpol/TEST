using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Shapes
{
    public class Pyramide : Shape
    {
        public int Name = 0;

        private Rectangle _bottom;
        public Rectangle Bottom
        {
            get { return _bottom; }
            set
            {
                _bottom = value;
                SetTriangles();
            }
        }

        private Vertex _top;
        public Vertex Top
        {
            get { return _top; }
            set
            {
                _top = value;
                SetTriangles();
            }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                SetTriangles();
            }
        }

        private Triangle[] sideTriangles = new Triangle[4];



        public Pyramide(Rectangle bottom, Vertex top, OpenGL openGL) : base(openGL)
        {
            _color = Color.Black;
            _bottom = new Rectangle
                (
                    bottom.Vertex_1,
                    bottom.Vertex_2,
                    bottom.Vertex_3,
                    bottom.Vertex_4,
                    _color,
                    OpenGL
                );
             
            _top = top;
            _bottom.Color = _color;

            SetTriangles();
        }

        public Pyramide(Rectangle bottom, Vertex top, Color color, OpenGL openGL) : base(openGL)
        {
            _bottom = bottom;
            _top = top;

            _color = color;
            _bottom.Color = color;
            SetTriangles();
        }
        

        public override void Draw()
        {
        }

        private void SetTriangles()
        {
            var triangles = new Triangle[6];

            triangles[0] = new Triangle(
                    Bottom.Vertex_1,
                    Bottom.Vertex_2,
                    Bottom.Vertex_3,
                    Color,
                    OpenGL
            );

            triangles[1] = new Triangle(
                    Bottom.Vertex_2,
                    Bottom.Vertex_4,
                    Bottom.Vertex_3,
                    Color,
                    OpenGL
            );

            triangles[2] = new Triangle(
                Bottom.Vertex_1,
                Bottom.Vertex_2,
                Top,
                Color,
                OpenGL
                );

            triangles[3] = new Triangle(
                Bottom.Vertex_1,
                Bottom.Vertex_3,
                Top,
                Color,
                OpenGL
                );

            triangles[4] = new Triangle(
                Bottom.Vertex_3,
                Bottom.Vertex_4,
                Top,
                Color,
                OpenGL
                );

            triangles[5] = new Triangle(
                Bottom.Vertex_4,
                Bottom.Vertex_2,
                Top,
                Color,
                OpenGL
                );

            for (int i = 0; i < 6; i++)
            {
                triangles[i].Name = Name + i;
            }
            

        }
    }
}
