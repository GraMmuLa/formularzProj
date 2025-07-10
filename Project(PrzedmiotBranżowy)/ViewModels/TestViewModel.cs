using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using Project_PrzedmiotBranzowy_Core.Services;
using System.Windows;

namespace Project_PrzedmiotBranzowy_.ViewModels
{
    internal class TestViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private Test _currentTest;
        private User _currentUser;
        private TestUser _testUser;
        private int _currentQuestionIndex;

        private List<ListBoxModel<Answer>> _chosenAnswers = new();

        private string _buttonContent;
        public string ButtonContent
        {
            get { return _buttonContent; }
            set { SetProperty(ref _buttonContent, value); }
        }

        private ListBoxModelList<Question, Answer> _currentQuestion;
        public ListBoxModelList<Question, Answer> CurrentQuestion
        {
            get { return _currentQuestion; }
            set { SetProperty(ref _currentQuestion, value); }
        }

        private ListBoxModel<Answer> _chosenAnswer;
        public ListBoxModel<Answer> ChosenAnswer
        {
            get { return _chosenAnswer; }
            set { SetProperty(ref _chosenAnswer, value); }
        }

        private DelegateCommand _nextQuestionCommand;
        public DelegateCommand NextQuestionCommand
        {
            get { return _nextQuestionCommand; }
            set { SetProperty(ref _nextQuestionCommand, value); }
        }

        public DelegateCommand<ListBoxModel<Answer>> SelectAnswerCommand { get; private set; }

        public TestViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
            SelectAnswerCommand  = new DelegateCommand<ListBoxModel<Answer>>((testAnswer) => SelectAnswer(testAnswer));
        }

        public void NextQuestion()
        {
            //TODO (if 0 questions)
            if (_currentTest.Questions.Count <= 0)
                return;
            if (_currentQuestion.ListBoxModels.All(x => x.IsSelected == false))
            {
                MessageBox.Show("Оберіть відповідь.");
                return;
            }
            if (ChosenAnswer != null)
                _chosenAnswers.Add(ChosenAnswer);
            //TODO (if equals or more than Questions.Count index)
            if (_currentQuestionIndex + 1 >= _currentTest.Questions.Count)
            {
                ButtonContent = "Завершити тест";
                //TODO
                NextQuestionCommand = new DelegateCommand(() =>
                {
                    if (_currentQuestion.ListBoxModels.All(x => x.IsSelected == false))
                    {
                        MessageBox.Show("Оберіть відповідь.");
                        return;
                    }
                    _navigationService.NavigateTo(ViewNamesNavigation.ContentRegion,
                    ViewNamesNavigation.HomeViewName);
                });

                if (ChosenAnswer is not null && ChosenAnswer.Value.IsCorrect)
                    ++_testUser.Marks;

                SetCurrentQuestion(_currentTest.Questions[_currentQuestionIndex++]);
                //TODO (+mark if correct answer)
            }
        }

        public void SelectAnswer(ListBoxModel<Answer> testAnswer)
        {
            foreach (var a in CurrentQuestion.ListBoxModels)
                a.IsSelected = a.Value.Id == testAnswer.Value.Id;
            ChosenAnswer = testAnswer;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _currentQuestionIndex = 0;
            NextQuestionCommand = new DelegateCommand(NextQuestion);
            ButtonContent = "Наступне питання";
            _currentUser = navigationContext.Parameters.GetValue<User>("user");
            _currentTest = navigationContext.Parameters.GetValue<Test>("test");

            _testUser = new()
            {
                User = _currentUser,
                Test = _currentTest,
                Marks = 0
            };

            SetCurrentQuestion(_currentTest.Questions[_currentQuestionIndex++]);
        }

        public void SetCurrentQuestion(Question question)
        {
            CurrentQuestion = new(question);

            CurrentQuestion.InitListBoxModels(question.Answers);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}