namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class NameIdViewModel
    {
        public NameIdViewModel()
        {
        }

        public NameIdViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}