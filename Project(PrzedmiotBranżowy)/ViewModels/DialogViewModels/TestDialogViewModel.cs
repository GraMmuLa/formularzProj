using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_BackEnd.DAL;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy__Core.Helpers;

namespace Project_PrzedmiotBranzowy_.ViewModels.DialogViewModels
{
    class TestDialogViewModel : BindableBase, IDialogAware
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

        private Question? _selectedQuestion;
        public Question? SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                SetProperty(ref _selectedQuestion, value);

                EditQuestionCommand?.RaiseCanExecuteChanged();
                DeleteQuestionCommand?.RaiseCanExecuteChanged();
            }
        }

        public DialogCloseListener RequestClose { get; set; }

        public DelegateCommand AddQuestionCommand { get; private set; }
        public DelegateCommand<Question> EditQuestionCommand { get; private set; }
        public DelegateCommand<Question> DeleteQuestionCommand { get; private set; }
        public DelegateCommand SaveTestCommand { get; private set; }


        public TestDialogViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Title = "Додати тест";

            Questions = new();
            Test = new();

            AddQuestionCommand = new DelegateCommand(() => AddQuestion());
            EditQuestionCommand = new DelegateCommand<Question>(DeleteQuestion, CanEditQuestion);
            DeleteQuestionCommand = new DelegateCommand<Question>(EditQuestion, CanDeleteQuestion);
            SaveTestCommand = new DelegateCommand(() => SaveTest());
        }

        private bool CanEditQuestion(Question question) => question != null;
        private bool CanDeleteQuestion(Question question) => question != null;

        public void AddQuestion()
        {
            DialogParameters parameters = new();
            parameters.Add("test", Test);

            _dialogService.ShowDialog(ViewNamesDialogs.QuestionDialogViewName, parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Question resultQuestion = result.Parameters.GetValue<Question>("question");
                    Test.Questions.Add(resultQuestion);
                    Questions.Add(resultQuestion);
                }
            });
        }

        public void DeleteQuestion(Question question)
        {
            Questions.Remove(question);
            Test.Questions.Remove(question);
            SelectedQuestion = null;
        }

        public void EditQuestion(Question question)
        {
            //TODO
            DialogParameters parameters = new()
            {
                { "test", Test },
                { "question", question }
            };

            _dialogService.ShowDialog(ViewNamesDialogs.QuestionDialogViewName, parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Question resultQuestion = result.Parameters.GetValue<Question>("question");

                    Question? question = Test.Questions.FirstOrDefault(x=>x.Id == resultQuestion.Id);
                    question = resultQuestion;

                    Questions.Remove(question);
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

            DialogParameters parameters = new()
            {
                { "test", Test }
            };

            RequestClose.Invoke(parameters, ButtonResult.OK);
        }


        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Test testToEdit;
            if(parameters.TryGetValue<Test>("test", out testToEdit))
            {
                Test = testToEdit;

                Questions = new(Test.Questions);
            }
        }
    }
}
