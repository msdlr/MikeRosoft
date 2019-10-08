namespace MikeRosoft.Design
{
    public class ApplicationUser
    {
        public string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string FirstSurname { get; set; }

        public virtual string SecondSurname { get; set; }
    }
}
