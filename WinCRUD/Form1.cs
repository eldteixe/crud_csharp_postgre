using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Repository;
using Repository.Entities;

namespace WinCRUD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "Hora: "+ DateTime.Now.ToLongTimeString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblData.Text = "Data: "+ DateTime.Now.ToShortDateString();
        }

        private void contatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmCadContatos frm = new frmCadContatos())
            {
                frm.ShowDialog();
            }
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmCadUsuarios frm = new frmCadUsuarios())
            {
                frm.ShowDialog();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void telefonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmTelefones frm = new frmTelefones())
            {
                frm.ShowDialog();
            }
        }               
    }
}
