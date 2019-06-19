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
public class GenerateMenu : MonoBehaviour {

    /* Declarations */
	public bool multiplePagesEnabled;
    private string groupName;
    private GameObject menuItemPrefab;
	private GameObject titleTextPrefab;
	private GameObject contentPanelPrefab;
	
	private GameObject currentMenuItem;
	private Transform currentMenuItemTransform;
	private GameObject currentTitleText;
	private Transform currentTitleTextTransform;

	
	private GameObject currentContentPanel;
	private Transform currentContentPanelTransform;

	private float currentXPosModifier;
	private float currentZPos;

    private float newXPos;
	private float newZPos;

	private Vector3 wantedPosition;
	
	private List<MenuFoodItem> tgiFridayItems;
	private List<MenuFoodItem> diMaggiosItems;
	private List<MenuFoodItem> selectedMenuItems;
	public List<MenuFoodItem> providerMenuItems;
	private string[] categories;

	void Start () {
        
		multiplePagesEnabled = true;
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
		
		categories = new string[] {
			"Starter",
			"Main",
			"Dessert"
		};
	
        if(multiplePagesEnabled){
			GenerateMultiplePages();
		} else {
			GenerateOnePage();
		}

	}


    // IN FINAL VERSION THIS WILL BE GENERIC USING LIST GAINED FROM API!
	private List<MenuFoodItem> SelectAllFoodItemsInTGICategory(string category){
		return tgiFridayItems.Where(x => x.Category == category).ToList();
	}     

	private List<MenuFoodItem> SelectAllFoodItemsInDiMaggiosCategory(string category){
		return diMaggiosItems.Where(x => x.Category == category).ToList();
	}    

