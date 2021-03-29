﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
        public string Misha { get; set; }
    }
}