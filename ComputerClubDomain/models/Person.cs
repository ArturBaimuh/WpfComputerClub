namespace Domain.models
{
    public class Person
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int NumberOfHours { get; set; }
        public bool Worker { get; set; }
        public int Comp { get; set; }
        public string Login { get; set; }
        public string Paswword { get; set; }

    }
}
