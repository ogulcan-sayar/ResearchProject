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
using SimulationSystem.ECSComponents;
using SimulationSystem.Systems;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.SerializedEditorClasses
{
    public class BoxColliderSerializedEditor : SerializedEditorAbstract
    {
        public BoxColliderSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Box Collider Serialized";
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
                    simButton.simObject.entity.RemoveComponent<ColliderComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(BoxColliderSerialized));
                }
            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);

            }
            controls.Clear();
            simButton.componentPanel.TotalInspectorPanelHeight -= 70;

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

            string[] vecValues = new string[3];
            var type = serializedCompItem.GetType();
            FieldInfo[] field = type.GetFields();

            for (int j = 0; j < 2; j++)
            {
                SimTextBox[] serializedFieldTexs = new SimTextBox[3];
                Vector3 fieldValue = (Vector3)field[j].GetValue(serializedCompItem);
                vecValues[0] = fieldValue.X.ToString();
                vecValues[1] = fieldValue.Y.ToString();
                vecValues[2] = fieldValue.Z.ToString();

                Label fieldName = new Label();
                fieldName.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                fieldName.Size = new Size((int)size.X, (int)size.Y);
                fieldName.Text = field[j].Name;
                fieldName.BackColor = Color.AliceBlue;
                fieldName.BringToFront();
                simButton.componentPanel.Controls.Add(fieldName);
                controls.Add(fieldName);

                ResetButton resButton = new ResetButton();
                resButton.Location = new Point((int)point.X * 2 + fieldName.Size.Width, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                resButton.Size = new Size((int)point.X, (int)point.Y);
                resButton.Text = "Reset";
                resButton.BackColor = Color.White;
                resButton.fieldId = 0;
                resButton.item = serializedCompItem;
                resButton.simPosText = serializedFieldTexs;
                resButton.Click += (sender2, e2) => resetButton_Click(sender2, e2); // new System.EventHandler(resetButton_Click);
                resButton.BringToFront();
                simButton.componentPanel.Controls.Add(resButton);
                controls.Add(resButton);

                for (int i = 0; i < 3; i++)
                {
                    serializedFieldTexs[i] = new SimTextBox();
                    serializedFieldTexs[i].Location = new Point((i * 30 + fieldName.Size.Width), simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                    serializedFieldTexs[i].Text = vecValues[i];
                    serializedFieldTexs[i].BackColor = Color.Yellow;
                    serializedFieldTexs[i].Size = new Size(30, 20);
                    serializedFieldTexs[i].textId = i;
                    serializedFieldTexs[i].fieldId = j;
                    serializedFieldTexs[i].serializedItem = serializedCompItem;
                    serializedFieldTexs[i].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2);
                    serializedFieldTexs[i].BringToFront();
                    simButton.componentPanel.Controls.Add(serializedFieldTexs[i]);
                    controls.Add(serializedFieldTexs[i]);

                }
                simButton.componentPanel.TotalInspectorPanelHeight += 20;
            }
            RemoveComponentButton();
            base.SetComponentInPanel(serializedCompItem);
            // panel.Controls.Add(simButton.componentPanel);
        }

        private void simulationProject_TextChanged(object sender, EventArgs e)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (float.TryParse(textBox.Text, out float result) == false)
            {
                textBox.Text = "0";
            }
            SetItem(textBox);
        }

        private void SetItem(SimTextBox textBox)
        {
            var type = textBox.serializedItem.GetType();
            var fields = type.GetFields();
            dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
            fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItemVector(textBox.textId, textBox.Text, obj));
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private Vector3 InitializeItemVector(int textId, string text, dynamic obj)
        {
            Vector3 itemVec = new Vector3(obj.X, obj.Y, obj.Z);
            float result = 0;
            switch (textId)
            {
                case 0:
                    itemVec.X = float.TryParse(text, out result) ? result : 0;
                    break;
                case 1:
                    itemVec.Y = float.TryParse(text, out result) ? result : 0;
                    break;
                case 2:
                    itemVec.Z = float.TryParse(text, out result) ? result : 0;
                    break;
                default:
                    break;
            }

            return itemVec;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            string text = "0";
            if (resetButton.fieldId == 1)
            {
                text = "1";
            }
            for (int i = 0; i < resetButton.simPosText.Length; i++)
            {
                resetButton.simPosText[i].Text = text;
            }
            ResetItem(resetButton.item, resetButton.fieldId, Convert.ToInt32(text));
        }

        private void ResetItem(SerializedComponent item, int fieldId, int value)
        {
            var type = item.GetType();
            var fields = type.GetFields();
            fields[fieldId].SetValue(item, new Vector3(value, value, value));
        }
    }
}
