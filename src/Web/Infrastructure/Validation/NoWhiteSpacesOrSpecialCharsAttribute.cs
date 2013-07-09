using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System.ComponentModel.DataAnnotations;
namespace ByteCarrot.Masslog.Web.Infrastructure.Validation
{
    public class NoWhiteSpacesOrSpecialCharsAttribute : RegularExpressionAttribute
    {
        public NoWhiteSpacesOrSpecialCharsAttribute()
            : base("^[a-zA-Z0-9]*$")
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return Strings.NoWhiteSpacesOrSpecialCharsMessage.Args(name);
        }
    }
}