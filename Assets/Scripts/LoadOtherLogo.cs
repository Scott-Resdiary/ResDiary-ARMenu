using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/*
 * A class that dynamically generates menus for any number of objects
 * This makes it possible to have as many or as few objects as is required with no limit
 * This makes adding extra targets easy 
 */ 
public class LoadOtherLogo : MonoBehaviour {

    /* Declarations */
    private string groupName;

	void Start () {
	    groupName = SceneManager.GetActiveScene().name;
		groupName = groupName.Substring(0, groupName.Length-5);

        Debug.Log(groupName);
        // Load Image, Make Material, Apply material
		Texture  texture = Resources.Load(("Images/Logos/" + groupName)) as Texture; //No need to specify extension.
        this.gameObject.GetComponent<Renderer>().material.mainTexture = texture;
    }
}