    private void GenerateOnePage(){
		// Get Prefabs
		menuItemPrefab = (GameObject)Resources.Load("Prefabs/MenuItemPanel", typeof(GameObject));
		titleTextPrefab = (GameObject)Resources.Load("Prefabs/TitleTextPanel", typeof(GameObject));
		
		/* Get the size of the Content Panel*/
		GameObject content = GameObject.Find("ScrollingMenuViewPort");
		Vector3 panelSize = content.GetComponent<Renderer>().bounds.size;
        Debug.Log(panelSize);

		/* Set initial Position values */
        currentZPos = (panelSize.z/2 - (float)0.05);
		currentXPosModifier = (panelSize.x/4);
		// Select Each Category
        for( int categoryNumber = 0; categoryNumber < categories.Length; categoryNumber++){
			// How many dishes in this category, currently hard coded! 
			if(groupName == "TGIFridays"){
				providerMenuItems = tgiFridayItems;
			    selectedMenuItems = SelectAllFoodItemsInTGICategory(categories[categoryNumber]);     
			} else {
				providerMenuItems = diMaggiosItems;
                selectedMenuItems = SelectAllFoodItemsInDiMaggiosCategory(categories[categoryNumber]);  
			}
			// Create A New Title
			currentTitleText = Instantiate(titleTextPrefab,this.gameObject.transform);
		    // Name It
		    currentTitleText.name = "Category-" + categoryNumber.ToString();
			// Get The Transform
		    currentTitleTextTransform = currentTitleText.GetComponent<Transform>();
	    	// Ensure Correct Rotation
	    	currentTitleTextTransform.Rotate(0,180,0);
    	    // Scale Appropriately
	        currentTitleTextTransform.localScale = new Vector3((float)0.4,currentTitleTextTransform.localScale.y,(float)0.1);

			// Calculate New Position
		    newZPos = (currentTitleTextTransform.position.z + currentZPos);
	        wantedPosition = new Vector3(currentTitleTextTransform.position.x,currentTitleTextTransform.position.y,newZPos);

	        // Assign New Position
            currentTitleTextTransform.SetPositionAndRotation(wantedPosition, currentTitleTextTransform.rotation);

			// Update current Z position
		    currentZPos = currentZPos - (currentTitleText.GetComponent<Renderer>().bounds.size.z + (float)0.1);

            // Update Text Value
			currentTitleText.GetComponentInChildren<TextMesh>().text = categories[categoryNumber];

			// Add Menu Items For Category
	        for(int itemNumber = 0; itemNumber < selectedMenuItems.Count; itemNumber++){
		        // Create a menu Item
		        currentMenuItem = Instantiate(menuItemPrefab,this.gameObject.transform);
		        // Name It
		        currentMenuItem.name = "Item-" + selectedMenuItems[itemNumber].Name;
		        // Get The Transform
		        currentMenuItemTransform = currentMenuItem.GetComponent<Transform>();
	    	    // Ensure Correct Rotation
	    	    currentMenuItemTransform.Rotate(0,180,0);
    	    	// Scale Appropriately
	        	currentMenuItemTransform.localScale = new Vector3((float)0.4,currentMenuItemTransform.localScale.y,(float)0.1);

	        	// Calculate New Position
		    	newZPos = (currentMenuItemTransform.position.z + currentZPos);

		    	if(itemNumber%2 == 1){
                    newXPos = (currentMenuItemTransform.position.x + currentXPosModifier );
		      		currentZPos = currentZPos - (currentMenuItem.GetComponent<Renderer>().bounds.size.z + (float)0.01);
		    	} else {
		    		newXPos = (currentMenuItemTransform.position.x - currentXPosModifier );
					if (itemNumber == (selectedMenuItems.Count - 1)){
                        currentZPos = currentZPos - (currentMenuItem.GetComponent<Renderer>().bounds.size.z + (float)0.02);
					}
		    	}
	        	wantedPosition = new Vector3(newXPos,(currentMenuItemTransform.position.y),newZPos);
	        	// Assign New Position
               currentMenuItemTransform.SetPositionAndRotation(wantedPosition, currentMenuItemTransform.rotation);

			   currentMenuItemTransform.Find("MenuItemNameText").GetComponent<TextMesh>().text = selectedMenuItems[itemNumber].Name;
			   currentMenuItemTransform.Find("MenuItemCostText").GetComponent<TextMesh>().text = "£" + selectedMenuItems[itemNumber].Cost.ToString();
    		}
		}
	}
    private void GenerateMultiplePages(){
			
		// Get Prefabs
		menuItemPrefab = (GameObject)Resources.Load("Prefabs/TabbedMenuItemPanel", typeof(GameObject));
		titleTextPrefab = (GameObject)Resources.Load("Prefabs/TitleTextPanel", typeof(GameObject));
		contentPanelPrefab = (GameObject)Resources.Load("Prefabs/ContentPanel", typeof(GameObject));
		
		/* Get the size of the Content Panel*/
		GameObject content = GameObject.Find("ScrollingMenuViewPort");
		Vector3 panelSize = content.GetComponent<Renderer>().bounds.size;

		// Select Each Category
        for( int categoryNumber = 0; categoryNumber < categories.Length; categoryNumber++){
			// How many dishes in this category, currently hard coded! 
			if(groupName == "TGIFridays"){
				providerMenuItems = tgiFridayItems;
			    selectedMenuItems = SelectAllFoodItemsInTGICategory(categories[categoryNumber]);     
			} else {
				providerMenuItems = diMaggiosItems;
                selectedMenuItems = SelectAllFoodItemsInDiMaggiosCategory(categories[categoryNumber]);  
			}

			/* Set initial Position values */
            currentZPos = (panelSize.z/2 - (float)0.05);
		    currentXPosModifier = (panelSize.x/4);

			currentContentPanel = Instantiate(contentPanelPrefab, this.gameObject.transform);
			currentContentPanelTransform = currentContentPanel.GetComponent<Transform>();
			currentContentPanel.name = "CategoryPanel-" + categoryNumber.ToString();
			// Create A New Title
			currentTitleText = Instantiate(titleTextPrefab,currentContentPanelTransform);
		    // Name It
		    currentTitleText.name = "Category-" + categoryNumber.ToString();
			// Get The Transform
		    currentTitleTextTransform = currentTitleText.GetComponent<Transform>();
	    	// Ensure Correct Rotation
	    	currentTitleTextTransform.Rotate(0,180,0);
    	    // Scale Appropriately
	        currentTitleTextTransform.localScale = new Vector3((float)0.4,currentTitleTextTransform.localScale.y,(float)0.1);

			// Calculate New Position
		    newZPos = (currentTitleTextTransform.position.z + currentZPos);

			wantedPosition = new Vector3(currentTitleTextTransform.position.x,(currentTitleTextTransform.position.y + (float)0.01),newZPos);

			if(categoryNumber == 0){
				currentContentPanel.SetActive(true);
			} else {
				currentContentPanel.SetActive(false);
			}
	        // Assign New Position
            currentTitleTextTransform.SetPositionAndRotation(wantedPosition, currentTitleTextTransform.rotation);

			// Update current Z position
		    currentZPos = currentZPos - (currentTitleText.GetComponent<Renderer>().bounds.size.z + (float)0.1);

            // Update Text Value
			currentTitleText.GetComponentInChildren<TextMesh>().text = categories[categoryNumber];

			// Add Menu Items For Category
	        for(int itemNumber = 0; itemNumber < selectedMenuItems.Count; itemNumber++){
		        // Create a menu Item
		        currentMenuItem = Instantiate(menuItemPrefab,currentContentPanelTransform);
		        // Name It
		        currentMenuItem.name = "Item-" + selectedMenuItems[itemNumber].Name;
		        // Get The Transform
		        currentMenuItemTransform = currentMenuItem.GetComponent<Transform>();
	    	    // Ensure Correct Rotation
	    	    currentMenuItemTransform.Rotate(0,180,0);
    	    	// Scale Appropriately
	        	currentMenuItemTransform.localScale = new Vector3((float)0.4,currentMenuItemTransform.localScale.y,(float)0.1);

	        	// Calculate New Position
		    	newZPos = (currentMenuItemTransform.position.z + currentZPos);

                currentZPos = currentZPos - (currentMenuItem.GetComponent<Renderer>().bounds.size.z + (float)0.02);
	
	        	wantedPosition = new Vector3(currentMenuItemTransform.position.x,(currentMenuItemTransform.position.y),newZPos);
	        	// Assign New Position
               currentMenuItemTransform.SetPositionAndRotation(wantedPosition, currentMenuItemTransform.rotation);

			   currentMenuItemTransform.Find("MenuItemNameText").GetComponent<TextMesh>().text = selectedMenuItems[itemNumber].Name;
			   currentMenuItemTransform.Find("MenuItemCostText").GetComponent<TextMesh>().text = "£" + selectedMenuItems[itemNumber].Cost.ToString();
    		}

		}
	}
		
}
