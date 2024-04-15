using procMethuselahRS.Binder;
using procMethuselahRS.Injector.System;
using procMethuselahRS.Manager;
using procMethuselahRS.Memory.Reading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace procMethuselahRS.UserControls
{
    public partial class IndexPage : UserControl, IUserControlManager
    {
        private readonly IProcessBinder _processBinder;
        private readonly TableLayoutPanel _tableLayoutPanel = new TableLayoutPanel();
        public event Action<UserControl, Process> RequestFullScreenView;
        public event Action<Process> RebindProcess;
        public const int Rows = 3;
        public const int Columns = 3;
        private readonly Color PanelColor = Color.Black;
        private int _currentClientCount = 0;
        private ContextMenuStrip gridContextMenu;
        private readonly IProcessOperator _processOperator;
        private readonly IMemoryOperator _memoryOperator;

        public IndexPage(IProcessBinder processBinder, IProcessOperator processOperator, IMemoryOperator memoryOperator)
        {
            InitializeComponent();
            _processBinder = processBinder ?? throw new ArgumentNullException(nameof(processBinder));
            _processOperator = processOperator ?? throw new ArgumentNullException(nameof(processOperator));
            _memoryOperator = memoryOperator ?? throw new ArgumentNullException(nameof(memoryOperator));
            InitializeTableLayoutPanel();
            panel1.Controls.Add(_tableLayoutPanel); // Ensure this panel1 is the one within the UserControl, not Form1.
            panel1.Resize += Panel1_Resize;
            InitializeContextMenu();
        }
        private void InitializeContextMenu()
        {
            gridContextMenu = new ContextMenuStrip();
            ToolStripMenuItem removeItem = new ToolStripMenuItem("Remove Client");
            removeItem.Click += RemoveItem_Click;
            gridContextMenu.Items.Add(removeItem);
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            if (gridContextMenu.SourceControl is Label titleLabel && titleLabel.Parent is Panel headerPanel && headerPanel.Parent is Panel containerPanel)
            {
                // Retrieve the mainPanel from the headerPanel's Tag property
                Panel mainPanel = headerPanel.Tag as Panel;
                // Retrieve the bound process from the mainPanel's Tag property
                Process boundProcess = mainPanel?.Tag as Process;

                // Before attempting to kill the process, check if it is valid and has not exited
                if (boundProcess != null && !boundProcess.HasExited)
                {
                    try
                    {
                        boundProcess.Kill();
                        boundProcess.WaitForExit(); // Wait for the process to exit
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to terminate process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Do not remove the cell if the process could not be killed
                    }
                }
                else
                {
                    MessageBox.Show("No process found or process has already exited.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Do not remove the cell if there is no process to kill
                }

                // The process has been killed successfully, proceed to remove the cell
                TableLayoutPanelCellPosition pos = _tableLayoutPanel.GetPositionFromControl(containerPanel);
                _tableLayoutPanel.Controls.Remove(containerPanel);
                containerPanel.Dispose();
                _currentClientCount--;
                ShiftGridElements(pos.Column, pos.Row);
            }
        }

        private void ShiftGridElements(int startCol, int startRow)
        {
            for (int i = startRow * Columns + startCol; i < _currentClientCount; i++)
            {
                int nextIndex = i + 1;
                int currentRow = i / Columns;
                int currentCol = i % Columns;
                int nextRow = nextIndex / Columns;
                int nextCol = nextIndex % Columns;

                Control nextControl = _tableLayoutPanel.GetControlFromPosition(nextCol, nextRow);
                if (nextControl != null)
                {
                    _tableLayoutPanel.SetCellPosition(nextControl, new TableLayoutPanelCellPosition(currentCol, currentRow));
                }
            }
        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            CenterTableLayoutPanel();
        }

        private void InitializeTableLayoutPanel()
        {
            _tableLayoutPanel.Anchor = AnchorStyles.None;
            _tableLayoutPanel.Size = new Size((int)(panel1.Width * 0.75), (int)(panel1.Height * 0.75));
            ConfigureRowsAndColumns();
            CenterTableLayoutPanel();
        }

        private void CenterTableLayoutPanel()
        {
            _tableLayoutPanel.Location = new Point(
                (panel1.Width - _tableLayoutPanel.Width) / 2,
                (panel1.Height - _tableLayoutPanel.Height) / 2);
        }

        private void ConfigureRowsAndColumns()
        {
            _tableLayoutPanel.RowCount = Rows;
            _tableLayoutPanel.ColumnCount = Columns;
            _tableLayoutPanel.ColumnStyles.Clear();
            _tableLayoutPanel.RowStyles.Clear();

            for (int i = 0; i < Rows; i++)
            {
                _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Rows));
            }

            for (int i = 0; i < Columns; i++)
            {
                _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / Columns));
            }
        }

        public void RebindProcessToPanel(Process process)
        {
            foreach (Control item in _tableLayoutPanel.Controls)
            {
                if (item is Panel containerPanel && containerPanel.Tag is Panel mainPanel && mainPanel.Tag == process)
                {
                    _processBinder.RebindProcessToPanel(process, mainPanel);
                    break;
                }
            }
        }

        public Panel AddGridElement(Process processToBind = null)
        {
            RuneScapeProcessManager RuneScapeProcess = new RuneScapeProcessManager(processToBind);

            if (_currentClientCount >= Rows * Columns)
            {
                MessageBox.Show("Maximum number of clients reached.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            _currentClientCount++;
            int clientId = _currentClientCount;
            int row = (clientId - 1) / Columns;
            int col = (clientId - 1) % Columns;

            var containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0)
            };
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = PanelColor,
                Name = $"mainPanel{row}_{col}",
                Tag = processToBind // This will store the process in the main panel's Tag property
            };

            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 20,
                BackColor = Color.Gray,
                Padding = new Padding(0),
                Tag = mainPanel // This will allow us to retrieve the main panel later
            };

            var titleLabel = new Label
            {
                Text = $"Client {clientId} " + processToBind.Id,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = mainPanel // This will allow us to retrieve the main panel later
            };

            headerPanel.Controls.Add(titleLabel);
            containerPanel.Controls.Add(mainPanel);
            containerPanel.Controls.Add(headerPanel);
            _tableLayoutPanel.Controls.Add(containerPanel, col, row);
            titleLabel.MouseUp += titleLabel_MouseUp;

            titleLabel.DoubleClick += (sender, e) =>
            {
                RequestFullScreenView?.Invoke(this, processToBind);
            };

            if (processToBind != null)
            {
                _processBinder.RebindProcessToPanel(processToBind, mainPanel);

                // Use the instances provided through the constructor
               // Injector.DllInjector dllInjector = new Injector.DllInjector(_processOperator, _memoryOperator);

                try
                {
                   // dllInjector.Inject(processToBind.Id);
               //     MessageBox.Show("DLL injected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to inject the DLL into the process. Error: {ex.Message}", "Injection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return mainPanel; // Return the panel that was created
        }
        private void headerPanel_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;
            Panel mainPanel = clickedControl?.Tag as Panel;
            Process boundProcess = mainPanel?.Tag as Process;
            RequestFullScreenView?.Invoke(this, boundProcess);
        }

        private void titleLabel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Label clickedLabel = sender as Label;
                gridContextMenu.Show(clickedLabel, e.Location);
            }
        }

        public TableLayoutPanel TableLayoutPanel
        {
            get { return _tableLayoutPanel; }
        }

        public int CurrentClientCount
        {
            get { return _currentClientCount; }
        }
    }
}
