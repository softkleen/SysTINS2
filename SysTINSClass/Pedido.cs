using Org.BouncyCastle.Crypto.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysTINSClass
{
    public class Pedido
    {
   

        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Data { get; set; }
        public string? Status { get; set; }
        public double Desconto { get; set; }
        public List<ItemPedido>? Items { get; set; }

        public Pedido()
        {
            Cliente = new();
            Usuario = new();
        }
        public Pedido(Usuario usuario, Cliente cliente)
        {
            Usuario = usuario;
            Cliente = cliente;
        }
        public Pedido(int id, Usuario usuario, Cliente cliente, DateTime data, string status, double desconto)
        {
            Id = id;
            Usuario = usuario;
            Cliente = cliente;
            Data = data;
            Status = status;
            Desconto = desconto;
        }
        public Pedido(int id,
                    Usuario usuario, 
                    Cliente cliente, 
                    DateTime data, 
                    string status, 
                    double desconto, 
                    List<ItemPedido> itens)
        {
            Id = id;
            Usuario = usuario;
            Cliente = cliente;
            Data = data;
            Status = status;
            Desconto = desconto;
            Items = itens;
        }
        public void Inserir() 
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_pedido_insert";
            cmd.Parameters.AddWithValue("spusuario_id",Usuario.Id);
            cmd.Parameters.AddWithValue("spcliente_id",Cliente.Id);
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void Atualizar() 
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_pedido_update";
            cmd.Parameters.AddWithValue("spid", Id);
            cmd.Parameters.AddWithValue("spstatus", Status);
            cmd.Parameters.AddWithValue("spdesconto", Desconto);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public static Pedido ObterPorId(int id)
        {
            Pedido pedido = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from pedidos where id = {id}";
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                pedido = new(
                        dr.GetInt32(0),
                        Usuario.ObterPorId(dr.GetInt32(1)),
                        Cliente.ObterPorId(dr.GetInt32(2)),
                        dr.GetDateTime(3),
                        dr.GetString(4),
                        dr.GetDouble(5),
                        ItemPedido.ObterItensPorPedidoId(dr.GetInt32(0))
                    ); 
            }
            return pedido;
        }
        public static List<Pedido> ObterLista()
        {
            List<Pedido> pedidos = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from pedidos ";
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                pedidos.Add(new(
                        dr.GetInt32(0),
                        Usuario.ObterPorId(dr.GetInt32(1)),
                        Cliente.ObterPorId(dr.GetInt32(2)),
                        dr.GetDateTime(3),
                        dr.GetString(4),
                        dr.GetDouble(5),
                        ItemPedido.ObterItensPorPedidoId(dr.GetInt32(0))
                    ));
            }
            return pedidos;
        }
    }
}
/*
 id int(4) AI PK 
usuario_id int(4) 
cliente_id int(4) 
data timestamp 
status char(1) 
desconto decimal(10,2)

`sp_pedido_insert`(spusuario_id int, spcliente_id int)
 `sp_pedido_update`(spid int, spstatus char(1), spdesconto decimal(10,2))
 */