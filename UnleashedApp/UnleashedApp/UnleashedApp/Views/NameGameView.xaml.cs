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
    public partial class NameGameView : ContentPage
    {
        private static NameGameViewModel viewModel;
        public static List<Employee> Employees { get; private set; }
        public static List<Employee> Males { get; private set; }
        public static List<Employee> Females { get; private set; }
        public static Employee CorrectEmployee { get; private set; }
        public const int AMOUNT = 3;

        public NameGameView()
        {
            InitializeComponent();
            viewModel = ViewModelLocator.Instance.NameGameViewModel;
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
                List<Employee> answers = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(Males, AMOUNT);
                CreateAnswers(answers);
            }
            else
            {
                Females = GetFemales();
                List<Employee> answers = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(Females, AMOUNT);
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
                employeeImage.Source = CorrectEmployee.PictureUrl;
                FillAnswerFields(answers);
                AddTapEvents();
            }
        }

        private void FillAnswerFields(List<Employee> employees)
        {
            optionOneButton.Text = employees[0].FirstName + " " + employees[0].LastName;
            optionTwoButton.Text = employees[1].FirstName + " " + employees[1].LastName;
            optionThreeButton.Text = employees[2].FirstName + " " + employees[2].LastName;
        }

        private void AddTapEvents()
        {
            optionOneButton.Clicked += OptionTapped;
            optionTwoButton.Clicked += OptionTapped;
            optionThreeButton.Clicked += OptionTapped;
            guessAnotherButton.Clicked += GuessAnotherTapped;
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
            rowDefinitionWrongOption.Height = new GridLength(0, GridUnitType.Auto);
            wrongOptionSelectedLabel.Text = text + " is not the name of this person. Please try again...";
        }

        private void GuessAnotherTapped(object sender, EventArgs e)
        {
            rowDefinitionWrongOption.Height = new GridLength(0, GridUnitType.Absolute);
            StartNewGame();
        }

        private void StartNewGame()
        {
            correctAnswerScrollView.IsVisible = false;
            InitializeFields();
            questionScrollView.IsVisible = true;
            wrongOptionSelectedLabel.Text = "";
        }

        private void ShowCorrectSelectionInformation()
        {
            questionScrollView.IsVisible = false;
            correctAnswerScrollView.IsVisible = true;
            image.Source = CorrectEmployee.PictureUrl;
            nameLabel.Text = CorrectEmployee.FirstName + " " + CorrectEmployee.LastName;
            functionLabel.Text = CorrectEmployee.Function;

            expectationsLabel.Text = CorrectEmployee.Expectations;
            motivationLabel.Text = CorrectEmployee.Motivation;
            needToKnowLabel.Text = CorrectEmployee.NeedToKnow;
            birthDateLabel.Text = CorrectEmployee.DateOfBirth.ToString("d MMM yyyy");
        }
    }
}

