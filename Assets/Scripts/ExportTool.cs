#if (UNITY_EDITOR_OSX) 

using UnityEditor;
using System.Collections.Generic;

public class ExportTool
{
    static void ExportXcodeProject () 
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);

		EditorUserBuildSettings.symlinkLibraries = true;
		EditorUserBuildSettings.development = true;
		EditorUserBuildSettings.allowDebugging = true;

		List<string> scenes = new List<string>();
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) 
		{
			if (EditorBuildSettings.scenes [i].enabled)
			{
				scenes.Add (EditorBuildSettings.scenes [i].path);
			}
		}
		BuildPipeline.BuildPlayer (scenes.ToArray (), "../Build/Movementvania", BuildTarget.StandaloneOSX, BuildOptions.None);
	}
}
#endif