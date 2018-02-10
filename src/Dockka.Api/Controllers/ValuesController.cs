using System;
using System.Collections.Generic;
using Dockka.Api.Controllers.Base;
using Dockka.Data.Context;
using Dockka.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dockka.Api.Controllers
{
    [Route("api/v1/persons")]
    public class PersonsController : BaseEntityController<Person>
    {
        public PersonsController(DockkaContext context) : base(context)
        {
        }
    }
}
