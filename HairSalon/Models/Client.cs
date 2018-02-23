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
      _id = id;
			_name = name;
			_stylistId = stylistId;
    }
  }
}
