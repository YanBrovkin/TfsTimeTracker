using System.Collections.Generic;

namespace WebApp.Models
{
    public class TasksViewModel
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<TfsWorkItem> Values { get; set; }
    }
}