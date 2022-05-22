using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECSEntegration.SerializedComponent;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.ECSComponents;
using SimulationSystem.Systems;

namespace SimulationWFA.SerializedEditorClasses
{
    public class ParticleSystemSerializedEditor : SerializedEditorAbstract
    {
        public ParticleSystemSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Particle System Serialized";
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

            Label[] fieldName = new Label[3];
            string[] vecValues = new string[3];
            var type = serializedCompItem.GetType();
            FieldInfo[] field = type.GetFields();

            for (int idx = 0; idx < 1; idx++)
            {
                SimTextBox[] serializedFieldTexs = new SimTextBox[3];
                Vector3 fieldValue = (Vector3)field[idx].GetValue(serializedCompItem);
                vecValues[0] = fieldValue.X.ToString();
                vecValues[1] = fieldValue.Y.ToString();
                vecValues[2] = fieldValue.Z.ToString();

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

                for (int i = 0; i < 3; i++)
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

            }

            ResetButton[] resButtonFloat = new ResetButton[2];
            SimTextBox[] simTextBoxText = new SimTextBox[2];
            for (int i = 0; i < 2; i++)
            {
                Label labelText = new Label();
                labelText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                labelText.Size = new Size(90, 20);
                labelText.Text = field[i + 1].Name;
                labelText.BackColor = Color.AliceBlue;
                labelText.BringToFront();
                simButton.componentPanel.Controls.Add(labelText);
                controls.Add(labelText);
                object o = field[i + 1].GetValue(serializedCompItem);

                simTextBoxText[i] = new SimTextBox();
                simTextBoxText[i].Location = new Point(labelText.Size.Width, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                simTextBoxText[i].Text = o.ToString();
                simTextBoxText[i].BackColor = Color.Yellow;
                simTextBoxText[i].Size = new Size(30, 20);
                simTextBoxText[i].textId = 3 + i;
                simTextBoxText[i].fieldId = i + 1;
                simTextBoxText[i].serializedItem = serializedCompItem;
                simTextBoxText[i].TextChanged += (sender3, e3) => simulationProject_TextChangedForFloat(sender3, e3, i * 2 + 2);
                simTextBoxText[i].BringToFront();
                controls.Add(simTextBoxText[i]);
                simButton.componentPanel.Controls.Add(simTextBoxText[i]);


                resButtonFloat[i] = new ResetButton();
                resButtonFloat[i].Location = new Point((int)point.X * 3 + 30, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                resButtonFloat[i].Size = new Size((int)point.X, (int)point.Y);
                resButtonFloat[i].Text = "Reset";
                resButtonFloat[i].BackColor = Color.White;
                resButtonFloat[i].fieldId = i + 1;
                resButtonFloat[i].item = serializedCompItem;
                resButtonFloat[i].simPosText = simTextBoxText;
                resButtonFloat[i].Click += (sender4, e4) => resetButtonForFloat_Click(sender4, e4); // new System.EventHandler(resetButton_Click);
                resButtonFloat[i].BringToFront();
                simButton.componentPanel.Controls.Add(resButtonFloat[i]);
                controls.Add(resButtonFloat[i]);

                simButton.componentPanel.TotalInspectorPanelHeight += 20;
            }

            Label boolLabel = new Label();
            boolLabel.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            boolLabel.Size = new Size(90, 20);
            boolLabel.Text = field[3].Name;
            boolLabel.BackColor = Color.AliceBlue;
            boolLabel.BringToFront();
            simButton.componentPanel.Controls.Add(boolLabel);
            controls.Add(boolLabel);

            CheckBox checkBox = new CheckBox();
            checkBox.Location = new Point((int)point.X * 2, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            checkBox.Size = new Size(20, 20);
            object check = field[3].GetValue(serializedCompItem);
            if (check.ToString() == "True")
                checkBox.Checked = true;
            else
                checkBox.Checked = false;

            checkBox.BringToFront();
            simButton.componentPanel.Controls.Add(checkBox);
            controls.Add(checkBox);
            checkBox.CheckedChanged += (sender4, e4) => checkBoxChanged_Click(sender4, e4, serializedCompItem);

            ResetButton resCheckButton = new ResetButton();
            resCheckButton.Location = new Point((int)point.X * 3 + 30, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            resCheckButton.Size = new Size((int)point.X, (int)point.Y);
            resCheckButton.Text = "Reset";
            resCheckButton.BackColor = Color.White;
            resCheckButton.fieldId = 4;
            resCheckButton.item = serializedCompItem;
            resCheckButton.checkBox = checkBox;
            resCheckButton.Click += (sender5, e5) => resetButtonForBoolean_Click(sender5, e5);
            resCheckButton.BringToFront();
            simButton.componentPanel.Controls.Add(resCheckButton);
            controls.Add(resCheckButton);

            simButton.componentPanel.TotalInspectorPanelHeight += 20;
            RemoveComponentButton();
            base.SetComponentInPanel(serializedCompItem);
        }

        private void resetButtonForBoolean_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            resetButton.checkBox.Checked = !resetButton.checkBox.Checked;

            var type = resetButton.item.GetType();
            var fields = type.GetFields();
            fields[3].SetValue(resetButton.item, resetButton.checkBox.Checked);

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private void checkBoxChanged_Click(object sender, EventArgs e, SerializedComponent serializedCompItem)
        {
            CheckBox checkBox = sender as CheckBox;

            var type = serializedCompItem.GetType();
            var fields = type.GetFields();
            fields[3].SetValue(serializedCompItem, checkBox.Checked);

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private void resetButtonForFloat_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            var type = resetButton.item.GetType();
            var fields = type.GetFields();

            fields[resetButton.fieldId].SetValue(resetButton.item, 0);

            resetButton.simPosText[resetButton.fieldId - 1].Text = "0";

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private void simulationProject_TextChangedForFloat(object sender, EventArgs e, int v)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false || (Int32.Parse(textBox.Text) == 0 && textBox.textId == 3))
            {
                textBox.Text = "0.1";
            }
            SetFloatItem(textBox);
        }

        private void SetFloatItem(SimTextBox textBox)
        {
            var type = textBox.serializedItem.GetType();
            var fields = type.GetFields();
            dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
            fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItemFloat(obj, textBox.Text));
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private object InitializeItemFloat(dynamic obj, string text)
        {
            float val = (float)Convert.ToDouble(text);
            return val;
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

        private object InitializeItemVector(int textId, string text, dynamic obj)
        {
            Vector3 itemVec = new Vector3(obj.X, obj.Y, obj.Z);
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

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
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
                    simButton.simObject.entity.RemoveComponent<ParticleComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(ParticleSerialized));
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
