using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.ECS.Entegration;

namespace SimulationWFA
{
    public class ResetButton : Button
    {
        public SerializedComponent item = null;
        public SimTextBox[] simPosText = new SimTextBox[3];
        public CheckBox checkBox = new CheckBox();
        public int fieldId = 0;
    }
}
