using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramLibrary;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.SharedData;
using SimulationSystem.Systems;
using SimulationWFA.MespSimulationSystem.ProgramLibrary;
using SimulationWFA.MespUtils;
using SimulationWFA.SerializedEditorClasses;
using TheSimulation.SerializedComponent;

namespace SimulationWFA
{
    public class SerializedEditor
    {

        public SerializedEditor()
        {


        }

        public List<SimTextBox> serializedTexts = new List<SimTextBox>();

        public List<SimTextBox[]> transformSerializedCompTexts = new List<SimTextBox[]>();
        public List<Label[]> meshSerializedCompLabels = new List<Label[]>();

        private Vector2 serializedComponentName = new Vector2(150, 60);
        private Vector2 vectorLocation = new Vector2(30, 20);
        private Vector2 vectorSize = new Vector2(30, 20);
        private Vector2 resetButtonLocation = new Vector2(100, 20);
        private Vector2 resetButtonSize = new Vector2(50, 20);

        private void simulationProject_TextChanged(object sender, EventArgs e, HierarchySimButton simButton)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
            {
                textBox.Text = "0";
            }
            SerializedEditor.SetItem(textBox, simButton);
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            for (int i = 0; i < resetButton.simPosText.Length; i++)
            {
                resetButton.simPosText[i].Text = "0";
            }
            SerializedEditor.ResetItem(resetButton.item, resetButton.fieldId);
        }

