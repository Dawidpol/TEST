using Models.Shapes;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Lights;
using System.Windows;
using System.Windows.Input;
using Models;

namespace CAD_CAM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int selectBufferSize = 100;
        uint[] selectBuffer = new uint[selectBufferSize];
        public MainWindow()
        {
            InitializeComponent();

        }
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.OpenGLEventArgs args)
        {
           //  Get the OpenGL instance that's been passed to us.
            OpenGL gl = args.OpenGL;

            gl.RenderMode(OpenGL.GL_RENDER);
            int nhits = 0;
            if (ApplicationData.Mode == AppMode.Selecting)
            {
                gl.RenderMode(OpenGL.GL_SELECT);
                Display(gl);
                nhits = gl.RenderMode(OpenGL.GL_RENDER);
                ApplicationData.Mode = AppMode.Drawing;
            }
            else
            {
                Display(gl);
            }

            if(nhits != 0)
                for (int i = 0, index = 0; i < nhits; i++)
                {
                    var nitems = selectBuffer[index++];
                    var zmin = selectBuffer[index++];
                    var zmax = selectBuffer[index++];

                    System.Console.WriteLine("Hit # {0} found {1} items on the name stack\n", i, nitems);
                    System.Console.WriteLine("\tZmin = {0}, Zmax = {1}\n", zmin, zmax);

                    for (int j = 0; j < nitems; j++)
                    {
                        var item = selectBuffer[index++];

                        ApplicationData.AllTriangles[(int)item].Color = Color.Red;

                        System.Console.WriteLine("\t{0} Item Name: {1}\n", j, item);
                    }
                    
                }

        }

        public void Display(OpenGL gl)
        {
            int[] viewport = new int[4];
            
            double dx;
            double dy;
            
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

            if (ApplicationData.Mode == AppMode.Selecting)
            {
                gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
                dx = viewport[2];
                dy = viewport[3];

                gl.PickMatrix(mousePreviousLocation.X, mousePreviousLocation.Y, 5, 5, viewport);

                gl.RenderMode(OpenGL.GL_SELECT);
            }


            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.

            //  Move the geometry into a fairly central position.
            gl.Translate(-translateX, translateY, rotatePyramidZ);

            //  Draw a pyramid. First, rotate the modelview matrix.
            gl.Rotate(rotatePyramidX, 0.0f, -1.0f, 0.0f);
            gl.Rotate(rotatePyramidY, -1.0f, 0.0f, 0.0f);

            if (ApplicationData.Mode == AppMode.Selecting)
            {
                gl.InitNames();
                gl.PushName(0xffffffff);
            }

            uint i = 0;
            foreach (var triangle in ApplicationData.AllTriangles)
            {
                triangle.Name = i++;
                triangle.Draw();
            }
            
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

            GL.SelectBuffer(selectBufferSize, selectBuffer);


            var bottom = new Rectangle(
                new Vertex(-1.0f, -1.0f, 1.0f),
                new Vertex(-1.0f, -1.0f, -1.0f),
                new Vertex(1.0f, -1.0f, 1.0f),
                new Vertex(1.0f, -1.0f, -1.0f),
                null,
                null
                );

            Pyramide pyramide = new Pyramide(
                bottom,
                new Vertex(0.0f, 1.0f, 0.0f),
                new Color(200, 100, 50, 255),
                GL
                );

            //pyramide.Draw();
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

        Point mousePreviousLocation = new Point(0, 0);

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

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Mode = ApplicationData.Mode == AppMode.Selecting ? AppMode.Drawing : AppMode.Selecting;
        }

        private void OpenGLControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ApplicationData.Mode = ApplicationData.Mode == AppMode.Selecting ? AppMode.Drawing : AppMode.Selecting;
        }
    }
}
