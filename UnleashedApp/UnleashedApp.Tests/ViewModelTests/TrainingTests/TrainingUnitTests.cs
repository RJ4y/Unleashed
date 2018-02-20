using System;
using System.Collections.Generic;
using UnleashedApp.ViewModels;
using UnleashedApp.Repositories.TrainingRepository;
using Moq;
using NUnit.Framework;
using UnleashedApp.Models;

namespace UnleashedApp.Tests.ViewModelTests.TrainingTests
{
    /// <summary>
    /// Summary description for TrainingUnitTests
    /// </summary>
    [TestFixture]
    public class TrainingUnitTests
    {
        private Mock<ITrainingRepository> _trainingRepoMock;
        private TrainingBuilder _trainingBuilder;
        private TrainingViewModel _trainingViewModel;

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext) { }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [SetUp]
        public void Setup()
        {
            _trainingRepoMock = new Mock<ITrainingRepository>();
            _trainingBuilder = new TrainingBuilder();
            _trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _trainingRepoMock = null;
            _trainingBuilder = null;
            _trainingViewModel = null;
        }

        [Test]
        public void LoadTrainingsShouldInitTrainingList()
        {
            var list = _trainingBuilder.InitList(0);
            _trainingRepoMock.Setup(x => x.GetAll()).Returns(list);

            _trainingViewModel.LoadTrainings();

            Assert.IsNotNull(_trainingViewModel.TrainingList);
            Assert.AreEqual(list, _trainingViewModel.TrainingList);
        }

        [Test]
        public void LoadTrainingsShouldCalculateTotal()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(100));

            _trainingViewModel.LoadTrainings();

            Assert.AreEqual(_trainingBuilder.ReturnTotal(), _trainingViewModel.TrainingTotal);
        }

        [Test]
        public void ConstructorShouldInitCommands()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            Assert.IsNotNull(_trainingViewModel.AddTrainingCommand);
        }

        [Test]
        public void RefreshShouldMaintainListLengthIfNothingChanged()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            _trainingViewModel.LoadTrainings();
            var firstList = _trainingViewModel.TrainingList;

            _trainingViewModel.Refresh();
            var secondList = _trainingViewModel.TrainingList;

            Assert.AreEqual(firstList.Count, secondList.Count);
        }

        [Test]
        public void RefreshShouldReturnBiggerList()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            _trainingViewModel.LoadTrainings();
            var firstCount = _trainingViewModel.TrainingList.Count;

            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(2));

            _trainingViewModel.Refresh();
            var secondCount = _trainingViewModel.TrainingList.Count;

            Assert.AreNotEqual(firstCount, secondCount);
            Assert.IsTrue(firstCount < secondCount);
        }

        [Test]
        public void SendInvoiceShouldConvertToYesOrNo()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            _trainingViewModel.SendInvoice = true;

            Assert.AreEqual("Yes", _trainingViewModel.GetSendInvoice());

            _trainingViewModel.SendInvoice = false;
            Assert.AreEqual("No", _trainingViewModel.GetSendInvoice());
        }

        [Test]
        public void SendInvoiceShouldReturnCorrectBoolean()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            _trainingViewModel.SetSendInvoice("Yes");
            Assert.IsTrue(_trainingViewModel.SendInvoice);

            _trainingViewModel.SetSendInvoice("No");
            Assert.IsFalse(_trainingViewModel.SendInvoice);
        }

        [Test]
        public void VerifyFormShouldReturnTrueIfAllParamatersAreTrue()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            _trainingViewModel.IsCostValid = true;
            _trainingViewModel.IsCityValid = true;
            _trainingViewModel.IsCompanyValid = true;
            _trainingViewModel.IsDaysValid = true;
            _trainingViewModel.IsEventValid = true;

            _trainingViewModel.VerifyForm();

            Assert.IsTrue(_trainingViewModel.IsValid);
        }

        [Test]
        public void VerifyFormShouldReturnFalseIfAParamaterIsFalse()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            _trainingViewModel.IsCostValid = true;
            _trainingViewModel.IsCityValid = true;
            _trainingViewModel.IsCompanyValid = false;
            _trainingViewModel.IsDaysValid = true;
            _trainingViewModel.IsEventValid = true;

            _trainingViewModel.VerifyForm();

            Assert.IsFalse(_trainingViewModel.IsValid);
        }
    }

    public class TrainingBuilder
    {
        private Random _random;
        private int _total;

        public List<Training> InitList(int count)
        {
            var list = new List<Training>();
            _random = new Random();
            _total = 0;

            for(var i = 0; i < count; i++)
            {
                var item = new Training
                {
                    First_Name = "FirstName",
                    Last_Name = "LastName",
                    City = "Neerpelt",
                    Company = "Company",
                    Date = new DateTime(),
                    Days = 3,
                    Info = "info",
                    Invoice = "Nee",
                    Team = "Unleashed",
                    TrainingName = "Training",

                    Cost = _random.Next(100000)
                };
                _total += item.Cost;

                list.Add(item);
            }

            return list;
        }

        public int ReturnTotal()
        {
            return _total;
        }
    }
}
