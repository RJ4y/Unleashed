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

        private void InitializeFields()
        {
            if (RandomizeService.GetRandomGender() == 'M')
            {
                Males = GetMales();
                List<Employee> answers = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(Males, 3);
                CreateAnswers(answers, true);
            }
            else
            {
                Females = GetFemales();
                List<Employee> answers = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(Females, 3);
                CreateAnswers(answers);
            }

        }

        private void CreateAnswers(List<Employee> answers, bool isMale = false)
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
            foreach (Employee employee in employees)
            {
                optionOne.Text = employees[0].FirstName;
                optionTwo.Text = employees[1].FirstName;
                optionThree.Text = employees[2].FirstName;
            }
        }

        private void AddTapEvents()
        {
            optionOne.Clicked += OptionTapped;
            optionTwo.Clicked += OptionTapped;
            optionThree.Clicked += OptionTapped;
        }

        private void OptionTapped(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == CorrectEmployee.FirstName)
            {
                ShowCorrectSelectionInformation();
            }
        }

        private void ShowCorrectSelectionInformation()
        {
            questionGrid.IsVisible = false;
            correctAnswerScrollView.IsVisible = true;
            image.Source = CorrectEmployee.PictureUrl;
            nameLabel.Text = CorrectEmployee.FirstName + " " + CorrectEmployee.LastName;
            functionLabel.Text = CorrectEmployee.Function;
            expectationsLabel.Text = "Expectations: " + CorrectEmployee.Expectations;
            motivationLabel.Text = "Motivation: " + CorrectEmployee.Motivation;
            needToKnowLabel.Text = "Need to know: " + CorrectEmployee.NeedToKnow;
            birthDateLabel.Text = "Birthdate: " + CorrectEmployee.DateOfBirth.ToString("d MMM yyyy");
            /*"Good job! It seems like you know alot about " " already! Here are some other things you might not know:"*/
        }
    }
}

