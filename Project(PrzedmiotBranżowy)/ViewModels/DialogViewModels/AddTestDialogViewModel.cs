using Project_PrzedmiotBranżowy_.Helpers;
using Project_PrzedmiotBranżowy_BackEnd.DAL;
using Project_PrzedmiotBranżowy_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Project_PrzedmiotBranżowy_.ViewModels.DialogViewModels
{
    class AddTestDialogViewModel : BindableBase, IDialogAware
    {
        private IDialogService _dialogService;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private Test _test;
        public Test Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }

        private ObservableCollection<Question> _questions;
        public ObservableCollection<Question> Questions
        {
            get => _questions;
            set => SetProperty(ref _questions, value);
        }

        public DialogCloseListener RequestClose { get; set; }

        public DelegateCommand AddQuestionCommand { get; private set; }
        public DelegateCommand SaveTestCommand { get; private set; }


        public AddTestDialogViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Title = "Додати тест";

            Questions = new();
            Test = new();

            AddQuestionCommand = new DelegateCommand(() => AddQuestion());
            SaveTestCommand = new DelegateCommand(() => SaveTest());
        }

        public void AddQuestion()
        {
            DialogParameters parameters = new();
            parameters.Add("test", Test);

            _dialogService.ShowDialog(ViewNamesDialogs.AddQuestionDialogViewName, parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Question resultQuestion = result.Parameters.GetValue<Question>("question");
                    Test.Questions.Add(resultQuestion);
                    Questions.Add(resultQuestion);
                }
            });
        }

        public void SaveTest()
        {
            if(string.IsNullOrEmpty(Test.Name))
            {
                MessageBox.Show("Введіть заголовок тесту.");
                return;
            }
            //TODO(minimum 4 tests)

            DialogParameters parameters = new();

            parameters.Add("test", Test);

            RequestClose.Invoke(parameters, ButtonResult.OK);
        }


        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
