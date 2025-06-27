using Microsoft.EntityFrameworkCore.Query.Internal;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Project_PrzedmiotBranzowy_.ViewModels.DialogViewModels
{
    class AddAnswerDialogViewModel : BindableBase, IDialogAware
    {
        public DialogCloseListener RequestClose { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private Answer _answer;
        public Answer Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        public DelegateCommand SaveAnswerCommand { get; private set; }

        public AddAnswerDialogViewModel()
        {
            Title = "Додати відповідь";

            SaveAnswerCommand = new DelegateCommand(() => SaveAnswer());

            Answer = new();
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Answer.Question = parameters.GetValue<Question>("question");
        }

        public void SaveAnswer()
        {
            if(string.IsNullOrEmpty(Answer.Title))
            {
                MessageBox.Show("Введіть заголовок питання.");
                return;
            }

            DialogParameters parameters = new()
                {
                    { "answer", Answer }
                };

            RequestClose.Invoke(parameters, ButtonResult.OK);
        }
    }
}
