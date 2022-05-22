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
    public class PointLightSerializedEditor : SerializedEditorAbstract
    {
        public PointLightSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Point Light Serialized";
            size = new Vector2(30, 20);
            point = new Vector2(50, 20);
            simButton = hierarchySimButton;
            controls = new List<Control>();
            removeComponentButton = new RemoveComponentButton();
            panel = inspectorPanel;
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

            ResetButton[] resButton = new ResetButton[4];

            Label[] fieldName = new Label[4];
            string[] vecValues = new string[4];
            var type = serializedCompItem.GetType();
            FieldInfo[] field = type.GetFields();

            for (int idx = 0; idx < 3; idx++)
            {
                SimTextBox[] serializedFieldTexs = new SimTextBox[4];
                Vector4 fieldValue = (Vector4)field[idx].GetValue(serializedCompItem);
                vecValues[0] = fieldValue.X.ToString();
                vecValues[1] = fieldValue.Y.ToString();
                vecValues[2] = fieldValue.Z.ToString();
                vecValues[3] = fieldValue.W.ToString();

                fieldName[idx] = new Label();
                fieldName[idx].Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                fieldName[idx].Size = new Size((int)size.X, (int)size.Y);
                fieldName[idx].Text = field[idx].Name;
                fieldName[idx].BackColor = Color.AliceBlue;
                fieldName[idx].BringToFront();
                simButton.componentPanel.Controls.Add(fieldName[idx]);
                controls.Add(fieldName[idx]);

                resButton[idx] = new ResetButton();
                resButton[idx].Location = new Point((int)point.X * 3 + fieldName[idx].Size.Width, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                resButton[idx].Size = new Size((int)point.X, (int)point.Y);
                resButton[idx].Text = "Reset";
                resButton[idx].BackColor = Color.White;
                resButton[idx].fieldId = idx;
                resButton[idx].item = serializedCompItem;
                resButton[idx].simPosText = serializedFieldTexs;
                resButton[idx].Click += (sender2, e2) => resetButton_Click(sender2, e2); // new System.EventHandler(resetButton_Click);
                resButton[idx].BringToFront();
                simButton.componentPanel.Controls.Add(resButton[idx]);
                controls.Add(resButton[idx]);

                for (int i = 0; i < 4; i++)
                {
                    serializedFieldTexs[i] = new SimTextBox();
                    serializedFieldTexs[i].Location = new Point((i * 30 + fieldName[idx].Size.Width), simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                    serializedFieldTexs[i].Text = vecValues[i];
                    serializedFieldTexs[i].BackColor = Color.Yellow;
                    serializedFieldTexs[i].Size = new Size(30, 20);
                    serializedFieldTexs[i].textId = i;
                    serializedFieldTexs[i].fieldId = idx;
                    serializedFieldTexs[i].serializedItem = serializedCompItem;
                    serializedFieldTexs[i].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2);
                    serializedFieldTexs[i].BringToFront();
                    simButton.componentPanel.Controls.Add(serializedFieldTexs[i]);
                    controls.Add(serializedFieldTexs[i]);

                }
                simButton.componentPanel.TotalInspectorPanelHeight += 20;

                if (idx == 2)
                {
                    RemoveComponentButton();
                }
            }
            base.SetComponentInPanel(serializedCompItem);
        }

        private void simulationProject_TextChanged(object sender, EventArgs e)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
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

        private Vector4 InitializeItemVector(int textId, string text, dynamic obj)
        {
            Vector4 itemVec = new Vector4(obj.X, obj.Y, obj.Z, obj.W);
            int result = 0;
            switch (textId)
            {
                case 0:
                    itemVec.X = Int32.TryParse(text, out result) ? result : 0;
                    break;
                case 1:
                    itemVec.Y = Int32.TryParse(text, out result) ? result : 0;
                    break;
                case 2:
                    itemVec.Z = Int32.TryParse(text, out result) ? result : 0;
                    break;
                case 3:
                    itemVec.W = Int32.TryParse(text, out result) ? result : 0;
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
            if (resetButton.fieldId == 1) //    VECTOR 4 İÇİN DÜZENLE
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
            fields[fieldId].SetValue(item, new Vector4(value, value, value, value));
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
                    simButton.simObject.entity.RemoveComponent<PointLightComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(PointLightSerialized));
                }
            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);

            }
            simButton.componentPanel.TotalInspectorPanelHeight -= 110;
            controls.Clear();
            base.removeComponentButton_Click(sender, e, name);

        }

    }
}
