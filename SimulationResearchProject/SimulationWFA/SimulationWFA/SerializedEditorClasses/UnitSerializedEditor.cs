using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.Systems;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SerializedEditorClasses
{
    public class UnitSerializedEditor : SerializedEditorAbstract
    {
        public UnitSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Unit Serialized";
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
                    simButton.simObject.entity.RemoveComponent<UnitComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(UnitSerialized));
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
            simButton.componentPanel.TotalInspectorPanelHeight += 20;
            controls.Add(serializedText);

            Label[] fieldName = new Label[4];
            string[] vecValues = new string[4];
            var type = serializedCompItem.GetType();
            FieldInfo[] field = type.GetFields();
            ResetButton[] resButtonFloat = new ResetButton[4];
            SimTextBox[] simTextBoxText = new SimTextBox[4];
            for (int i = 0; i < 4; i++)
            {
                Label labelText = new Label();
                labelText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                labelText.Size = new Size(90, 20);
                labelText.Text = field[i].Name;
                labelText.BackColor = Color.AliceBlue;
                labelText.BringToFront();
                simButton.componentPanel.Controls.Add(labelText);
                controls.Add(labelText);
                object o = field[i].GetValue(serializedCompItem);

                simTextBoxText[i] = new SimTextBox();
                simTextBoxText[i].Location = new Point(labelText.Size.Width, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                simTextBoxText[i].Text = o.ToString();
                simTextBoxText[i].BackColor = Color.Yellow;
                simTextBoxText[i].Size = new Size(30, 20);
                simTextBoxText[i].textId = i;
                simTextBoxText[i].fieldId = i;
                simTextBoxText[i].serializedItem = serializedCompItem;
                simTextBoxText[i].TextChanged += (sender3, e3) => simulationProject_TextChanged(sender3, e3, i);
                simTextBoxText[i].BringToFront();
                controls.Add(simTextBoxText[i]);
                simButton.componentPanel.Controls.Add(simTextBoxText[i]);


                resButtonFloat[i] = new ResetButton();
                resButtonFloat[i].Location = new Point((int)point.X * 3 + 30, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                resButtonFloat[i].Size = new Size((int)point.X, (int)point.Y);
                resButtonFloat[i].Text = "Reset";
                resButtonFloat[i].BackColor = Color.White;
                resButtonFloat[i].fieldId = i;
                resButtonFloat[i].item = serializedCompItem;
                resButtonFloat[i].simPosText = simTextBoxText;
                resButtonFloat[i].Click += (sender4, e4) => resetButton_Click(sender4, e4); // new System.EventHandler(resetButton_Click);
                resButtonFloat[i].BringToFront();
                simButton.componentPanel.Controls.Add(resButtonFloat[i]);
                controls.Add(resButtonFloat[i]);

                simButton.componentPanel.TotalInspectorPanelHeight += 20;
            }
            RemoveComponentButton();
            base.SetComponentInPanel(serializedCompItem);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            var type = resetButton.item.GetType();
            var fields = type.GetFields();

            fields[resetButton.fieldId].SetValue(resetButton.item, 0);

            resetButton.simPosText[resetButton.fieldId].Text = "0";

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private void simulationProject_TextChanged(object sender, EventArgs e, int v)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (float.TryParse(textBox.Text, out float result) == false)
            {
                textBox.Text = "0.1";
            }
            SetItem(textBox);
        }

        private void SetItem(SimTextBox textBox)
        {
            var type = textBox.serializedItem.GetType();
            var fields = type.GetFields();
            dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
            fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItem(obj, textBox.Text));
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private object InitializeItem(dynamic obj, string text)
        {
            float val = (float)Convert.ToDouble(text);
            return val;
        }
    }
}
