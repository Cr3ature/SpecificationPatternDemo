namespace SpecificationPattern.Demo.CrossCutting.Entities
{
    public class Episode : IBaseEntity
    {
        public Character Hero { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}
