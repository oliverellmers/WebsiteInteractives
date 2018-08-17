using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetHeapSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerSettings.WebGL.emscriptenArgs = "-s WASM_MEM_MAX=512MB";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
