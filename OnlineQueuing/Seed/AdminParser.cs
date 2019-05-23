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
            AdminsFromJson = new StreamReader(Configuration["AdminListLocation"]).ReadToEnd();
            ApplicationContext = applicationContext;
            AdminParserDTO = JsonConvert.DeserializeObject<AdminParserDTO>(AdminsFromJson);
        }
    }
}
