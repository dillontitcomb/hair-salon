using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
		private int _stylistId;

    public Client(string name, int stylistId, int id = 0)
    {
			_stylistId = stylistId;
			_name = name;
			_id = id;
    }
		public override bool Equals(System.Object otherClient)
    {
    if (!(otherClient is Client))
    {
    return false;
    }
    else
    {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool stylistEquality = (this.GetName() == newClient.GetName());
        return (idEquality && stylistEquality);
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
		public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients ORDER BY name ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(1);
				int clientId = rdr.GetInt32(2);
				int id = rdr.GetInt32(0);

        Client newClient = new Client(name, clientId, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allClients;
    }
		public void Save()
	 	{
			MySqlConnection conn = DB.Connection();
			conn.Open();

			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@clientName, @stylistId);";

			MySqlParameter name = new MySqlParameter();
			name.ParameterName = "@clientName";
			name.Value = this._name;
			cmd.Parameters.Add(name);

			MySqlParameter stylistId = new MySqlParameter();
			stylistId.ParameterName = "@stylistId";
			stylistId.Value = this._stylistId;
			cmd.Parameters.Add(stylistId);

			cmd.ExecuteNonQuery();
			_id = (int) cmd.LastInsertedId;

			conn.Close();
			if (conn != null)
			{
			conn.Dispose();
			}
	 	}
		public static Client Find(int id)
    {
			MySqlConnection conn = DB.Connection();
			conn.Open();

			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";

			MySqlParameter thisId = new MySqlParameter();
			thisId.ParameterName = "@thisId";
			thisId.Value = id;
			cmd.Parameters.Add(thisId);

			var rdr = cmd.ExecuteReader() as MySqlDataReader;

			int clientId = 0;
			int stylistId = 0;
			string clientName = "";

			while (rdr.Read())
			{
			  clientId = rdr.GetInt32(0);
			  clientName = rdr.GetString(1);
				stylistId = rdr.GetInt32(2);
			}
			Client foundClient = new Client(clientName, stylistId, clientId);
			conn.Close();
			if (conn != null)
			{
			  conn.Dispose();
			}
			return foundClient;
    }
		public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
