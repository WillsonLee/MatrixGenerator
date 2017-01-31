namespace MatrixGenerator
{
    partial class MatrixGenerator
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatrixGenerator));
            this.funcToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.funcToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.actionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.paraToolStrip = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.matrixDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rowAssimilateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colAssimilateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellGoUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellGoDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellGoLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellGoRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRowColToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rowGoUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rowGoDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colGoLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colGoRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.rowAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.belowAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteColToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.workspaceToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.getRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getColToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funcToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matrixDataGridView)).BeginInit();
            this.tableContextMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // funcToolStrip
            // 
            this.funcToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.funcToolStripComboBox,
            this.clearToolStripButton,
            this.saveToolStripButton,
            this.actionToolStripButton});
            this.funcToolStrip.Location = new System.Drawing.Point(0, 0);
            this.funcToolStrip.Name = "funcToolStrip";
            this.funcToolStrip.Size = new System.Drawing.Size(352, 25);
            this.funcToolStrip.TabIndex = 1;
            this.funcToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel1.Text = "函数:";
            // 
            // funcToolStripComboBox
            // 
            this.funcToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.funcToolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.funcToolStripComboBox.Items.AddRange(new object[] {
            "(生成)Eye",
            "(生成)Linspace",
            "(生成)Logspace",
            "(生成)Ones",
            "(生成)Random",
            "(生成)RangeVector",
            "(生成)StiffnessMatrix",
            "(生成)TransMatrix",
            "(处理)TriUp",
            "(处理)TriLow",
            "(处理)Symmetry",
            "(处理)AntiSymmetry",
            "(处理)Reverse",
            "(处理)Transfer",
            "(处理)SubMatrix",
            "(处理)ToStepMatrix",
            "(处理)Reshape",
            "(处理)GetRowVector",
            "(处理)GetColVector",
            "(处理)MinOfRow",
            "(处理)MinOfCol",
            "(处理)MaxOfRow",
            "(处理)MaxOfCol",
            "(处理)SumRow",
            "(处理)SumCol",
            "(处理)MapMinMax",
            "(处理)MapMinMaxOnCol",
            "(处理)Positive",
            "(处理)Negative",
            "(处理)RowSwitch",
            "(处理)ColumnSwitch",
            "(处理)RowMultiple",
            "(处理)ColumnMultiple",
            "(处理)RowMulAdd",
            "(处理)ColumnMulAdd",
            "(数学)Abs",
            "(数学)Acos",
            "(数学)Asin",
            "(数学)Atan",
            "(数学)Ceiling",
            "(数学)Cos",
            "(数学)Cosh",
            "(数学)Exp",
            "(数学)Floor",
            "(数学)Log",
            "(数学)Log10",
            "(数学)Round",
            "(数学)Sign",
            "(数学)Sin",
            "(数学)Sinh",
            "(数学)Sqrt",
            "(数学)Tan",
            "(数学)Tanh",
            "(数学)Truncate"});
            this.funcToolStripComboBox.Name = "funcToolStripComboBox";
            this.funcToolStripComboBox.Size = new System.Drawing.Size(170, 25);
            this.funcToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.funcToolStripComboBox_SelectedIndexChanged);
            // 
            // clearToolStripButton
            // 
            this.clearToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearToolStripButton.Image")));
            this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Size = new System.Drawing.Size(36, 22);
            this.clearToolStripButton.Text = "清除";
            this.clearToolStripButton.ToolTipText = "清除工作空间所有矩阵";
            this.clearToolStripButton.Click += new System.EventHandler(this.clearToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(36, 22);
            this.saveToolStripButton.Text = "保存";
            this.saveToolStripButton.ToolTipText = "将当前矩阵存入工作空间";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // actionToolStripButton
            // 
            this.actionToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.actionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.actionToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("actionToolStripButton.Image")));
            this.actionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionToolStripButton.Name = "actionToolStripButton";
            this.actionToolStripButton.Size = new System.Drawing.Size(36, 22);
            this.actionToolStripButton.Text = "执行";
            this.actionToolStripButton.ToolTipText = "执行当前函数操作";
            this.actionToolStripButton.Click += new System.EventHandler(this.actionToolStripButton_Click);
            // 
            // paraToolStrip
            // 
            this.paraToolStrip.Location = new System.Drawing.Point(0, 25);
            this.paraToolStrip.Name = "paraToolStrip";
            this.paraToolStrip.Size = new System.Drawing.Size(352, 25);
            this.paraToolStrip.TabIndex = 2;
            this.paraToolStrip.Text = "toolStrip2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.matrixDataGridView);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 202);
            this.panel1.TabIndex = 3;
            // 
            // matrixDataGridView
            // 
            this.matrixDataGridView.AllowUserToAddRows = false;
            this.matrixDataGridView.AllowUserToDeleteRows = false;
            this.matrixDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.matrixDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrixDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.matrixDataGridView.ContextMenuStrip = this.tableContextMenuStrip;
            this.matrixDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matrixDataGridView.Location = new System.Drawing.Point(0, 0);
            this.matrixDataGridView.Name = "matrixDataGridView";
            this.matrixDataGridView.RowHeadersWidth = 70;
            this.matrixDataGridView.RowTemplate.Height = 23;
            this.matrixDataGridView.Size = new System.Drawing.Size(352, 179);
            this.matrixDataGridView.TabIndex = 1;
            this.matrixDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.matrixDataGridView_CellEndEdit);
            this.matrixDataGridView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.matrixDataGridView_Scroll);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "C1";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "C2";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "C3";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "C4";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tableContextMenuStrip
            // 
            this.tableContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rowAssimilateToolStripMenuItem,
            this.colAssimilateToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.copyRowColToolStripMenuItem,
            this.toolStripMenuItem2,
            this.rowAddToolStripMenuItem,
            this.colAddToolStripMenuItem,
            this.deleteRowToolStripMenuItem,
            this.deleteColToolStripMenuItem,
            this.getRowToolStripMenuItem,
            this.getColToolStripMenuItem});
            this.tableContextMenuStrip.Name = "tableContextMenuStrip";
            this.tableContextMenuStrip.Size = new System.Drawing.Size(153, 258);
            // 
            // rowAssimilateToolStripMenuItem
            // 
            this.rowAssimilateToolStripMenuItem.Name = "rowAssimilateToolStripMenuItem";
            this.rowAssimilateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rowAssimilateToolStripMenuItem.Text = "同化整行";
            this.rowAssimilateToolStripMenuItem.Click += new System.EventHandler(this.rowAssimilateToolStripMenuItem_Click);
            // 
            // colAssimilateToolStripMenuItem
            // 
            this.colAssimilateToolStripMenuItem.Name = "colAssimilateToolStripMenuItem";
            this.colAssimilateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.colAssimilateToolStripMenuItem.Text = "同化整列";
            this.colAssimilateToolStripMenuItem.Click += new System.EventHandler(this.colAssimilateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cellGoUpToolStripMenuItem,
            this.cellGoDownToolStripMenuItem,
            this.cellGoLeftToolStripMenuItem,
            this.cellGoRightToolStripMenuItem});
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "复制";
            // 
            // cellGoUpToolStripMenuItem
            // 
            this.cellGoUpToolStripMenuItem.Name = "cellGoUpToolStripMenuItem";
            this.cellGoUpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cellGoUpToolStripMenuItem.Text = "向上复制";
            this.cellGoUpToolStripMenuItem.Click += new System.EventHandler(this.cellGoUpToolStripMenuItem_Click);
            // 
            // cellGoDownToolStripMenuItem
            // 
            this.cellGoDownToolStripMenuItem.Name = "cellGoDownToolStripMenuItem";
            this.cellGoDownToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cellGoDownToolStripMenuItem.Text = "向下复制";
            this.cellGoDownToolStripMenuItem.Click += new System.EventHandler(this.cellGoDownToolStripMenuItem_Click);
            // 
            // cellGoLeftToolStripMenuItem
            // 
            this.cellGoLeftToolStripMenuItem.Name = "cellGoLeftToolStripMenuItem";
            this.cellGoLeftToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cellGoLeftToolStripMenuItem.Text = "向左复制";
            this.cellGoLeftToolStripMenuItem.Click += new System.EventHandler(this.cellGoLeftToolStripMenuItem_Click);
            // 
            // cellGoRightToolStripMenuItem
            // 
            this.cellGoRightToolStripMenuItem.Name = "cellGoRightToolStripMenuItem";
            this.cellGoRightToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cellGoRightToolStripMenuItem.Text = "向右复制";
            this.cellGoRightToolStripMenuItem.Click += new System.EventHandler(this.cellGoRightToolStripMenuItem_Click);
            // 
            // copyRowColToolStripMenuItem
            // 
            this.copyRowColToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rowGoUpToolStripMenuItem,
            this.rowGoDownToolStripMenuItem,
            this.colGoLeftToolStripMenuItem,
            this.colGoRightToolStripMenuItem});
            this.copyRowColToolStripMenuItem.Name = "copyRowColToolStripMenuItem";
            this.copyRowColToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyRowColToolStripMenuItem.Text = "整行(列)复制";
            // 
            // rowGoUpToolStripMenuItem
            // 
            this.rowGoUpToolStripMenuItem.Name = "rowGoUpToolStripMenuItem";
            this.rowGoUpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rowGoUpToolStripMenuItem.Text = "整行向上复制";
            this.rowGoUpToolStripMenuItem.Click += new System.EventHandler(this.rowGoUpToolStripMenuItem_Click);
            // 
            // rowGoDownToolStripMenuItem
            // 
            this.rowGoDownToolStripMenuItem.Name = "rowGoDownToolStripMenuItem";
            this.rowGoDownToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rowGoDownToolStripMenuItem.Text = "整行向下复制";
            this.rowGoDownToolStripMenuItem.Click += new System.EventHandler(this.rowGoDownToolStripMenuItem_Click);
            // 
            // colGoLeftToolStripMenuItem
            // 
            this.colGoLeftToolStripMenuItem.Name = "colGoLeftToolStripMenuItem";
            this.colGoLeftToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.colGoLeftToolStripMenuItem.Text = "整列向左复制";
            this.colGoLeftToolStripMenuItem.Click += new System.EventHandler(this.colGoLeftToolStripMenuItem_Click);
            // 
            // colGoRightToolStripMenuItem
            // 
            this.colGoRightToolStripMenuItem.Name = "colGoRightToolStripMenuItem";
            this.colGoRightToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.colGoRightToolStripMenuItem.Text = "整列向右复制";
            this.colGoRightToolStripMenuItem.Click += new System.EventHandler(this.colGoRightToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // rowAddToolStripMenuItem
            // 
            this.rowAddToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upAddToolStripMenuItem,
            this.belowAddToolStripMenuItem});
            this.rowAddToolStripMenuItem.Name = "rowAddToolStripMenuItem";
            this.rowAddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rowAddToolStripMenuItem.Text = "插入行";
            // 
            // upAddToolStripMenuItem
            // 
            this.upAddToolStripMenuItem.Name = "upAddToolStripMenuItem";
            this.upAddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.upAddToolStripMenuItem.Text = "上方插入";
            this.upAddToolStripMenuItem.Click += new System.EventHandler(this.upAddToolStripMenuItem_Click);
            // 
            // belowAddToolStripMenuItem
            // 
            this.belowAddToolStripMenuItem.Name = "belowAddToolStripMenuItem";
            this.belowAddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.belowAddToolStripMenuItem.Text = "下方插入";
            this.belowAddToolStripMenuItem.Click += new System.EventHandler(this.belowAddToolStripMenuItem_Click);
            // 
            // colAddToolStripMenuItem
            // 
            this.colAddToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftAddToolStripMenuItem,
            this.rightAddToolStripMenuItem});
            this.colAddToolStripMenuItem.Name = "colAddToolStripMenuItem";
            this.colAddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.colAddToolStripMenuItem.Text = "插入列";
            // 
            // leftAddToolStripMenuItem
            // 
            this.leftAddToolStripMenuItem.Name = "leftAddToolStripMenuItem";
            this.leftAddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.leftAddToolStripMenuItem.Text = "左边插入";
            this.leftAddToolStripMenuItem.Click += new System.EventHandler(this.leftAddToolStripMenuItem_Click);
            // 
            // rightAddToolStripMenuItem
            // 
            this.rightAddToolStripMenuItem.Name = "rightAddToolStripMenuItem";
            this.rightAddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rightAddToolStripMenuItem.Text = "右边插入";
            this.rightAddToolStripMenuItem.Click += new System.EventHandler(this.rightAddToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteRowToolStripMenuItem.Text = "删除整行";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // deleteColToolStripMenuItem
            // 
            this.deleteColToolStripMenuItem.Name = "deleteColToolStripMenuItem";
            this.deleteColToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteColToolStripMenuItem.Text = "删除整列";
            this.deleteColToolStripMenuItem.Click += new System.EventHandler(this.deleteColToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workspaceToolStripDropDownButton,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 179);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(352, 23);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // workspaceToolStripDropDownButton
            // 
            this.workspaceToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.workspaceToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("workspaceToolStripDropDownButton.Image")));
            this.workspaceToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.workspaceToolStripDropDownButton.Name = "workspaceToolStripDropDownButton";
            this.workspaceToolStripDropDownButton.Size = new System.Drawing.Size(69, 21);
            this.workspaceToolStripDropDownButton.Text = "工作空间";
            this.workspaceToolStripDropDownButton.ToolTipText = "选择工作空间中的矩阵";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 18);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(352, 177);
            // 
            // getRowToolStripMenuItem
            // 
            this.getRowToolStripMenuItem.Name = "getRowToolStripMenuItem";
            this.getRowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.getRowToolStripMenuItem.Text = "仅留当前行";
            this.getRowToolStripMenuItem.Click += new System.EventHandler(this.getRowToolStripMenuItem_Click);
            // 
            // getColToolStripMenuItem
            // 
            this.getColToolStripMenuItem.Name = "getColToolStripMenuItem";
            this.getColToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.getColToolStripMenuItem.Text = "仅留当前列";
            this.getColToolStripMenuItem.Click += new System.EventHandler(this.getColToolStripMenuItem_Click);
            // 
            // MatrixGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.paraToolStrip);
            this.Controls.Add(this.funcToolStrip);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(352, 252);
            this.Name = "MatrixGenerator";
            this.Size = new System.Drawing.Size(352, 252);
            this.funcToolStrip.ResumeLayout(false);
            this.funcToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matrixDataGridView)).EndInit();
            this.tableContextMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip funcToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStrip paraToolStrip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton actionToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton workspaceToolStripDropDownButton;
        private System.Windows.Forms.ToolStripButton clearToolStripButton;
        private System.Windows.Forms.ToolStripComboBox funcToolStripComboBox;
        private System.Windows.Forms.ContextMenuStrip tableContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem rowAssimilateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colAssimilateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cellGoUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cellGoDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cellGoLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cellGoRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRowColToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rowGoUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rowGoDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colGoLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colGoRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem rowAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem belowAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteColToolStripMenuItem;
        private System.Windows.Forms.DataGridView matrixDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem getRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getColToolStripMenuItem;

    }
}
