using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

namespace DremuGodot.Script.GamePlayer
{
	public partial class GameController : Node
	{
		public override void _Ready()
		{
			string chartPath = "res://Chart/TestJson.json";
			string jsonString = FileAccess.GetFileAsString(chartPath);
			Dictionary json = Json.ParseString(jsonString).AsGodotDictionary();
			
			GD.Print(json["Name"]);
		}
	}
}
