using UnityEngine;
using System.Collections;

public class FixView : MonoBehaviour {


	void Start()
	{
		Screen.SetResolution ((int)Screen.width, (int)Screen.height,true);
	}
}