using AAD_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AAD_CRUD.Pages.Funcionarios
{
	public class ContactModel : PageModel
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
					String sql = "SELECT [Nome], [Contacto], [DescTC]" +
						"FROM [AAD].[dbo].[Contacto] Join TipoContacto on Contacto.TipoContactoTCID = TipoContacto.TCID Join Funcionario on Contacto.FuncionarioNIF = Funcionario.NIF WHERE Funcionario.NIF = @nif";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@nif", Nif);
						using (SqlDataReader reader = command.ExecuteReader())
						{

							if (reader.Read())
							{
								funcionario.Nome = reader.GetString(0);
								contactos.Contactos = reader.GetInt32(1);
								contactos.DescTipoContacto = reader.GetString(2);


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



		//public void OnPost()
		//{

		//	try
		//	{
		//		string connectionstring = "Data Source=HUGO;Initial Catalog=AAD;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
		//		using (SqlConnection connection = new SqlConnection(connectionstring))
		//		{
		//			connection.Open();
		//			String sql = "DELETE From Funcionario WHERE NIF=@nif";
		//			using (SqlCommand command = new SqlCommand(sql, connection))
		//			{
		//				int Nif = Int32.Parse(Request.Query["nif"]);
		//				command.Parameters.AddWithValue("@nif", Nif);
		//				command.ExecuteNonQuery();
		//			}


		//		}
		//	}

		//	catch (Exception ex)
		//	{
		//		errorMessage = ex.Message;
		//		return;
		//	}
		//	Response.Redirect("/Funcionarios/Index");


		//}
	}
}
