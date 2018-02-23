using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests
  {
    // public void Dispose()
    // {
    //   Stylist.DeleteAll();
    // }
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
  }
}
