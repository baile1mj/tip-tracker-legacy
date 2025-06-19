namespace TipTracker.Models
{
    public class Server
    {
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool PrintChit { get; set; }

        public override string ToString()
        {
            return $"{Number}  {LastName}, {FirstName}";
        }
    }
}
