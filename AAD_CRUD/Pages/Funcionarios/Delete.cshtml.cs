using AAD_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AAD_CRUD.Pages.Funcionarios
{
    public class DeleteModel : PageModel
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
				string connectionstring = "Data Source=HUGO;Initial Catalog=AAD;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
				using (SqlConnection connection = new SqlConnection(connectionstring))
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

			try
			{
				string connectionstring = "Data Source=HUGO;Initial Catalog=AAD;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
				using (SqlConnection connection = new SqlConnection(connectionstring))
				{
					connection.Open();
					String sql = "DELETE From Funcionario WHERE NIF=@nif";
					String sql2 = "DELETE From Contacto WHERE FuncionarioNIF=@nif";
					using (SqlCommand command = new SqlCommand(sql2, connection))
					{
						int Nif = Int32.Parse(Request.Query["nif"]);
						command.Parameters.AddWithValue("@nif", Nif);
						command.ExecuteNonQuery();
					}
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						int Nif = Int32.Parse(Request.Query["nif"]);
						command.Parameters.AddWithValue("@nif", Nif);
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
