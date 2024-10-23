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
using ZstdSharp.Unsafe;

namespace SysTINSApp
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            cmbNivel.DataSource = Nivel.ObterLista();
            cmbNivel.DisplayMember = "Nome";
            cmbNivel.ValueMember = "Id";

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Usuario usuario = new(
                txtNome.Text, 
                txtEmail.Text, 
                txtSenha.Text, 
                Nivel.ObterPorId(Convert.ToInt32(cmbNivel.SelectedValue))
                );
            usuario.Inserir();
            if (usuario.Id > 0) 
            {
                MessageBox.Show($"Usuário {usuario.Id} inserido com sucesso");
                btnInserir.Enabled = false;
            }

        }
    }
}
