using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AAD_CRUD.Models;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

namespace AAD_CRUD.Pages.Funcionarios
{
    public class IndexModel : PageModel
    {
        public List<Funcionario> funcionarios = new List<Funcionario>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=HUGO;Initial Catalog=AAD;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = "SELECT TOP (1000) [NIF],[Nome],[Apelido],[DataNasc],[DataAdmissao],[Morada],[Email],[CPCP],[Localidade] FROM [AAD].[dbo].[Funcionario] Join CP on Funcionario.CPCP = Cp.CP";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())
                            {
                                Funcionario funcionario = new Funcionario();
                                funcionario.NIF = reader.GetInt32(0);
                                funcionario.Nome = reader.GetString(1);
                                funcionario.Apelido = reader.GetString(2);
                                funcionario.DataNasc = reader.GetDateTime(3);
                                funcionario.DataAdmissao = reader.GetDateTime(4);
                                funcionario.Morada = reader.GetString(5);
                                funcionario.Email = reader.GetString(6);
                                funcionario.CPCP = reader.GetInt32(7);
                                funcionario.Localidade = reader.GetString(8);

                                funcionarios.Add(funcionario);

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }

        
    }
}
