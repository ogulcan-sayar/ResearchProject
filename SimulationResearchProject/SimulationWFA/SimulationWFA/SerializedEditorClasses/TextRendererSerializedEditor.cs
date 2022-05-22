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
using SimulationSystem.ECSComponents;
using SimulationSystem.Systems;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.SerializedEditorClasses
{
    public class TextRendererSerializedEditor : SerializedEditorAbstract
    {
        public TextRendererSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Text Renderer Serialized";
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

            string[] vecValues = new string[3];
            var type = serializedCompItem.GetType();
            FieldInfo[] field = type.GetFields();

            SimTextBox[] serializedFieldTexs = new SimTextBox[3];
            Vector3 fieldValue = (Vector3)field[0].GetValue(serializedCompItem);
            vecValues[0] = fieldValue.X.ToString();
            vecValues[1] = fieldValue.Y.ToString();
            vecValues[2] = fieldValue.Z.ToString();

            Label fieldName = new Label();
            fieldName.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            fieldName.Size = new Size((int)size.X, (int)size.Y);
            fieldName.Text = field[0].Name;
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
                serializedFieldTexs[i].fieldId = 0;
                serializedFieldTexs[i].serializedItem = serializedCompItem;
                serializedFieldTexs[i].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2);
                serializedFieldTexs[i].BringToFront();
                simButton.componentPanel.Controls.Add(serializedFieldTexs[i]);
                controls.Add(serializedFieldTexs[i]);

            }
            simButton.componentPanel.TotalInspectorPanelHeight += 20;

            SimTextBox[] serializedFieldPosTexs = new SimTextBox[2];
            string[] vecPosValues = new string[2];
            Vector2 fieldVPosalue = (Vector2)field[1].GetValue(serializedCompItem);
            vecPosValues[0] = fieldVPosalue.X.ToString();
            vecPosValues[1] = fieldVPosalue.Y.ToString();

            Label fieldPosName = new Label();
            fieldPosName.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            fieldPosName.Size = new Size((int)size.X, (int)size.Y);
            fieldPosName.Text = field[1].Name;
            fieldPosName.BackColor = Color.AliceBlue;
            fieldPosName.BringToFront();
            simButton.componentPanel.Controls.Add(fieldPosName);
            controls.Add(fieldPosName);

            for (int i = 0; i < 2; i++)
            {
                serializedFieldPosTexs[i] = new SimTextBox();
                serializedFieldPosTexs[i].Location = new Point((i * 30 + fieldName.Size.Width), simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
                serializedFieldPosTexs[i].Text = vecPosValues[i];
                serializedFieldPosTexs[i].BackColor = Color.Yellow;
                serializedFieldPosTexs[i].Size = new Size(30, 20);
                serializedFieldPosTexs[i].textId = i + 3;
                serializedFieldPosTexs[i].fieldId = 1;
                serializedFieldPosTexs[i].serializedItem = serializedCompItem;
                serializedFieldPosTexs[i].TextChanged += (sender2, e2) => simulationProjectVec2_TextChanged(sender2, e2);
                serializedFieldPosTexs[i].BringToFront();
                simButton.componentPanel.Controls.Add(serializedFieldPosTexs[i]);
                controls.Add(serializedFieldPosTexs[i]);
            }
            simButton.componentPanel.TotalInspectorPanelHeight += 20;



            Label fieldFloatName = new Label();
            fieldFloatName.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            fieldFloatName.Size = new Size((int)size.X, (int)size.Y);
            fieldFloatName.Text = field[2].Name;
            fieldFloatName.BackColor = Color.AliceBlue;
            fieldFloatName.BringToFront();
            simButton.componentPanel.Controls.Add(fieldFloatName);
            controls.Add(fieldFloatName);


            SimTextBox simTextBox = new SimTextBox();
            float val = (float)field[2].GetValue(serializedCompItem);
            simTextBox.Location = new Point((fieldName.Size.Width), simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            simTextBox.Text = val.ToString();
            simTextBox.BackColor = Color.Yellow;
            simTextBox.Size = new Size(30, 20);
            simTextBox.textId = 5;
            simTextBox.fieldId = 2;
            simTextBox.serializedItem = serializedCompItem;
            simTextBox.TextChanged += (sender2, e2) => simulationProjectFloat_TextChanged(sender2, e2);
            simTextBox.BringToFront();
            simButton.componentPanel.Controls.Add(simTextBox);
            controls.Add(simTextBox);

            ResetButton resetButton = new ResetButton();
            resetButton.Location = new Point((int)point.X * 2 + fieldName.Size.Width, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            resetButton.Size = new Size((int)point.X, (int)point.Y);
            resetButton.Text = "Reset";
            resetButton.BackColor = Color.White;
            resetButton.fieldId = 2;
            resetButton.item = serializedCompItem;
            resetButton.simPosText[0] = simTextBox;
            resetButton.Click += (sender2, e2) => resetButtonFloat_Click(sender2, e2); // new System.EventHandler(resetButton_Click);
            resetButton.BringToFront();
            simButton.componentPanel.Controls.Add(resetButton);
            controls.Add(resetButton);

            simButton.componentPanel.TotalInspectorPanelHeight += 20;

            Label fieldStringName = new Label();
            fieldStringName.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            fieldStringName.Size = new Size((int)size.X, (int)size.Y);
            fieldStringName.Text = field[3].Name;
            fieldStringName.BackColor = Color.AliceBlue;
            fieldStringName.BringToFront();
            simButton.componentPanel.Controls.Add(fieldStringName);
            controls.Add(fieldStringName);


            SimTextBox simTextStringBox = new SimTextBox();
            string valString = (string)field[3].GetValue(serializedCompItem);
            simTextStringBox.Location = new Point((fieldName.Size.Width), simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            simTextStringBox.Text = valString;
            simTextStringBox.BackColor = Color.Yellow;
            simTextStringBox.Size = new Size(30 + valString.Length, 20);
            simTextStringBox.textId = 5;
            simTextStringBox.fieldId = 2;
            simTextStringBox.serializedItem = serializedCompItem;
            simTextBox.BringToFront();
            simButton.componentPanel.Controls.Add(simTextStringBox);
            controls.Add(simTextStringBox);

            ResetButton resetStringButton = new ResetButton();
            resetStringButton.Location = new Point((int)point.X * 2 + fieldName.Size.Width, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            resetStringButton.Size = new Size((int)point.X, (int)point.Y);
            resetStringButton.Text = "Reset";
            resetStringButton.BackColor = Color.White;
            resetStringButton.fieldId = 3;
            resetStringButton.item = serializedCompItem;
            resetStringButton.simPosText[0] = simTextStringBox;
            resetStringButton.Click += (sender2, e2) => resetButtonString_Click(sender2, e2, valString);
            resetStringButton.BringToFront();
            simButton.componentPanel.Controls.Add(resetStringButton);
            controls.Add(resetStringButton);

            simButton.componentPanel.TotalInspectorPanelHeight += 20;

            RemoveComponentButton();
            base.SetComponentInPanel(serializedCompItem);
        }

        private void simulationProjectVec2_TextChanged(object sender, EventArgs e)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
            {
                textBox.Text = "0";
            }
            var type = textBox.serializedItem.GetType();
            var fields = type.GetFields();
            dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
            fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItemVector2(textBox.textId, textBox.Text, obj));
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private object InitializeItemVector2(int textId, string text, dynamic obj)
        {
            Vector2 itemVec = new Vector2(obj.X, obj.Y);
            int result = 0;
            switch (textId)
            {
                case 0:
                    itemVec.X = Int32.TryParse(text, out result) ? result : 0;
                    break;
                case 1:
                    itemVec.Y = Int32.TryParse(text, out result) ? result : 0;
                    break;
                default:
                    break;
            }

            return itemVec;
        }

        private void resetButtonString_Click(object sender, EventArgs e, string valString)
        {
            ResetButton resetButton = sender as ResetButton;
            resetButton.simPosText[0].Text = valString;

            var type = resetButton.item.GetType();
            var fields = type.GetFields();
            object obj = valString;
            fields[resetButton.fieldId].SetValue(resetButton.item, obj);
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private void resetButtonFloat_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            resetButton.simPosText[0].Text = "0";

            var type = resetButton.item.GetType();
            var fields = type.GetFields();
            object obj = fields[resetButton.fieldId].GetValue(resetButton.item);
            fields[resetButton.fieldId].SetValue(resetButton.item, obj);
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private void simulationProjectFloat_TextChanged(object sender, EventArgs e)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
            {
                textBox.Text = "0";
            }
            var type = textBox.serializedItem.GetType();
            var fields = type.GetFields();
            object obj = (float)Convert.ToDouble(textBox.Text);
            fields[2].SetValue(textBox.serializedItem, obj);
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
                    simButton.simObject.entity.RemoveComponent<TextRendererComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(TextRendererSerialized));
                }
            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);

            }
            controls.Clear();
            simButton.componentPanel.TotalInspectorPanelHeight -= 130;
            base.removeComponentButton_Click(sender, e, name);

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

        private Vector3 InitializeItemVector(int textId, string text, dynamic obj)
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
        }
    }
}
