using Makro.Core.ConsoleTester.Implementation.Models;
using System.Collections.Generic;
using System;

namespace Makro.Core.ConsoleTester.Implementation
{
    public class TodoContext
    {

        public TodoContext()
        {
            TodoItems = new List<TodoItem>();
        }

        public List<TodoItem> TodoItems { get; set; }

        internal void SaveChanges()
        {
            return;//ToDo
        }
    }
}