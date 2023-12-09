using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplication2
{
    class Program
    {
        private const String cs = @"Data Source=GHISLAIN\SQLEXPRESS;Initial Catalog=INSE;Trusted_Connection=true";
        private const string baseURL = "https://geo.api.gouv.fr/communes/";
        private const string endurl = "?fields=&format=json";

        static async Task Main(string[] args)
        {
            await ReadDataCommune();
        }

        private static async Task ReadDataCommune()
        {
            List<Ville> villes = new List<Ville>();
            
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(baseURL + endurl))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    villes = JsonConvert.DeserializeObject<List<Ville>>(json);
                }
                else
                {
                    Console.WriteLine("Erreur de requête : " + response.StatusCode);
                    return;
                }
            }

            foreach (Ville ville in villes)
            {
                string uri = string.Concat(baseURL + ville.code + endurl);

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Ville item = JsonConvert.DeserializeObject<Ville>(json);

                        if (item.population == null)
                        {
                            item.population = "0";
                        }

                        try
                        {
                            using (SqlConnection connection = new SqlConnection(cs))
                            {
                                connection.Open();

                                // Insérer la population dans la table de la base de données INSE
                                string query = "UPDATE Commune SET population = @population WHERE Code_commune = @code";
                                SqlCommand command = new SqlCommand(query, connection);
                                command.Parameters.AddWithValue("@population", item.population);
                                command.Parameters.AddWithValue("@code", item.code);

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Population de " + item.nom + " mise à jour.");
                                }
                                else
                                {
                                    Console.WriteLine("Aucune mise à jour pour la population de " + item.nom);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erreur lors de l'insertion dans la base de données : " + ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erreur de requête pour la ville " + ville.nom + " : " + response.StatusCode);
                    }
                }
            }
        }
    }    
}
