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
    public partial class Form_DocTypeSetting : Form
    {
        public Form_DocTypeSetting()
        {
            InitializeComponent();
        }
        private void Form_DocTypeSetting_Load(object sender, EventArgs e)
        {
            using (Entities context = new Entities())
            {
                var query = from q in context.DocumentType select q;
                listBoxAdv1.DisplayMember = "doctype";
                listBoxAdv1.DataSource = query.ToList();
            }
        }
        private void listBoxAdv1_ItemClick(object sender, EventArgs e)
        {
            DocumentType dt = listBoxAdv1.SelectedValue as DocumentType;
            textBoxX1.Text = dt.doctype;
            textBoxX2.Text = dt.doctypeshort;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {//添加
            using (Entities context = new Entities())
            {
                context.DocumentType.Add(new DocumentType()
                {
                    doctype = textBoxX1.Text.Trim(),
                    doctypeshort = textBoxX2.Text.Trim().ToUpper()
                });
                context.SaveChanges();
                Form_DocTypeSetting_Load(null, null);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {//删除
            if (listBoxAdv1.SelectedValue == null) { return; }
            DocumentType dt = listBoxAdv1.SelectedValue as DocumentType;
            using (Entities context = new Entities())
            {
                context.DocumentType.Remove(context.DocumentType.Single(p => p.doctype == dt.doctype));
                context.SaveChanges();
            }
            Form_DocTypeSetting_Load(null, null);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
