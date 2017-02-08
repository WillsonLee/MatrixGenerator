using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MatrixTool;

namespace MatrixGenerator
{
    /// <summary>
    /// 矩阵生成器类
    /// </summary>
    /// 
    [Serializable]
    public partial class MatrixGenerator: UserControl
    {
        private List<Matrix> workspace = new List<Matrix>();//工作空间保存的矩阵
        private Matrix currentMatrix;
        private int selectedIndex = -1;//-1表示没选中
        private int funcIndex = 0;//选中函数序号
        private bool checkedState;
        /// <summary>
        /// 当前的矩阵
        /// </summary>
        public Matrix CurrentMatrix
        {
            get { return currentMatrix; }
            set
            {
                currentMatrix = value;
                #region 刷新视图操作
                //如果当前矩阵设为null
                try
                {
                    int row_test = currentMatrix.Rows;
                    saveToolStripButton.Enabled = true;
                    matrixDataGridView.ContextMenuStrip = tableContextMenuStrip; 
                }
                catch
                {
                    matrixDataGridView.ContextMenuStrip = null;
                    for (int i = 0; i < matrixDataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < matrixDataGridView.Columns.Count; j++)
                        {
                            matrixDataGridView[j, i].Value = "";
                            matrixDataGridView[j, i].ReadOnly = true;
                        }
                    }
                    saveToolStripButton.Enabled = false;
                    toolStripStatusLabel1.Text = "当前矩阵为null";
                    return; 
                }
                //调整显示行数并加上行号
                if (currentMatrix.Rows > matrixDataGridView.Rows.Count)
                {
                    for (int i = matrixDataGridView.Rows.Count + 1; i <= currentMatrix.Rows; i++)
                    {
                        matrixDataGridView.Rows.Add();
                        matrixDataGridView.Rows[matrixDataGridView.Rows.Count - 1].HeaderCell.Value = "R" + Convert.ToString(i);
                    }
                }
                //调整显示列数并加上列号
                if (currentMatrix.Columns > matrixDataGridView.Columns.Count)
                {
                    for (int i = matrixDataGridView.Columns.Count + 1; i <= currentMatrix.Columns; i++)
                    {
                        matrixDataGridView.Columns.Add("Column" + Convert.ToString(i), "C" + Convert.ToString(i));
                        matrixDataGridView.Columns[i - 1].SortMode = DataGridViewColumnSortMode.NotSortable;//使列不可排序
                        if (i > 654)
                        {
                            break;
                        }
                    }
                }
                //只刷新显示部分
                int first_rowIndex = matrixDataGridView.FirstDisplayedScrollingRowIndex;
                int first_colIndex = matrixDataGridView.FirstDisplayedScrollingColumnIndex;
                int row_display_count = matrixDataGridView.DisplayedRowCount(true);
                int col_display_count = matrixDataGridView.DisplayedColumnCount(true);
                for (int i = 0; i < row_display_count; i++)
                {
                    for (int j = 0; j < col_display_count; j++)
                    {
                        int row = i + first_rowIndex;
                        int col = j + first_colIndex;
                        if (row >= 0 && row < currentMatrix.Rows && col >= 0 && col < currentMatrix.Columns)
                        {
                            matrixDataGridView[col, row].Value = currentMatrix[row, col].ToString();
                            matrixDataGridView[col, row].ReadOnly = false;
                        }
                        else
                        {
                            matrixDataGridView[col, row].Value = "";
                            matrixDataGridView[col, row].ReadOnly = true;
                        }
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 选中工作空间的矩阵序号
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value>=-1)
                {
                    selectedIndex = value;
                    //后续处理操作
                    if (selectedIndex > -1)//有选中的才更改视图
                    {
                        CurrentMatrix = new Matrix(workspace.ElementAt(selectedIndex));
                        toolStripStatusLabel1.Text = "选中工作空间序号为" + Convert.ToString(selectedIndex) + "的矩阵";
                    }
                }
            }
        }
        /// <summary>
        /// 工作空间中的变量(只读)
        /// </summary>
        public List<Matrix> Workspace
        {
            get { return workspace; }
            set{ }
        }
        /// <summary>
        /// 变量空间的变量名列表
        /// </summary>
        public List<string> varNameList = new List<string>();

        /// <summary>
        /// 构造一个矩阵生成器对象
        /// </summary>
        public MatrixGenerator()
        {
            InitializeComponent();
            CurrentMatrix = Matrix.Ones(4);
            CurrentMatrix = null;
            funcToolStripComboBox.SelectedIndex = 5;
            toolStripStatusLabel1.Text = "就绪...";
        }

