using UnityEngine;
using System.Collections;

public class Options
{
	public float sensitivity = 5;
	public float fov = 60;
	public bool showHelp = true;

	public int[] currentEquipmentIndeces = new int[] { 0, 0, 0, 1, 1, 1 };

	static Options options;
	public static Options main
	{
		get
		{
			if (options == null) Load();
			return options;
		}
	}

	static string OptionPath
	{
		get
		{
			return System.IO.Path.Combine(System.Environment.CurrentDirectory, "options.xml");
		}
	}

	static void Load()
	{
		System.IO.FileStream fs = null;

		try
		{
			fs = new System.IO.FileStream(OptionPath, System.IO.FileMode.Open);
		}
		catch (System.Exception ex)
		{
			Debug.Log("exception when tried to load options.xml: ");
			Debug.Log(ex.ToString());
			Debug.Log("creating options.xml");

			options = new Options();
			options.Save();
			fs = new System.IO.FileStream(OptionPath, System.IO.FileMode.Open);
		}
		finally
		{
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Options));
			options = serializer.Deserialize(fs) as Options;
			
			fs.Close();
		}
	}

	public void Save()
	{
		System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Options));

		//System.IO.FileStream fs = new System.IO.FileStream(OptionPath, System.IO.FileMode.Create);
		var sw = new System.IO.StreamWriter(OptionPath, false, System.Text.Encoding.UTF8);
		serializer.Serialize(sw, this);

		sw.Close();
	}

	public bool ShowGUI()
	{
		var itemHeight = 0.08f;
		var y = 0.1f;
		var labelRect = new Rect(0.1f, y, 0.2f, itemHeight);
		var sliderRect = new Rect(labelRect.xMax, y, 0.5f, itemHeight);
		var numberRect = new Rect(sliderRect.xMax + 0.05f, y, 0.05f, itemHeight);
		labelRect = GameGUI.ConvertRect(labelRect);
		sliderRect = GameGUI.ConvertRect(sliderRect);
		numberRect = GameGUI.ConvertRect(numberRect);

		GUI.Label(labelRect, "SENSITIVITY");
		options.sensitivity = GUI.HorizontalSlider(sliderRect, options.sensitivity, 0, 30);
		GUI.Label(numberRect, string.Format("{0:f1}", options.sensitivity));

		labelRect.y += labelRect.height;
		sliderRect.y += sliderRect.height;
		numberRect.y += numberRect.height;

		GUI.Label(labelRect, "FOV");
		options.fov = GUI.HorizontalSlider(sliderRect, options.fov, 60, 90);
		GUI.Label(numberRect, "" + (int)options.fov);

		if (GameGUI.BackButton())
		{
			Save();
			return true;
		}
		
		return false;
	}
}