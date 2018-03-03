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
