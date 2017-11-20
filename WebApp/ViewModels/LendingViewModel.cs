using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ViewModels
{
    public class LendingViewModel
    {
        public Game Game { get; set; }
        public Friend Friend { get; set; }
    }
}