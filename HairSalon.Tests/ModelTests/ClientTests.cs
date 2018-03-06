using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
			Stylist.DeleteAll();
      Specialty.DeleteAll();
    }
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dillon_titcomb_test;";
    }
		[TestMethod]
	 	public void GetAll_DataBaseEmptyAtFirst_0()
	 	{
			//Arrange, Act
		 int result = Client.GetAll().Count;
		 Console.WriteLine("This is the number of clients in DB: " + result);

		  //Assert
		  Assert.AreEqual(0, result);
	 }
	 	[TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("Jennifer", 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();
      Console.WriteLine("ID from DB: " + result);
      Console.WriteLine("local ID: " + testId);
      //Assert
      Assert.AreEqual(testId, result);
    }
		[TestMethod]
		public void Find_FindsClientInDatabase_Client()
		{
		  //Arrange
		  Client testClient = new Client("James", 1);
		  testClient.Save();

		  //Act
		  Client foundClient = Client.Find(testClient.GetId());

		  //Assert
		  Assert.AreEqual(testClient, foundClient);
		}
    [TestMethod]
    public void Delete_DeletesClientAssociations_ClientList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Tim");
      testStylist.Save();

      Client testClient = new Client("James");
      testClient.Save();

      //Act
      testClient.AddStylist(testStylist);
      testClient.Delete();

      List<Client> resultStylistClients = testStylist.GetClients();
      List<Client> testStylistClients = new List<Client> {};

      //Assert
      CollectionAssert.AreEqual(resultStylistClients, testStylistClients);
    }
    [TestMethod]
    public void Test_AddStylist_AddsStylistToClient()
    {
      //Arrange
      Client testClient = new Client("Bob");
      testClient.Save();

      Stylist testStylist = new Stylist("Tom");
      testStylist.Save();

      Stylist testStylist2 = new Stylist("Tommy");
      testStylist2.Save();

      //Act
      testClient.AddStylist(testStylist);
      testClient.AddStylist(testStylist2);

      List<Stylist> result = testClient.GetStylists();
      List<Stylist> testList = new List<Stylist>{testStylist, testStylist2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetStylists_ReturnsAllClientStylists_StylistList()
    {
      //Arrange
      Client testClient = new Client("Bob");
      testClient.Save();

      Stylist testStylist1 = new Stylist("Tom");
      testStylist1.Save();

      Stylist testStylist2 = new Stylist("Tommy");
      testStylist2.Save();

      //Act
      testClient.AddStylist(testStylist1);
      List<Stylist> savedStylists = testClient.GetStylists();
      List<Stylist> testList = new List<Stylist> {testStylist1};

      //Assert
      CollectionAssert.AreEqual(testList, savedStylists);
    }
  }
}
