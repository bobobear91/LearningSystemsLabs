namespace LS_Lab1___Neural_Network
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsButtonRemove = new System.Windows.Forms.ToolStripSplitButton();
            this.removeTraningDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTestDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeBothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnShowTestMatrix = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTestInformation = new System.Windows.Forms.Label();
            this.lblTestData = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnShowTrainMatrix = new System.Windows.Forms.Button();
            this.lblTraningStatus = new System.Windows.Forms.Label();
            this.lblTraningInformation = new System.Windows.Forms.Label();
            this.lblTraningData = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelANNButtons = new System.Windows.Forms.Panel();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.artificialNeuralNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fuzzySystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geneticAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicProgrammingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reininforcedLearningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelANNButtons.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1010, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.artificialNeuralNetworkToolStripMenuItem,
            this.fuzzySystemToolStripMenuItem,
            this.geneticAlgorithmToolStripMenuItem,
            this.dynamicProgrammingToolStripMenuItem,
            this.reininforcedLearningToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trainingDataToolStripMenuItem,
            this.testDataToolStripMenuItem,
            this.loadBothToolStripMenuItem});
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.loadFileToolStripMenuItem.Text = "Load file";
            // 
            // trainingDataToolStripMenuItem
            // 
            this.trainingDataToolStripMenuItem.Name = "trainingDataToolStripMenuItem";
            this.trainingDataToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.trainingDataToolStripMenuItem.Text = "Training Data";
            this.trainingDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataMenuItem_Click);
            // 
            // testDataToolStripMenuItem
            // 
            this.testDataToolStripMenuItem.Name = "testDataToolStripMenuItem";
            this.testDataToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.testDataToolStripMenuItem.Text = "Test Data";
            this.testDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataMenuItem_Click);
            // 
            // loadBothToolStripMenuItem
            // 
            this.loadBothToolStripMenuItem.Name = "loadBothToolStripMenuItem";
            this.loadBothToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.loadBothToolStripMenuItem.Text = "Load Both";
            this.loadBothToolStripMenuItem.Click += new System.EventHandler(this.loadDataMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsButtonRemove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1010, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsButtonRemove
            // 
            this.tsButtonRemove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeTraningDataToolStripMenuItem,
            this.removeTestDataToolStripMenuItem,
            this.removeBothToolStripMenuItem});
            this.tsButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonRemove.Name = "tsButtonRemove";
            this.tsButtonRemove.Size = new System.Drawing.Size(47, 22);
            this.tsButtonRemove.Text = "Data";
            // 
            // removeTraningDataToolStripMenuItem
            // 
            this.removeTraningDataToolStripMenuItem.Name = "removeTraningDataToolStripMenuItem";
            this.removeTraningDataToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.removeTraningDataToolStripMenuItem.Text = "Remove Traning data";
            // 
            // removeTestDataToolStripMenuItem
            // 
            this.removeTestDataToolStripMenuItem.Name = "removeTestDataToolStripMenuItem";
            this.removeTestDataToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.removeTestDataToolStripMenuItem.Text = "Remove Test Data";
            // 
            // removeBothToolStripMenuItem
            // 
            this.removeBothToolStripMenuItem.Name = "removeBothToolStripMenuItem";
            this.removeBothToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.removeBothToolStripMenuItem.Text = "Remove Both";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 563);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(80, 17);
            this.statusStripLabel.Text = "{Status-Label}";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1010, 514);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panelANNButtons, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(499, 508);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 376);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Information";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.41751F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.58249F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(487, 311);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnShowTestMatrix, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblTestInformation, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblTestData, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 144);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.48837F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.51163F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(481, 159);
            this.tableLayoutPanel5.TabIndex = 12;
            // 
            // btnShowTestMatrix
            // 
            this.btnShowTestMatrix.Location = new System.Drawing.Point(2, 74);
            this.btnShowTestMatrix.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowTestMatrix.Name = "btnShowTestMatrix";
            this.btnShowTestMatrix.Size = new System.Drawing.Size(80, 29);
            this.btnShowTestMatrix.TabIndex = 4;
            this.btnShowTestMatrix.Text = "Matrix";
            this.btnShowTestMatrix.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Status";
            // 
            // lblTestInformation
            // 
            this.lblTestInformation.AutoSize = true;
            this.lblTestInformation.Location = new System.Drawing.Point(3, 22);
            this.lblTestInformation.Name = "lblTestInformation";
            this.lblTestInformation.Size = new System.Drawing.Size(94, 20);
            this.lblTestInformation.TabIndex = 1;
            this.lblTestInformation.Text = "Information:";
            // 
            // lblTestData
            // 
            this.lblTestData.AutoSize = true;
            this.lblTestData.Location = new System.Drawing.Point(3, 0);
            this.lblTestData.Name = "lblTestData";
            this.lblTestData.Size = new System.Drawing.Size(83, 20);
            this.lblTestData.TabIndex = 0;
            this.lblTestData.Text = "Test Data:";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnShowTrainMatrix, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.lblTraningStatus, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.lblTraningInformation, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblTraningData, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.48837F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.51163F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(481, 123);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // btnShowTrainMatrix
            // 
            this.btnShowTrainMatrix.Location = new System.Drawing.Point(2, 69);
            this.btnShowTrainMatrix.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowTrainMatrix.Name = "btnShowTrainMatrix";
            this.btnShowTrainMatrix.Size = new System.Drawing.Size(80, 29);
            this.btnShowTrainMatrix.TabIndex = 4;
            this.btnShowTrainMatrix.Text = "Matrix";
            this.btnShowTrainMatrix.UseVisualStyleBackColor = true;
            // 
            // lblTraningStatus
            // 
            this.lblTraningStatus.AutoSize = true;
            this.lblTraningStatus.Location = new System.Drawing.Point(3, 42);
            this.lblTraningStatus.Name = "lblTraningStatus";
            this.lblTraningStatus.Size = new System.Drawing.Size(60, 20);
            this.lblTraningStatus.TabIndex = 2;
            this.lblTraningStatus.Text = "Status:";
            // 
            // lblTraningInformation
            // 
            this.lblTraningInformation.AutoSize = true;
            this.lblTraningInformation.Location = new System.Drawing.Point(3, 22);
            this.lblTraningInformation.Name = "lblTraningInformation";
            this.lblTraningInformation.Size = new System.Drawing.Size(94, 20);
            this.lblTraningInformation.TabIndex = 1;
            this.lblTraningInformation.Text = "Information:";
            // 
            // lblTraningData
            // 
            this.lblTraningData.AutoSize = true;
            this.lblTraningData.Location = new System.Drawing.Point(3, 0);
            this.lblTraningData.Name = "lblTraningData";
            this.lblTraningData.Size = new System.Drawing.Size(105, 20);
            this.lblTraningData.TabIndex = 0;
            this.lblTraningData.Text = "Traning Data:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.26653F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.73347F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(508, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(499, 508);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panelANNButtons
            // 
            this.panelANNButtons.Controls.Add(this.button2);
            this.panelANNButtons.Controls.Add(this.btnTest);
            this.panelANNButtons.Controls.Add(this.btnTrain);
            this.panelANNButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelANNButtons.Location = new System.Drawing.Point(3, 385);
            this.panelANNButtons.Name = "panelANNButtons";
            this.panelANNButtons.Size = new System.Drawing.Size(493, 40);
            this.panelANNButtons.TabIndex = 2;
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(6, 6);
            this.btnTrain.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(80, 29);
            this.btnTrain.TabIndex = 16;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(90, 6);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(80, 29);
            this.btnTest.TabIndex = 17;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 29);
            this.button2.TabIndex = 19;
            this.button2.Text = "Extra";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // artificialNeuralNetworkToolStripMenuItem
            // 
            this.artificialNeuralNetworkToolStripMenuItem.Name = "artificialNeuralNetworkToolStripMenuItem";
            this.artificialNeuralNetworkToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.artificialNeuralNetworkToolStripMenuItem.Text = "Artificial Neural Network";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.chart1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(168, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.8765F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.12351F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(328, 502);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(322, 400);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // fuzzySystemToolStripMenuItem
            // 
            this.fuzzySystemToolStripMenuItem.Name = "fuzzySystemToolStripMenuItem";
            this.fuzzySystemToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.fuzzySystemToolStripMenuItem.Text = "Fuzzy System";
            // 
            // geneticAlgorithmToolStripMenuItem
            // 
            this.geneticAlgorithmToolStripMenuItem.Name = "geneticAlgorithmToolStripMenuItem";
            this.geneticAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.geneticAlgorithmToolStripMenuItem.Text = "Genetic Algorithm";
            // 
            // dynamicProgrammingToolStripMenuItem
            // 
            this.dynamicProgrammingToolStripMenuItem.Name = "dynamicProgrammingToolStripMenuItem";
            this.dynamicProgrammingToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.dynamicProgrammingToolStripMenuItem.Text = "Dynamic Programming";
            // 
            // reininforcedLearningToolStripMenuItem
            // 
            this.reininforcedLearningToolStripMenuItem.Name = "reininforcedLearningToolStripMenuItem";
            this.reininforcedLearningToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.reininforcedLearningToolStripMenuItem.Text = "Reininforced learning";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelANNButtons.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDataToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripSplitButton tsButtonRemove;
        private System.Windows.Forms.ToolStripMenuItem removeTraningDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTestDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeBothToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTraningData;
        private System.Windows.Forms.Label lblTraningInformation;
        private System.Windows.Forms.Label lblTraningStatus;
        private System.Windows.Forms.Button btnShowTrainMatrix;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lblTestData;
        private System.Windows.Forms.Label lblTestInformation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShowTestMatrix;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ToolStripMenuItem loadBothToolStripMenuItem;
        private System.Windows.Forms.Panel panelANNButtons;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.ToolStripMenuItem artificialNeuralNetworkToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fuzzySystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geneticAlgorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dynamicProgrammingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reininforcedLearningToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

