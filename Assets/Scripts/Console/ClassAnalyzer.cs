using System;
using System.Linq;
using Assets.Scripts.Console;
using UnityEngine;

public class ClassAnalyzer : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    PrintPublicMethods(typeof(DialogEventManagement));   
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void PrintPublicMethods(Type t)
    {
        foreach (var methodInfo in t.GetMethods())
        {
            if (methodInfo.GetCustomAttributes(true).Any(o => o.GetType() == typeof(ConsoleCommandAttribute)))
            {
                print(methodInfo.Name);
                foreach (var parameterInfo in methodInfo.GetParameters())
                {
                    print(parameterInfo.Name);
                }
            }
        }
    }
}
