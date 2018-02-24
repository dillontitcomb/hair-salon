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
		public void GetStylist_FindsAssignedStylist_Stylist()
		{
		  //Arrange
		 	Stylist testStylist = new Stylist("James");
			testStylist.Save();
			int matchingId = testStylist.GetId();
			Client testClient = new Client("John", matchingId);

			testClient.Save();

		  //Act
		  Stylist foundStylist = testClient.GetStylist(matchingId);

		  //Assert
		  Assert.AreEqual(testStylist.GetName(), foundStylist.GetName());
		}
  }
}
