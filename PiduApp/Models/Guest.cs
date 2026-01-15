using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiduApp.Models
{
	public class Guest
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool WillAttend { get; set; }
        public int PyhaId { get; set; } // Viide pühale, kuhu registreerutakse
        public virtual Pyha Pyha { get; set; } // Navigatsiooni omadus
    }
}