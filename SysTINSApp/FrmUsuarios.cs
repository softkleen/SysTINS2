using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
            // carregando o comboBox de níveis 
            cmbNivel.DataSource = Nivel.ObterLista();
            cmbNivel.DisplayMember = "Nome";
            cmbNivel.ValueMember = "Id";

            // carregando o datagrid de usuários
            CarregaGridUsuarios();
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
                // carrega grid
                CarregaGridUsuarios();
                MessageBox.Show($"Usuário {usuario.Nome} inserido com sucesso");
                btnInserir.Enabled = false;
            }
        }
        private void CarregaGridUsuarios()
        {
            dgvUsuarios.Rows.Clear();
            var listaDeUsuarios = Usuario.ObterLista();
            int linha = 0;
            foreach (var usuario in listaDeUsuarios)
            {
                dgvUsuarios.Rows.Add();
                dgvUsuarios.Rows[linha].Cells[0].Value = usuario.Id;
                dgvUsuarios.Rows[linha].Cells[1].Value = usuario.Nome;
                dgvUsuarios.Rows[linha].Cells[2].Value = usuario.Email;
                dgvUsuarios.Rows[linha].Cells[3].Value = usuario.Nivel.Nome;
                dgvUsuarios.Rows[linha].Cells[4].Value = usuario.Ativo;
                linha++;
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int linhaAtual = dgvUsuarios.CurrentRow.Index;
            int idUser = Convert.ToInt32(dgvUsuarios.Rows[linhaAtual].Cells[0].Value);
            var usuario = Usuario.ObterPorId(idUser);
            txtId.Text = usuario.Id.ToString();
            txtNome.Text = usuario.Nome;
            txtEmail.Text = usuario.Email;
            chkAtivo.Checked = usuario.Ativo;
            cmbNivel.SelectedValue = usuario.Nivel.Id;
            btnAtualizar.Enabled = true;

            //MessageBox.Show(idUser.ToString());
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new();
            usuario.Id = int.Parse(txtId.Text);
            usuario.Nome = txtNome.Text;
            usuario.Senha = txtSenha.Text;
            usuario.Nivel = Nivel.ObterPorId(Convert.ToInt32(cmbNivel.SelectedValue));
            if (usuario.Atualizar())
            {
                CarregaGridUsuarios();
                MessageBox.Show("Usuário atualizado com sucesso!");
            }

        }
    }
}
