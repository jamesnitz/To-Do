﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace simple_ToDo.Models
{
    public class TodoItem
    {
  
        public int Id { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]

        public int TodoStatusId { get; set; }

        public TodoStatus TodoStatus { get; set; }
        [Required]

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
