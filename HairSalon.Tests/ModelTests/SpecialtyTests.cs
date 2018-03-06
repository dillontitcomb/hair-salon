using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
			Stylist.DeleteAll();
      Specialty.DeleteAll();
    }
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=dillon_titcomb_test;";
    }
		[TestMethod]
	 public void GetAll_DataBaseEmptyAtFirst_0()
	 {
		 //Arrange, Act
		 int result = Stylist.GetAll().Count;

		 //Assert
		 Assert.AreEqual(0, result);
	 }
	 	[TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Specialty testSpecialty = new Specialty ("Perms", 0);

      //Act
      testSpecialty.Save();
      Specialty savedSpecialty = Specialty.GetAll()[0];

      int result = savedSpecialty.GetId();
      int testId = testSpecialty.GetId();
      //Assert
      Assert.AreEqual(testId, result);
    }
		[TestMethod]
		public void Find_FindsSpecialtyInDatabase_Specialty()
		{
		  //Arrange
		  Specialty testSpecialty = new Specialty("James");
		  testSpecialty.Save();

		  //Act
		  Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

		  //Assert
		  Assert.AreEqual(testSpecialty, foundSpecialty);
		}
    [TestMethod]
    public void Delete_DeletesSpecialtyAssociations_SpecialtyList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Tim");
      testStylist.Save();

      Specialty testSpecialty = new Specialty("James");
      testSpecialty.Save();

      //Act
      testSpecialty.AddStylist(testStylist);
      testSpecialty.Delete();

      List<Specialty> resultStylistSpecialties = testStylist.GetSpecialties();
      List<Specialty> testStylistSpecialties = new List<Specialty> {};

      //Assert
      CollectionAssert.AreEqual(resultStylistSpecialties, testStylistSpecialties);
    }
    [TestMethod]
    public void Test_AddStylist_AddsStylistToSpecialty()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Bob");
      testSpecialty.Save();

      Stylist testStylist = new Stylist("Tom");
      testStylist.Save();

      Stylist testStylist2 = new Stylist("Tommy");
      testStylist2.Save();

      //Act
      testSpecialty.AddStylist(testStylist);
      testSpecialty.AddStylist(testStylist2);

      List<Stylist> result = testSpecialty.GetStylists();
      List<Stylist> testList = new List<Stylist>{testStylist, testStylist2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetStylists_ReturnsAllSpecialtyStylists_StylistList()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Bob");
      testSpecialty.Save();

      Stylist testStylist1 = new Stylist("Tom");
      testStylist1.Save();

      Stylist testStylist2 = new Stylist("Tommy");
      testStylist2.Save();

      //Act
      testSpecialty.AddStylist(testStylist1);
      List<Stylist> savedStylists = testSpecialty.GetStylists();
      List<Stylist> testList = new List<Stylist> {testStylist1};

      //Assert
      CollectionAssert.AreEqual(testList, savedStylists);
    }
  }
}
