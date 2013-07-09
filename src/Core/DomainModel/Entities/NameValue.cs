namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class NameValue
    {
        public NameValue()
        {
        }

        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}