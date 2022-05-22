using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramLibrary;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.ECSComponents;
using SimulationSystem.Systems;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.SerializedEditorClasses
{
    public class SkinnedMeshSerializedEditor : SerializedEditorAbstract
    {
        public SkinnedMeshSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Skinned Mesh Renderer Serialized";
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
                    simButton.simObject.entity.RemoveComponent<SkinnedMeshRendererComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(SkinnedMeshSerialized));
                }

            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);
            }
            simButton.componentPanel.TotalInspectorPanelHeight -= 70;
            simButton.componentPanel.Controls.Remove(removeComponentButton);
            controls.Clear();
            base.removeComponentButton_Click(sender,e,name);
        }

        public override void SetComponentInPanel(SerializedComponent serializedCompItem)
        {
            SkinnedMeshSerialized skinnedMeshRendererSerialized = serializedCompItem as SkinnedMeshSerialized;
            SimTextBox serializedText = new SimTextBox();
            serializedText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            serializedText.Text = name;
            serializedText.BackColor = Color.Red;
            serializedText.Size = new Size(150, 60 + simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.BringToFront();
            simButton.componentPanel.Controls.Add(serializedText);
            simButton.componentPanel.TotalInspectorPanelHeight += serializedText.Size.Height;
            controls.Add(serializedText);

            Label skinnedMeshRendLabels = new Label();
            string[] skinnedMeshFiles = Directory.GetDirectories(SimPath.ModelsPath);
            skinnedMeshRendLabels.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            skinnedMeshRendLabels.Text = "SkinnedMesh";
            skinnedMeshRendLabels.BackColor = Color.Yellow;
            skinnedMeshRendLabels.Size = new Size(60, 20);

            simButton.componentPanel.Controls.Add(skinnedMeshRendLabels);
            controls.Add(skinnedMeshRendLabels);

            var type = serializedCompItem.GetType();
            var fields = type.GetFields();
            dynamic obj = fields[0].GetValue(serializedCompItem);
            string scenegltfPath = null;
            if (obj == null)
            {
                FileInfo fileInfo = new FileInfo(skinnedMeshFiles[0]);
                scenegltfPath = fileInfo.Name + Path.DirectorySeparatorChar + "scene.gltf";
                skinnedMeshRendererSerialized.modelPath = scenegltfPath;
            }

            ComboBox skinnedMeshComboBoxes = new ComboBox();
            skinnedMeshComboBoxes.Location = new Point(60, simButton.componentPanel.TotalInspectorPanelHeight - simButton.componentPanel.VerticalScroll.Value);
            if (obj == null)
            {
                FileInfo fileInfo = new FileInfo(skinnedMeshFiles[0]);
                skinnedMeshComboBoxes.Text = fileInfo.Name;
            }
            else
                skinnedMeshComboBoxes.Text = obj;
            skinnedMeshComboBoxes.TextChanged += (sender, e) => skinnedMeshComboBoxes_Changed(sender, e, serializedCompItem);
            skinnedMeshComboBoxes.BackColor = Color.White;

            for (int j = 0; j < skinnedMeshFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(skinnedMeshFiles[j]);
                skinnedMeshComboBoxes.Items.Add(fileInfo.Name.ToString());
            }

            simButton.componentPanel.Controls.Add(skinnedMeshComboBoxes);
            simButton.componentPanel.TotalInspectorPanelHeight += skinnedMeshRendLabels.Height;
            controls.Add(skinnedMeshComboBoxes);
            RemoveComponentButton();
            base.SetComponentInPanel(serializedCompItem);
        }

        private void skinnedMeshComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedCompItem)
        {
            SkinnedMeshSerialized skinnedMeshRendererSerialized = serializedCompItem as SkinnedMeshSerialized;
            dynamic obj = sender;
            string filename =  obj.Text + Path.DirectorySeparatorChar + "scene.gltf";
             
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    skinnedMeshRendererSerialized.modelPath = filename;
                }

            });
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }
    }
}
