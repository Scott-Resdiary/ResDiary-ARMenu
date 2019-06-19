using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to send out raycast when screen is touched, if it hits, then tells pin to load the zone menu
 * N.B. a raycast is an invisible beam used to detect collsion
 */ 
public class LineOfSight : MonoBehaviour {

     public Ray ray;
     public RaycastHit hit;

     private bool canClick;
     private float timer;
     public float clickDelay;


     private GameObject imageTarget;

     private GameObject oldPanel;
     private GameObject newPanel;
     private int panelTracker;
     private Transform panelManager;
     private int panelCount;

     private bool multiplePagesEnabled;
     private List<MenuFoodItem> providerMenuItems;
     private MenuFoodItem  clickedMenuItem;

     private GameObject itemDetailsPanel;
	/*
	 * Checks every frame if the screen has been tapped, if so, it raycasts
	 */

    void Start(){
       imageTarget = GameObject.Find("ImageTarget");
       panelTracker = 0;
       panelManager = GameObject.Find("ScrollingMenuViewPort").transform;
       panelCount = panelManager.childCount;
       canClick = true;
       timer = 0f;
       clickDelay = 0.5f;
       multiplePagesEnabled = GameObject.Find("ScrollingMenuViewPort").GetComponent<GenerateMenu>().multiplePagesEnabled;
       providerMenuItems = GameObject.Find("ScrollingMenuViewPort").GetComponent<GenerateMenu>().providerMenuItems;
       clickedMenuItem = null;
       if(!multiplePagesEnabled){
           GameObject.Find("NextButton").SetActive(false);
           GameObject.Find("PrevButton").SetActive(false);
       }
       itemDetailsPanel = GameObject.Find("ItemDetailsPanel");
       itemDetailsPanel.SetActive(false);
     }

	void Update()
	{
		/* If the screen has been tapped */
        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && canClick == true)
        {
            canClick = false;
            timer = clickDelay;
			/* if running it in Unity Engine, then there is no tap, simply a button click */
			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			}else { /* else it's a touch screen device not running inside Unity */
				ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			}
			
            RaycastHit hit;
			/* Since only Pin's have colliders if the ray hit it must have hit a pin */
            if (Physics.Raycast(ray, out hit, 1000)) {

                if(hit.collider != null){
                    if(hit.collider.name == "NextButton"){
                        NextButton();
                    } else if(hit.collider.name == "PrevButton"){
                        PrevButton();
                    } else if(hit.collider.name == "BackButton") {
                        itemDetailsPanel.SetActive(false);
                        panelManager.gameObject.SetActive(true);
                    } else {
                        TryLoadDescription(hit.collider.name.Remove(0,5));
                    }
                }
            }
        }

        timer-= Time.deltaTime;

        if(timer <= 0 && canClick == false){
            canClick = true;
        }
	} 

     private void NextButton(){
         oldPanel = panelManager.GetChild(panelTracker).gameObject;
         if(panelTracker == (panelCount - 1)){
             panelTracker = 0;
         } else {
             panelTracker++;
         }
         newPanel = panelManager.GetChild(panelTracker).gameObject;
         SwitchMenus(oldPanel,newPanel);
     }

     private void PrevButton(){
         oldPanel = panelManager.GetChild(panelTracker).gameObject;
         if(panelTracker == 0){
             panelTracker = (panelCount - 1);
         } else {
             panelTracker--;
         }
         newPanel = panelManager.GetChild(panelTracker).gameObject;
         SwitchMenus(oldPanel,newPanel);
     }

     private void SwitchMenus(GameObject oldPanel, GameObject newPanel){
         oldPanel.SetActive(false);
         newPanel.SetActive(true);
     }

     private void TryLoadDescription(string itemName){
         foreach(MenuFoodItem item in providerMenuItems){
             Debug.Log(itemName + " ---- " + item.Name);
             if(itemName == item.Name){
                 clickedMenuItem = item;
             }

             Debug.Log(clickedMenuItem);

             if(clickedMenuItem != null){
                 itemDetailsPanel.transform.GetChild(0).GetComponent<TextMesh>().text = clickedMenuItem.Name;
                 itemDetailsPanel.transform.GetChild(1).GetComponent<TextMesh>().text = "£" + clickedMenuItem.Cost.ToString();
                 itemDetailsPanel.transform.GetChild(2).GetComponent<TextMesh>().text = clickedMenuItem.Description;
                 itemDetailsPanel.SetActive(true);
                 panelManager.gameObject.SetActive(false);
             }


         }
         

     }
 }