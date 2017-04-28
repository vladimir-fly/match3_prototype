using System.Collections;
using System.Collections.Generic;

using UnityEngine;
	
namespace m3Prototype
{
	public class Application : MonoBehaviour 
	{
		[SerializeField] public Playground Playground;
	    [SerializeField] public int PlaygroundWidth;
	    [SerializeField] public int PlaygroundHeight;
		[SerializeField] public Camera Camera;

		void Start()
		{
            Playground.Init(PlaygroundWidth, PlaygroundHeight);
		}
	}
}
