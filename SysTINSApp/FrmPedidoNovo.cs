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
    public partial class FrmPedidoNovo : Form
    {
        public FrmPedidoNovo()
        {
            InitializeComponent();
        }

        private void btnInserePedido_Click(object sender, EventArgs e)
        {
            Pedido pedido = new(
                Usuario.ObterPorId(Program.UsuarioLogado.Id),
                Cliente.ObterPorId(int.Parse(txtIdCliente.Text))
                );
            pedido.Inserir();
            txtIdPedido.Text = pedido.Id.ToString();
            grbIndentificacao.Enabled = false;
            grbItens.Enabled = true;

        }
    }
}