        public void SetSerializedItemOnEditor(SerializedComponent serializedCompItem, HierarchySimButton simButton, Panel inspectorPanel, int totalCompLenght)
        {
            //int idx = 0;
            //var type = serializedCompItem.GetType();
            //FieldInfo[] fields = type.GetFields();

            //List<Control> vec3Controls = new List<Control>();
            //List<Control> vec4Controls = new List<Control>();
            //List<Control> meshControls = new List<Control>();
            //List<Control> singleCamControls = new List<Control>();
            //List<Control> singleControls = new List<Control>();
            //List<Control> singleTextControls = new List<Control>();
            // PrepareSerializedCompName(simButton, serializedCompItem, vec3Controls, meshControls, singleControls, vec4Controls, singleCamControls, singleTextControls);

            //foreach (var field in fields)
            //{
            string name = serializedCompItem.GetName();
            switch (serializedCompItem.GetName())
            {
                case "Transform Serialized":
                    {
                        TransformSerializedEditor transformSerializedEditor = new TransformSerializedEditor(simButton, inspectorPanel);
                        transformSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Camera Serialized":
                    {
                        CameraSerializedEditor cameraSerialized = new CameraSerializedEditor(simButton, inspectorPanel);
                        cameraSerialized.SetComponentInPanel(serializedCompItem);
                        break;

                    }
                case "Box Collider Serialized":
                    {
                        BoxColliderSerializedEditor boxColliderSerialized = new BoxColliderSerializedEditor(simButton, inspectorPanel);
                        boxColliderSerialized.SetComponentInPanel(serializedCompItem);
                        break;

                    }
                case "Directional Light Serialized":
                    {
                        DirectionalLightSerializedEditor directionalLightSerializedEditor = new DirectionalLightSerializedEditor(simButton, inspectorPanel);
                        directionalLightSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;

                    }
                case "Mesh Renderer Serialized":
                    {
                        MeshRendererSerializedEditor meshRendererSerializedEditor = new MeshRendererSerializedEditor(simButton, inspectorPanel);
                        meshRendererSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Point Light Serialized":
                    {
                        PointLightSerializedEditor pointLightSerializedEditor = new PointLightSerializedEditor(simButton, inspectorPanel);
                        pointLightSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Spot Light Serialized":
                    {
                        SpotLightSerializedEditor spotLightSerializedEditor = new SpotLightSerializedEditor(simButton, inspectorPanel);
                        spotLightSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Particle Serialized":
                    {
                        ParticleSystemSerializedEditor particleSystemSerializedEditor = new ParticleSystemSerializedEditor(simButton, inspectorPanel);
                        particleSystemSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Trigger Serialized":
                    {
                        TriggerSerializedEditor triggerSerializedEditor = new TriggerSerializedEditor(simButton, inspectorPanel);
                        triggerSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Text Renderer Serialized":
                    {
                        TextRendererSerializedEditor textRendererSerializedEditor = new TextRendererSerializedEditor(simButton, inspectorPanel);
                        textRendererSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Obstacle Serialized":
                    {
                        ObstacleSerializedEditor obstacleSerializedEditor = new ObstacleSerializedEditor(simButton, inspectorPanel);
                        obstacleSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Target Serialized":
                    {
                        TargetSerializedEditor targetSerializedEditor = new TargetSerializedEditor(simButton, inspectorPanel);
                        targetSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Unit Serialized":
                    {
                        UnitSerializedEditor unitSerializedEditor = new UnitSerializedEditor(simButton, inspectorPanel);
                        unitSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                case "Skinned Mesh Serialized":
                    {
                        SkinnedMeshSerializedEditor skinnedMeshSerializedEditor = new SkinnedMeshSerializedEditor(simButton, inspectorPanel);
                        skinnedMeshSerializedEditor.SetComponentInPanel(serializedCompItem);
                        break;
                    }
                //case "Mesh":
                //    {
                //        PrepareMeshCase(simButton, serializedCompItem, meshControls);
                //        break;
                //    }
                //case "Material":
                //    {
                //        PrepareMaterialCase(simButton, serializedCompItem, vec3Controls, meshControls);
                //        break;
                //    }
                //case "Single":
                //    {
                //        PrepareFloatCase(simButton, serializedCompItem, field, singleControls, singleCamControls, singleTextControls, idx);
                //        break;
                //    }
                //case "Vector4":
                //    {
                //        PrepareVector4Case(field, idx, serializedCompItem, simButton, vec4Controls);
                //        break;
                //    }

                default:
                    break;
                    //}
                    //idx++;
            }

            //simButton.componentPanel.TotalInspectorPanelHeight += 20;
            //inspectorPanel.Controls.Add(simButton.componentPanel);

        }

        private void PrepareVector4Case(FieldInfo field, int idx, SerializedComponent serializedCompItem, HierarchySimButton simButton, List<Control> vec4Controls)
        {
            ResetButton[] resButton = new ResetButton[4];
            Label[] fieldName = new Label[4];
            string[] vecValues = new string[4];

            Vector4 fieldValue = (Vector4)field.GetValue(serializedCompItem);
            vecValues[0] = fieldValue.X.ToString();
            vecValues[1] = fieldValue.Y.ToString();
            vecValues[2] = fieldValue.Z.ToString();
            vecValues[3] = fieldValue.W.ToString();

            SimTextBox[] serializedFieldTexs = new SimTextBox[4];

            fieldName[idx] = new Label();
            fieldName[idx].Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            fieldName[idx].Size = new Size(30, 20);
            fieldName[idx].Text = field.Name;
            fieldName[idx].BackColor = Color.AliceBlue;
            fieldName[idx].BringToFront();
            vec4Controls.Add(fieldName[idx]);
            simButton.componentPanel.Controls.Add(fieldName[idx]);

            resButton[idx] = new ResetButton();
            resButton[idx].Location = new Point((int)resetButtonLocation.X + fieldName[idx].Size.Width * 2, simButton.componentPanel.TotalInspectorPanelHeight);
            resButton[idx].Size = new Size((int)resetButtonSize.X, (int)resetButtonSize.Y);
            resButton[idx].Text = "Reset";
            resButton[idx].BackColor = Color.White;
            resButton[idx].fieldId = idx;
            resButton[idx].item = serializedCompItem;
            resButton[idx].simPosText = serializedFieldTexs;
            resButton[idx].Click += new System.EventHandler(resetButton_Click);
            resButton[idx].BringToFront();
            vec4Controls.Add(resButton[idx]);
            simButton.componentPanel.Controls.Add(resButton[idx]);

            for (int i = 0; i < 4; i++)
            {
                serializedFieldTexs[i] = new SimTextBox();
                serializedFieldTexs[i].Location = new Point((i * (int)vectorLocation.X + fieldName[idx].Size.Width), simButton.componentPanel.TotalInspectorPanelHeight);
                serializedFieldTexs[i].Text = vecValues[i];
                serializedFieldTexs[i].BackColor = Color.Yellow;
                serializedFieldTexs[i].Size = new Size((int)vectorSize.X, (int)vectorSize.Y);
                serializedFieldTexs[i].textId = i;
                serializedFieldTexs[i].fieldId = idx;
                serializedFieldTexs[i].serializedItem = serializedCompItem;
                serializedFieldTexs[i].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2, simButton);
                serializedFieldTexs[i].BringToFront();
                vec4Controls.Add(serializedFieldTexs[i]);
                simButton.componentPanel.Controls.Add(serializedFieldTexs[i]);
            }
            simButton.componentPanel.TotalInspectorPanelHeight += (int)vectorSize.Y;

            if (idx == 2)
            {
                RemoveComponentButton removeComponentButton = new RemoveComponentButton();
                removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
                removeComponentButton.Size = new Size(110, 20);
                removeComponentButton.Text = "RemoveComponent";
                removeComponentButton.BackColor = Color.White;
                removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e, simButton, serializedCompItem, vec4Controls);  //new System.EventHandler(removeComponentButton_Click);
                removeComponentButton.BringToFront();
                simButton.componentPanel.Controls.Add(removeComponentButton);
                simButton.componentPanel.TotalInspectorPanelHeight += 20;
            }
        }

        private void PrepareFloatCase(HierarchySimButton simButton, SerializedComponent serializedCompItem, FieldInfo field, List<Control> floatControls, List<Control> singleCamControls, List<Control> singleTextControls, int idx)
        {
            string name = serializedCompItem.GetName();
            switch (name)
            {
                case "Camera Serialized":
                    Label labelCam = new Label();
                    labelCam.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
                    labelCam.Size = new Size(90, 20);
                    labelCam.Text = field.Name;
                    labelCam.BackColor = Color.AliceBlue;
                    labelCam.BringToFront();
                    singleCamControls.Add(labelCam);
                    simButton.componentPanel.Controls.Add(labelCam);

                    object cam = field.GetValue(serializedCompItem);

                    SimTextBox simTextBoxCam = new SimTextBox();
                    simTextBoxCam.Location = new Point(labelCam.Size.Width, simButton.componentPanel.TotalInspectorPanelHeight);
                    simTextBoxCam.Text = cam.ToString();
                    simTextBoxCam.BackColor = Color.Yellow;
                    simTextBoxCam.Size = new Size(30, 20);
                    simTextBoxCam.textId = 0;
                    simTextBoxCam.serializedItem = serializedCompItem;
                    simTextBoxCam.TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2, simButton);
                    simTextBoxCam.BringToFront();
                    singleCamControls.Add(simTextBoxCam);
                    simButton.componentPanel.Controls.Add(simTextBoxCam);
                    simButton.componentPanel.TotalInspectorPanelHeight += labelCam.Size.Height;
                    idx++;
                    if (idx == 4)
                    {
                        RemoveComponentButton removeComponentButtonCam = new RemoveComponentButton();
                        removeComponentButtonCam.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
                        removeComponentButtonCam.Size = new Size(110, 20);
                        removeComponentButtonCam.Text = "RemoveComponent";
                        removeComponentButtonCam.BackColor = Color.White;
                        removeComponentButtonCam.Click += (sender, e) => removeComponentButton_Click(sender, e, simButton, serializedCompItem, singleCamControls);  //new System.EventHandler(removeComponentButton_Click);
                        removeComponentButtonCam.BringToFront();
                        simButton.componentPanel.Controls.Add(removeComponentButtonCam);
                        simButton.componentPanel.TotalInspectorPanelHeight += 20;
                        idx = 0;
                    }
                    break;
                case "Text Renderer Serialized":
                    Label labelText = new Label();
                    labelText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
                    labelText.Size = new Size(90, 20);
                    labelText.Text = field.Name;
                    labelText.BackColor = Color.AliceBlue;
                    labelText.BringToFront();
                    singleTextControls.Add(labelText);
                    simButton.componentPanel.Controls.Add(labelText);

                    object o = field.GetValue(serializedCompItem);

                    SimTextBox simTextBoxText = new SimTextBox();
                    simTextBoxText.Location = new Point(labelText.Size.Width, simButton.componentPanel.TotalInspectorPanelHeight);
                    simTextBoxText.Text = o.ToString();
                    simTextBoxText.BackColor = Color.Yellow;
                    simTextBoxText.Size = new Size(30, 20);
                    simTextBoxText.textId = 0;
                    simTextBoxText.serializedItem = serializedCompItem;
                    simTextBoxText.TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2, simButton);
                    simTextBoxText.BringToFront();
                    singleTextControls.Add(simTextBoxText);
                    simButton.componentPanel.Controls.Add(simTextBoxText);
                    simButton.componentPanel.TotalInspectorPanelHeight += labelText.Size.Height;

                    RemoveComponentButton removeComponentButtonText = new RemoveComponentButton();
                    removeComponentButtonText.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
                    removeComponentButtonText.Size = new Size(110, 20);
                    removeComponentButtonText.Text = "RemoveComponent";
                    removeComponentButtonText.BackColor = Color.White;
                    removeComponentButtonText.Click += (sender, e) => removeComponentButton_Click(sender, e, simButton, serializedCompItem, singleTextControls);  //new System.EventHandler(removeComponentButton_Click);
                    removeComponentButtonText.BringToFront();
                    simButton.componentPanel.Controls.Add(removeComponentButtonText);
                    simButton.componentPanel.TotalInspectorPanelHeight += 20;
                    break;
                default:
                    break;
            }

        }

        private void PrepareSerializedCompName(HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> vec3Controls, List<Control> meshControls, List<Control> floatControls, List<Control> vec4Controls, List<Control> singleCamControls, List<Control> singleTextControls)
        {
            SimTextBox serializedText = new SimTextBox();
            serializedText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.Text = serializedCompItem.GetName();
            serializedText.BackColor = Color.Red;
            serializedText.Size = new Size((int)serializedComponentName.X, (int)serializedComponentName.Y + simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.BringToFront();
            serializedTexts.Add(serializedText);
            switch (serializedText.Text)
            {
                case "Transform Serialized":
                    vec3Controls.Add(serializedText);
                    break;
                case "Mesh Serialized":
                    meshControls.Add(serializedText);
                    break;
                case "Camera Serialized":
                    singleCamControls.Add(serializedText);
                    break;
                case "Directional Light Comp":
                    vec4Controls.Add(serializedText);
                    break;
                case "Spot Light Comp":
                    vec4Controls.Add(serializedText);
                    break;
                case "Text Renderer Serialized":
                    singleTextControls.Add(serializedText);
                    break;
                default:
                    break;
            }
            simButton.componentPanel.Controls.Add(serializedTexts[serializedTexts.Count - 1]);
            simButton.componentPanel.TotalInspectorPanelHeight += serializedText.Size.Height;
        }

        private void PrepareMaterialCase(HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> vec3Controls, List<Control> meshControls)
        {
            Label matRendLabels = new Label();
            string[] matFiles = Directory.GetFiles(SimPath.MaterialsPath, "*.mat", SearchOption.AllDirectories);
            matRendLabels.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            matRendLabels.Text = "Material";
            matRendLabels.BackColor = Color.Yellow;
            matRendLabels.Size = new Size(60, 20);
            simButton.componentPanel.Controls.Add(matRendLabels);
            meshControls.Add(matRendLabels);

            ComboBox matComboBoxes = new ComboBox();
            matComboBoxes = new ComboBox();
            matComboBoxes.Location = new Point(60, simButton.componentPanel.TotalInspectorPanelHeight);
            matComboBoxes.Text = "Add Material";
            matComboBoxes.TextChanged += (sender, e) => matComboBoxes_Changed(sender, e, serializedCompItem, simButton);
            matComboBoxes.BackColor = Color.White;

            for (int j = 0; j < matFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(matFiles[j]);
                matComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            simButton.componentPanel.Controls.Add(matComboBoxes);
            meshControls.Add(matComboBoxes);
            simButton.componentPanel.TotalInspectorPanelHeight += matRendLabels.Height;

            RemoveComponentButton removeComponentButton = new RemoveComponentButton();
            removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
            removeComponentButton.Size = new Size(110, 20);
            removeComponentButton.Text = "RemoveComponent";
            removeComponentButton.BackColor = Color.White;
            removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e, simButton, serializedCompItem, meshControls);  //new System.EventHandler(removeComponentButton_Click);
            removeComponentButton.BringToFront();
            simButton.componentPanel.Controls.Add(removeComponentButton);
            simButton.componentPanel.TotalInspectorPanelHeight += 20;

            meshControls.Add(removeComponentButton);
        }

        private void PrepareMeshCase(HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> meshControls)
        {
            Label meshRendLabels = new Label();
            string[] meshFiles = Directory.GetFiles(SimPath.MeshesPath, "*.mesh", SearchOption.AllDirectories);
            meshRendLabels.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            meshRendLabels.Text = "Mesh";
            meshRendLabels.BackColor = Color.Yellow;
            meshRendLabels.Size = new Size(60, 20);
            simButton.componentPanel.Controls.Add(meshRendLabels);
            meshControls.Add(meshRendLabels);

            ComboBox meshComboBoxes = new ComboBox();
            meshComboBoxes = new ComboBox();
            meshComboBoxes.Location = new Point(60, simButton.componentPanel.TotalInspectorPanelHeight);
            meshComboBoxes.Text = "Add Mesh";
            meshComboBoxes.TextChanged += (sender, e) => meshComboBoxes_Changed(sender, e, serializedCompItem, simButton);
            meshComboBoxes.BackColor = Color.White;

            for (int j = 0; j < meshFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(meshFiles[j]);
                meshComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            simButton.componentPanel.Controls.Add(meshComboBoxes);
            meshControls.Add(meshComboBoxes);
            simButton.componentPanel.TotalInspectorPanelHeight += meshRendLabels.Height;
        }

        private void PrepareVector3Case(FieldInfo field, int idx, SerializedComponent serializedCompItem, HierarchySimButton simButton, List<Control> vec3Controls)
        {
            ResetButton[] resButton = new ResetButton[3];
            Label[] fieldName = new Label[3];
            string[] vecValues = new string[3];

            Vector3 fieldValue = (Vector3)field.GetValue(serializedCompItem);
            vecValues[0] = fieldValue.X.ToString();
            vecValues[1] = fieldValue.Y.ToString();
            vecValues[2] = fieldValue.Z.ToString();

            SimTextBox[] serializedFieldTexs = new SimTextBox[3];

            fieldName[idx] = new Label();
            fieldName[idx].Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            fieldName[idx].Size = new Size(30, 20);
            fieldName[idx].Text = field.Name;
            fieldName[idx].BackColor = Color.AliceBlue;
            fieldName[idx].BringToFront();
            vec3Controls.Add(fieldName[idx]);
            simButton.componentPanel.Controls.Add(fieldName[idx]);

            resButton[idx] = new ResetButton();
            resButton[idx].Location = new Point((int)resetButtonLocation.X + fieldName[idx].Size.Width, simButton.componentPanel.TotalInspectorPanelHeight);
            resButton[idx].Size = new Size((int)resetButtonSize.X, (int)resetButtonSize.Y);
            resButton[idx].Text = "Reset";
            resButton[idx].BackColor = Color.White;
            resButton[idx].fieldId = idx;
            resButton[idx].item = serializedCompItem;
            resButton[idx].simPosText = serializedFieldTexs;
            resButton[idx].Click += new System.EventHandler(resetButton_Click);
            resButton[idx].BringToFront();
            vec3Controls.Add(resButton[idx]);
            simButton.componentPanel.Controls.Add(resButton[idx]);

            for (int i = 0; i < 3; i++)
            {
                serializedFieldTexs[i] = new SimTextBox();
                serializedFieldTexs[i].Location = new Point((i * (int)vectorLocation.X + fieldName[idx].Size.Width), simButton.componentPanel.TotalInspectorPanelHeight);
                serializedFieldTexs[i].Text = vecValues[i];
                serializedFieldTexs[i].BackColor = Color.Yellow;
                serializedFieldTexs[i].Size = new Size((int)vectorSize.X, (int)vectorSize.Y);
                serializedFieldTexs[i].textId = i;
                serializedFieldTexs[i].fieldId = idx;
                serializedFieldTexs[i].serializedItem = serializedCompItem;
                serializedFieldTexs[i].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2, simButton);
                serializedFieldTexs[i].BringToFront();
                vec3Controls.Add(serializedFieldTexs[i]);
                simButton.componentPanel.Controls.Add(serializedFieldTexs[i]);
            }
            simButton.componentPanel.TotalInspectorPanelHeight += (int)vectorSize.Y;

            if (idx == 2)
            {
                RemoveComponentButton removeComponentButton = new RemoveComponentButton();
                removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
                removeComponentButton.Size = new Size(110, 20);
                removeComponentButton.Text = "RemoveComponent";
                removeComponentButton.BackColor = Color.White;
                removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e, simButton, serializedCompItem, vec3Controls);  //new System.EventHandler(removeComponentButton_Click);
                removeComponentButton.BringToFront();
                simButton.componentPanel.Controls.Add(removeComponentButton);
                simButton.componentPanel.TotalInspectorPanelHeight += 20;
            }
        }

        private void removeComponentButton_Click(object sender, EventArgs e, HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> deletedControl)
        {
            /*
            Type serializedType = SerializedComponentPool.GetSerializedComponent(serializedCompItem.GetName());
            Type compType = SerializedComponentPool.GetComponentForRemove(serializedCompItem.GetName());

            if (serializedType != null && compType != null)
            {
                RemoveComponentButton removeComponentButton = sender as RemoveComponentButton;
                EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                    editorFunction = () => {
                        simButton.simObject.entity.RemoveComponent<TransformComp>(); //TODO: DÜZELT
                        simButton.simObject.objectData.RemoveSerializedComp(serializedType);
                    }

                });

                foreach (var control in vec3Controls)
                {
                    simButton.componentPanel.Controls.Remove(control);
                }
                simButton.componentPanel.TotalInspectorPanelHeight -= 120;
                simButton.componentPanel.Controls.Remove(removeComponentButton);
                vec3Controls.Clear();
            }

            else
            {
                MessageBox.Show("There is an error while removing component");
            }
            */
            //Geçiçi
            RemoveComponentButton removeComponentButton = sender as RemoveComponentButton;
            switch (serializedCompItem.GetName())
            {
                case "Transform Serialized":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            simButton.simObject.entity.RemoveComponent<TransformComp>(); //TODO: DÜZELT
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(TransformSerialized));
                        }

                    });

                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 120;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();
                    break;
                case "Mesh Serialized":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            simButton.simObject.entity.RemoveComponent<MeshRendererComp>(); //TODO: DÜZELT
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(MeshRendererSerialized));
                        }

                    });

                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 100;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();

