using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectDocumentDataBase
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private  void Form1_Load(object sender, EventArgs e)
        {
            Func<int> myfuc = new Func<int>(() => { return TestDataBaseConn(); });
            myfuc.BeginInvoke(new AsyncCallback(ar => { int k = myfuc.EndInvoke(ar);}), "");
            TestFileDir();
            LoadPjtType();
        }
        
        private int TestDataBaseConn()
        {
            using (Entities context = new Entities())
            {
                var query = from q in context.Document select q;
                return query.Count();
            }
        }
        private void TestFileDir()
        {
            PublicStatic.StorePath = Application.StartupPath + "\\项目文档";
            if (!System.IO.Directory.Exists(PublicStatic.StorePath)) { System.IO.Directory.CreateDirectory(PublicStatic.StorePath); }

            PublicStatic.UnfiledPath = Application.StartupPath + "\\未归档";
            if (!System.IO.Directory.Exists(PublicStatic.UnfiledPath)) { System.IO.Directory.CreateDirectory(PublicStatic.UnfiledPath); }

            PublicStatic.ModelPath = Application.StartupPath + "\\文档模板";
            if (!System.IO.Directory.Exists(PublicStatic.ModelPath)) { System.IO.Directory.CreateDirectory(PublicStatic.ModelPath); }

            PublicStatic.TempPath = Application.StartupPath + "\\Temp";
            if (!System.IO.Directory.Exists(PublicStatic.TempPath)) { System.IO.Directory.CreateDirectory(PublicStatic.TempPath); }

            LoadModel();
        }
        private void LoadModel()
        {
            foreach (var q in System.IO.Directory.GetDirectories(PublicStatic.ModelPath))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(q);
                comboBoxItem1.Items.Add(di.Name);
            }
        }

        private Dictionary<string, ListBoxAdv> _ProjListAdvDic = new Dictionary<string, ListBoxAdv>();
        private void LoadPjtType()
        {
            using (Entities context = new Entities())
            {
                var query = from q in context.ProjectType select q;
                foreach (var q in query)
                {
                    //建立容器
                    SideNavPanel snp = new SideNavPanel();
                    snp.Dock = System.Windows.Forms.DockStyle.Fill;
                    //内部列表
                    ListBoxAdv listbox = new ListBoxAdv();
                    listbox.Dock = DockStyle.Fill;
                    listbox.AutoScroll = true;
                    listbox.ItemDoubleClick += Listbox_ItemDoubleClick;
                    listbox.ItemCheck += Listbox_ItemCheck;
                    var pjtlistquery = from p in context.Project where p.projettype == q.projtype select p;
                    listbox.DisplayMember = "projectname";
                    listbox.DataSource = pjtlistquery.ToList();
                    snp.Controls.Add(listbox);
                    _ProjListAdvDic.Add(q.projtype, listbox);

                    //建立边栏按钮
                    SideNavItem sin = new SideNavItem();
                    sin.Checked = false;
                    sin.Panel = snp;
                    sin.Text = q.projtype;
                    sideNav1.Controls.Add(snp);
                    sideNav1.Items.Add(sin);
                    
                }
            }
        }

        private void Listbox_ItemCheck(object sender, ListBoxAdvItemCheckEventArgs e)
        {//选择文件

        }

        private Dictionary<string, ListBoxAdv> _DocListAdvDic = new Dictionary<string, ListBoxAdv>();
        private void Listbox_ItemDoubleClick(object sender, MouseEventArgs e)
        {
            sideNav2.Items.Clear();
            _DocListAdvDic.Clear();
            BaseItem bc = sender as BaseItem;
            using (Entities context = new Entities())
            {
                var query = from q in context.Document where q.projname == bc.Text group q by q.doctypename into g select new { type= g.Key, doc =g };
                foreach (var q in query)
                {
                    //建立容器
                    SideNavPanel snp = new SideNavPanel();
                    snp.Dock = System.Windows.Forms.DockStyle.Fill;
                    //内部列表
                    ListBoxAdv listbox = new ListBoxAdv();
                    listbox.Dock = DockStyle.Fill;
                    listbox.AutoScroll = true;
                    listbox.ItemDoubleClick += DocList_ItemDounleChick;
                    var pjtlistquery = from p in context.Document where p.doctypename == q.type select p;
                    listbox.DisplayMember = "docname";
                    listbox.DataSource = pjtlistquery.ToList();
                    snp.Controls.Add(listbox);
                    _DocListAdvDic.Add(q.type, listbox);

                    //建立边栏按钮
                    SideNavItem sin = new SideNavItem();
                    sin.Checked = false;
                    sin.Panel = snp;
                    sin.Text = q.type;
                    sideNav2.Controls.Add(snp);
                    sideNav2.Items.Add(sin);
                }

            }
            sideNav2.Refresh();
        }

        private void DocList_ItemDounleChick(object sender, MouseEventArgs e)
        {//打开文档
            BaseItem bc = sender as BaseItem;
            ItemBindingData data = bc.Tag as ItemBindingData;
            Document doc = data.DataItem as Document;
            System.Diagnostics.Process.Start(doc.filepath);
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            new Form_ProjectTypeSetting().ShowDialog();
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            new Form_DocTypeSetting().ShowDialog();
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {//新建项目
            var fcp = new Form_CreateProject();
            if (fcp.ShowDialog() == DialogResult.OK)
            {
                using (Entities context = new Entities())
                {
                    var pjtlistquery = from q in context.Project where q.projettype == fcp.ProjType select q;
                    _ProjListAdvDic[fcp.ProjType].DataSource = pjtlistquery.ToList();
                }
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {//删除项目
            if (sideNav1.SelectedItem != null)
            {
                if (_ProjListAdvDic[sideNav1.SelectedItem.Text].SelectedValue != null)
                {
                    Project p = _ProjListAdvDic[sideNav1.SelectedItem.Text].SelectedValue as Project;
                    using (Entities context = new Entities())
                    {
                        var query = from q in context.Project where q.projectname == p.projectname select q;
                        context.Project.RemoveRange(query); 
                        context.SaveChanges();

                        var pjtlistquery = from q in context.Project where q.projettype == sideNav1.SelectedItem.Text select q;
                        _ProjListAdvDic[sideNav1.SelectedItem.Text].DataSource = pjtlistquery.ToList();
                    }
                   
                }
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {//新建文档
            new Form_CreateDoc().ShowDialog();
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {//删除文档
            if (sideNav2.SelectedItem != null)
            {
                if (_DocListAdvDic[sideNav2.SelectedItem.Text].SelectedValue != null)
                {
                    Document p = _DocListAdvDic[sideNav2.SelectedItem.Text].SelectedValue as Document;
                    using (Entities context = new Entities())
                    {
                        var query = from q in context.Document where q.docname == p.docname select q;
                        context.Document.RemoveRange(query);
                        context.SaveChanges();

                        var pjtlistquery = from q in context.Document where q.doctypename == sideNav2.SelectedItem.Text select q;
                        _DocListAdvDic[sideNav2.SelectedItem.Text].DataSource = pjtlistquery.ToList();
                    }

                }
            }
        }

        private void comboBoxItem1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxItem1.Text)) { return; }
            if (!System.IO.Directory.Exists(PublicStatic.ModelPath + "\\" + comboBoxItem1.Text)) { return; }
            comboBoxItem2.Items.Clear();
            foreach (var q in System.IO.Directory.GetFiles(PublicStatic.ModelPath + "\\" + comboBoxItem1.Text))
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(q);
                comboBoxItem2.Items.Add(fi.Name);
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {//打开模板
            string modlpath = PublicStatic.ModelPath + "\\" + comboBoxItem1.Text + "\\" + comboBoxItem2.Text;
            if (!System.IO.File.Exists(modlpath)) { MessageBox.Show("无此文件！");return; }
            string tempfilename = PublicStatic.TempPath +"\\"+DateTime.Now.ToString("yyyyMMddHHmmss")+ comboBoxItem2.Text;
            System.IO.File.Copy(modlpath, tempfilename);
            System.Diagnostics.Process.Start(tempfilename);
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {//清理临时文件夹
            foreach (var q in System.IO.Directory.GetFiles(PublicStatic.TempPath))
            {
                System.IO.File.Delete(q);
            }
            MessageBox.Show("清理完成！");
        }
    }
}
