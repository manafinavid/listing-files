using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
#pragma warning disable IDE0059 // Unnecessary assignment of a value
namespace listing_files
{
    public partial class Form1 : Form
    {
        string Direct="";
        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.images;
        }
        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            //Setting ProgressBar Maximum Value  
            progressBar1.Maximum = Directory.GetFiles(Dir, "*.*", SearchOption.AllDirectories).Length + Directory.GetDirectories(Dir, "**", SearchOption.AllDirectories).Length;
            TreeNode tds = treeView1.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadSubDirectories(Dir, tds);
            LoadFiles(Dir, tds);
        }

        public static bool Search_on_clipboard(ref string temp,ref Form1 main)
        {
            main.dataGridView1.Rows.Clear();
            int j;
            int count = 0;
            bool result = false;
            temp = temp.ToLower().ToString();
            for (int i = 0; i < main.names.Count; i++)
            {
                j = main.names[i].ToLower().ToString().IndexOf(temp, 0);
                if (j != -1)
                {
                    main.dataGridView1.Rows.Add(main.names[i], main.paths[i]);
                    count++;
                    result = true;
                }
            }
            main.label2.Text = count.ToString();
            return result;
        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(dir); 
            foreach (string subdirectory in subdirectoryEntries)
            {

                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.StateImageIndex = 0;
                tds.Tag = di.FullName;
                LoadSubDirectories(subdirectory, tds);
                LoadFiles(subdirectory, tds);
                UpdateProgress();

            }
        }
        List<string> names=new List<string>();
        List<string> paths=new List<string>();
        private void LoadFiles(string dir, TreeNode td)
        {
            string[] Files = Directory.GetFiles(dir, "*.*");

            // Loop through them to see files 
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                names.Add(fi.Name);
                paths.Add(file);
                tds.StateImageIndex = 1;
                UpdateProgress();
            }
        }
        private void UpdateProgress()
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                progressBar1.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
                Application.DoEvents();
            }
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Folder.ShowDialog() == DialogResult.OK)
            {
                LoadDirectory(Folder.SelectedPath);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefleahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Direct!="")
            LoadDirectory(Direct);
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        private void ByNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            string temp="";
            if (InputBox("", "", ref temp)==DialogResult.OK)
            {
                temp = temp.ToLower().ToString();
                dataGridView1.Rows.Clear();
                int j;
                for (int i = 0; i < names.Count; i++)
                {
                    j = names[i].ToLower().ToString().IndexOf(temp, 0);
                    if (j != -1)
                    {
                        dataGridView1.Rows.Add(names[i], paths[i]);
                        count++;
                    }
                }
                label2.Text = count.ToString();
            }
        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            System.Diagnostics.Process.Start(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(treeView1.SelectedNode!=null)
            System.Diagnostics.Process.Start(treeView1.SelectedNode.Tag.ToString());
        }

        private void ByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            string temp="";
            if (InputBox("", "use this format : keyword1|keyword2|...", ref temp) == DialogResult.OK)
            {
                List<string> vs = new List<string>();
                int x = 0;
                int U;
                dataGridView1.Rows.Clear();
                if (temp == "")
                    return;
                temp += '|';
                for (int i=0;(x=temp.IndexOf('|'))!=-1 ; i++)
                {
                    vs.Add(temp.Substring(0, x));
                    temp = temp.Substring(x +1);
                }
                for(int i = -1; ;)
                {
                    W:i++;
                    if(!(i < names.Count))
                    {
                        break;
                    }
                    for (int k=0; k < vs.Count; k++)
                    {
                        U = names[i].ToLower().IndexOf(vs[k].ToLower());
                        if (U==-1)
                            goto W;
                    }
                    dataGridView1.Rows.Add(names[i], paths[i]);
                    count++;
                }
                label2.Text = count.ToString();
            }
        }

        private void ToPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "Text| *.txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
            List<string> temp = new List<string>();
            temp.AddRange(names.ToArray());
            temp.Sort();
            string Out = "";
            for(int Q=0;Q<temp.Count;Q++)
            {
                Out += temp[Q] + "\n";
            }
            System.IO.File.WriteAllText(open.FileName, Out);
            }
        }

        private void ByClipBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 temp = this;
            form.clip_board clip = new form.clip_board(ref temp);
            clip.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0&&X>0)
            {
                MessageBox.Show(dataGridView1.Rows[X].Cells[1].Value.ToString());
            }
        }
        int X = 0;
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            X = e.RowIndex;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

        public const int FO_DELETE = 3;
        public const int FOF_ALLOWUNDO = 0x40;
        public const int FOF_NOCONFIRMATION = 0x10;
        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                var shf = new SHFILEOPSTRUCT();
                shf.wFunc = FO_DELETE;
                shf.fFlags = FOF_ALLOWUNDO;
                shf.pFrom = dataGridView1.Rows[X].Cells[1].Value.ToString() + '\0' + '\0';
                SHFileOperation(ref shf);
                dataGridView1.Rows.Remove(dataGridView1.Rows[X]);
                dataGridView1.ClearSelection();
                X = 0;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form.AboutBox1 box1 = new form.AboutBox1();
            box1.ShowDialog();
        }
    }
}