                    break;
                case "Directional Light Comp":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            simButton.simObject.entity.RemoveComponent<DirectionalLightComp>();
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(DirectionalLightSerialized));
                        }

                    });
                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 130;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();
                    break;
                case "Spot Light Comp":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            simButton.simObject.entity.RemoveComponent<SpotLightComp>();
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(SpotLightSerialized));
                        }

                    });
                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 130;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();
                    break;
                case "Camera Serialized":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            simButton.simObject.entity.RemoveComponent<CameraComp>();
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(CameraSerialized));
                        }

                    });
                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 130;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();
                    break;
                case "Text Renderer Serialized":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            //simButton.simObject.entity.RemoveComponent<comp>(); TEXT RENDERER COMP ?
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(TextRendererSerialized));
                        }

                    });
                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 30;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();
                    break;
                default:
                    break;
            }
        }

        private void matComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedCompItem, HierarchySimButton simButton)
        {
            MeshRendererSerialized meshRendererSerialized = serializedCompItem as MeshRendererSerialized;
            dynamic obj = sender;
            string filename = obj.Text;

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    meshRendererSerialized.materialPath = filename;
                }

            });
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });

        }

        private void meshComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedComponent, HierarchySimButton simButton)
        {

            MeshRendererSerialized meshRendererSerialized = serializedComponent as MeshRendererSerialized;
            dynamic obj = sender;
            string filename = obj.Text;

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    meshRendererSerialized.meshPath = filename;
                }

            });
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }


        private static Vector3 InitializeItemVector(int idx, string text, dynamic obj)
        {
            Vector3 itemVec = new Vector3(obj.X, obj.Y, obj.Z);
            int result = 0;
            switch (idx)
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
        private static void SetItem(SimTextBox textBox, HierarchySimButton simButton)
        {
            var type = textBox.serializedItem.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    var fields = type.GetFields();
                    dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
                    fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItemVector(textBox.textId, textBox.Text, obj));
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                        refreshedSimObj = simButton.simObject
                    });
                    break;


                default:
                    break;
            }
        }

        public static void ResetItem(SerializedComponent serializedItem, int fieldId)
        {
            var type = serializedItem.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    var fields = type.GetFields();
                    fields[fieldId].SetValue(serializedItem, new Vector3(0, 0, 0));
                    break;


                default:
                    break;
            }
        }
    }
}
