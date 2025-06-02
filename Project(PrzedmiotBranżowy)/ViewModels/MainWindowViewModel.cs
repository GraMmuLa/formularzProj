using Project_PrzedmiotBranżowy_.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_.ViewModels
{
    internal class MainWindowViewModel
    {
        private readonly ApplicationDbContext _dbContext;

        public MainWindowViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
