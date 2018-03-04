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

    public Client(string name, int id = 0)
    {
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
        bool clientEquality = (this.GetName() == newClient.GetName());
        return (idEquality && clientEquality);
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
				int id = rdr.GetInt32(0);

        Client newClient = new Client(name, id);
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
			cmd.CommandText = @"INSERT INTO clients (name) VALUES (@clientName);";

			MySqlParameter name = new MySqlParameter();
			name.ParameterName = "@clientName";
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
			string clientName = "";

			while (rdr.Read())
			{
			  clientId = rdr.GetInt32(0);
			  clientName = rdr.GetString(1);
			}
			Client foundClient = new Client(clientName, clientId);
			conn.Close();
			if (conn != null)
			{
			  conn.Dispose();
			}
			return foundClient;
    }
    public void AddStylist(Stylist chosenStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_clients (stylist_id, client_id) VALUES (@stylistId, @clientId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@stylistId";
      stylist_id.Value = chosenStylist.GetId();
      cmd.Parameters.Add(stylist_id);

      MySqlParameter client_id = new MySqlParameter();
      client_id.ParameterName = "@clientId";
      client_id.Value = _id;
      cmd.Parameters.Add(client_id);

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
      cmd.CommandText = @"SELECT stylists.* FROM clients
          JOIN stylists_clients ON (clients.id = stylists_clients.client_id)
          JOIN stylists ON (stylists_clients.stylist_id = stylists.id)
          WHERE clients.id = @ClientId;";

      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
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
