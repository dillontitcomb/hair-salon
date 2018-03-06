using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _name;

    public Specialty(string name, int id = 0)
    {
			_name = name;
			_id = id;
    }
		public override bool Equals(System.Object otherSpecialty)
    {
	    if (!(otherSpecialty is Specialty))
	    {
	    return false;
	    }
	    else
	    {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());
        bool specialtyEquality = (this.GetName() == newSpecialty.GetName());
        return (idEquality && specialtyEquality);
	    }
    }
		public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

		public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties ORDER BY name ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
				int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Specialty newSpecialty = new Specialty(name, id);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }
		public void Save()
	 	{
			MySqlConnection conn = DB.Connection();
			conn.Open();

			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@specialtyName);";

			MySqlParameter name = new MySqlParameter();
			name.ParameterName = "@specialtyName";
			name.Value = this._name;
			cmd.Parameters.Add(name);

			cmd.ExecuteNonQuery();
			_id = (int) cmd.LastInsertedId;

			conn.Close();
			if (conn != null)
			{
			conn.Dispose();
			}
	 	}
		public static Specialty Find(int id)
    {
			MySqlConnection conn = DB.Connection();
			conn.Open();

			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM specialties WHERE id = @thisId;";

			MySqlParameter thisId = new MySqlParameter();
			thisId.ParameterName = "@thisId";
			thisId.Value = id;
			cmd.Parameters.Add(thisId);

			var rdr = cmd.ExecuteReader() as MySqlDataReader;

			int specialtyId = 0;
			string specialtyName = "";

			while (rdr.Read())
			{
			  specialtyId = rdr.GetInt32(0);
			  specialtyName = rdr.GetString(1);
			}
			Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);
			conn.Close();
			if (conn != null)
			{
			  conn.Dispose();
			}
			return foundSpecialty;
    }
    public void UpdateSpecialty(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@stylistId, @specialtyId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@stylistId";
      stylist_id.Value = newStylist.GetId();
      cmd.Parameters.Add(stylist_id);

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@specialtyId";
      specialty_id.Value = _id;
      cmd.Parameters.Add(specialty_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialties
          JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
          JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
          WHERE specialties.id = @SpecialtyId;";

      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@SpecialtyId";
      clientIdParameter.Value = _id;
      cmd.Parameters.Add(clientIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        stylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return stylists;
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = (@"DELETE FROM specialties WHERE id = @searchId; DELETE FROM stylists_specialties WHERE specialty_id = @searchId;");

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
		public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
