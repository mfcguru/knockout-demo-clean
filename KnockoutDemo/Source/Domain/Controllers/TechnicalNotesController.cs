﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnockoutDemo.Source.Domain.Controllers
{
    public class TechnicalNotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
