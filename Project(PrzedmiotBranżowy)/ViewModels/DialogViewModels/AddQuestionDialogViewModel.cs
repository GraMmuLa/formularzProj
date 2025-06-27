using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using System.Collections.ObjectModel;
using System.Windows;
using Project_PrzedmiotBranzowy_.ViewNames;

namespace Project_PrzedmiotBranzowy_.ViewModels.DialogViewModels
{
    class AddQuestionDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IDialogService _dialogService;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DialogCloseListener RequestClose { get; set; }

        private Question _question;
        public Question Question
        {
            get => _question;
            set => SetProperty(ref _question, value);
        }

        private ObservableCollection<Answer> _answers;
        public ObservableCollection<Answer> Answers
        {
            get => _answers;
            set => SetProperty(ref _answers, value);
        }

        public DelegateCommand AddAnswerCommand { get; private set; }
        public DelegateCommand SaveQuestionCommand { get; private set; }

        public AddQuestionDialogViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Title = "Додати питання";

            Answers = new();
            Question = new();

            AddAnswerCommand = new DelegateCommand(() => AddAnswer());

            SaveQuestionCommand = new DelegateCommand(() => SaveQuestion());
        }

        public void AddAnswer()
        {
            DialogParameters parameters = new();

            parameters.Add("question", Question);

            _dialogService.ShowDialog(ViewNamesDialogs.AddAnswerDialogViewName, parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Answer resultAnswer = result.Parameters.GetValue<Answer>("answer");
                    if (Answers.Count == 0)
                        resultAnswer.IsCorrect = true;
                    Answers.Add(resultAnswer);
                    Question.Answers.Add(resultAnswer);
                }
            });
        }

        public void SaveQuestion()
        {
            if(string.IsNullOrEmpty(Question.Title))
            {
                MessageBox.Show("Введіть заголовок питання.");
                return;
            }
            //TODO(minimum 4 answers)

            DialogParameters parameters = new();

            parameters.Add("question", Question);

            RequestClose.Invoke(parameters, ButtonResult.OK);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Question.Test = parameters.GetValue<Test>("test");
        }
    }
}
