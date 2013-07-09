namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class PrivilegesViewModel
    {
        public string ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public bool CanBrowse { get; set; }

        public bool CanModify { get; set; }
    }
}