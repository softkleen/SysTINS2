using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysTINSClass
{
    public class Produto
    {
        public int Id { get; set; }
        public string? CodBar { get; set; }
        public string? Descricao { get; set; }
        public double ValorUnit { get; set; }
        public string? UnidadeVenda { get; set; }
        public Categoria Categoria { get; set; }
        public double EstoqueMinimo { get; set; }
        public double ClasseDesconto { get; set; }
        public DateTime DataCad { get; set; }

        public Produto()
        {
            Categoria = new();
        }



        public Produto( string codBar, string? descricao, double valorUnit, string? unidadeVenda, Categoria categoria, double estoqueMinimo, double classeDesconto)
        {
            CodBar = codBar;
            Descricao = descricao;
            ValorUnit = valorUnit;
            UnidadeVenda = unidadeVenda;
            Categoria = categoria;
            EstoqueMinimo = estoqueMinimo;
            ClasseDesconto = classeDesconto;
        }
        public Produto(int id, string codBar, string? descricao, double valorUnit, string? unidadeVenda, Categoria categoria, double estoqueMinimo, double classeDesconto, DateTime dataCad)
        {
            Id = id;
            CodBar = codBar;
            Descricao = descricao;
            ValorUnit = valorUnit;
            UnidadeVenda = unidadeVenda;
            Categoria = categoria;
            EstoqueMinimo = estoqueMinimo;
            ClasseDesconto = classeDesconto;
            DataCad = dataCad;
        }

        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_produto_insert";
            cmd.Parameters.AddWithValue("spcod_barras", CodBar);
            cmd.Parameters.AddWithValue("spdescricao", Descricao);
            cmd.Parameters.AddWithValue("spvalor_unit", ValorUnit);
            cmd.Parameters.AddWithValue("spunidade_venda", UnidadeVenda);
            cmd.Parameters.AddWithValue("spcategoria_id", Categoria.Id);
            cmd.Parameters.AddWithValue("spestoque_minimo", EstoqueMinimo);
            cmd.Parameters.AddWithValue("spclasse_desconto", ClasseDesconto);
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
        }
        public static Produto ObterPorId(int id)
        {
            Produto produto = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from produtos where id = {id}";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                produto = new(
                   dr.GetInt32(0),
                   dr.GetString(1),
                   dr.GetString(2),
                   dr.GetDouble(3),
                   dr.GetString(4),
                   Categoria.ObterPorId(dr.GetInt32(5)),
                   dr.GetDouble(6),
                   dr.GetDouble(7),
                   dr.GetDateTime(9)
                    );
            }
            return produto;
        }
        public static Produto ObterPorCodBar(string codbar)
        {
            Produto produto = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from produtos where cod_barras = {codbar}";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                produto = new(
                   dr.GetInt32(0),
                   dr.GetString(1),
                   dr.GetString(2),
                   dr.GetDouble(3),
                   dr.GetString(4),
                   Categoria.ObterPorId(dr.GetInt32(5)),
                   dr.GetDouble(6),
                   dr.GetDouble(7),
                   dr.GetDateTime(9)
                    );
            }
            return produto;
        }
        public static List<Produto> ObterLista()
        {
            List<Produto> produtos = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from produtos order by descricao asc";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                produtos.Add(new(
                   dr.GetInt32(0),
                   dr.GetString(1),
                   dr.GetString(2),
                   dr.GetDouble(3),
                   dr.GetString(4),
                   Categoria.ObterPorId(dr.GetInt32(5)),
                   dr.GetDouble(6),
                   dr.GetDouble(7),
                   dr.GetDateTime(9)
                    ));
            }
            return produtos;
        }
        public bool Atualizar()
        {
            bool resposta = false;
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_produto_update";
            cmd.Parameters.AddWithValue("spid", Id);
            cmd.Parameters.AddWithValue("spcod_barras", CodBar);
            cmd.Parameters.AddWithValue("spdescricao", Descricao);
            cmd.Parameters.AddWithValue("spvalor_unit", ValorUnit);
            cmd.Parameters.AddWithValue("spunidade_venda", UnidadeVenda);
            cmd.Parameters.AddWithValue("spcategoria_id", Categoria.Id);
            cmd.Parameters.AddWithValue("spestoque_minimo", EstoqueMinimo);
            cmd.Parameters.AddWithValue("spclasse_desconto", ClasseDesconto);
            if (cmd.ExecuteNonQuery() > 0)
            {
                cmd.Connection.Close();
                resposta = true;
            }
            return resposta;
        }
        public void Excluir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_produto_delete";
            cmd.Parameters.AddWithValue("spid", Id);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

    }
}
