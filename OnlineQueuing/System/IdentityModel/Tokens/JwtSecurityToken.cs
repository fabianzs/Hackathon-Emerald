namespace System.IdentityModel.Tokens
{
    internal class JwtSecurityToken
    {
        private string token;

        public JwtSecurityToken(string token)
        {
            this.token = token;
        }
    }
}