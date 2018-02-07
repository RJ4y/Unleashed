using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnleashedApp.ViewModels;
using UnleashedApp.Repositories.TrainingRepository;
using Moq;
using System.Collections.ObjectModel;
using UnleashedApp.Models;

namespace UnleashedApp.Tests.TrainingTests
{
    /// <summary>
    /// Summary description for TrainingUnitTests
    /// </summary>
    [TestClass]
    public class TrainingUnitTests
    {
        private Mock<ITrainingRepository> trainingRepoMock;
        private TrainingBuilder trainingBuilder;
        private Random random;

        public TrainingUnitTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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

        [TestInitialize]
        public void Setup()
        {
            trainingRepoMock = new Mock<ITrainingRepository>();
            trainingBuilder = new TrainingBuilder();
            random = new Random();
        }

        [TestCleanup]
        public void TearDown()
        {
            trainingRepoMock = null;
            trainingBuilder = null;
            random = null;
        }

        [TestMethod]
        public void ConstructorShouldInitTrainingList()
        {
            trainingRepoMock.Setup(x => x.GetAll()).Returns(trainingBuilder.InitList(0));

            var trainingViewModel = new TrainingViewModel(trainingRepoMock.Object);

            Assert.AreNotEqual(null, trainingViewModel.TrainingList);
        }

        [TestMethod]
        public void ConstructorShouldCalculateTotal()
        {
            trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(trainingBuilder.InitList(100));

            var trainingViewModel = new TrainingViewModel(trainingRepoMock.Object);

            Assert.AreEqual(trainingBuilder.ReturnTotal(), trainingViewModel.TrainingTotal);
        }

        [TestMethod]
        public void RefreshShouldMaintainListIfNothingChanged()
        {
            trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(trainingRepoMock.Object);
            var firstList = trainingViewModel.TrainingList;

            trainingViewModel.Refresh();
            var secondList = trainingViewModel.TrainingList;

            Assert.AreEqual(firstList, secondList);
        }

        [TestMethod]
        public void RefreshShouldReturnBiggerList()
        {
            trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(trainingRepoMock.Object);
            var firstCount = trainingViewModel.TrainingList.Count;

            trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(trainingBuilder.InitList(2));

            trainingViewModel.Refresh();
            var secondCount = trainingViewModel.TrainingList.Count;

            Assert.AreNotEqual(firstCount, secondCount);
            Assert.IsTrue(firstCount < secondCount);
        }

        [TestMethod]
        public void SendInvoiceShouldConvertToYesOrNo()
        {
            trainingRepoMock.Setup(trainingList => trainingList.GetAll()).Returns(trainingBuilder.InitList(1));

            var trainingViewModel = new TrainingViewModel(trainingRepoMock.Object);

            trainingViewModel.SendInvoice = true;
            Assert.AreEqual("Ja", trainingViewModel.getSendInvoice());

            trainingViewModel.SendInvoice = false;
            Assert.AreEqual("Nee", trainingViewModel.getSendInvoice());
        }
    }

    public class TrainingBuilder
    {
        private Random random;
        private int total;

        public TrainingBuilder() { }

        public List<Training> InitList(int count)
        {
            var list = new List<Training>();
            random = new Random();
            total = 0;

            for(int i = 0; i < count; i++)
            {
                var item = new Training();
                item.First_Name = "FirstName";
                item.Last_Name = "LastName";
                item.City = "Neerpelt";
                item.Company = "Company";
                item.Date = new DateTime();
                item.Days = 3;
                item.Info = "info";
                item.Invoice = "Nee";
                item.Team = "Unleashed";
                item.TrainingName = "Training";

                item.Cost = random.Next(100000);
                total += item.Cost;

                list.Add(item);
            }

            return list;
        }

        public int ReturnTotal()
        {
            return total;
        }
    }
}
