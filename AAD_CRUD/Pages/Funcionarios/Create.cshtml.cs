using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AAD_CRUD.Models;
using System;
using Microsoft.Data.SqlClient;

namespace AAD_CRUD.Pages.Funcionarios
{
    public class CreateModel : PageModel
    {
        public Funcionario funcionario =new Funcionario();
		public Contacto contactos = new Contacto();
		public String errorMessage = "";
		public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            funcionario.NIF = Int32.Parse(Request.Form["nif"]);
			funcionario.Nome = Request.Form["nome"];
			funcionario.Apelido = Request.Form["apelido"];
			funcionario.DataNasc = DateTime.Parse(Request.Form["dataNasc"]);
			funcionario.DataAdmissao = DateTime.Parse(Request.Form["dataAdmissao"]);
			funcionario.Morada = Request.Form["morada"];
			funcionario.Email = Request.Form["email"];
			funcionario.CPCP = Int32.Parse(Request.Form["cpcp"]);
			contactos.TipoContactoTCID = Int32.Parse(Request.Form["tcid"]);
			contactos.Contactos = Int32.Parse(Request.Form["contacto"]);

			if(funcionario.Nome.Length == 0 || funcionario.Apelido.Length == 0 || funcionario.Morada.Length == 0 || funcionario.Email.Length == 0)
			{
				errorMessage = "Os campos têm que ser todos preenchidos!";
				return;
			}

			try
			{
				string connectionstring = "Data Source=HUGO;Initial Catalog=AAD;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
				using (SqlConnection connection = new SqlConnection(connectionstring))
				{
					connection.Open();
					String sql = "INSERT INTO Funcionario(NIF, Nome, Apelido, DataNasc, DataAdmissao, Morada, Email, CPCP) Values(@NIF, @Nome, @Apelido, @DataNasc, @DataAdmissao, @Morada, @Email, @CPCP);" +
						"INSERT INTO Contacto(TipoContactoTCID, FuncionarioNIF, Contacto) Values(@TCID, @NIF, @Contacto);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("NIF", funcionario.NIF);
						command.Parameters.AddWithValue("Nome", funcionario.Nome);
						command.Parameters.AddWithValue("Apelido", funcionario.Apelido);
						command.Parameters.AddWithValue("DataNasc", funcionario.DataNasc);
						command.Parameters.AddWithValue("DataAdmissao", funcionario.DataAdmissao);
						command.Parameters.AddWithValue("Morada", funcionario.Morada);
						command.Parameters.AddWithValue("Email", funcionario.Email);
						command.Parameters.AddWithValue("CPCP", funcionario.CPCP);
						command.Parameters.AddWithValue("TCID", contactos.TipoContactoTCID);
						command.Parameters.AddWithValue("Contacto", contactos.Contactos);

						command.ExecuteNonQuery();
					}
				}
			}

			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("/Funcionarios/Index");





		}

	}
}
