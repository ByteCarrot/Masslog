namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class Exception
    {
        public string Type { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public string Name { get; set; }
        
        public Exception InnerException { get; set; }
    }
}