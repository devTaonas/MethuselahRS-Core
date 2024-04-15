using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MethuselahRS.Binder
{
    public class ProcessBinder : IProcessBinder
    {
        private readonly TableLayoutPanel _tableLayoutPanel;
        private readonly List<Process> _processes = new List<Process>();
        private int _currentProcessIndex;

        public ProcessBinder(TableLayoutPanel tableLayoutPanel)
        {
            _tableLayoutPanel = tableLayoutPanel ?? throw new ArgumentNullException(nameof(tableLayoutPanel));
        }

        public void BindProcesses(string processName)
        {
            if (string.IsNullOrEmpty(processName)) throw new ArgumentException("Process name cannot be null or empty.", nameof(processName));

            _processes.Clear();
            _processes.AddRange(Process.GetProcessesByName(processName));
            _currentProcessIndex = 0; // Reset the index to start from the first process.
            UpdatePanels();
        }

        public void RebindProcessToPanel(Process process, Panel panel)
        {
            IntPtr mainWindowHandle = process.MainWindowHandle;
            if (mainWindowHandle == IntPtr.Zero) return;

            int style = Natives.Methods.GetWindowLong(mainWindowHandle, Natives.Methods.GWL_STYLE);
            style &= ~(Natives.Methods.WS_CAPTION | Natives.Methods.WS_HSCROLL | Natives.Methods.WS_VSCROLL); // Remove scroll bars
            style |= Natives.Methods.WS_CHILD;
            Natives.Methods.SetWindowLong(mainWindowHandle, Natives.Methods.GWL_STYLE, style);
            Natives.Methods.SetParent(mainWindowHandle, panel.Handle);
            Natives.Methods.MoveWindow(mainWindowHandle, 0, 0, panel.ClientSize.Width, panel.ClientSize.Height, true);
        }

        public void NextProcess()
        {
            if (_processes.Count == 0) return;

            _currentProcessIndex = (_currentProcessIndex + 1) % _processes.Count;
            UpdatePanels();
        }

        public void UpdatePanels()
        {
            foreach (Control item in _tableLayoutPanel.Controls)
            {
                if (item is Panel containerPanel && containerPanel.Tag is Panel mainPanel)
                {
                    int panelIndex = _tableLayoutPanel.Controls.IndexOf(containerPanel);
                    UpdatePanel(mainPanel, panelIndex);
                }
            }
        }

        private void UpdatePanel(Panel mainPanel, int panelIndex)
        {
            if (panelIndex >= _processes.Count)
            {
                return; // If there's no process for this panel, simply return.
            }

            Process process = _processes[panelIndex];
            IntPtr mainWindowHandle = process.MainWindowHandle;

            if (mainWindowHandle == IntPtr.Zero) return;

            mainPanel.Tag = process; // Store the process in the main panel's Tag for later use if needed.

            int style = Natives.Methods.GetWindowLong(mainWindowHandle, Natives.Methods.GWL_STYLE);
            style &= ~(Natives.Methods.WS_CAPTION | Natives.Methods.WS_HSCROLL | Natives.Methods.WS_VSCROLL); // Remove scroll bars
            style |= Natives.Methods.WS_CHILD;
            Natives.Methods.SetWindowLong(mainWindowHandle, Natives.Methods.GWL_STYLE, style);
            Natives.Methods.SetParent(mainWindowHandle, mainPanel.Handle);
            Natives.Methods.MoveWindow(mainWindowHandle, 0, 0, mainPanel.ClientSize.Width, mainPanel.ClientSize.Height, true);
        }
    }
}
