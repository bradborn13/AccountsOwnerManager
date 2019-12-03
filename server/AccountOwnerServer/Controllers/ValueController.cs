using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        private IRepositoryWrapper _repoWrapper;

        public ValueController(IRepositoryWrapper repoWrapper,ILoggerManager logger)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }
        [HttpGet]
        public IEnumerable<string> get()
        {
            var adminAccounts = _repoWrapper.Account.FindByCondition(x =>x.AccountType.Equals("admin"));
            var owners = _repoWrapper.Owner.FindAll();

            return new String[] {"string","string" };
                
        }
    }
}
