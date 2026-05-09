using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kursach_Backend
{
    public class AuthOptions
    {
        public const string ISSUER = "Kursach_Backend_App_Server";
        public const string AUDIENCE = "Kursach_Backend_App_Client";
        public const string KEY = "C10F6BEF-C883-4F6B-9302-42D35854B388";
        public const int LIFETIME = 480;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
