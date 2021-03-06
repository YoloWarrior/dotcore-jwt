﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cor.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetyaService;

namespace cor.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IUserService _userService;
        private PetyaServiceSoapClient _client;
        public ValuesController(IUserService userService)
        {
            _client = new PetyaServiceSoapClient(PetyaServiceSoapClient.EndpointConfiguration.PetyaServiceSoap);
            _userService = userService;
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: администратор");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { _client.HelloWorldAsync().Result.Body.HelloWorldResult, _userService.GetLogin() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
