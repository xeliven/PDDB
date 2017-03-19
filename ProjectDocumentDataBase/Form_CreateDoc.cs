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
    public partial class Form_CreateDoc : Form
    {
        public Form_CreateDoc()
        {
            InitializeComponent();
        }

        private string _FilePath;
        private string _FileName;
        private string _ProjType;
        private string _ProjTypeshort;
        private string _Projname;
        private string _ProjNameshort;
        private string _DocType;
        private string _DocTypeShort;

        public string FilePath
        {
            get
            {
                return _FilePath;
            }

            set
            {
                _FilePath = value;
            }
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }

            set
            {
                _FileName = value;
            }
        }

        public string ProjType
        {
            get
            {
                return _ProjType;
            }

            set
            {
                _ProjType = value;
            }
        }

        public string ProjTypeshort
        {
            get
            {
                return _ProjTypeshort;
            }

            set
            {
                _ProjTypeshort = value;
            }
        }

        public string Projname
        {
            get
            {
                return _Projname;
            }

            set
            {
                _Projname = value;
            }
        }

        public string ProjNameshort
        {
            get
            {
                return _ProjNameshort;
            }

            set
            {
                _ProjNameshort = value;
            }
        }

        public string DocType
        {
            get
            {
                return _DocType;
            }

            set
            {
                _DocType = value;
            }
        }

        public string DocTypeShort
        {
            get
            {
                return _DocTypeShort;
            }

            set
            {
                _DocTypeShort = value;
            }
        }

        private void Form_CreateDoc_Load(object sender, EventArgs e)
        {
            using (Entities context = new Entities())
            {
                //绑定项目类型
                var query = from q in context.ProjectType select q;
                comboBoxEx1.DisplayMember = "projtype";
                comboBoxEx1.DataSource = query.ToList();
                //绑定文档类型
                var doctyepqueery = from q in context.DocumentType select q;
                comboBoxEx3.DisplayMember = "doctype";
                comboBoxEx3.DataSource = doctyepqueery.ToList();
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = false;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBoxX1.Text = fd.FileName;
                _FilePath = fd.FileName;

                System.IO.FileInfo fi = new System.IO.FileInfo(fd.FileName);
                labelX10.Text = fi.CreationTime.ToString();
                labelX11.Text = fi.LastWriteTime.ToString();
            }
        }
        private void comboBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxEx1.Text)) { return; }
            ProjectType pt = comboBoxEx1.SelectedValue as ProjectType;
            using (Entities context = new Entities())
            {
                var query = from q in context.Project where q.projettype == pt.projtype select q;
                if (query.Count() != 0) { comboBoxEx2.DisplayMember = "projectname"; comboBoxEx2.DataSource = query.ToList(); comboBoxEx2.Enabled = true; }
                _ProjType = pt.projtype;
                _ProjTypeshort = pt.projtypeshot;
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedValue == null) { labelX4.Text = "无项目类型"; return; }
            ProjectType pt = comboBoxEx1.SelectedValue as ProjectType;
            if (comboBoxEx2.SelectedValue == null) { labelX4.Text = "无项目名称"; return; }
            Project p = comboBoxEx2.SelectedValue as Project;
            if (comboBoxEx3.SelectedValue == null) { labelX4.Text = "无文档类型"; return; }
            DocumentType dt = comboBoxEx3.SelectedValue as DocumentType;

            using (Entities context = new Entities())
            {
                var selectquery = from q in context.Document where q.docname == textBoxX2.Text.Trim() && q.projname == p.projectname select q;
                if (selectquery.Count() != 0) { MessageBox.Show("本项目中文件名有重复");  return; }
                
                System.IO.FileInfo fi = new System.IO.FileInfo(textBoxX1.Text);
                string savepath = PublicStatic.StorePath + "\\" + pt.projtype + "\\" + p.projectname + "\\" + dt.doctype +"\\";
                string savename = textBoxX2.Text.Trim() + fi.Extension;
                if (!System.IO.Directory.Exists(savepath)) { System.IO.Directory.CreateDirectory(savepath); }
                System.IO.File.Copy(textBoxX1.Text.Trim(), savepath+savename);
                context.Document.Add(new Document()
                {
                    msgdatetime = DateTime.Now,
                    msgflag = 0,
                    docname = textBoxX2.Text.Trim(),
                    docnumber = labelX3.Text,
                    doctypename = dt.doctype,
                    doctypeshort = dt.doctypeshort,
                    filepath = savepath + savename,
                    projname = p.projectname,
                    projshort = p.projshort,
                    projtypename = pt.projtype,
                    projtypeshot = pt.projtypeshot,
                    modifydatetime = fi.CreationTime,
                });
                context.SaveChanges();
                
            }
            this.DialogResult = DialogResult.OK;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedValue==null) { labelX4.Text = "无项目类型"; return; }
            ProjectType pt = comboBoxEx1.SelectedValue as ProjectType;
            if (comboBoxEx2.SelectedValue == null) { labelX4.Text = "无项目名称"; return; }
            Project p = comboBoxEx2.SelectedValue as Project;
            if (comboBoxEx3.SelectedValue == null) { labelX4.Text = "无文档类型"; return; }
            DocumentType dt = comboBoxEx3.SelectedValue as DocumentType;
            textBoxX3.Text = PublicStatic.GetDocNumber(pt.projtypeshot, p.projshort, dt.doctypeshort, DateTime.Now);
        }

       
    }
}
