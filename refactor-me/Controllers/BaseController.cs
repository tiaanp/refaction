using AutoMapper;
using refactor_me.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace refactor_me.Controllers
{
   
    public class BaseController : ApiController
    {
        public IRefactorMeProvider _RefactorMeProvider {get;set; }

        protected BaseController( IRefactorMeProvider refactorMeProvider )
        {
            _RefactorMeProvider = refactorMeProvider;

        }
    }
}
