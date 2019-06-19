using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

/*
 * A class that dynamically generates menus for any number of objects
 * This makes it possible to have as many or as few objects as is required with no limit
 * This makes adding extra targets easy 
 */ 
public class GenerateOnePageMenu : MonoBehaviour {

    /* Declarations */
    private string groupName;
    private GameObject menuItemPrefab;
	private GameObject titleTextPrefab;
	
	private GameObject currentMenuItem;
	private Transform currentMenuItemTransform;
	private GameObject currentTitleText;
	private Transform currentTitleTextTransform;

	private float currentXPosModifier;
	private float currentZPos;

    private float newXPos;
	private float newZPos;

	private Vector3 wantedPosition;
	
	private List<MenuFoodItem> tgiFridayItems;
	private List<MenuFoodItem> diMaggiosItems;
	private List<MenuFoodItem> selectedMenuItems;

	void Start () {
        // Get Group name (NEEDS DELETED IN FINAL VERSION)
	    groupName = SceneManager.GetActiveScene().name;
		groupName = groupName.Substring(0, groupName.Length-5);

		// Fake Data
        tgiFridayItems = new List<MenuFoodItem>();
        tgiFridayItems.Add(new MenuFoodItem(1,"Mushroom Poori",6.50, "Starter", "Mushroom Poori in a sweet sauce"));
        tgiFridayItems.Add(new MenuFoodItem(2,"Garlic Bread",5.00, "Starter", "Bread and garlic butter"));		
        tgiFridayItems.Add(new MenuFoodItem(3,"Satay Skewer",5.25, "Starter", "Chicken Stabbed on a peanut stick"));
		tgiFridayItems.Add(new MenuFoodItem(4,"Burger",8.50, "Main", "BRGR classic"));
		tgiFridayItems.Add(new MenuFoodItem(5,"Duck Waffle",9.95, "Main", "The original Duffle"));
		tgiFridayItems.Add(new MenuFoodItem(6,"Ice Cream",4.00, "Dessert", "What flavours? ICE CREAM FLAVOUR"));
		tgiFridayItems.Add(new MenuFoodItem(7,"Brownie",6.50, "Dessert", "Home made with a special spice"));
		tgiFridayItems.Add(new MenuFoodItem(8,"Chicken Poori",7.75, "Main", "Chicken Poori in a sweet sauce"));
		tgiFridayItems.Add(new MenuFoodItem(9,"Chow Mein",8.00, "Main", "Noodles"));
		tgiFridayItems.Add(new MenuFoodItem(10,"Saffron Meringue",8.00, "Dessert", "Fancy Meringue"));

		diMaggiosItems = new List<MenuFoodItem>();
        diMaggiosItems.Add(new MenuFoodItem(1,"Mushroom Poori",6.50, "Starter", "Mushroom Poori in a sweet sauce"));
        diMaggiosItems.Add(new MenuFoodItem(2,"Garlic Bread",5.00, "Starter", "Bread and garlic butter"));		
        diMaggiosItems.Add(new MenuFoodItem(3,"Satay Skewer",5.25, "Starter", "Chicken Stabbed on a peanut stick"));
		diMaggiosItems.Add(new MenuFoodItem(4,"Burger",8.50, "Main", "BRGR classic"));
		diMaggiosItems.Add(new MenuFoodItem(6,"Ice Cream",4.00, "Dessert", "What flavours? ICE CREAM FLAVOUR"));
		diMaggiosItems.Add(new MenuFoodItem(7,"Brownie",6.50, "Dessert", "Home made with a special spice"));
		diMaggiosItems.Add(new MenuFoodItem(8,"Chicken Poori",7.75, "Main", "Chicken Poori in a sweet sauce"));
		diMaggiosItems.Add(new MenuFoodItem(9,"Spaghetti Bolognese",8.00, "Main", "Noodles"));
		diMaggiosItems.Add(new MenuFoodItem(10,"Saffron Meringue",8.00, "Dessert", "Fancy Meringue"));
		
		string[] categories = new string[] {
			"Starter",
			"Main",
			"Dessert"
		};
	}


    // IN FINAL VERSION THIS WILL BE GENERIC USING LIST GAINED FROM API!
	private List<MenuFoodItem> SelectAllFoodItemsInTGICategory(string category){
		return tgiFridayItems.Where(x => x.Category == category).ToList();
	}     

	private List<MenuFoodItem> SelectAllFoodItemsInDiMaggiosCategory(string category){
		return diMaggiosItems.Where(x => x.Category == category).ToList();
	}    

		
}
