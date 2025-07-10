using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Project_PrzedmiotBranzowy_.ViewModels.DialogViewModels
{
    class QuestionDialogViewModel : BindableBase, IDialogAware
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

        private Answer? _selectedAnswer;
        public Answer? SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                SetProperty(ref _selectedAnswer, value);
                EditAnswerCommand?.RaiseCanExecuteChanged();
                DeleteAnswerCommand?.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand AddAnswerCommand { get; private set; }
        public DelegateCommand<Answer>? EditAnswerCommand { get; private set; }
        public DelegateCommand<Answer>? DeleteAnswerCommand { get; private set; }
        public DelegateCommand SaveQuestionCommand { get; private set; }

        public QuestionDialogViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Title = "Додати питання";

            Answers = new();
            Question = new();

            AddAnswerCommand = new DelegateCommand(AddAnswer);
            EditAnswerCommand = new DelegateCommand<Answer>(EditAnswer, CanEditAnswer);
            DeleteAnswerCommand = new DelegateCommand<Answer>(DeleteAnswer, CanDeleteAnswer);
            SaveQuestionCommand = new DelegateCommand(SaveQuestion);
        }

        private bool CanEditAnswer(Answer answer) => answer != null;
        private bool CanDeleteAnswer(Answer answer) => answer != null;

        private void AddAnswer()
        {
            DialogParameters parameters = new()
            {
                { "question", Question }
            };

            _dialogService.ShowDialog(ViewNamesDialogs.AnswerDialogViewName, parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Answer resultAnswer = result.Parameters.GetValue<Answer>("answer");

                    Answers.Add(resultAnswer);
                    Question.Answers.Add(resultAnswer);
                }
            });
        }

        private void EditAnswer(Answer answer)
        {
            DialogParameters parameters = new()
            {
                { "question", Question },
                { "answer", answer }
            };

            _dialogService.ShowDialog(ViewNamesDialogs.AnswerDialogViewName, parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Answer resultAnswer = result.Parameters.GetValue<Answer>("answer");

                    Answers.Remove(answer);
                    Answers.Add(resultAnswer);

                    Answer answerToEdit = Question.Answers.First(x => x.Id == answer.Id);
                    answerToEdit = resultAnswer;
                }
            });
        }

        private void DeleteAnswer(Answer answer)
        {
            Question.Answers.Remove(answer);
            Answers.Remove(answer);
            SelectedAnswer = null;
        }

        private void SaveQuestion()
        {
            if(string.IsNullOrEmpty(Question.Title))
            {
                MessageBox.Show("Введіть заголовок питання.");
                return;
            }
            else if(Question.Answers.All(x=>x.IsCorrect == false))
            {
                MessageBox.Show("Виділить одну відповідь як правильну.");
                return;
            }
            else if(Question.Answers.Count < 4)
            {
                MessageBox.Show("Додано менше 4 відповідей.");
                return;
            }

            DialogParameters parameters = new()
            {
                { "question", Question }
            };

            RequestClose.Invoke(parameters, ButtonResult.OK);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Question.Test = parameters.GetValue<Test>("test");

            Question questionToEdit;
            if(parameters.TryGetValue<Question>("question", out questionToEdit))
            {
                Question = questionToEdit;

                Answers = new(Question.Answers);
            }
        }
    }
}
