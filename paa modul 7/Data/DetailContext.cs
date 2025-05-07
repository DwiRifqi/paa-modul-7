using Npgsql;
using paa_modul_7.Helpers;
using paa_modul_7.Models;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace paa_modul_7.Data
{
    public class DetailContext : IDisposable
    {
        private readonly string _constr;
        private string errorMsg;
        private string name;
        private string age;
        private int detail;
        private readonly SqlDBHelper _db;
        public DetailContext(string constr)
        {
            _constr = constr;
            _db = new SqlDBHelper(_constr);
        }

        public void InsertPersonDetails(List<PersonDetailFromAPI> personDetails)
        {
            string query = @"INSERT INTO person_detail (id, name, saldo, hutang) VALUES (@id, @name, @saldo, @hutang);";

            try
            {
                foreach (var person in personDetails)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_constr))
                    {
                        connection.Open();
                        using (NpgsqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = query;
                            cmd.Parameters.AddWithValue("id", person.Id);
                            cmd.Parameters.AddWithValue("name", person.Name);
                            cmd.Parameters.AddWithValue("saldo", person.Saldo);
                            cmd.Parameters.AddWithValue("hutang", person.Hutang);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                throw;
            }
        }

        public PersonDetail getPersonDetail(int id)
        {
            string query = @"SELECT p.id, p.name, p.age, pd.saldo, pd.hutang FROM person p
                     JOIN person_detail pd ON p.id = pd.id WHERE p.id = @id;";
            PersonDetail personDetail = null;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_constr))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("id", id);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                personDetail = new PersonDetail
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Age = reader.GetInt32(2),
                                    Detail = new Detail
                                    {
                                        saldo = reader.GetInt32(3),
                                        hutang = reader.GetInt32(4)
                                    }
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                throw;
            }

            return personDetail;
        }
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
