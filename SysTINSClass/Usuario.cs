using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_usuario_insert";
            cmd.Parameters.Add("spnome",MySql.Data.MySqlClient.MySqlDbType.VarChar).Value=Nome;
            cmd.Parameters.AddWithValue("spemail", Email);
            cmd.Parameters.AddWithValue("spsenha", Senha);
            cmd.Parameters.AddWithValue("spnivel", Nivel);
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Id = dr.GetInt32(0);
            }
            cmd.Connection.Close();
        }
    }
}

//CREATE DEFINER =`root`@`localhost` PROCEDURE `sp_usuario_insert`(
//-- parâmetros da procedure
//spnome varchar(60), spemail varchar(60), spsenha varchar(32), spnivel int)
//begin
//	insert into usuarios 
//	values (0, spnome, spemail, md5(spsenha), spnivel, default);
//select* from usuarios where id = last_insert_id();
//end$$


//id int(4) AI PK 
//nome varchar(60) 
//email varchar(60) 
//senha varchar(32) 
//nivel_id int(11) 
//ativo bit(1)