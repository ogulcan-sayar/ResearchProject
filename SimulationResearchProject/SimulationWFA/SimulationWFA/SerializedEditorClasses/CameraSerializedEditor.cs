using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.Systems;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.SerializedEditorClasses
{
    public class CameraSerializedEditor : SerializedEditorAbstract
    {
        public CameraSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Camera Serialized";
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
            removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e, name);
            removeComponentButton.BringToFront();
            simButton.componentPanel.Controls.Add(removeComponentButton);
            controls.Add(removeComponentButton);
            simButton.componentPanel.TotalInspectorPanelHeight += 30;
        }

        public override void removeComponentButton_Click(object sender, EventArgs e, string name)
        {
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    simButton.simObject.entity.RemoveComponent<CameraComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(CameraSerialized));
                }
            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);

            }
            simButton.componentPanel.TotalInspectorPanelHeight -= 130;
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
            controls.Add(serializedText);
            simButton.componentPanel.TotalInspectorPanelHeight += serializedText.Size.Height;

            var type = serializedCompItem.GetType();
            FieldInfo[] field = type.GetFields();
            Label[] label = new Label[4];
            SimTextBox[] simTextBox = new SimTextBox[4];
            ResetButton[] resetButtons = new ResetButton[4];
            for (int idx = 0; idx < 4; idx++)
            {
                float fieldValue = (float)field[idx].GetValue(serializedCompItem);

                label[idx] = new Label();
                label[idx].Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                label[idx].Size = new Size(90, 20);
                label[idx].Text = field[idx].Name;
                label[idx].BackColor = Color.AliceBlue;
                label[idx].BringToFront();
                simButton.componentPanel.Controls.Add(label[idx]);
                controls.Add(label[idx]);

                object cam = field[idx].GetValue(serializedCompItem);

                simTextBox[idx] = new SimTextBox();
                simTextBox[idx].Location = new Point(label[idx].Size.Width, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                simTextBox[idx].Text = cam.ToString();
                simTextBox[idx].BackColor = Color.Yellow;
                simTextBox[idx].Size = new Size(30, 20);
                simTextBox[idx].textId = 0;
                simTextBox[idx].serializedItem = serializedCompItem;
                simTextBox[idx].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2);
                simTextBox[idx].BringToFront();
                simButton.componentPanel.Controls.Add(simTextBox[idx]);
                controls.Add(simTextBox[idx]);


                resetButtons[idx] = new ResetButton();
                resetButtons[idx].Location = new Point((int)point.X * 2 + 30, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                resetButtons[idx].Text = "Reset";
                resetButtons[idx].Size = new Size((int)point.X, (int)point.Y);
                resetButtons[idx].fieldId = idx;
                resetButtons[idx].item = serializedCompItem;
                resetButtons[idx].simPosText = simTextBox;
                resetButtons[idx].Click += (sender2, e2) => resetButton_Click(sender2, e2);
                resetButtons[idx].BringToFront();
                simButton.componentPanel.Controls.Add(resetButtons[idx]);
                controls.Add(resetButtons[idx]);

                simButton.componentPanel.TotalInspectorPanelHeight += label[idx].Size.Height;
                if (idx == 3)
                {
                    RemoveComponentButton();
                }
            }
            base.SetComponentInPanel(serializedCompItem);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            resetButton.simPosText[resetButton.fieldId].Text = "0";
        }

        private void simulationProject_TextChanged(object sender, EventArgs e)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
            {
                textBox.Text = "0";
            }
            var type = textBox.serializedItem.GetType();
            var fields = type.GetFields();
            object obj = (float)Convert.ToDouble(textBox.Text);  
            fields[textBox.fieldId].SetValue(textBox.serializedItem, obj);
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }
    }
}
