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
    public partial class Form_CreateProject : Form
    {
        public Form_CreateProject()
        {
            InitializeComponent();
        }

        private void Form_CreateProject_Load(object sender, EventArgs e)
        {
            using (Entities context = new Entities())
            {
                var query = from q in context.ProjectType select q;
                comboBoxEx1.DisplayMember = "projtype";
                comboBoxEx1.DataSource = query.ToList();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private string _ProjType;

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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            using (Entities context = new Entities())
            {
                if (context.Project.Count(p => p.projectname == textBoxX1.Text.Trim()) != 0)
                { labelX4.Text = "已经有此项目";   return; }
                context.Project.Add(new Project()
                {
                    projectname = textBoxX1.Text.Trim(),
                    projshort = textBoxX2.Text.Trim().ToUpper(),
                     projettype = comboBoxEx1.Text,
                });
                context.SaveChanges();
                _ProjType = comboBoxEx1.Text;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
