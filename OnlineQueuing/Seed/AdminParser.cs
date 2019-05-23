using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineQueuing.Data;
using OnlineQueuing.DTO;
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
            AdminsFromJson = new StreamReader(".\\Seed\\admins.json").ReadToEnd();
            this.ApplicationContext = applicationContext;
            AdminParserDTO = JsonConvert.DeserializeObject<AdminParserDTO>(AdminsFromJson);
        }

        public void FillUpDbWithAdmins()
        {
            
        }
    }
}
