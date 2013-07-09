using System.Collections.Generic;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public interface IPrivilegesList
    {
        List<PrivilegesViewModel> Privileges { get; set; }
    }
}