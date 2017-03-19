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
    public partial class Form_ProjectTypeSetting : Form
    {
        public Form_ProjectTypeSetting()
        {
            InitializeComponent();
        }

        private void Form_ProjectTypeSetting_Load(object sender, EventArgs e)
        {
            using (Entities  context = new Entities())
            {
                var query = from q in context.ProjectType select q;
                listBoxAdv1.DisplayMember = "projtype";
                listBoxAdv1.DataSource = query.ToList();
            }           
        }
        private void listBoxAdv1_ItemClick(object sender, EventArgs e)
        {
            ProjectType pt = listBoxAdv1.SelectedValue as ProjectType;
            textBoxX1.Text = pt.projtype;
            textBoxX2.Text = pt.projtypeshot;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {//添加
            using (Entities context = new Entities())
            {
                context.ProjectType.Add(new ProjectType()
                {
                    projtype = textBoxX1.Text.Trim(),
                    projtypeshot = textBoxX2.Text.Trim().ToUpper()
                });
                context.SaveChanges();
                Form_ProjectTypeSetting_Load(null, null);
                string path = PublicStatic.StorePath + "\\" + textBoxX1.Text.Trim();
                if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {//删除
            if (listBoxAdv1.SelectedValue == null) { return; }
            ProjectType pt = listBoxAdv1.SelectedValue as ProjectType;
            using (Entities context = new Entities())
            {
                context.ProjectType.Remove(context.ProjectType.Single(p => p.projtype == pt.projtype));
                context.SaveChanges();
                System.IO.Directory.Move(PublicStatic.StorePath + "\\" + pt.projtype, PublicStatic.UnfiledPath);
            }
            Form_ProjectTypeSetting_Load(null, null);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
