namespace LS_Lab1
{
    partial class MainForm
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
            this.menuStripBar = new System.Windows.Forms.MenuStrip();
            this.toolStripBar = new System.Windows.Forms.ToolStrip();
            this.statusStripBar = new System.Windows.Forms.StatusStrip();
            this.statuslStripBarTooLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripBar.SuspendLayout();
            this.statusStripBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripBar
            // 
            this.menuStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.analyzeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripBar.Location = new System.Drawing.Point(0, 0);
            this.menuStripBar.Name = "menuStripBar";
            this.menuStripBar.Size = new System.Drawing.Size(1098, 24);
            this.menuStripBar.TabIndex = 0;
            this.menuStripBar.Text = "menuStrip1";
            // 
            // toolStripBar
            // 
            this.toolStripBar.Location = new System.Drawing.Point(0, 24);
            this.toolStripBar.Name = "toolStripBar";
            this.toolStripBar.Size = new System.Drawing.Size(1098, 25);
            this.toolStripBar.TabIndex = 1;
            this.toolStripBar.Text = "toolStrip1";
            // 
            // statusStripBar
            // 
            this.statusStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslStripBarTooLabel});
            this.statusStripBar.Location = new System.Drawing.Point(0, 563);
            this.statusStripBar.Name = "statusStripBar";
            this.statusStripBar.Size = new System.Drawing.Size(1098, 22);
            this.statusStripBar.TabIndex = 2;
            this.statusStripBar.Text = "statusStrip1";
            // 
            // statuslStripBarTooLabel
            // 
            this.statuslStripBarTooLabel.Name = "statuslStripBarTooLabel";
            this.statuslStripBarTooLabel.Size = new System.Drawing.Size(39, 17);
            this.statuslStripBarTooLabel.Text = "Ready";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.analyzeToolStripMenuItem.Text = "Analyze";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 585);
            this.Controls.Add(this.statusStripBar);
            this.Controls.Add(this.toolStripBar);
            this.Controls.Add(this.menuStripBar);
            this.MainMenuStrip = this.menuStripBar;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStripBar.ResumeLayout(false);
            this.menuStripBar.PerformLayout();
            this.statusStripBar.ResumeLayout(false);
            this.statusStripBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripBar;
        private System.Windows.Forms.ToolStrip toolStripBar;
        private System.Windows.Forms.StatusStrip statusStripBar;
        private System.Windows.Forms.ToolStripStatusLabel statuslStripBarTooLabel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}

