using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Color
    {
        public static Color Black 
        {
            get
            {
                return new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }

        public static Color White
        {
            get
            {
                return new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
