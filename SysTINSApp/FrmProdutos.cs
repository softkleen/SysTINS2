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
    public partial class FrmProdutos : Form
    {
        public FrmProdutos()
        {
            InitializeComponent();
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            CarregaComboCategoria();
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            var listaProdutos = Produto.ObterLista();
            dgvProdutos.Rows.Clear();
            int linha = 0;
            foreach (var produto in listaProdutos)
            {
                dgvProdutos.Rows.Add();
                dgvProdutos.Rows[linha].Cells[0].Value = produto.Id;
                dgvProdutos.Rows[linha].Cells[1].Value = produto.CodBar;
                dgvProdutos.Rows[linha].Cells[2].Value = produto.Descricao;
                dgvProdutos.Rows[linha].Cells[3].Value = "R$ " + produto.ValorUnit.ToString("#0.00");
                dgvProdutos.Rows[linha].Cells[4].Value = produto.UnidadeVenda;
                dgvProdutos.Rows[linha].Cells[5].Value = produto.Categoria.Nome;
                dgvProdutos.Rows[linha].Cells[6].Value = produto.EstoqueMinimo;
                dgvProdutos.Rows[linha].Cells[7].Value = (produto.ClasseDesconto * 100).ToString("#0.00") + "%";
                dgvProdutos.Rows[linha].Cells[8].Value = produto.DataCad;
                linha++;
            }
        }

        private void CarregaComboCategoria()
        {
            // carregando combox com as categorias
            var categorias = Categoria.ObterLista();
            categorias.Add(new(0, ">>>>>>>>Nova Categoria<<<<<<<", "NWC"));
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nome";
            cmbCategoria.ValueMember = "Id";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Produto produto = new(
                txtCodBar.Text,
                txtDescricao.Text,
                double.Parse(txtValorUnit.Text),
                txtUnidadeVenda.Text,
                Categoria.ObterPorId(Convert.ToInt32(cmbCategoria.SelectedValue)),
                (double)npEstoqueMinimo.Value,//cast
                double.Parse(txtDesconto.Text)
                );
            produto.Inserir();
            if (produto.Id > 0)
            {
                txtId.Text = produto.Id.ToString();
                CarregaGrid();
                MessageBox.Show($"Produto cadastrado com o ID {produto.Id}");
            }
            else
            {
                MessageBox.Show("Falha ao inserir o produto!");
            }
        }

        private void cmbCategoria_TextChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.ValueMember != "" && Convert.ToUInt32(cmbCategoria.SelectedValue) == 0)
            {
                panel1.Visible = true;
                txtNewCat.Focus();
                gpProdutos.Enabled = false;

            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Categoria categoria = new(txtNewCat.Text, txtNewCatSigla.Text);
            categoria.Inserir();
            txtNewCat.Clear();
            CarregaComboCategoria();
            cmbCategoria.SelectedValue = categoria.Id;
            panel1.Visible = false;
            gpProdutos.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            gpProdutos.Enabled = true;
            cmbCategoria.SelectedIndex = 0;
        }
    }
}
