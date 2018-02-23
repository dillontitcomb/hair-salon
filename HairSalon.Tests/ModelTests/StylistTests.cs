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
      Stylist.DeleteAll();
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
  }
}
