using UnityEngine;
using System.Collections;
using UnityEditor;
using Fungus;
using System;
using System.Reflection;

[CustomEditor(typeof(GenerateDialogSound))]
public class GenerateDialogSoundButton : Editor
{
	private string outputFolder = "/Users/benikm91/Documents/FHNW/Semester6/sdent/Project/Sdent-Game/sdentGame/Assets/Sound/Dialog";

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("Generate Dialog Sound (for selected blocks)"))
		{
			var go = Selection.activeGameObject;
			var flowchart = go.GetComponentInChildren<Flowchart> ();
			var blocks = flowchart.SelectedBlocks;
			blocks.ForEach (GenerateSoundFiles);
		}
	}

	private void GenerateSoundFiles(Block block)
	{
		Debug.Log ("Starting Block " + block.BlockName);
		var name = block.BlockName;
		var counter = 0;
		foreach (var command in block.CommandList) 
		{
			if (command is Say)
				GenerateSoundFile (command as Say, name, counter++);
			else
				GenerateSoundFile (command as Command);
		}
	}

	private void GenerateSoundFile(Command command) 
	{
		Debug.Log ("Skipping " + command);
	}

	private void GenerateSoundFile(Say sayCommand, string name, int counter) 
	{
		if (sayCommand._Character != null)
			Debug.Log (sayCommand._Character.name);
		if (sayCommand._Character != null && sayCommand._Character.GetComponent<Character>().NameText == "SAIwA") 
		{	
			var filename = String.Format("{0}{1}", name, counter);
			var filepath = String.Format ("{0}/{1}", outputFolder, filename);
			var storyText = sayCommand.GetStandardText ();

			var proc = new System.Diagnostics.Process ();
			proc.StartInfo.FileName = "/bin/bash";

			var sayBashCommand = String.Format("say {0} -o {1}", storyText, filepath+".aiff");

			proc.StartInfo.Arguments = String.Format("-c \"{0} \"", sayBashCommand);
			proc.StartInfo.UseShellExecute = true; 
			proc.StartInfo.RedirectStandardOutput = false;
			proc.Start ();

			proc.WaitForExit ();

			Type type = sayCommand.GetType();
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

			FieldInfo finfo = type.GetField("voiceOverClip", bindingFlags);

			var audio = (AudioClip) AssetDatabase.LoadAssetAtPath("Assets/Sound/Dialog/"+filename+".aiff", typeof(AudioClip));

			finfo.SetValue(sayCommand, audio);

			Debug.Log ("Generated " + name+counter + " ("+audio+")");
		}
	}

}