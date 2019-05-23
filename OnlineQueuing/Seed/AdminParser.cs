using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineQueuing.Data;
using OnlineQueuing.DTO;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Seed
{
    public class AdminParser
    {
        public ApplicationContext ApplicationContext { get; set; }
        public string AdminsFromJson { get; set; }
        public AdminParserDTO AdminParserDTO { get; set; }
        public IConfiguration Configuration { get; set; }

        public AdminParser(ApplicationContext applicationContext, IConfiguration configuration)
        {
            this.Configuration = configuration;
            AdminsFromJson = new StreamReader(configuration["AdminListLocation"]).ReadToEnd();
            this.ApplicationContext = applicationContext;
            AdminParserDTO = JsonConvert.DeserializeObject<AdminParserDTO>(AdminsFromJson);
        }

        public void FillUpDbWithAdmins()
        {
            var admins = AdminParserDTO.Admins;

            foreach (var user in admins)
            {
                User adminToAdd = new User() { Email = user.Email, Role = user.Role, Name = user.Name };
                ApplicationContext.Add(adminToAdd);
                ApplicationContext.SaveChanges();
            }
        }
    }
}
