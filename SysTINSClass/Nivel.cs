using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysTINSClass
{
    public class Nivel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Sigla { get; set; }

        public Nivel() { } // método construtor (new)

        public Nivel( string? nome, string? sigla)
        {
            Nome = nome;
            Sigla = sigla;
        }
        public Nivel(int id, string? nome, string? sigla)
        {
            Id = id;
            Nome = nome;
            Sigla = sigla;
        }

        // inserir
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"insert niveis (nome, sigla) values ('{Nome}','{Sigla}') ";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        // consultar por id
        public static Nivel ObterPorId(int id)
        {
            Nivel nivel = new();
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"select id, nome, sigla from niveis where id = {id}";
            var dr = cmd.ExecuteReader();
            if (dr.Read()) 
            {
                //nivel.Id = dr.GetInt32(0);
                //nivel.Nome = dr.GetString(1);
                //nivel.Sigla = dr.GetString(2);
                nivel = new(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
            }
            cmd.Connection.Close();
            return nivel;
        }
        // obter lista
        public static List<Nivel> ObterLista() 
        {
            List<Nivel> lista = new();
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from niveis order by nome asc";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new(dr.GetInt32(0), dr.GetString(1), dr.GetString(2)));
            }
            cmd.Connection.Close();
            return lista;
        }
        // atualizar
        public bool Atualizar() 
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"update niveis set nome = '{Nome}', sigla = '{Sigla}' where id = {Id}";
            return cmd.ExecuteNonQuery()>0?true:false;   
        }
        // deletar nivel
        public void Excluir() 
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = $"delete from niveis where id = {Id}";
            cmd.ExecuteNonQuery();
        } 

    }
}
