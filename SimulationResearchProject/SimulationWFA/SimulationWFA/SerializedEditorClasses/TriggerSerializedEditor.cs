using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECSEntegration.SerializedComponent;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.ECSComponents;
using SimulationSystem.Systems;

namespace SimulationWFA.SerializedEditorClasses
{
    public class TriggerSerializedEditor : SerializedEditorAbstract
    {
        public TriggerSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Trigger Serialized";
            size = new Vector2(30, 20);
            point = new Vector2(50, 20);
            simButton = hierarchySimButton;
            controls = new List<Control>();
            removeComponentButton = new RemoveComponentButton();
            panel = inspectorPanel;
        }

        public override void RemoveComponentButton()
        {
            removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            removeComponentButton.Size = new Size(140, 20);
            removeComponentButton.Text = "Remove Component";
            removeComponentButton.BackColor = Color.White;
            removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e, name);  //new System.EventHandler(removeComponentButton_Click);
            removeComponentButton.BringToFront();
            simButton.componentPanel.Controls.Add(removeComponentButton);
            controls.Add(removeComponentButton);
            simButton.componentPanel.TotalInspectorPanelHeight += 30;
        }

        public override void removeComponentButton_Click(object sender, EventArgs e, string name)
        {
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    simButton.simObject.entity.RemoveComponent<TriggerComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(TriggerSerialized));
                }
            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);

            }
            simButton.componentPanel.TotalInspectorPanelHeight -= 50;
            controls.Clear();
            base.removeComponentButton_Click(sender, e, name);

        }

        public override void SetComponentInPanel(SerializedComponent serializedCompItem)
        {
            SimTextBox serializedText = new SimTextBox();
            serializedText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            serializedText.Text = name;
            serializedText.BackColor = Color.Red;
            serializedText.Size = new Size(150, 60 + simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.BringToFront();
            simButton.componentPanel.Controls.Add(serializedText);
            simButton.componentPanel.TotalInspectorPanelHeight += serializedText.Size.Height;
            controls.Add(serializedText);

            RemoveComponentButton();
            base.SetComponentInPanel(serializedCompItem);
        }
    }
}
