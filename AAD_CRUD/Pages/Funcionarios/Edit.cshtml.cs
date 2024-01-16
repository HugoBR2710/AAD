using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AAD_CRUD.Models;
using Microsoft.Data.SqlClient;

namespace AAD_CRUD.Pages.Funcionarios
{
	public class EditModel : PageModel
	{

		public Funcionario funcionario = new Funcionario();
		public Contacto contactos = new Contacto();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
			int Nif = Int32.Parse(Request.Query["nif"]);

			try
			{
				//string connectionstring = "Data Source=HUGO;Initial Catalog=AAD;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
				using (var connection = Dbconn.DBConnection())
                {
					connection.Open();
					String sql = "Select [NIF],[Nome],[Apelido],[DataNasc],[DataAdmissao],[Morada],[Email],[CPCP],[Localidade], [Contacto], [TipoContactoTCID] FROM [AAD].[dbo].[Funcionario] Join CP on Funcionario.CPCP = Cp.CP Join Contacto On Funcionario.NIF = Contacto.FuncionarioNIF Join TipoContacto on Contacto.TipoContactoTCID = TipoContacto.TCID WHERE NIF = @nif";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@nif", Nif);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								funcionario.NIF = reader.GetInt32(0);
								funcionario.Nome = reader.GetString(1);
								funcionario.Apelido = reader.GetString(2);
								funcionario.DataNasc = reader.GetDateTime(3);
								funcionario.DataAdmissao = reader.GetDateTime(4);
								funcionario.Morada = reader.GetString(5);
								funcionario.Email = reader.GetString(6);
								funcionario.CPCP = reader.GetInt32(7);
								funcionario.Localidade = reader.GetString(8);
								contactos.Contactos = reader.GetInt32(9);
								contactos.TipoContactoTCID = reader.GetInt32(10);
							}
						}

					}
				}
			}

			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

		}



		public void OnPost()
		{
			funcionario.NIF = Int32.Parse(Request.Form["NIF2"]);
			funcionario.Nome = Request.Form["nome"];
			funcionario.Apelido = Request.Form["apelido"];
			funcionario.DataNasc = DateTime.Parse(Request.Form["dataNasc"]);
			funcionario.DataAdmissao = DateTime.Parse(Request.Form["dataAdmissao"]);
			funcionario.Morada = Request.Form["morada"];
			funcionario.Email = Request.Form["email"];
			funcionario.CPCP = Int32.Parse(Request.Form["cpcp"]);
			contactos.Contactos = Int32.Parse(Request.Form["contacto"]);
			contactos.TipoContactoTCID = Int32.Parse(Request.Form["tipocontacto"]);


			if (funcionario.Nome.Length == 0 || funcionario.Apelido.Length == 0 || funcionario.Morada.Length == 0 || funcionario.Email.Length == 0)
			{
				errorMessage = "Os campos têm que ser todos preenchidos!";
				return;
			}

			try
			{
				//string connectionstring = "Data Source=HUGO;Initial Catalog=AAD;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
				using (var connection = Dbconn.DBConnection())
                {
					connection.Open();
					String sql = "UPDATE Funcionario " +
					"SET NIF=@NIF2, Nome=@nome, Apelido=@apelido, DataNasc=@dataNasc, DataAdmissao=@dataAdmissao, Morada=@morada, Email=@email, CPCP=@cpcp " +
					"WHERE NIF=@nif";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						int Nif = Int32.Parse(Request.Query["nif"]);
						command.Parameters.AddWithValue("@nif", Nif);
						command.Parameters.AddWithValue("NIF2", funcionario.NIF);
						command.Parameters.AddWithValue("Nome", funcionario.Nome);
						command.Parameters.AddWithValue("Apelido", funcionario.Apelido);
						command.Parameters.AddWithValue("DataNasc", funcionario.DataNasc);
						command.Parameters.AddWithValue("DataAdmissao", funcionario.DataAdmissao);
						command.Parameters.AddWithValue("Morada", funcionario.Morada);
						command.Parameters.AddWithValue("Email", funcionario.Email);
						command.Parameters.AddWithValue("CPCP", funcionario.CPCP);

						command.ExecuteNonQuery();
					}
					String sql2 = "UPDATE Contacto " +
					"SET Contacto=@contactos, TipoContactoTCID=@tipocontacto, FuncionarioNIF=@NIF2 WHERE FuncionarioNIF=@nif";
					using (SqlCommand command = new SqlCommand(sql2, connection))
					{
						int Nif = Int32.Parse(Request.Query["nif"]);
						command.Parameters.AddWithValue("@nif", Nif);
						command.Parameters.AddWithValue("NIF2", funcionario.NIF);
						command.Parameters.AddWithValue("contactos", contactos.Contactos);
						command.Parameters.AddWithValue("tipocontacto", contactos.TipoContactoTCID);
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
