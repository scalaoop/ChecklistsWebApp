﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Checklists.Core.Models
{
    public class Task
    {
        public Task()
        {
            DateAdded = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public bool Checked { get; set; }

        public DateTime DateAdded { get; set; }

        public int ChecklistId { get; set; }
        public Checklist Checklist { get; set; }

        public bool IsDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Check()
        {
            if (!Checked)
                Checked = true;
        }

        public void Uncheck()
        {
            if (Checked)
                Checked = false;
        }
    }
}