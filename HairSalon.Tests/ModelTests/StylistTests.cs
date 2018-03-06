using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
			Stylist.DeleteAll();
      Specialty.DeleteAll();
    }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dillon_titcomb_test;";
    }
		[TestMethod]
	 public void GetAll_DataBaseEmptyAtFirst_0()
	 {
		 //Arrange, Act
		 int result = Stylist.GetAll().Count;
		 Console.WriteLine("This is the number of stylists in DB: " + result);

		 //Assert
		 Assert.AreEqual(0, result);
	 }
	 	[TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jennifer",0);

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();
      Console.WriteLine("ID from DB: " + result);
      Console.WriteLine("local ID: " + testId);
      //Assert
      Assert.AreEqual(testId, result);
    }
		[TestMethod]
		public void Find_FindsStylistInDatabase_Stylist()
		{
		  //Arrange
		  Stylist testStylist = new Stylist("James");
		  testStylist.Save();

		  //Act
		  Stylist foundStylist = Stylist.Find(testStylist.GetId());

		  //Assert
		  Assert.AreEqual(testStylist, foundStylist);
		}
    [TestMethod]
    public void Delete_DeletesClientAssociations_ClientList()
    {
      //Arrange
      Client testClient = new Client("Tim");
      testClient.Save();

      Stylist testStylist = new Stylist("James");
      testStylist.Save();

      //Act
      testStylist.AddClient(testClient);
      testStylist.Delete();

      List<Stylist> resultClientStylists = testClient.GetStylists();
      List<Stylist> testClientStylists = new List<Stylist> {};

      //Assert
      CollectionAssert.AreEqual(resultClientStylists, testClientStylists);
    }

    [TestMethod]
    public void Test_AddClient_AddsClientToStylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Bob");
      testStylist.Save();

      Client testClient = new Client("Tom");
      testClient.Save();

      Client testClient2 = new Client("Tommy");
      testClient2.Save();

      //Act
      testStylist.AddClient(testClient);
      testStylist.AddClient(testClient2);

      List<Client> result = testStylist.GetClients();
      List<Client> testList = new List<Client>{testClient, testClient2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetClients_ReturnsAllStylistClients_ClientList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Bob");
      testStylist.Save();

      Client testClient1 = new Client("Tom");
      testClient1.Save();

      Client testClient2 = new Client("Tommy");
      testClient2.Save();

      //Act
      testStylist.AddClient(testClient1);
      List<Client> savedClients = testStylist.GetClients();
      List<Client> testList = new List<Client> {testClient1};

      //Assert
      CollectionAssert.AreEqual(testList, savedClients);
    }
  }
}
