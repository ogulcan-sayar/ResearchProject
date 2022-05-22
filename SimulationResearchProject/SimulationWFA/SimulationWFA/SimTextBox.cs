using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.ECS.Entegration;

namespace SimulationWFA
{
    public class SimTextBox : TextBox
    {
        public SerializedComponent serializedItem = null;
        public int textId = 0;
        public int fieldId = 0;

        public SimTextBox()
        {
        }
    }
}
