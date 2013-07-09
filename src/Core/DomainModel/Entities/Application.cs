namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class Application : Entity
    {
        public string Name { get; set; }

        public string LoggingRules { get; set; }
    }
}