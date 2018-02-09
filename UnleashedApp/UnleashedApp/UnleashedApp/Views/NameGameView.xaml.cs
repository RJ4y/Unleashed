using System;
using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NameGameView
    {
        public static List<Employee> Employees { get; private set; }
        public static List<Employee> Males { get; private set; }
        public static List<Employee> Females { get; private set; }
        public static Employee CorrectEmployee { get; private set; }
        public const int Amount = 3;

        public NameGameView()
        {
            InitializeComponent();
            NameGameViewModel viewModel = ViewModelLocator.Instance.NameGameViewModel;
            Employees = viewModel.Employees;
            if (Employees != null && Employees.Count > 0)
            {
                InitializeFields();
            }
        }

        private void InitializeFields()
        {
            if (RandomizeService.GetRandomGender() == 'M')
            {
                Males = GetMales();
                List<Employee> answers = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(Males, Amount);
                CreateAnswers(answers);
            }
            else
            {
                Females = GetFemales();
                List<Employee> answers = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(Females, Amount);
                CreateAnswers(answers);
            }
        }

        private List<Employee> GetMales()
        {
            return GetListWithGender('M');
        }

        private List<Employee> GetFemales()
        {
            return GetListWithGender('F');
        }

        private List<Employee> GetListWithGender(char gender)
        {
            List<Employee> list = new List<Employee>();
            foreach (Employee e in Employees)
            {
                if (Char.ToUpper(e.Gender) == gender)
                {
                    list.Add(e);
                }
            }

            return list;
        }

        private void CreateAnswers(List<Employee> answers)
        {
            if (answers != null)
            {
                CorrectEmployee = RandomizeService.GetRandomObjectFromList(answers);
                EmployeeImage.Source = CorrectEmployee.PictureUrl;
                FillAnswerFields(answers);
                AddTapEvents();
            }
        }

        private void FillAnswerFields(List<Employee> employees)
        {
            OptionOneButton.Text = employees[0].FirstName + " " + employees[0].LastName;
            OptionTwoButton.Text = employees[1].FirstName + " " + employees[1].LastName;
            OptionThreeButton.Text = employees[2].FirstName + " " + employees[2].LastName;
        }

        private void AddTapEvents()
        {
            OptionOneButton.Clicked += OptionTapped;
            OptionTwoButton.Clicked += OptionTapped;
            OptionThreeButton.Clicked += OptionTapped;
            GuessAnotherButton.Clicked += GuessAnotherTapped;
        }

        private void OptionTapped(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == CorrectEmployee.FirstName + " " + CorrectEmployee.LastName)
            {
                ShowCorrectSelectionInformation();
            }
            else
            {
                ShowWrongSelectionMessage(button.Text);
            }
        }

        private void ShowWrongSelectionMessage(string text)
        {
            RowDefinitionWrongOption.Height = new GridLength(0, GridUnitType.Auto);
            WrongOptionSelectedLabel.Text = text + " is not the name of this person. Please try again...";
        }

        private void GuessAnotherTapped(object sender, EventArgs e)
        {
            RowDefinitionWrongOption.Height = new GridLength(0, GridUnitType.Absolute);
            StartNewGame();
        }

        private void StartNewGame()
        {
            CorrectAnswerScrollView.IsVisible = false;
            InitializeFields();
            QuestionScrollView.IsVisible = true;
            WrongOptionSelectedLabel.Text = "";
        }

        private void ShowCorrectSelectionInformation()
        {
            QuestionScrollView.IsVisible = false;
            CorrectAnswerScrollView.IsVisible = true;
            Image.Source = CorrectEmployee.PictureUrl;
            NameLabel.Text = CorrectEmployee.FirstName + " " + CorrectEmployee.LastName;
            FunctionLabel.Text = CorrectEmployee.Function;

            ExpectationsLabel.Text = CorrectEmployee.Expectations;
            MotivationLabel.Text = CorrectEmployee.Motivation;
            NeedToKnowLabel.Text = CorrectEmployee.NeedToKnow;
            BirthDateLabel.Text = CorrectEmployee.DateOfBirth.ToString("d MMM yyyy");
        }
    }
}