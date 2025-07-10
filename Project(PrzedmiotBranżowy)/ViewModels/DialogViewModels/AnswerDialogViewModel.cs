using Project_PrzedmiotBranzowy_BackEnd.Models;
using System.Windows;

namespace Project_PrzedmiotBranzowy_.ViewModels.DialogViewModels
{
    class AnswerDialogViewModel : BindableBase, IDialogAware
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

        public AnswerDialogViewModel()
        {
            Title = "Додати відповідь";

            SaveAnswerCommand = new DelegateCommand(SaveAnswer);

            Answer = new();
        }

        public bool CanSaveAnswer() => !string.IsNullOrEmpty(Answer.Title);

        public void SaveAnswer()
        {
            if (string.IsNullOrEmpty(Answer.Title))
            {
                MessageBox.Show("Введіть заголовок відповіді.");
                return;
            }

            DialogParameters parameters = new()
            {
                { "answer", Answer }
            };

            RequestClose.Invoke(parameters, ButtonResult.OK);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Answer.Question = parameters.GetValue<Question>("question");

            Answer answerToEdit;
            if (parameters.TryGetValue<Answer>("answer", out answerToEdit))
            {
                Answer = answerToEdit;
            }
        }
    }
}
