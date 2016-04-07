using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DndHelper.Models
{
    public class Player
    {
        [ScaffoldColumn(false)]
        public int PlayerId { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string PlayerName { get; set; }
    }
}