        #region 辅助函数
        //参数输入工具栏重置
        private void ResetParaToolStrip()
        {
            int itemNum_temp = paraToolStrip.Items.Count;
            for (int i = 0; i < itemNum_temp; i++)//先把参数工具栏清空
            {
                paraToolStrip.Items.RemoveAt(0);
            }
            paraToolStrip.Items.Add(new ToolStripLabel("参数:"));
            switch (funcIndex)
            {
                case 0:
                    #region case0
                    paraToolStrip.Items.Add(new ToolStripLabel("阶数:"));
                    paraToolStrip.Items.Add(new ToolStripTextBox());
                    break;//eye
                    #endregion
                case 1:
                    #region case1
                    paraToolStrip.Items.Add(new ToolStripLabel("起始值:"));
                    ToolStripTextBox temp = new ToolStripTextBox();
                    temp.TextBox.Width = 45;
                    paraToolStrip.Items.Add(temp);
                    paraToolStrip.Items.Add(new ToolStripLabel("终止值:"));
                    temp = new ToolStripTextBox();
                    temp.TextBox.Width = 45;
                    paraToolStrip.Items.Add(temp);
                    paraToolStrip.Items.Add(new ToolStripLabel("分段数:"));
                    temp = new ToolStripTextBox();
                    temp.TextBox.Width = 45;
                    paraToolStrip.Items.Add(temp);
                    CheckBox toCol_Set = new CheckBox();
                    toCol_Set.Checked = true;
                    checkedState = true;
                    toCol_Set.CheckedChanged += new EventHandler(this.Reset_case1_checkedChanged);
                    ToolStripControlHost host1 = new ToolStripControlHost(toCol_Set);
                    paraToolStrip.Items.Add(host1);
                    paraToolStrip.Items.Add(new ToolStripLabel("转置"));
                    break;//Linspace
                    #endregion
                case 2:
                    #region case2
                    paraToolStrip.Items.Add(new ToolStripLabel("起始值:"));
                    ToolStripTextBox temp2 = new ToolStripTextBox();
                    temp2.TextBox.Width = 45;
                    paraToolStrip.Items.Add(temp2);
                    paraToolStrip.Items.Add(new ToolStripLabel("终止值:"));
                    temp2 = new ToolStripTextBox();
                    temp2.TextBox.Width = 45;
                    paraToolStrip.Items.Add(temp2);
                    paraToolStrip.Items.Add(new ToolStripLabel("分段数:"));
                    temp2 = new ToolStripTextBox();
                    temp2.TextBox.Width = 45;
                    paraToolStrip.Items.Add(temp2);
                    CheckBox toCol_Set2 = new CheckBox();
                    toCol_Set2.Checked = true;
                    checkedState = true;
                    toCol_Set2.CheckedChanged += new EventHandler(this.Reset_case1_checkedChanged);
                    ToolStripControlHost host2 = new ToolStripControlHost(toCol_Set2);
                    paraToolStrip.Items.Add(host2);
                    paraToolStrip.Items.Add(new ToolStripLabel("转置"));
                    break;//Logspace
                    #endregion
                case 3:
                    #region case3
                    paraToolStrip.Items.Add(new ToolStripLabel("行数:"));
                    ToolStripTextBox temp3 = new ToolStripTextBox();
                    temp3.TextBox.Width = 40;
                    paraToolStrip.Items.Add(temp3);
                    paraToolStrip.Items.Add(new ToolStripLabel("列数:"));
                    temp3 = new ToolStripTextBox();
                    temp3.TextBox.Width = 40;
                    paraToolStrip.Items.Add(temp3);
                    CheckBox equalRowCol = new CheckBox();
                    equalRowCol.Checked = false;
                    equalRowCol.CheckedChanged += new System.EventHandler(this.Reset_case3_checkedChanged);
                    ToolStripControlHost host_temp = new ToolStripControlHost(equalRowCol);
                    paraToolStrip.Items.Add(host_temp);
                    paraToolStrip.Items.Add(new ToolStripLabel("限定行列数相同"));
                    break;//Ones
                    #endregion
                case 4:
                    #region case4
                    paraToolStrip.Items.Add(new ToolStripLabel("行数:"));
                    ToolStripTextBox temp4 = new ToolStripTextBox();
                    temp4.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp4);
                    paraToolStrip.Items.Add(new ToolStripLabel("列数:"));
                    temp4 = new ToolStripTextBox();
                    temp4.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp4);
                    CheckBox equalRowCol4 = new CheckBox();
                    equalRowCol4.Checked = false;
                    equalRowCol4.CheckedChanged += new System.EventHandler(this.Reset_case3_checkedChanged);
                    ToolStripControlHost host_temp4 = new ToolStripControlHost(equalRowCol4);
                    paraToolStrip.Items.Add(host_temp4);
                    paraToolStrip.Items.Add(new ToolStripLabel("限定行列数相同"));
                    paraToolStrip.Items.Add(new ToolStripLabel("随机下限:"));
                    temp4 = new ToolStripTextBox();
                    temp4.TextBox.Width = 90;
                    temp4.Text = "0";
                    paraToolStrip.Items.Add(temp4);
                    paraToolStrip.Items.Add(new ToolStripLabel("随机上限:"));
                    temp4 = new ToolStripTextBox();
                    temp4.TextBox.Width = 90;
                    temp4.Text = "1";
                    paraToolStrip.Items.Add(temp4);
                    break;//Random
                    #endregion
                case 5:
                    #region case5
                    paraToolStrip.Items.Add(new ToolStripLabel("起始值:"));
                    ToolStripTextBox temp5=new ToolStripTextBox();
                    temp5.TextBox.Width=50;
                    paraToolStrip.Items.Add(temp5);
                    paraToolStrip.Items.Add(new ToolStripLabel("步距:"));
                    temp5=new ToolStripTextBox();
                    temp5.TextBox.Width=50;
                    paraToolStrip.Items.Add(temp5);
                    paraToolStrip.Items.Add(new ToolStripLabel("终止值:"));
                    temp5=new ToolStripTextBox();
                    temp5.TextBox.Width=50;
                    paraToolStrip.Items.Add(temp5);
                    CheckBox toCol_Set5 = new CheckBox();
                    toCol_Set5.Checked = true;
                    checkedState = true;
                    toCol_Set5.CheckedChanged += new EventHandler(this.Reset_case1_checkedChanged);
                    ToolStripControlHost host5 = new ToolStripControlHost(toCol_Set5);
                    paraToolStrip.Items.Add(host5);
                    paraToolStrip.Items.Add(new ToolStripLabel("转置"));
                    break;//RangeVector
                    #endregion
                case 6:
                    #region case6
                    paraToolStrip.Items.Add(new ToolStripLabel("EI="));
                    ToolStripTextBox temp6=new ToolStripTextBox();
                    temp6.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp6);
                    paraToolStrip.Items.Add(new ToolStripLabel("EA="));
                    temp6=new ToolStripTextBox();
                    temp6.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp6);
                    paraToolStrip.Items.Add(new ToolStripLabel("L="));
                    temp6=new ToolStripTextBox();
                    temp6.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp6);
                    break;//StiffnessMatrix
                    #endregion
                case 7:
                    #region case7
                    paraToolStrip.Items.Add(new ToolStripLabel("角度:"));
                    ToolStripTextBox temp7 = new ToolStripTextBox();
                    paraToolStrip.Items.Add(temp7);
                    break;//TransMatrix
                    #endregion
                case 8:
                    #region case8
                    paraToolStrip.Items.Add(new ToolStripLabel("填充值:"));
                    ToolStripTextBox temp8 = new ToolStripTextBox();
                    temp8.Text = "0";
                    paraToolStrip.Items.Add(temp8);
                    break;//TriUp
                    #endregion
                case 9: 
                    #region case9
                    paraToolStrip.Items.Add(new ToolStripLabel("填充值:"));
                    ToolStripTextBox temp9 = new ToolStripTextBox();
                    temp9.Text = "0";
                    paraToolStrip.Items.Add(temp9);
                    break;//TriLow
                    #endregion
                case 10: 
                    #region case10
                    CheckBox reverse_set = new CheckBox();
                    reverse_set.Checked = false;
                    checkedState = false;
                    reverse_set.CheckedChanged += new EventHandler(this.Reset_case10_checkedChanged);
                    ToolStripControlHost host10 = new ToolStripControlHost(reverse_set);
                    paraToolStrip.Items.Add(host10);
                    paraToolStrip.Items.Add(new ToolStripLabel("保留下三角"));
                    break;//Symmetry
                    #endregion
                case 11: 
                    #region case11
                    CheckBox reverse_set11 = new CheckBox();
                    reverse_set11.Checked = false;
                    checkedState = false;
                    reverse_set11.CheckedChanged += new EventHandler(this.Reset_case10_checkedChanged);
                    ToolStripControlHost host11 = new ToolStripControlHost(reverse_set11);
                    paraToolStrip.Items.Add(host11);
                    paraToolStrip.Items.Add(new ToolStripLabel("保留下三角"));
                    break;//AntiSymmetry
                    #endregion
                case 12: break;//Reverse
                case 13: break;//Transfer
                case 14:
                    #region case14
                    paraToolStrip.Items.Add(new ToolStripLabel("行数:"));
                    ToolStripTextBox temp14=new ToolStripTextBox();
                    temp14.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp14);
                    paraToolStrip.Items.Add(new ToolStripLabel("列数:"));
                    temp14=new ToolStripTextBox();
                    temp14.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp14);
                    CheckBox reverse_set14 = new CheckBox();
                    reverse_set14.Checked = false;
                    checkedState = false;
                    reverse_set14.CheckedChanged += new EventHandler(this.Reset_case10_checkedChanged);
                    ToolStripControlHost host14 = new ToolStripControlHost(reverse_set14);
                    paraToolStrip.Items.Add(host14);
                    paraToolStrip.Items.Add(new ToolStripLabel("从行列处往后取"));
                    break;//SubMatrix
                    #endregion
                case 15: break;//ToStepMatrix
                case 16:
                    #region case16
                    paraToolStrip.Items.Add(new ToolStripLabel("行数:"));
                    ToolStripTextBox temp16 = new ToolStripTextBox();
                    temp16.TextBox.Width = 40;
                    paraToolStrip.Items.Add(temp16);
                    paraToolStrip.Items.Add(new ToolStripLabel("列数:"));
                    temp16 = new ToolStripTextBox();
                    temp16.TextBox.Width = 40;
                    paraToolStrip.Items.Add(temp16);
                    break;//Reshape
                    #endregion
                case 17: 
                    #region case17
                    paraToolStrip.Items.Add(new ToolStripLabel("获取行号:"));
                    ToolStripTextBox temp17 = new ToolStripTextBox();
                    paraToolStrip.Items.Add(temp17);
                    break;//GetRowVector
                    #endregion
                case 18: 
                    #region case18
                    paraToolStrip.Items.Add(new ToolStripLabel("获取列号:"));
                    ToolStripTextBox temp18 = new ToolStripTextBox();
                    paraToolStrip.Items.Add(temp18);
                    break;//GetColVector
                    #endregion
                case 19: break;//MinOfRow
                case 20: break;//MinOfCol
                case 21: break;//MaxOfRow
                case 22: break;//MaxOfCol
                case 23: break;//SumRow
                case 24: break;//SumCol
                case 25: break;//MapMinMax
                case 26: break;//MapMinMaxOnCol
                case 27: break;//Positive
                case 28: break;//Negative
                case 29:
                    #region case29
                    paraToolStrip.Items.Add(new ToolStripLabel("互换行:"));
                    ToolStripTextBox temp29=new ToolStripTextBox();
                    temp29.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp29);
                    paraToolStrip.Items.Add(new ToolStripLabel("互换行:"));
                    temp29=new ToolStripTextBox();
                    temp29.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp29);
                    break;//RowSwitch
                    #endregion
                case 30: 
                    #region case30
                    paraToolStrip.Items.Add(new ToolStripLabel("互换列:"));
                    ToolStripTextBox temp30=new ToolStripTextBox();
                    temp30.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp30);
                    paraToolStrip.Items.Add(new ToolStripLabel("互换列:"));
                    temp30=new ToolStripTextBox();
                    temp30.TextBox.Width=60;
                    paraToolStrip.Items.Add(temp30);
                    break;//ColumnSwitch
                    #endregion
                case 31: 
                    #region case31
                    paraToolStrip.Items.Add(new ToolStripLabel("倍乘行号:"));
                    ToolStripTextBox temp31=new ToolStripTextBox();
                    temp31.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp31);
                    paraToolStrip.Items.Add(new ToolStripLabel("倍数:"));
                    temp31=new ToolStripTextBox();
                    temp31.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp31);
                    break;//RowMultiple
                    #endregion
                case 32: 
                    #region case32
                    paraToolStrip.Items.Add(new ToolStripLabel("倍乘列号:"));
                    ToolStripTextBox temp32=new ToolStripTextBox();
                    temp32.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp32);
                    paraToolStrip.Items.Add(new ToolStripLabel("倍数:"));
                    temp32=new ToolStripTextBox();
                    temp32.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp32);
                    break;//ColumnMultiple
                    #endregion
                case 33:
                    #region case33
                    paraToolStrip.Items.Add(new ToolStripLabel("倍加行:"));
                    ToolStripTextBox temp33=new ToolStripTextBox();
                    temp33.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp33);
                    paraToolStrip.Items.Add(new ToolStripLabel("倍乘行:"));
                    temp33=new ToolStripTextBox();
                    temp33.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp33);
                    paraToolStrip.Items.Add(new ToolStripLabel("倍数:"));
                    temp33=new ToolStripTextBox();
                    temp33.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp33);
                    break;//RowMulAdd
                    #endregion
                case 34: 
                    #region case34
                    paraToolStrip.Items.Add(new ToolStripLabel("倍加列:"));
                    ToolStripTextBox temp34=new ToolStripTextBox();
                    temp34.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp34);
                    paraToolStrip.Items.Add(new ToolStripLabel("倍乘列:"));
                    temp34=new ToolStripTextBox();
                    temp34.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp34);
                    paraToolStrip.Items.Add(new ToolStripLabel("倍数:"));
                    temp34=new ToolStripTextBox();
                    temp34.TextBox.Width = 50;
                    paraToolStrip.Items.Add(temp34);
                    break;//ColumnMulAdd
                    #endregion
                case 35: break;//Abs
                case 36: break;//Acos
                case 37: break;//Asin
                case 38: break;//Atan
                case 39: break;//Ceiling
                case 40: break;//Cos
                case 41: break;//Cosh
                case 42: break;//Exp
                case 43: break;//Floor
                case 44: break;//Log
                case 45: break;//Log10
                case 46: break;//Round
                case 47: break;//Sign
                case 48: break;//Sin
                case 49: break;//Sinh
                case 50: break;//Sqrt
                case 51: break;//Tan
                case 52: break;//Tanh
                case 53: break;//Truncate
                case 54:
                    #region case54
                    paraToolStrip.Items.Add(new ToolStripLabel("当前矩阵"));
                    ToolStripComboBox symbolFun = new ToolStripComboBox();
                    symbolFun.ComboBox.Width = 5;
                    symbolFun.ComboBox.Items.Add("+");
                    symbolFun.ComboBox.Items.Add("-");
                    symbolFun.ComboBox.Items.Add("*");
                    symbolFun.ComboBox.Items.Add("/");
                    symbolFun.ComboBox.Items.Add("\\");
                    symbolFun.ComboBox.Items.Add("%");
                    symbolFun.ComboBox.Items.Add(".*");
                    symbolFun.ComboBox.Items.Add("./");
                    symbolFun.ComboBox.Items.Add(">");
                    symbolFun.ComboBox.Items.Add(">=");
                    symbolFun.ComboBox.Items.Add("<");
                    symbolFun.ComboBox.Items.Add("<=");
                    symbolFun.FlatStyle = FlatStyle.Standard;
                    symbolFun.DropDownStyle = ComboBoxStyle.DropDownList;
                    symbolFun.SelectedIndex = 0;
                    paraToolStrip.Items.Add(symbolFun);
                    ToolStripComboBox rightVal = new ToolStripComboBox();
                    rightVal.ComboBox.Width = 30;
                    for (int i = 0; i < workspaceToolStripDropDownButton.DropDownItems.Count; i++)
                    {
                        rightVal.Items.Add(workspaceToolStripDropDownButton.DropDownItems[i].Text);
                    }
                    if (rightVal.Items.Count > 0)
                    {
                        rightVal.SelectedIndex = 0;
                    }
                    rightVal.FlatStyle = FlatStyle.Standard;
                    rightVal.DropDownStyle = ComboBoxStyle.DropDownList;
                    paraToolStrip.Items.Add(rightVal);
                    #endregion
                    break;//符号运算
            }
        }
        //函数实现
        private void PerformFunction()
        {
            switch (funcIndex)
            {
                case 0:
                    #region case0
                    int dim;
                    try
                    {
                        dim = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        if (dim <= 0)//阶数为负也算参数错误
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.Eye(dim);
                        toolStripStatusLabel1.Text = "已生成" + Convert.ToString(dim) + "阶单位数组";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//eye
                    #endregion
                case 1:
                    #region case1
                    double begin, end;
                    int num;
                    try
                    {
                        begin = Convert.ToDouble(paraToolStrip.Items[2].Text);
                        end = Convert.ToDouble(paraToolStrip.Items[4].Text);
                        num = Convert.ToInt32(paraToolStrip.Items[6].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        Matrix tempM = Matrix.LinspaceVector(begin, end, num);
                        if (checkedState)
                        {
                            tempM=Matrix.Transfer(tempM);
                        }
                        CurrentMatrix = tempM;
                        toolStripStatusLabel1.Text = "已生成线性空间向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Linspace
                    #endregion
                case 2:
                    #region case2
                    double begin2, end2;
                    int num2;
                    try
                    {
                        begin2 = Convert.ToDouble(paraToolStrip.Items[2].Text);
                        end2 = Convert.ToDouble(paraToolStrip.Items[4].Text);
                        num2 = Convert.ToInt32(paraToolStrip.Items[6].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        Matrix tempM = Matrix.LogspaceVector(begin2, end2, num2);
                        if (checkedState)
                        {
                            tempM = Matrix.Transfer(tempM);
                        }
                        CurrentMatrix = tempM;
                        toolStripStatusLabel1.Text = "已生成线性空间向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Logspace
                    #endregion
                case 3:
                    #region case3
                    int row, col;
                    try
                    {
                        row = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        col = Convert.ToInt32(paraToolStrip.Items[4].Text);
                        if (row <= 0 || col <= 0)
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.Ones(row, col);
                        toolStripStatusLabel1.Text = "已生成全为1矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Ones
                    #endregion
                case 4:
                    #region case4
                    int row4, col4;
                    double min, max;
                    try
                    {
                        row4 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        col4 = Convert.ToInt32(paraToolStrip.Items[4].Text);
                        min = Convert.ToDouble(paraToolStrip.Items[8].Text);
                        max = Convert.ToDouble(paraToolStrip.Items[10].Text);
                        if (row4 <= 0 || col4 <= 0)
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.Random(row4, col4, min, max);
                        toolStripStatusLabel1.Text = "已生成随机矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Random
                    #endregion
                case 5:
                    #region case5
                    double begin5, incre, end5;
                    try
                    {
                        begin5 = Convert.ToDouble(paraToolStrip.Items[2].Text);
                        incre = Convert.ToDouble(paraToolStrip.Items[4].Text);
                        end5 = Convert.ToDouble(paraToolStrip.Items[6].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        Matrix tempM = Matrix.RangeVector(begin5, incre, end5);
                        if (checkedState)
                        {
                            tempM = Matrix.Transfer(tempM);
                        }
                        CurrentMatrix = tempM;
                        toolStripStatusLabel1.Text = "已生成递增(减)向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//RangeVector
                    #endregion
                case 6:
                    #region case6
                    double ei, ea, l;
                    try
                    {
                        ei = Convert.ToDouble(paraToolStrip.Items[2].Text);
                        ea = Convert.ToDouble(paraToolStrip.Items[4].Text);
                        l = Convert.ToDouble(paraToolStrip.Items[6].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.StiffnessMatrix(ei, ea, l);
                        toolStripStatusLabel1.Text = "已生成刚度矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//StiffnessMatrix
                    #endregion
                case 7: 
                    #region case7
                    double angle;
                    try
                    {
                        angle = Convert.ToDouble(paraToolStrip.Items[2].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.TransMatrix(angle);
                        toolStripStatusLabel1.Text = "已生成局部坐标系到整体坐标系转换矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//TransMatrix
                    #endregion
                case 8: 
                    #region case8
                    double fill;
                    try
                    {
                        fill = Convert.ToDouble(paraToolStrip.Items[2].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.TriUp(CurrentMatrix, fill);
                        toolStripStatusLabel1.Text = "已生成上三角矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//TriUp
                    #endregion
                case 9: 
                    #region case9
                    double fill9;
                    try
                    {
                        fill9 = Convert.ToDouble(paraToolStrip.Items[2].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.TriLow(CurrentMatrix, fill9);
                        toolStripStatusLabel1.Text = "已生成下三角矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//TriLow
                    #endregion
                case 10: 
                    #region case10
                    try
                    {
                        CurrentMatrix = Matrix.Symmetry(CurrentMatrix, checkedState);
                        toolStripStatusLabel1.Text = "已生成对称矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Symmetry
                    #endregion
                case 11:
                    #region case11
                    try
                    {
                        CurrentMatrix = Matrix.Antisymmetry(CurrentMatrix, checkedState);
                        toolStripStatusLabel1.Text = "已生成反对称矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//AntiSymmetry
                    #endregion
                case 12: 
                    #region case12
                    try
                    {
                        CurrentMatrix = Matrix.Reverse(CurrentMatrix);
                        toolStripStatusLabel1.Text = "求逆完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Reverse
                    #endregion
                case 13:
                    #region case13
                    try
                    {
                        CurrentMatrix = Matrix.Transfer(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已转置";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Transfer
                    #endregion
                case 14:
                    #region case14
                    int row14, col14;
                    try
                    {
                        row14 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        col14 = Convert.ToInt32(paraToolStrip.Items[4].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.SubMatrix(CurrentMatrix, row14, col14, checkedState);
                        toolStripStatusLabel1.Text = "已生成子矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//SubMatrix
                    #endregion
                case 15:
                    #region case15
                    try
                    {
                        CurrentMatrix = Matrix.ToStepMatrix(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已化为阶梯矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//ToStepMatrix
                    #endregion
                case 16:
                    #region case16
                    int row16, col16;
                    try
                    {
                        row16 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        col16 = Convert.ToInt32(paraToolStrip.Items[4].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.Reshape(CurrentMatrix, row16, col16);
                        toolStripStatusLabel1.Text = "已生成重排矩阵";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Reshape
                    #endregion
                case 17: 
                    #region case17
                    int row17;
                    try
                    {
                        row17 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.GetRowVector(CurrentMatrix, row17 - 1);
                        toolStripStatusLabel1.Text = "已取出行向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//GetRowVector
                    #endregion
                case 18: 
                    #region case18
                    int col18;
                    try
                    {
                        col18 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.GetColVector(CurrentMatrix, col18 - 1);
                        toolStripStatusLabel1.Text = "已取出列向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//GetColVector
                    #endregion
                case 19:
                    #region case19
                    try
                    {
                        CurrentMatrix = Matrix.MinOfRow(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已取出列向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//MinOfRow
                    #endregion
                case 20:
                    #region case20
                    try
                    {
                        CurrentMatrix = Matrix.MinOfCol(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已取出行向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//MinOfCol
                    #endregion
                case 21:
                    #region case21
                    try
                    {
                        CurrentMatrix = Matrix.MaxOfRow(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已取出列向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//MaxOfRow
                    #endregion
                case 22:
                    #region case22
                    try
                    {
                        CurrentMatrix = Matrix.MaxOfCol(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已取出行向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//MaxOfCol
                    #endregion
                case 23:
                    #region case23
                    try
                    {
                        CurrentMatrix = Matrix.SumRow(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已取出行向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//SumRow
                    #endregion
                case 24:
                    #region case24
                    try
                    {
                        CurrentMatrix = Matrix.SumCol(CurrentMatrix);
                        toolStripStatusLabel1.Text = "已取出列向量";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//SumCol
                    #endregion
                case 25:
                    #region case25
                    try
                    {
                        CurrentMatrix = Matrix.MapMinMaxRow(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵范围上下限向量已取出";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//MapMinMax
                    #endregion
                case 26:
                    #region case26
                    try
                    {
                        CurrentMatrix = Matrix.MapMinMaxOnCol(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵范围上下限向量已取出";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//MapMinMaxOnCol
                    #endregion
                case 27:
                    #region case27
                    try
                    {
                        CurrentMatrix = Matrix.Positive(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵元素已取正";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Positive
                    #endregion
                case 28:
                    #region case28
                    try
                    {
                        CurrentMatrix = Matrix.Negative(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵元素已取负";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Negative
                    #endregion
                case 29: 
                    #region case29
                    int row29, col29;
                    try
                    {
                        row29 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        col29 = Convert.ToInt32(paraToolStrip.Items[4].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.RowSwitch(CurrentMatrix, row29 - 1, col29 - 1);
                        toolStripStatusLabel1.Text = "行互换完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//RowSwitch
                    #endregion
                case 30: 
                    #region case30
                    int col30_1, col30_2;
                    try
                    {
                        col30_1 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        col30_2 = Convert.ToInt32(paraToolStrip.Items[4].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.ColumnSwitch(CurrentMatrix, col30_1 - 1, col30_2 - 1);
                        toolStripStatusLabel1.Text = "列互换完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//ColumnSwitch
                    #endregion
                case 31: 
                    #region case31
                    int row31;
                    double mul;
                    try
                    {
                        row31 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        mul = Convert.ToDouble(paraToolStrip.Items[4].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.RowMultiple(CurrentMatrix, row31 - 1, mul);
                        toolStripStatusLabel1.Text = "倍乘完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//RowMultiple
                    #endregion
                case 32:
                    #region case32
                    int col32;
                    double mul32;
                    try
                    {
                        col32 = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        mul32 = Convert.ToDouble(paraToolStrip.Items[4].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.ColumnMultiple(CurrentMatrix, col32 - 1, mul32);
                        toolStripStatusLabel1.Text = "倍乘完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//ColumnMultiple
                    #endregion
                case 33:
                    #region case33
                    int row_to_add, row_src;
                    double mul33;
                    try
                    {
                        row_to_add = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        row_src = Convert.ToInt32(paraToolStrip.Items[4].Text);
                        mul33 = Convert.ToDouble(paraToolStrip.Items[6].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.RowMulAdd(CurrentMatrix, row_to_add - 1, mul33, row_src - 1);
                        toolStripStatusLabel1.Text = "行倍加完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//RowMulAdd
                    #endregion
                case 34: 
                    #region case34
                    int col_to_add, col_src;
                    double mul34;
                    try
                    {
                        col_to_add = Convert.ToInt32(paraToolStrip.Items[2].Text);
                        col_src = Convert.ToInt32(paraToolStrip.Items[4].Text);
                        mul34 = Convert.ToDouble(paraToolStrip.Items[6].Text);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        CurrentMatrix = Matrix.ColumnMulAdd(CurrentMatrix, col_src - 1, mul34, col_to_add - 1);
                        toolStripStatusLabel1.Text = "列倍加完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//ColumnMulAdd
                    #endregion
                case 35:
                    #region case35
                    try
                    {
                        CurrentMatrix = Matrix.Abs(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取绝对值完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Abs
                    #endregion
                case 36:
                    #region case36
                    try
                    {
                        CurrentMatrix = Matrix.Acos(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取反余弦成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Acos
                    #endregion
                case 37:
                    #region case37
                    try
                    {
                        CurrentMatrix = Matrix.Asin(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取反正弦成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Asin
                    #endregion
                case 38:
                    #region case38
                    try
                    {
                        CurrentMatrix = Matrix.Atan(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取反正切成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Atan
                    #endregion
                case 39:
                    #region case39
                    try
                    {
                        CurrentMatrix = Matrix.Ceiling(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取最小上限值成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Ceiling
                    #endregion
                case 40:
                    #region case40
                    try
                    {
                        CurrentMatrix = Matrix.Cos(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取正弦值成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Cos
                    #endregion
                case 41:
                    #region case41
                    try
                    {
                        CurrentMatrix = Matrix.Cosh(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取正弦双曲值成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Cosh
                    #endregion
                case 42:
                    #region case42
                    try
                    {
                        CurrentMatrix = Matrix.Exp(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取自然指数值成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Exp
                    #endregion
                case 43:
                    #region case43
                    try
                    {
                        CurrentMatrix = Matrix.Floor(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取最大下限值成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Floor
                    #endregion
                case 44:
                    #region case44
                    try
                    {
                        CurrentMatrix = Matrix.Log(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取自然对数成功";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Log
                    #endregion
                case 45:
                    #region case45
                    try
                    {
                        CurrentMatrix = Matrix.Log10(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素以10为底取对数完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Log10
                    #endregion
                case 46:
                    #region case46
                    try
                    {
                        CurrentMatrix = Matrix.Round(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素四舍五入完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Round
                    #endregion
                case 47:
                    #region case47
                    try
                    {
                        CurrentMatrix = Matrix.Sign(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素求符号函数值完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Sign
                    #endregion
                case 48:
                    #region case48
                    try
                    {
                        CurrentMatrix = Matrix.Sin(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取正弦值完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Sin
                    #endregion
                case 49:
                    #region case49
                    try
                    {
                        CurrentMatrix = Matrix.Sinh(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取正弦双曲值完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Sinh
                    #endregion
                case 50:
                    #region case50
                    try
                    {
                        CurrentMatrix = Matrix.Sqrt(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素开根号完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Sqrt
                    #endregion
                case 51:
                    #region case51
                    try
                    {
                        CurrentMatrix = Matrix.Tan(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取正切值完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Tan
                    #endregion
                case 52:
                    #region case52
                    try
                    {
                        CurrentMatrix = Matrix.Tanh(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取正切双曲值完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Tanh
                    #endregion
                case 53:
                    #region case53
                    try
                    {
                        CurrentMatrix = Matrix.Truncate(CurrentMatrix);
                        toolStripStatusLabel1.Text = "矩阵所有元素取整数部分完成";
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//Truncate
                    #endregion
                case 54:
                #region case54
                    Matrix rightPara;
                    try
                    {
                        ToolStripComboBox comboTemp54 = (ToolStripComboBox)(paraToolStrip.Items[3]);
                        rightPara = workspace.ElementAt(comboTemp54.SelectedIndex);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "参数输入有误!";
                        break;
                    }
                    try
                    {
                        ToolStripComboBox comboTemp54 = (ToolStripComboBox)(paraToolStrip.Items[2]);
                        switch (comboTemp54.SelectedIndex)
                        {
                            case 0: CurrentMatrix = this.Add(rightPara); break;
                            case 1: CurrentMatrix = this.Minus(rightPara); break;
                            case 2: CurrentMatrix = this.Multiple(rightPara); break;
                            case 3: CurrentMatrix = this.RightDevide(rightPara); break;
                            case 4: CurrentMatrix = this.LeftDevide(rightPara); break;
                            case 5: CurrentMatrix = this.Mod(rightPara); break;
                            case 6: CurrentMatrix = this.DotMultiple(rightPara); break;
                            case 7: CurrentMatrix = this.DotDevide(rightPara); break;
                            case 8: CurrentMatrix = this.MoreThan(rightPara); break;
                            case 9: CurrentMatrix = this.NotLessThan(rightPara); break;
                            case 10: CurrentMatrix = this.LessThan(rightPara); break;
                            case 11: CurrentMatrix = this.NotMoreThan(rightPara); break;
                        }
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "函数调用出错!";
                    }
                    break;//符号运算
                #endregion
            }
        }
        //函数描述
        private void FunctionTip()
        {
            switch (funcIndex)
            {
                case 0: toolStripStatusLabel1.Text = "当前命令将生成单位矩阵"; break;//eye
                case 1: toolStripStatusLabel1.Text = "当前命令将生成线性空间向量"; break;//Linspace
                case 2: toolStripStatusLabel1.Text = "当前命令将生成对数空间向量"; break;//Logspace
                case 3: toolStripStatusLabel1.Text = "当前命令将生成元素全为1的矩阵"; break;//Ones
                case 4: toolStripStatusLabel1.Text = "当前命令将生成随机数矩阵"; break;//Random
                case 5: toolStripStatusLabel1.Text = "当前命令将生成递增(减)行向量"; break;//RangeVector
                case 6: toolStripStatusLabel1.Text = "当前命令将生成结构力学中的刚度矩阵"; break;//StiffnessMatrix
                case 7: toolStripStatusLabel1.Text = "当前命令将生成结构力学中的转换矩阵"; break;//TransMatrix
                case 8: toolStripStatusLabel1.Text = "当前命令将当前矩阵变换成上三角矩阵"; break;//TriUp
                case 9: toolStripStatusLabel1.Text = "当前命令将当前矩阵变换成下三角矩阵"; break;//TriLow
                case 10: toolStripStatusLabel1.Text = "当前命令将当前矩阵变换成对称矩阵"; break;//Symmetry
                case 11: toolStripStatusLabel1.Text = "当前命令将当前矩阵变换成反对称矩阵"; break;//AntiSymmetry
                case 12: toolStripStatusLabel1.Text = "当前命令将当前矩阵求逆"; break;//Reverse
                case 13: toolStripStatusLabel1.Text = "当前命令将当前矩阵转置"; break;//Transfer
                case 14: toolStripStatusLabel1.Text = "当前命令将当前矩阵提取子矩阵"; break;//SubMatrix
                case 15: toolStripStatusLabel1.Text = "当前命令将当前矩阵变换成阶梯矩阵"; break;//ToStepMatrix
                case 16: toolStripStatusLabel1.Text = "当前命令将当前矩阵重排"; break;//Reshape
                case 17: toolStripStatusLabel1.Text = "当前命令将当前矩阵提取行向量"; break;//GetRowVector
                case 18: toolStripStatusLabel1.Text = "当前命令将当前矩阵提取列向量"; break;//GetColVector
                case 19: toolStripStatusLabel1.Text = "当前命令将当前矩阵提取每一行最小构成列向量"; break;//MinOfRow
                case 20: toolStripStatusLabel1.Text = "当前命令将当前矩阵提取每一列最小构成行向量"; break;//MinOfCol
                case 21: toolStripStatusLabel1.Text = "当前命令将当前矩阵提取每一行最大构成列向量"; break;//MaxOfRow
                case 22: toolStripStatusLabel1.Text = "当前命令将当前矩阵提取每一列最大构成行向量"; break;//MaxOfCol
                case 23: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有行求和"; break;//SumRow
                case 24: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有列求和"; break;//SumCol
                case 25: toolStripStatusLabel1.Text = "当前命令将当前矩阵求出每一列范围的行向量"; break;//MapMinMax
                case 26: toolStripStatusLabel1.Text = "当前命令将当前矩阵求出每一行范围的列向量"; break;//MapMinMaxOnCol
                case 27: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素取正"; break;//Positive
                case 28: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素取负"; break;//Negative
                case 29: toolStripStatusLabel1.Text = "当前命令将当前矩阵某两行互换"; break;//RowSwitch
                case 30: toolStripStatusLabel1.Text = "当前命令将当前矩阵某两列互换"; break;//ColumnSwitch
                case 31: toolStripStatusLabel1.Text = "当前命令将当前矩阵某一行倍乘"; break;//RowMultiple
                case 32: toolStripStatusLabel1.Text = "当前命令将当前矩阵某一列倍乘"; break;//ColumnMultiple
                case 33: toolStripStatusLabel1.Text = "当前命令将当前矩阵某一行倍加到另一行"; break;//RowMulAdd
                case 34: toolStripStatusLabel1.Text = "当前命令将当前矩阵某一列倍加到另一列"; break;//ColumnMulAdd
                case 35: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求绝对值"; break;//Abs
                case 36: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求反余弦"; break;//Acos
                case 37: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求反正弦"; break;//Asin
                case 38: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求反正切"; break;//Atan
                case 39: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求最小上限整数"; break;//Ceiling
                case 40: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求余弦值"; break;//Cos
                case 41: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求余弦双曲函数值"; break;//Cosh
                case 42: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求自然指数值"; break;//Exp
                case 43: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求最大下限整数"; break;//Floor
                case 44: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求自然对数值"; break;//Log
                case 45: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求10为底的对数值"; break;//Log10
                case 46: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求四舍五入值"; break;//Round
                case 47: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求符号函数值"; break;//Sign
                case 48: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求正弦值"; break;//Sin
                case 49: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求正弦双曲函数值"; break;//Sinh
                case 50: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求根号"; break;//Sqrt
                case 51: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求正切值"; break;//Tan
                case 52: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求正切双曲函数值"; break;//Tanh
                case 53: toolStripStatusLabel1.Text = "当前命令将当前矩阵所有元素求整数部分"; break;//Truncate
                case 54: toolStripStatusLabel1.Text = "当前命令将计算相应二目符号运算结果"; break;//符号运算
            }
        }
        //符号运算辅助
        private Matrix Add(Matrix right)
        {
            return currentMatrix + right;
        }
        private Matrix Minus(Matrix right)
        {
            return currentMatrix - right;
        }
        private Matrix Multiple(Matrix right)
        {
            return currentMatrix * right;
        }
        private Matrix RightDevide(Matrix right)
        {
            return currentMatrix / right;
        }
        private Matrix LeftDevide(Matrix right)
        {
            return Matrix.Reverse(currentMatrix) * right;
        }
        private Matrix Mod(Matrix right)
        {
            return currentMatrix % right;
        }
        private Matrix DotMultiple(Matrix right)
        {
            return Matrix.DotMultiple(currentMatrix, right);
        }
        private Matrix DotDevide(Matrix right)
        {
            return Matrix.DotDevide(currentMatrix, right);
        }
        private Matrix MoreThan(Matrix right)
        {
            return currentMatrix > right;
        }
        private Matrix NotLessThan(Matrix right)
        {
            return currentMatrix >= right;
        }
        private Matrix LessThan(Matrix right)
        {
            return currentMatrix < right;
        }
        private Matrix NotMoreThan(Matrix right)
        {
            return currentMatrix <= right;
        }
        //参数工具栏控件的事件监听
        private void Reset_case1_checkedChanged(object sender, EventArgs e)
        {
            CheckBox sender_obj = (CheckBox)sender;
            checkedState = sender_obj.Checked;
            if (!checkedState)
            {
                toolStripStatusLabel1.Text = "生成行向量效率将远低于列向量,且只显示655列";
            }
            else
            {
                toolStripStatusLabel1.Text = "";
            }
        }
        private void Reset_case3_checkedChanged(object sender, EventArgs e)
        {
            CheckBox sender_obj = (CheckBox)sender;
            if (sender_obj.Checked)
            {
                paraToolStrip.Items[4].Enabled = false;
                paraToolStrip.Items[4].Text = paraToolStrip.Items[2].Text;
                paraToolStrip.Items[2].TextChanged += new EventHandler(this.Reset_case3_checkedChanged_call);
            }
            else
            {
                paraToolStrip.Items[4].Enabled = true;
                paraToolStrip.Items[2].TextChanged -= new EventHandler(this.Reset_case3_checkedChanged_call);
            }
        }
        private void Reset_case3_checkedChanged_call(object sender, EventArgs e)
        {
            paraToolStrip.Items[4].Text = paraToolStrip.Items[2].Text;
        }
        private void Reset_case10_checkedChanged(object sender, EventArgs e)
        {
            CheckBox sender_obj = (CheckBox)sender;
            checkedState = sender_obj.Checked;
        }
        #endregion

        #region 上下文菜单响应
        private void rowAssimilateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                for (int i = 0; i < CurrentMatrix.Columns; i++)
                {
                    matrix_temp[row_sel, i] = matrix_temp[row_sel, col_sel];
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void colAssimilateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                for (int i = 0; i < CurrentMatrix.Rows; i++)
                {
                    matrix_temp[i, col_sel] = matrix_temp[row_sel, col_sel];
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void cellGoUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel > 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp[row_sel - 1, col_sel] = matrix_temp[row_sel, col_sel];
                CurrentMatrix = matrix_temp;
            }
        }

        private void cellGoDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < (CurrentMatrix.Rows - 1) && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp[row_sel + 1, col_sel] = matrix_temp[row_sel, col_sel];
                CurrentMatrix = matrix_temp;
            }
        }

        private void cellGoLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel > 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp[row_sel, col_sel - 1] = matrix_temp[row_sel, col_sel];
                CurrentMatrix = matrix_temp;
            }
        }

        private void cellGoRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < (CurrentMatrix.Columns - 1))
            {
                matrix_temp[row_sel, col_sel + 1] = matrix_temp[row_sel, col_sel];
                CurrentMatrix = matrix_temp;
            }
        }

        private void rowGoUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel > 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                for (int i = 0; i < CurrentMatrix.Columns; i++)
                {
                    matrix_temp[row_sel - 1, i] = matrix_temp[row_sel, i];
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void rowGoDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < (CurrentMatrix.Rows - 1) && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                for (int i = 0; i < CurrentMatrix.Columns; i++)
                {
                    matrix_temp[row_sel + 1, i] = matrix_temp[row_sel, i];
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void colGoLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel > 0 && col_sel < CurrentMatrix.Columns)
            {
                for (int i = 0; i < CurrentMatrix.Rows; i++)
                {
                    matrix_temp[i, col_sel - 1] = matrix_temp[i, col_sel];
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void colGoRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < (CurrentMatrix.Columns - 1))
            {
                for (int i = 0; i < CurrentMatrix.Rows; i++)
                {
                    matrix_temp[i, col_sel + 1] = matrix_temp[i, col_sel];
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void upAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.AddRow(matrix_temp, Matrix.Zeros(1, matrix_temp.Columns));
                int newRow = CurrentMatrix.Rows;
                while (row_sel != newRow)
                {
                    matrix_temp = Matrix.RowSwitch(matrix_temp, newRow - 1, newRow);
                    newRow -= 1;
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void belowAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.AddRow(matrix_temp, Matrix.Zeros(1, matrix_temp.Columns));
                int newRow = CurrentMatrix.Rows - 1;
                while (row_sel != newRow)
                {
                    matrix_temp = Matrix.RowSwitch(matrix_temp, newRow, newRow + 1);
                    newRow -= 1;
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void leftAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.AddCol(matrix_temp, Matrix.Zeros(matrix_temp.Rows, 1));
                int newCol = CurrentMatrix.Columns;
                while (col_sel != newCol)
                {
                    matrix_temp = Matrix.ColumnSwitch(matrix_temp, newCol - 1, newCol);
                    newCol -= 1;
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void rightAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.AddCol(matrix_temp, Matrix.Zeros(matrix_temp.Rows, 1));
                int newCol = CurrentMatrix.Columns - 1;
                while (col_sel != newCol)
                {
                    matrix_temp = Matrix.ColumnSwitch(matrix_temp, newCol, newCol + 1);
                    newCol -= 1;
                }
                CurrentMatrix = matrix_temp;
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.RemoveRow(matrix_temp, row_sel);
                CurrentMatrix = matrix_temp;
            }
        }

        private void deleteColToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.RemoveCol(matrix_temp, col_sel);
                CurrentMatrix = matrix_temp;
            }
        }

        private void getRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.GetRowVector(matrix_temp, row_sel);
                CurrentMatrix = matrix_temp;
            }
        }

        private void getColToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row_sel, col_sel;
            row_sel = matrixDataGridView.CurrentCell.RowIndex;
            col_sel = matrixDataGridView.CurrentCell.ColumnIndex;
            Matrix matrix_temp = new Matrix(CurrentMatrix);
            if (row_sel >= 0 && row_sel < CurrentMatrix.Rows && col_sel >= 0 && col_sel < CurrentMatrix.Columns)
            {
                matrix_temp = Matrix.GetColVector(matrix_temp, col_sel);
                CurrentMatrix = matrix_temp;
            }
        }
        #endregion

        private void actionToolStripButton_Click(object sender, EventArgs e)
        {
            PerformFunction();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            VarNameInput varName = new VarNameInput();
            if (varName.ShowDialog() == DialogResult.OK)
            {
                if (varName.nameTextBox.Text != "")
                {
                    workspaceToolStripDropDownButton.DropDownItems.Add(varName.nameTextBox.Text);
                    varNameList.Add(varName.nameTextBox.Text);
                }
                else
                {
                    workspaceToolStripDropDownButton.DropDownItems.Add("矩阵" + workspace.Count.ToString());
                    varNameList.Add(varName.nameTextBox.Text);
                }
                workspace.Add(new Matrix(currentMatrix));
                workspaceToolStripDropDownButton.DropDownItems[workspaceToolStripDropDownButton.DropDownItems.Count - 1].Click
                    += new System.EventHandler(this.toolStripMenuItem2_Click);//增加事件处理
                toolStripStatusLabel1.Text = workspaceToolStripDropDownButton.DropDownItems[workspace.Count - 1].Text
                                                + "已存入工作空间...";
            }
        }

        private void funcToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            funcIndex = funcToolStripComboBox.SelectedIndex;
            this.ResetParaToolStrip();
            this.FunctionTip();
        }

        //编辑结束后要更改相应数据
        private void matrixDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row_num = e.RowIndex;
            int col_num = e.ColumnIndex;
            string cell_Text = (string)matrixDataGridView[col_num, row_num].Value;
            double changedVal;
            try
            {
                changedVal = Convert.ToDouble(cell_Text);
                currentMatrix[row_num, col_num] = changedVal;
                toolStripStatusLabel1.Text = "第" + Convert.ToString(row_num + 1) + "行、第" + Convert.ToString(col_num + 1) +
                                                "列元素变更为" + Convert.ToString(currentMatrix[row_num, col_num]);
            }
            catch
            {
                changedVal = currentMatrix[row_num, col_num];
                matrixDataGridView[col_num, row_num].Value = Convert.ToString(changedVal);
                toolStripStatusLabel1.Text = "元素更改错误,输入只能为数字!";
            }
        }

        private void clearToolStripButton_Click(object sender, EventArgs e)
        {
            int num_items=workspaceToolStripDropDownButton.DropDownItems.Count;
            for (int i = 0; i < num_items; i++)
            {
                workspaceToolStripDropDownButton.DropDownItems.RemoveAt(0);
            }
            workspace.RemoveRange(0, workspace.Count);
            varNameList.RemoveRange(0, varNameList.Count);
            SelectedIndex = -1;//取消选中
            toolStripStatusLabel1.Text = "工作空间已清空...";
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem sender_obj = (ToolStripMenuItem)sender;
            for (int i = 0; i < workspace.Count; i++)
            {
                if (sender_obj == workspaceToolStripDropDownButton.DropDownItems[i])
                {
                    SelectedIndex = i;
                    break;
                }
            }
        }

        private void matrixDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (currentMatrix == null)
                return;
            int first_rowIndex = matrixDataGridView.FirstDisplayedScrollingRowIndex;
            int first_colIndex = matrixDataGridView.FirstDisplayedScrollingColumnIndex;
            int row_display_count = matrixDataGridView.DisplayedRowCount(true);
            int col_display_count = matrixDataGridView.DisplayedColumnCount(true);
            for (int i = 0; i < row_display_count; i++)
            {
                for (int j = 0; j < col_display_count; j++)
                {
                    int row = i + first_rowIndex;
                    int col = j + first_colIndex;
                    if (row >= 0 && row < currentMatrix.Rows && col >= 0 && col < currentMatrix.Columns)
                    {
                        matrixDataGridView[col, row].Value = currentMatrix[row, col].ToString();
                        matrixDataGridView[col, row].ReadOnly = false;
                    }
                    else
                    {
                        matrixDataGridView[col, row].Value = "";
                        matrixDataGridView[col, row].ReadOnly = true;
                    }
                }
            }
        }

    }
}
