using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SysTINSClass
{
    public class Usuario
    {
        public int Id;
        public string? Nome;
        public string? Email;
        public string? Senha;
        public Nivel Nivel;
        public bool Ativo;

        public Usuario() 
        {
            Nivel = new();
        }
        public Usuario(string nome, string email, string senha, Nivel nivel)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Nivel = nivel;
        }
        public Usuario(string nome, string email, string senha, Nivel nivel, bool ativo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Nivel = nivel;
            Ativo = ativo;
        }
        public Usuario(int id, string nome, string email, string senha, Nivel nivel, bool ativo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Nivel = nivel;
            Ativo = ativo;
        }
        // inserir 
        public void Inserir() 
        {
            var cmd = Banco.Abrir();

           // cmd.CommandText = $"insert into usuarios values (0, '{Nome}', '{Email}', md5('{Senha}'), {Nivel.Id}, default);";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_usuario_insert";
            cmd.Parameters.Add("spnome",MySql.Data.MySqlClient.MySqlDbType.VarChar).Value=Nome;
            cmd.Parameters.AddWithValue("spemail", Email);
            cmd.Parameters.AddWithValue("spsenha", Senha);
            cmd.Parameters.AddWithValue("spnivel", Nivel.Id);
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Id = dr.GetInt32(0);
            }
            cmd.Connection.Close();
        }
        // ObterPorId
        public static Usuario ObterPorId(int id) 
        {
            Usuario usuario = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from usuarios where id = {id}";
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                usuario = new(
                        dr.GetInt32(0),
                        dr.GetString(1),
                        dr.GetString(2),
                        dr.GetString(3),
                        Nivel.ObterPorId(dr.GetInt32(4)),
                        dr.GetBoolean(5)
                    );
            }
            return usuario;
        }
        public List<Usuario> ObterLista() 
        {
            List<Usuario> lista = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from usuarios order by nome asc";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new(
                        dr.GetInt32(0),
                        dr.GetString(1),
                        dr.GetString(2),
                        dr.GetString(3),
                        Nivel.ObterPorId(dr.GetInt32(4)),
                        dr.GetBoolean(5)
                    )
                );
            }
            return lista;            
        }
        public bool Atualizar() 
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_usuario_altera";
            cmd.Parameters.AddWithValue("spid",Id);
            cmd.Parameters.AddWithValue("spnome", Nome);
            cmd.Parameters.AddWithValue("spsenha", Senha);
            cmd.Parameters.AddWithValue("spnivel", Nivel.Id);
            return cmd.ExecuteNonQuery() > 0 ? true : false;
        }
        // efetuar login
        public static Usuario EfetuarLogin(string email, string senha)
        {
            Usuario usuario = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from usuarios where email = '{email}' and senha = md5('{senha}') and ativo = 1";
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                usuario = new(
                        dr.GetInt32(0),
                        dr.GetString(1),
                        dr.GetString(2),
                        dr.GetString(3),
                        Nivel.ObterPorId(dr.GetInt32(4)),
                        dr.GetBoolean(5)
                    );
            }
            return usuario;
        }

    }
}
//id int(4) AI PK 
//nome varchar(60) 
//email varchar(60) 
//senha varchar(32) 
//nivel_id int(11) 
//ativo bit(1)

//CREATE DEFINER =`root`@`localhost` PROCEDURE `sp_usuario_altera`(
//-- parâmetros da procedure
//spid int, spnome varchar(60), spsenha varchar(32), spnivel int)
//begin
//	update usuarios 
//	set nome = spnome, senha = md5(spsenha), nivel_id = spnivel where id = spid;
//end$$

//CREATE DEFINER =`root`@`localhost` PROCEDURE `sp_usuario_insert`(
//-- parâmetros da procedure
//spnome varchar(60), spemail varchar(60), spsenha varchar(32), spnivel int)
//begin
//	insert into usuarios 
//	values (0, spnome, spemail, md5(spsenha), spnivel, default);
//select* from usuarios where id = last_insert_id();
//end$$


