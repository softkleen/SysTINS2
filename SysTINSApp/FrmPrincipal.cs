using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysTINSClass;

namespace SysTINSApp
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void novoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmUsuarios frmUsuarios = new();
            //frmUsuarios.MdiParent = this;
            frmUsuarios.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //var cmd = Banco.Abrir();
            //cmd.CommandText = "select * from niveis where id = 2";
            //var dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    MessageBox.Show($" olá {dr.GetString(1)}");
            //}
            //else 
            //{
            //    MessageBox.Show("Nivel não encontrado");
            //}
            Nivel nivel = new(1024,"Aten", "ATD");
            nivel.Id = 1025;
        }
    }
}
