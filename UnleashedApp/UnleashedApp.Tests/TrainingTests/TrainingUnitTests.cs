using System;
using System.Collections.Generic;
using UnleashedApp.ViewModels;
using UnleashedApp.Repositories.TrainingRepository;
using Moq;
using NUnit.Framework;
using UnleashedApp.Models;

namespace UnleashedApp.Tests.TrainingTests
{
    /// <summary>
    /// Summary description for TrainingUnitTests
    /// </summary>
    [TestFixture]
    public class TrainingUnitTests
    {
        private Mock<ITrainingRepository> _trainingRepoMock;
        private TrainingBuilder _trainingBuilder;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
        }

        [TearDown]
        public void TearDown()
        {
            _trainingRepoMock = null;
            _trainingBuilder = null;
        }

        [Test]
        public void ConstructorShouldInitTrainingList()
        {
            _trainingRepoMock.Setup(x => x.GetAll()).Returns(_trainingBuilder.InitList(0));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object);

            Assert.AreNotEqual(null, trainingViewModel.TrainingList);
        }

        [Test]
        public void ConstructorShouldCalculateTotal()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(100));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object);

            Assert.AreEqual(_trainingBuilder.ReturnTotal(), trainingViewModel.TrainingTotal);
        }

        [Test]
        public void ConstructorShouldInitCommands()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object);

            Assert.IsNotNull(trainingViewModel.AddTrainingCommand);
        }

        [Test]
        public void RefreshShouldMaintainListLengthIfNothingChanged()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object);
            var firstList = trainingViewModel.TrainingList;

            trainingViewModel.Refresh();
            var secondList = trainingViewModel.TrainingList;

            Assert.AreEqual(firstList.Count, secondList.Count);
        }

        [Test]
        public void RefreshShouldReturnBiggerList()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object);
            var firstCount = trainingViewModel.TrainingList.Count;

            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(2));

            trainingViewModel.Refresh();
            var secondCount = trainingViewModel.TrainingList.Count;

            Assert.AreNotEqual(firstCount, secondCount);
            Assert.IsTrue(firstCount < secondCount);
        }

        [Test]
        public void SendInvoiceShouldConvertToYesOrNo()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object)
            {
                SendInvoice = true
            };
            Assert.AreEqual("Yes", trainingViewModel.GetSendInvoice());

            trainingViewModel.SendInvoice = false;
            Assert.AreEqual("No", trainingViewModel.GetSendInvoice());
        }

        [Test]
        public void SendInvoiceShouldReturnCorrectBoolean()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object);

            trainingViewModel.SetSendInvoice("Yes");
            Assert.IsTrue(trainingViewModel.SendInvoice);

            trainingViewModel.SetSendInvoice("No");
            Assert.IsFalse(trainingViewModel.SendInvoice);
        }

        [Test]
        public void VerifyFormShouldReturnTrueIfAllParamatersAreTrue()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object)
            {
                IsCityValid = true,
                IsCompanyValid = true,
                IsCostValid = true,
                IsDaysValid = true,
                IsEventValid = true,
            };

            trainingViewModel.VerifyForm();

            Assert.IsTrue(trainingViewModel.IsValid);
        }

        [Test]
        public void VerifyFormShouldReturnFalseIfAParamaterIsFalse()
        {
            _trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(_trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(_trainingRepoMock.Object)
            {
                IsCityValid = true,
                IsCompanyValid = false,
                IsCostValid = true,
                IsDaysValid = true,
                IsEventValid = true,
            };

            trainingViewModel.VerifyForm();

            Assert.IsFalse(trainingViewModel.IsValid);
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
