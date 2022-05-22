using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using SimulationSystem;
using System.Dynamic;
using SimulationWFA.MespSimulationSystem.ProgramLibrary;
using SimulationSystem.Systems;
using System.Numerics;
using System.Collections.Generic;
using SimulationSystem.ECS.Entegration;
using ProgramLibrary;
using SimulationWFA.MespUtils;
using SimulationWFA.SimulationAlgorithms;

namespace SimulationWFA
{
    public partial class SimulationProject : Form
    {
        SerializedEditor serializedEditor = new SerializedEditor();
        public List<HierarchySimButton> hierarchySimButList = new List<HierarchySimButton>();
        int hierarchyHeight = 0;
        HierarchySimButton lastSimButton = null;
        public SimulationProject()
        {
            hierarchyHeight = 0;
            InitializeComponent();
            ShowProjectFiles();
            SceneConfigurationSystemTest.SceneIsReadyEvent += InvokeHierarchy;
            PathRequestManager.OnAlgoDoneEvent += InvokeAlgorithmsIsDone;
            Task.Run(() => RunEditorWindow());

        }

        public void InvokeHierarchy()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { CreateHierarchyPanel(); }));
            }
        }

        public void InvokeAlgorithmsIsDone()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { AlgorithmIsDone(); }));
            }
        }

        private void AlgorithmIsDone()
        {
            AStarDistanceTextBox.Text = PathRequestManager.algorithmDistanceDictionary["AStar"];
            DijkstraDistanceTextBox.Text = PathRequestManager.algorithmDistanceDictionary["Dijkstra"];
            PrimsDistanceTextBox.Text = PathRequestManager.algorithmDistanceDictionary["Prims"];
            DFSDistanceTextBox.Text = PathRequestManager.algorithmDistanceDictionary["DFS"];
            BFSDistanceTextBox.Text = PathRequestManager.algorithmDistanceDictionary["BFS"];
            CustomDistanceTextBox.Text = PathRequestManager.algorithmDistanceDictionary["Custom"];

            AStarMsTextBox.Text = PathRequestManager.algorithmMSDictionary["AStar"];
            DijkstraMsTextBox.Text = PathRequestManager.algorithmMSDictionary["Dijkstra"];
            PrimsMsTextBox.Text = PathRequestManager.algorithmMSDictionary["Prims"];
            DFSMsTextBox.Text = PathRequestManager.algorithmMSDictionary["DFS"];
            BFSMsTextBox.Text = PathRequestManager.algorithmMSDictionary["BFS"];
            CustomMsTextBox.Text = PathRequestManager.algorithmMSDictionary["Custom"];

            ChoosenAlgoTextBox.Text = PathRequestManager.findedAlgorithmName; 
        }

        public void CreateHierarchyPanel()
        {
            SimObject[] simObjects = SimObject.GetChildren(SimObject.Hiearchy);
            foreach (var simObject in simObjects)
            {
                HierarchySimButton hierarchyButton = new HierarchySimButton();
                hierarchyButton.Location = new Point(10, 30 + hierarchyHeight);
                hierarchyButton.Name = "hierarchyButton";
                hierarchyButton.Size = new System.Drawing.Size(75, 23);
                hierarchyButton.Text = simObject.objectData.name;
                hierarchyButton.BackColor = Color.White;
                hierarchyButton.simObject = simObject;
                hierarchyButton.id = hierarchyHeight / 30;
                hierarchyButton.componentPanel.Location = new Point(5, 30);
                hierarchyButton.componentPanel.Name = "ComponentPanel";
                hierarchyButton.componentPanel.Size = new System.Drawing.Size(180, 400);
                hierarchyButton.componentPanel.BackColor = Color.AliceBlue;
                hierarchyButton.componentPanel.AutoScroll = true;
                //hierarchyButton.BringToFront();
                hierarchyButton.Click += (sender2, e2) => hierarchyButton_Click(sender2, e2, hierarchyButton.id); //new System.EventHandler(hierarchyButton_Click);
                hieararchyPanel.Controls.Add(hierarchyButton);
                hierarchyHeight += 30;

                SimTextBox textBox = new SimTextBox();
                textBox.Name = "ChangeNameTextBox" + simObject.objectData.name;
                textBox.Location = new Point(hierarchyButton.Location.X + 80, hierarchyButton.Location.Y);
                textBox.Size = new System.Drawing.Size(75, 23);
                textBox.Text = "Change Name";
                textBox.textId = hierarchyHeight / 30;
                textBox.BackColor = Color.White;
                hierarchyButton.BringToFront();
                textBox.TextChanged += (sender2, e2) => hierarchyButton_TextChanged(sender2, e2, hierarchyButton);
                hieararchyPanel.Controls.Add(textBox);

            }
            addObjectButton.Location = new Point(addObjectButton.Location.X, hierarchyHeight + 30);
        }
        private void ShowProjectFiles() //program açıldığı anda projectste istenilen path içindeki dosyaları gosterir.
        {
            Cursor.Current = Cursors.WaitCursor;
            projectsTreeView.Nodes.Clear();
            if (folderBrowserDialog1.SelectedPath == "")
            {
                foreach (var item in Directory.GetDirectories(SimPath.FindDirectoryPath("SimulationResearchProject")))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(item);
                    var node = projectsTreeView.Nodes.Add(directoryInfo.Name, directoryInfo.Name, 0, 0);
                    node.Tag = directoryInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                foreach (var item in Directory.GetFiles(SimPath.FindDirectoryPath("SimulationResearchProject")))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    var node = projectsTreeView.Nodes.Add(fileInfo.Name, fileInfo.Name, 1, 1);
                    node.Tag = fileInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                Cursor.Current = Cursors.Default;
            }

        }
        private void RunEditorWindow() //Editor window oluşturulup çalıştırılır.
        {
            EditorWindowSystem editorWindow = new EditorWindowSystem();
            editorWindow.CreateEditorWindow();
        }

        #region ClickEvents

        private void openToolStripMenuItem_Click(object sender, EventArgs e) //Penceredeki open tool'u
        {
            if (sender.ToString() == "New")
            {
                MessageBox.Show("New");
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)//Penceredeki exit tool'u
        {
            Close();
        }

        private void simulateButton_Click(object sender, EventArgs e) //Editor windowda suan kapalı olan simulate butonu için
        {

        }

        private void OpenProjectFolder(TreeNodeMouseClickEventArgs e) //Seçilen folder'ın açılması için
        {
            //e.Node.BackColor = Color.FromArgb(255, 255, 255);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = ((FileInfo)e.Node.Tag).FullName;
            info.UseShellExecute = true;
            Process process = Process.Start(info);
            Thread.Sleep(2000);
            //SetParent(process.MainWindowHandle, this.Handle);
        }
        private void projectsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) //Project files kısmında iç içe dosyalarda gezmek için
        {
            if (e.Node.Tag == null)
            {
                //return
            }

            else if (e.Node.Tag.GetType() == typeof(DirectoryInfo))
            {
                e.Node.Nodes.Clear();
                foreach (var item in Directory.GetDirectories(((DirectoryInfo)e.Node.Tag).FullName))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(item);
                    var node = e.Node.Nodes.Add(directoryInfo.Name, directoryInfo.Name, 0, 0);
                    node.Tag = directoryInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                foreach (var item in Directory.GetFiles(((DirectoryInfo)e.Node.Tag).FullName))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    var node = e.Node.Nodes.Add(fileInfo.Name, fileInfo.Name, 1, 1);
                    node.Tag = fileInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }
                e.Node.Expand();
            }
            else
            {
                Task.Run(() => OpenProjectFolder(e));
            }
        }

        private void addObjectButton_Click(object sender, EventArgs e/*, ref int i*/) //Hierarchy'e obje ekler.
        {
            var simObject = SimObject.NewSimObject("Empty" /*+ i.ToString()*/);
            HierarchySimButton hierarchyButton = new HierarchySimButton();
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorCreateSimObjEvent { simObject = simObject });
            hierarchyButton.Location = new Point(10, 30 + hierarchyHeight - hieararchyPanel.VerticalScroll.Value);
            hierarchyButton.Name = "hierarchyButton";
            hierarchyButton.Size = new System.Drawing.Size(75, 23);
            hierarchyButton.Text = simObject.objectData.name;
            hierarchyButton.BackColor = Color.White;
            hierarchyButton.simObject = simObject;
            hierarchyButton.id = hierarchyHeight / 30;
            hierarchyButton.componentPanel.Location = new Point(5, 30);
            hierarchyButton.componentPanel.Name = "ComponentPanel";
            hierarchyButton.componentPanel.Size = new System.Drawing.Size(180, 400);
            hierarchyButton.componentPanel.BackColor = Color.AliceBlue;
            hierarchyButton.componentPanel.AutoScroll = true;
            hierarchyButton.BringToFront();
            hierarchyButton.Click += (sender2, e2) => hierarchyButton_Click(sender2, e2, hierarchyButton.id); //new System.EventHandler(hierarchyButton_Click);
            hieararchyPanel.Controls.Add(hierarchyButton);
            SimTextBox textBox = new SimTextBox();
            textBox.Name = "ChangeNameTextBox" + simObject.objectData.name;
            textBox.Location = new Point(hierarchyButton.Location.X + 80, hierarchyButton.Location.Y);
            textBox.Size = new System.Drawing.Size(75, 23);
            textBox.Text = "Change Name";
            textBox.textId = hierarchyHeight / 30;
            textBox.BackColor = Color.White;
            textBox.BringToFront();
            textBox.TextChanged += (sender2, e2) => hierarchyButton_TextChanged(sender2, e2, hierarchyButton);
            hieararchyPanel.Controls.Add(textBox);
            hierarchyHeight += 30;
            addObjectButton.Location = new Point(addObjectButton.Location.X, hierarchyHeight + 30 - hieararchyPanel.VerticalScroll.Value);
            //i++;
        }
        private void hierarchyButton_TextChanged(object sender, EventArgs e, HierarchySimButton hierarchySimButton)
        {
            TextBox textBox = sender as TextBox;
            hierarchySimButton.Text = textBox.Text;
            hierarchySimButton.simObject.objectData.name = textBox.Text;
        }

        private void hierarchyButton_Click(object sender, EventArgs e, int idx) //Hierarchy' deki objeye tıklandığında girer
        {
            HierarchySimButton simButton = sender as HierarchySimButton;
            //Control[] control = Controls.Find("hierarchyButton", true);
            // HierarchySimButton simButton = (HierarchySimButton)control[idx];

            if (hierarchySimButList.Contains(simButton) == false)
            {

                if (lastSimButton != null)
                {
                    HierarchySimButton simButtonOld = lastSimButton;
                    simButtonOld.componentPanel.Visible = false;
                }
                simButton.componentPanel.Location = new Point(5, 30);
                simButton.componentPanel.Name = "ComponentPanel";
                simButton.componentPanel.Size = new System.Drawing.Size(300, 500);
                simButton.componentPanel.BackColor = Color.AliceBlue;
                simButton.componentPanel.VerticalScroll.Enabled = true;

                Button addComponentButton = new Button();
                addComponentButton.Location = new Point(10, simButton.componentPanel.Height - simButton.componentPanel.VerticalScroll.Value);
                addComponentButton.Size = new Size(100, 30);
                addComponentButton.BackColor = Color.White;
                addComponentButton.Text = "Add Component";
                addComponentButton.Name = "AddComponentButton";
                addComponentButton.Click += (sender2, e2) => addComponentButton_Click(sender2, e2, simButton); // new System.EventHandler(addComponentButton_Click);
                addComponentButton.BringToFront();
                simButton.componentPanel.Controls.Add(addComponentButton);

                Button removeSimObject = new Button();
                removeSimObject.Location = new Point(120, simButton.componentPanel.Height - simButton.componentPanel.VerticalScroll.Value);
                removeSimObject.Size = new Size(130, 30);
                removeSimObject.BackColor = Color.White;
                removeSimObject.Text = "Remove " + simButton.simObject.objectData.name;
                removeSimObject.Name = "RemoveSimObjectButton";
                removeSimObject.Click += (sender3, e3) => RemoveSimObjectButton_Click(sender3, e3, simButton);
                removeSimObject.BringToFront();
                simButton.componentPanel.Controls.Add(removeSimObject);

                foreach (var item in simButton.simObject.objectData.GetSerializedComponents())
                {
                    simButton.serializedComponentList.Add(item.GetName());
                    serializedEditor.SetSerializedItemOnEditor(item, simButton, inspectorPanel, simButton.simObject.objectData.GetSerializedComponents().Length);
                }

                hierarchySimButList.Add(simButton);

            }
            else
            {
                if (lastSimButton != null)
                {
                    HierarchySimButton simButtonOld = lastSimButton;
                    simButtonOld.componentPanel.Visible = false;
                    simButtonOld.componentPanel.Enabled = false;
                }

                simButton.componentPanel.Visible = true;
                simButton.componentPanel.Enabled = true;
            }
            lastSimButton = simButton;
        }

        private void RemoveSimObjectButton_Click(object sender3, EventArgs e3, HierarchySimButton simButton)
        {
            if (simButton.Location.Y == hierarchyHeight - hieararchyPanel.VerticalScroll.Value)
            {
                hierarchyHeight -= 30;
                addObjectButton.Location = new Point(addObjectButton.Location.X, addObjectButton.Location.Y - 30);
            }
            simButton.componentPanel.Visible = false;
            simButton.componentPanel.Controls.Clear();
            Control[] changeNameTextBox = hieararchyPanel.Controls.Find("ChangeNameTextBox" + simButton.simObject.objectData.name, true);
            Control[] simButtons = hieararchyPanel.Controls.Find("hierarchyButton", true);
            hieararchyPanel.Controls.Remove(simButton);
            hieararchyPanel.Controls.Remove(changeNameTextBox[0]);

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRemoveSimObj {
                removedSimObj = simButton.simObject
            });
        }

        private void addComponentButton_Click(object sender, EventArgs e, HierarchySimButton simButton) //Inspectorde component eklemeyen button
        {
            Control[] addComponentbutton = simButton.componentPanel.Controls.Find("AddComponentButton", true);
            addComponentbutton[0].Enabled = false;
            Button[] buttons = new Button[SerializedComponentPool.SerializedCompTypes.Count];
            int idx = 0;
            foreach (var item in SerializedComponentPool.SerializedCompTypes)
            {
                buttons[idx] = new Button();
                buttons[idx].Location = new Point(10, (idx * 20) + simButton.componentPanel.TotalInspectorPanelHeight + 30 - simButton.componentPanel.VerticalScroll.Value);
                buttons[idx].Size = new Size(100, 20);
                buttons[idx].Text = SerializedComponentPool.SerializedCompNames[item.Key];
                buttons[idx].BackColor = Color.White;
                buttons[idx].BringToFront();
                buttons[idx].Click += (sender2, e2) => componentsButton_Click(sender2, e2, simButton, item.Key, buttons, addComponentbutton[0]);
                simButton.componentPanel.Controls.Add(buttons[idx]);


                idx++;
            }

        }

        private void componentsButton_Click(object sender, EventArgs e, HierarchySimButton simButton, int idx, Button[] buttons, Control addComponentButton) //Inspectorde componentleri göstermeye yarayan button
        {
            addComponentButton.Enabled = true;

            foreach (var button in buttons)
            {
                button.Visible = false;
            }
            SerializedComponent serializedComponent = SerializedComponentPool.ReturnNewComponentFromList(idx);



            if (simButton.serializedComponentList.Contains(serializedComponent.GetName()) == false)
            {
                serializedEditor.SetSerializedItemOnEditor(serializedComponent, simButton, inspectorPanel, simButton.simObject.objectData.GetSerializedComponents().Length);
                simButton.serializedComponentList.Add(serializedComponent.GetName());
            }
            else
            {
                MessageBox.Show("You cannot add one more " + serializedComponent.GetName() + " Component");
            }

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorAddCompSimObjEvent {
                simObject = simButton.simObject,
                serializedComponent = serializedComponent,
            });

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh { refreshedSimObj = simButton.simObject });
        }

        #endregion

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneManager.SaveScene("testScene");
        }

        private void AStarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            PathRequestManager.algorithmDictionary["AStar"] = checkBox.Checked;
        }

        private void DijkstraCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            PathRequestManager.algorithmDictionary["Dijkstra"] = checkBox.Checked;
        }

        private void PrimsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            PathRequestManager.algorithmDictionary["Prims"] = checkBox.Checked;
        }

        private void DFSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            PathRequestManager.algorithmDictionary["DFS"] = checkBox.Checked;
        }

        private void BFSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            PathRequestManager.algorithmDictionary["BFS"] = checkBox.Checked;
        }

        private void CustomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            PathRequestManager.algorithmDictionary["Custom"] = checkBox.Checked;
        }


    }



}
