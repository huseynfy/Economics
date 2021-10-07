using Ekonomiks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekonomiks.ViewModels
{
    public class AllViewModel
    {
        public Contact Contacts;
        public About Abouts;
        public IEnumerable<News> News;
        public IEnumerable<Interview> Interviews;
        public IEnumerable<Actual> Actuals;
        public IEnumerable<Project> Projects;
        public IEnumerable<Message> Messages;
        public News GetNews;
        public Interview GetInterview;
        public Actual GetActual;
        public Project GetProject;
        public string Header;

        
    }
}
