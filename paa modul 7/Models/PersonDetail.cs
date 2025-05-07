namespace paa_modul_7.Models
{
    public class Detail
    {
        internal int saldo;
        internal int hutang;

        public int Saldo { get; set; }
        public int Hutang { get; set; }
    }
    
    public class PersonDetail : Person
    {
        public Detail? Detail { get; set; }
    }
}
