using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem;

namespace SimulationWFA
{
    public class HierarchySimButton : Button
    {


        public SimObject simObject;
        public int id;
        public ComponentPanel componentPanel = new ComponentPanel();
        public List<string> serializedComponentList = new List<string>();
        public HierarchySimButton() { }
        public HierarchySimButton(SimObject simObject, int id, ComponentPanel componentPanel, List<string> serializedComponentList)
        {
            this.simObject = simObject;
            this.id = id;
            this.componentPanel = componentPanel;
            this.serializedComponentList = serializedComponentList;
        }
    }
}
