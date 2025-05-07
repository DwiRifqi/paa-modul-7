using Npgsql;
using paa_modul_7.Helpers;
using paa_modul_7.Models;

namespace paa_modul_7.Data
{
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;

        public PersonContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"SELECT id, name, age FROM person; ");
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Person()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString(),
                        Age = int.Parse(reader["age"].ToString()),
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }

        public Person getPersonById(int id)
        {
            string query = "SELECT id, name, age FROM person WHERE id = @id";
            SqlDBHelper db = new SqlDBHelper (this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return new Person()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString(),
                        Age = int.Parse(reader["age"].ToString()),
                    };
                }
                cmd.Dispose(); 
                db.closeConnection();
                return null;
            }
            catch (Exception ex)
            { 
                __ErrorMsg = ex.Message;
                return null;
            }
        }
    }
}
