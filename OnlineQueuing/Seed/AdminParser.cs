using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineQueuing.Data;
using OnlineQueuing.DTO;
using OnlineQueuing.Entities;
using System.IO;

namespace OnlineQueuing.Seed
{
    public class AdminParser
    {
        public ApplicationContext applicationContext;
        public string adminsFromJson;
        public AdminParserDTO adminParserDTO;
        public IConfiguration configuration;

        public AdminParser(ApplicationContext applicationContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            adminsFromJson = new StreamReader(configuration["AdminListLocation"]).ReadToEnd();
            this.applicationContext = applicationContext;
            adminParserDTO = JsonConvert.DeserializeObject<AdminParserDTO>(adminsFromJson);
        }

        public void FillUpDbWithAdmins()
        {
            var admins = adminParserDTO.Admins;

            foreach (var user in admins)
            {
                User adminToAdd = new User() { Email = user.Email, Role = user.Role, Name = user.Name };
                applicationContext.Add(adminToAdd);
                applicationContext.SaveChanges();
            }
        }
    }
}
