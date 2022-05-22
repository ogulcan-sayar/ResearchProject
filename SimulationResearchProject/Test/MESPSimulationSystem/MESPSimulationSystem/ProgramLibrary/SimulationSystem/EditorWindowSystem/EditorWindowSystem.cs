using System;
using RenderLibrary.IO;
using SimulationSystem.Systems;

namespace SimulationSystem
{
    public class EditorWindowSystem
    {
        
        
        public void CreateSimulationSystem()
        {
            Screen screen = new Screen();
            screen.Create(800, 600);
            if (screen.screenAdress == IntPtr.Zero) return;

            SimulationEvents editorWindowEvents = new SimulationEvents(new ECSEditorController());

            editorWindowEvents.Awake();
            editorWindowEvents.Start();
            
            while (!screen.ShouldClose())
            {
                screen.ProcessWindowInput();
                editorWindowEvents.Update();
                editorWindowEvents.LateUpdate();
                
                screen.Update();
                editorWindowEvents.Render();
                screen.NewFrame();
            }
            
            editorWindowEvents.OnSimulationQuit();
            screen.Terminate();
        }
    }
}