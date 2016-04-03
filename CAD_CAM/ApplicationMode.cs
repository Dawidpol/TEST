using Models.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAD_CAM
{
    public enum AppMode { Drawing, Selecting }
    public static class ApplicationData
    {
        public static AppMode Mode = AppMode.Drawing;
        public static List<Triangle> AllTriangles = new List<Triangle>();
    }
}
