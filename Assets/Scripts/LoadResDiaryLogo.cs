using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/*
 * Load Resdiary Logo
 */ 
public class LoadResDiaryLogo : MonoBehaviour {
	void Start () {
        // Load Image, Make Material, Apply material
		Texture  texture = Resources.Load("Images/Logos/Resdiary") as Texture; //No need to specify extension.
        this.gameObject.GetComponent<Renderer>().material.mainTexture = texture;
	}		
}
