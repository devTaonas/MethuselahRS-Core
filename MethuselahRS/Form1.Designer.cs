namespace MethuselahRS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.launchClient = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Inject = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lbl_Test = new System.Windows.Forms.Label();
            this.lbl_InterfaceOpen = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cb_OverlayBank = new System.Windows.Forms.CheckBox();
            this.cb_OverlayInventory = new System.Windows.Forms.CheckBox();
            this.btn_ShowOverlay = new System.Windows.Forms.Button();
            this.cb_OverlayPlayer = new System.Windows.Forms.CheckBox();
            this.cb_OverlayGameObject = new System.Windows.Forms.CheckBox();
            this.cb_OverlayNPC = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_loop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_objectname = new System.Windows.Forms.TextBox();
            this.cb_go = new System.Windows.Forms.CheckBox();
            this.cb_npc = new System.Windows.Forms.CheckBox();
            this.btn_MouseOverTest = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbl_MouseOverText = new System.Windows.Forms.Label();
            this.lbl_testlabel = new System.Windows.Forms.Label();
            this.lbl_NPCScreenCoords = new System.Windows.Forms.Label();
            this.lbl_ready = new System.Windows.Forms.Label();
            this.btn_TestButtonTwo = new System.Windows.Forms.Button();
            this.btn_TestButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_playeraddress = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 183);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 568);
            this.panel1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Black;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(553, 151);
            this.listBox1.TabIndex = 0;
            // 
            // launchClient
            // 
            this.launchClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.launchClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.launchClient.FlatAppearance.BorderSize = 0;
            this.launchClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launchClient.ForeColor = System.Drawing.Color.Gainsboro;
            this.launchClient.Location = new System.Drawing.Point(11, 11);
            this.launchClient.Margin = new System.Windows.Forms.Padding(2);
            this.launchClient.Name = "launchClient";
            this.launchClient.Size = new System.Drawing.Size(96, 23);
            this.launchClient.TabIndex = 9;
            this.launchClient.Text = "Launch Client";
            this.launchClient.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Controls.Add(this.lbl_playeraddress);
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.lbl_MouseOverText);
            this.panel2.Controls.Add(this.lbl_testlabel);
            this.panel2.Controls.Add(this.lbl_NPCScreenCoords);
            this.panel2.Controls.Add(this.lbl_ready);
            this.panel2.Controls.Add(this.btn_TestButtonTwo);
            this.panel2.Controls.Add(this.btn_TestButton);
            this.panel2.Controls.Add(this.launchClient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(966, 183);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Location = new System.Drawing.Point(399, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(567, 183);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.btn_Inject);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(559, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Injection Test";
            // 
            // btn_Inject
            // 
            this.btn_Inject.Location = new System.Drawing.Point(17, 19);
            this.btn_Inject.Name = "btn_Inject";
            this.btn_Inject.Size = new System.Drawing.Size(186, 23);
            this.btn_Inject.TabIndex = 0;
            this.btn_Inject.Text = "Inject Methuselah";
            this.btn_Inject.UseVisualStyleBackColor = true;
            this.btn_Inject.Click += new System.EventHandler(this.btn_Inject_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Black;
            this.tabPage4.Controls.Add(this.lbl_Test);
            this.tabPage4.Controls.Add(this.lbl_InterfaceOpen);
            this.tabPage4.ForeColor = System.Drawing.Color.White;
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(559, 157);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Player Info";
            // 
            // lbl_Test
            // 
            this.lbl_Test.AutoSize = true;
            this.lbl_Test.ForeColor = System.Drawing.Color.White;
            this.lbl_Test.Location = new System.Drawing.Point(6, 12);
            this.lbl_Test.Name = "lbl_Test";
            this.lbl_Test.Size = new System.Drawing.Size(60, 13);
            this.lbl_Test.TabIndex = 12;
            this.lbl_Test.Text = "Player Info";
            // 
            // lbl_InterfaceOpen
            // 
            this.lbl_InterfaceOpen.AutoSize = true;
            this.lbl_InterfaceOpen.ForeColor = System.Drawing.Color.White;
            this.lbl_InterfaceOpen.Location = new System.Drawing.Point(265, 12);
            this.lbl_InterfaceOpen.Name = "lbl_InterfaceOpen";
            this.lbl_InterfaceOpen.Size = new System.Drawing.Size(81, 13);
            this.lbl_InterfaceOpen.TabIndex = 17;
            this.lbl_InterfaceOpen.Text = "Interface Open";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(559, 157);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Overlay / Mouse over";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.cb_loop);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.tb_objectname);
            this.panel3.Controls.Add(this.cb_go);
            this.panel3.Controls.Add(this.cb_npc);
            this.panel3.Controls.Add(this.btn_MouseOverTest);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(553, 151);
            this.panel3.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cb_OverlayBank);
            this.panel4.Controls.Add(this.cb_OverlayInventory);
            this.panel4.Controls.Add(this.btn_ShowOverlay);
            this.panel4.Controls.Add(this.cb_OverlayPlayer);
            this.panel4.Controls.Add(this.cb_OverlayGameObject);
            this.panel4.Controls.Add(this.cb_OverlayNPC);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(277, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(276, 151);
            this.panel4.TabIndex = 20;
            // 
            // cb_OverlayBank
            // 
            this.cb_OverlayBank.AutoSize = true;
            this.cb_OverlayBank.ForeColor = System.Drawing.Color.White;
            this.cb_OverlayBank.Location = new System.Drawing.Point(18, 106);
            this.cb_OverlayBank.Name = "cb_OverlayBank";
            this.cb_OverlayBank.Size = new System.Drawing.Size(130, 17);
            this.cb_OverlayBank.TabIndex = 25;
            this.cb_OverlayBank.Text = "Bank / BankInventory";
            this.cb_OverlayBank.UseVisualStyleBackColor = true;
            // 
            // cb_OverlayInventory
            // 
            this.cb_OverlayInventory.AutoSize = true;
            this.cb_OverlayInventory.ForeColor = System.Drawing.Color.White;
            this.cb_OverlayInventory.Location = new System.Drawing.Point(18, 85);
            this.cb_OverlayInventory.Name = "cb_OverlayInventory";
            this.cb_OverlayInventory.Size = new System.Drawing.Size(74, 17);
            this.cb_OverlayInventory.TabIndex = 24;
            this.cb_OverlayInventory.Text = "Inventory";
            this.cb_OverlayInventory.UseVisualStyleBackColor = true;
            // 
            // btn_ShowOverlay
            // 
            this.btn_ShowOverlay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_ShowOverlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_ShowOverlay.FlatAppearance.BorderSize = 0;
            this.btn_ShowOverlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ShowOverlay.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_ShowOverlay.Location = new System.Drawing.Point(75, 127);
            this.btn_ShowOverlay.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ShowOverlay.Name = "btn_ShowOverlay";
            this.btn_ShowOverlay.Size = new System.Drawing.Size(125, 23);
            this.btn_ShowOverlay.TabIndex = 21;
            this.btn_ShowOverlay.Text = "Show Overlay";
            this.btn_ShowOverlay.UseVisualStyleBackColor = false;
            this.btn_ShowOverlay.Click += new System.EventHandler(this.btn_ShowOverlay_Click);
            // 
            // cb_OverlayPlayer
            // 
            this.cb_OverlayPlayer.AutoSize = true;
            this.cb_OverlayPlayer.ForeColor = System.Drawing.Color.White;
            this.cb_OverlayPlayer.Location = new System.Drawing.Point(17, 62);
            this.cb_OverlayPlayer.Name = "cb_OverlayPlayer";
            this.cb_OverlayPlayer.Size = new System.Drawing.Size(56, 17);
            this.cb_OverlayPlayer.TabIndex = 23;
            this.cb_OverlayPlayer.Text = "Player";
            this.cb_OverlayPlayer.UseVisualStyleBackColor = true;
            // 
            // cb_OverlayGameObject
            // 
            this.cb_OverlayGameObject.AutoSize = true;
            this.cb_OverlayGameObject.ForeColor = System.Drawing.Color.White;
            this.cb_OverlayGameObject.Location = new System.Drawing.Point(17, 39);
            this.cb_OverlayGameObject.Name = "cb_OverlayGameObject";
            this.cb_OverlayGameObject.Size = new System.Drawing.Size(85, 17);
            this.cb_OverlayGameObject.TabIndex = 22;
            this.cb_OverlayGameObject.Text = "GameObject";
            this.cb_OverlayGameObject.UseVisualStyleBackColor = true;
            // 
            // cb_OverlayNPC
            // 
            this.cb_OverlayNPC.AutoSize = true;
            this.cb_OverlayNPC.ForeColor = System.Drawing.Color.White;
            this.cb_OverlayNPC.Location = new System.Drawing.Point(17, 16);
            this.cb_OverlayNPC.Name = "cb_OverlayNPC";
            this.cb_OverlayNPC.Size = new System.Drawing.Size(46, 17);
            this.cb_OverlayNPC.TabIndex = 21;
            this.cb_OverlayNPC.Text = "NPC";
            this.cb_OverlayNPC.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(35, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Overlay";
            // 
            // cb_loop
            // 
            this.cb_loop.AutoSize = true;
            this.cb_loop.ForeColor = System.Drawing.Color.White;
            this.cb_loop.Location = new System.Drawing.Point(141, 67);
            this.cb_loop.Name = "cb_loop";
            this.cb_loop.Size = new System.Drawing.Size(54, 17);
            this.cb_loop.TabIndex = 19;
            this.cb_loop.Text = "Loop?";
            this.cb_loop.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Object Name :";
            // 
            // tb_objectname
            // 
            this.tb_objectname.Location = new System.Drawing.Point(84, 64);
            this.tb_objectname.Name = "tb_objectname";
            this.tb_objectname.Size = new System.Drawing.Size(51, 21);
            this.tb_objectname.TabIndex = 17;
            // 
            // cb_go
            // 
            this.cb_go.AutoSize = true;
            this.cb_go.ForeColor = System.Drawing.Color.White;
            this.cb_go.Location = new System.Drawing.Point(67, 41);
            this.cb_go.Name = "cb_go";
            this.cb_go.Size = new System.Drawing.Size(88, 17);
            this.cb_go.TabIndex = 16;
            this.cb_go.Text = "Game Object";
            this.cb_go.UseVisualStyleBackColor = true;
            // 
            // cb_npc
            // 
            this.cb_npc.AutoSize = true;
            this.cb_npc.ForeColor = System.Drawing.Color.White;
            this.cb_npc.Location = new System.Drawing.Point(67, 15);
            this.cb_npc.Name = "cb_npc";
            this.cb_npc.Size = new System.Drawing.Size(46, 17);
            this.cb_npc.TabIndex = 15;
            this.cb_npc.Text = "NPC";
            this.cb_npc.UseVisualStyleBackColor = true;
            // 
            // btn_MouseOverTest
            // 
            this.btn_MouseOverTest.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_MouseOverTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_MouseOverTest.FlatAppearance.BorderSize = 0;
            this.btn_MouseOverTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MouseOverTest.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_MouseOverTest.Location = new System.Drawing.Point(147, 96);
            this.btn_MouseOverTest.Margin = new System.Windows.Forms.Padding(2);
            this.btn_MouseOverTest.Name = "btn_MouseOverTest";
            this.btn_MouseOverTest.Size = new System.Drawing.Size(125, 23);
            this.btn_MouseOverTest.TabIndex = 14;
            this.btn_MouseOverTest.Text = "Mouse Over";
            this.btn_MouseOverTest.UseVisualStyleBackColor = false;
            this.btn_MouseOverTest.Click += new System.EventHandler(this.btn_MouseOverTest_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(559, 157);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Display Chat";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbl_MouseOverText
            // 
            this.lbl_MouseOverText.AutoSize = true;
            this.lbl_MouseOverText.ForeColor = System.Drawing.Color.White;
            this.lbl_MouseOverText.Location = new System.Drawing.Point(12, 128);
            this.lbl_MouseOverText.Name = "lbl_MouseOverText";
            this.lbl_MouseOverText.Size = new System.Drawing.Size(84, 13);
            this.lbl_MouseOverText.TabIndex = 19;
            this.lbl_MouseOverText.Text = "MouseOverText";
            // 
            // lbl_testlabel
            // 
            this.lbl_testlabel.AutoSize = true;
            this.lbl_testlabel.ForeColor = System.Drawing.Color.White;
            this.lbl_testlabel.Location = new System.Drawing.Point(12, 96);
            this.lbl_testlabel.Name = "lbl_testlabel";
            this.lbl_testlabel.Size = new System.Drawing.Size(56, 13);
            this.lbl_testlabel.TabIndex = 18;
            this.lbl_testlabel.Text = "Test Label";
            // 
            // lbl_NPCScreenCoords
            // 
            this.lbl_NPCScreenCoords.AutoSize = true;
            this.lbl_NPCScreenCoords.ForeColor = System.Drawing.Color.White;
            this.lbl_NPCScreenCoords.Location = new System.Drawing.Point(12, 61);
            this.lbl_NPCScreenCoords.Name = "lbl_NPCScreenCoords";
            this.lbl_NPCScreenCoords.Size = new System.Drawing.Size(100, 13);
            this.lbl_NPCScreenCoords.TabIndex = 16;
            this.lbl_NPCScreenCoords.Text = "NPC Screen Coords";
            // 
            // lbl_ready
            // 
            this.lbl_ready.AutoSize = true;
            this.lbl_ready.ForeColor = System.Drawing.Color.White;
            this.lbl_ready.Location = new System.Drawing.Point(12, 41);
            this.lbl_ready.Name = "lbl_ready";
            this.lbl_ready.Size = new System.Drawing.Size(43, 13);
            this.lbl_ready.TabIndex = 13;
            this.lbl_ready.Text = "Ready?";
            // 
            // btn_TestButtonTwo
            // 
            this.btn_TestButtonTwo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_TestButtonTwo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_TestButtonTwo.FlatAppearance.BorderSize = 0;
            this.btn_TestButtonTwo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TestButtonTwo.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_TestButtonTwo.Location = new System.Drawing.Point(257, 11);
            this.btn_TestButtonTwo.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TestButtonTwo.Name = "btn_TestButtonTwo";
            this.btn_TestButtonTwo.Size = new System.Drawing.Size(125, 23);
            this.btn_TestButtonTwo.TabIndex = 11;
            this.btn_TestButtonTwo.Text = "TEST BUTTON";
            this.btn_TestButtonTwo.UseVisualStyleBackColor = false;
            this.btn_TestButtonTwo.Click += new System.EventHandler(this.btn_TestButtonTwo_Click);
            // 
            // btn_TestButton
            // 
            this.btn_TestButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_TestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_TestButton.FlatAppearance.BorderSize = 0;
            this.btn_TestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TestButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_TestButton.Location = new System.Drawing.Point(132, 11);
            this.btn_TestButton.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TestButton.Name = "btn_TestButton";
            this.btn_TestButton.Size = new System.Drawing.Size(96, 23);
            this.btn_TestButton.TabIndex = 10;
            this.btn_TestButton.Text = "START";
            this.btn_TestButton.UseVisualStyleBackColor = false;
            this.btn_TestButton.Click += new System.EventHandler(this.btn_TestButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_playeraddress
            // 
            this.lbl_playeraddress.AutoSize = true;
            this.lbl_playeraddress.ForeColor = System.Drawing.Color.White;
            this.lbl_playeraddress.Location = new System.Drawing.Point(12, 156);
            this.lbl_playeraddress.Name = "lbl_playeraddress";
            this.lbl_playeraddress.Size = new System.Drawing.Size(76, 13);
            this.lbl_playeraddress.TabIndex = 22;
            this.lbl_playeraddress.Text = "PlayerAddress";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 751);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button launchClient;
        private System.Windows.Forms.Button btn_TestButton;
        private System.Windows.Forms.Button btn_TestButtonTwo;
        private System.Windows.Forms.Label lbl_Test;
        private System.Windows.Forms.Label lbl_ready;
        private System.Windows.Forms.Button btn_MouseOverTest;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_objectname;
        private System.Windows.Forms.CheckBox cb_go;
        private System.Windows.Forms.CheckBox cb_npc;
        private System.Windows.Forms.CheckBox cb_loop;
        private System.Windows.Forms.Label lbl_NPCScreenCoords;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox cb_OverlayPlayer;
        public System.Windows.Forms.CheckBox cb_OverlayGameObject;
        public System.Windows.Forms.CheckBox cb_OverlayNPC;
        private System.Windows.Forms.Button btn_ShowOverlay;
        private System.Windows.Forms.Label lbl_InterfaceOpen;
        private System.Windows.Forms.Label lbl_testlabel;
        private System.Windows.Forms.Label lbl_MouseOverText;
        public System.Windows.Forms.CheckBox cb_OverlayInventory;
        public System.Windows.Forms.CheckBox cb_OverlayBank;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_Inject;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lbl_playeraddress;
    }
}

