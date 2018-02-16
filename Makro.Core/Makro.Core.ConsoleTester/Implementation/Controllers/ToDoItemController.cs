using Makro.Core.ConsoleTester.Implementation.Models;
using Makro.Core.Webservice.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Makro.Core.ConsoleTester.Implementation.Controllers
{
    public class ToDoItemController : IBasicController
    {
        private readonly TodoContext _context;

        public ToDoItemController()
        {
            _context = new TodoContext();

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "DefaultItem", Id = _context.TodoItems.Count + 1, IsComplete = false });
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpPost]
         public void Post()
        {
            throw new Exception("Post incoming");
        }

    }
}
