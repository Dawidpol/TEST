using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Shapes
{
    public abstract class Shape
    {
        public Shape(OpenGL openGL)
        {
            _openGL = openGL;
        }

        private OpenGL _openGL;
        public OpenGL OpenGL
        {
            get { return _openGL; }
            set { _openGL = value; }
        }
        public abstract void Draw();
    }
}
