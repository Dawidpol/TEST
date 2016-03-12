using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAD_CAM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.OpenGLEventArgs args)
        {
            Light light = new Light()
            {
                On = true,
                Name = "light",
                GLCode = OpenGL.GL_LIGHT0
            };
             
            //  Get the OpenGL instance that's been passed to us.
            OpenGL gl = args.OpenGL;

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //  Move the geometry into a fairly central position.
            //gl.Translate(-1.5f, 0.0f, -6.0f);
            gl.Translate(-translateX, translateY, rotatePyramidZ);
            //  Draw a pyramid. First, rotate the modelview matrix.
            //gl.Translate(-translateX, -translateY, -rotatePyramidZ);
            gl.Rotate(rotatePyramidX, 0.0f, -1.0f, 0.0f);
            gl.Rotate(rotatePyramidY, -1.0f, 0.0f, 0.0f);
            //gl.Scale(rotatePyramidZ, rotatePyramidZ, rotatePyramidZ);
            /*float distance = 1;      // Straight line distance between the camera and look at point

            // Calculate the camera position using the distance and angles
            float camX = (float)(distance * -Math.Sin(rotatePyramidX * (Math.PI / 180)) * Math.Cos((rotatePyramidY) * (Math.PI / 180)));
            float camY = (float)(distance * -Math.Sin((rotatePyramidY) * (Math.PI / 180)));
            float camZ = (float)(-distance * Math.Cos((rotatePyramidX) * (Math.PI / 180)) * Math.Cos((rotatePyramidY) * (Math.PI / 180)));

            // Set the camera position and lookat point
            gl.LookAt(camX, camY, camZ,   // Camera position
                      0.0, 0.0, 0.0,    // Look at point
                      0.0, 1.0, 0.0);   // Up vector*/

            //
            //  Start drawing triangles.
            gl.Begin(OpenGL.GL_TRIANGLES);
            
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            
            gl.End();


            gl.LoadIdentity();
            gl.Translate(0.0f, 2.0f, 0.0f);
            gl.Translate(-translateX, translateY, rotatePyramidZ);
            gl.Begin(OpenGL.GL_TRIANGLES);

            gl.Color(1.0f, 0.8f, 0.8f);

            gl.Vertex(0.0f, 0.5f, 0.0f);
            gl.Vertex(-0.5f, -0.5f, 0.5f);
            gl.Vertex(0.0f, -0.5f, -0.5f);

            gl.Vertex(0.0f, 0.5f, 0.0f);
            gl.Vertex(0.0f, -0.5f, -0.5f);
            gl.Vertex(0.5f, -0.5f, 0.0f);


            gl.End();

        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //  Enable the OpenGL depth testing functionality.
            var GL = args.OpenGL;
            
        
            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shininess = { 50.0f };
            float[] light_position = { 0.0f, 2.0f, 0.0f, 0.0f };
            float[] light_ambient = { 0.5f, 0.5f, 0.5f, 1.0f };

            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.ShadeModel(OpenGL.GL_SMOOTH);
            GL.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, mat_specular);
            GL.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, mat_shininess);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light_position);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light_ambient);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, mat_specular);

            GL.Enable(OpenGL.GL_LIGHTING);
            GL.Enable(OpenGL.GL_LIGHT0);
            GL.Enable(OpenGL.GL_DEPTH_TEST);
            GL.Enable(OpenGL.GL_COLOR_MATERIAL);
            GL.Enable(OpenGL.GL_CULL_FACE);
        }

        float rotatePyramidX = 0;
        float rotatePyramidY = 0;
        float rotatePyramidZ = 1.0f;
        float translateX = 0;
        float translateY = 0;
        float rquad = 0;

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            // Get the OpenGL instance.
            OpenGL gl = args.OpenGL;

            // Load and clear the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // Perform a perspective transformation
            gl.Perspective(45.0f, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                0.1f, 100.0f);

            // Load the modelview.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        bool isLeftButtonDown = false;

        private void OpenGLControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isLeftButtonDown = true;
            Cursor = Cursors.SizeAll;
        }

        private void OpenGLControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isLeftButtonDown = false;
            Cursor = Cursors.Arrow;
        }

        bool isRightButtonDown = false;

        private void OpenGLControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            isRightButtonDown = true;
            Cursor = Cursors.SizeAll;
        }

        private void OpenGLControl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            isRightButtonDown = false;
            Cursor = Cursors.Arrow;
        }

        private void OpenGLControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            rotatePyramidZ += e.Delta / 200.0f;
        }

        private void OpenGLControl_MouseLeave(object sender, MouseEventArgs e)
        {
            isLeftButtonDown = false;
            isRightButtonDown = false;
        }

        Point mousePreviousLocation = new Point(0,0);

        private void OpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(sender as IInputElement);
            float deltaX = (float)(mousePreviousLocation.X - mousePosition.X);
            float deltaY = (float)(mousePreviousLocation.Y - mousePosition.Y);

            if (isLeftButtonDown)
            {
                rotatePyramidX += deltaX;
                rotatePyramidY += deltaY; 
            }

            if (isRightButtonDown)
            {
                translateX += deltaX / 75;
                translateY += deltaY / 75;
            }

            mousePreviousLocation = mousePosition;
        }

        
    }
}
