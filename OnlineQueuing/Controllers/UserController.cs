using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineQueuing.Data;

namespace OnlineQueuing.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext applicationContext;
        

        public UserController(ApplicationContext application)
        {
       
            this.applicationContext = application;
        }

        [HttpPost("appintments")]
        public IActionResult PostNewAppoinment()
        {
            List<DateTime> dataTimes = applicationContext.

            if ()
            {
                return Created("", new { message = "Success" });
            }
            return BadRequest();
        }

    }
}