using UnityEngine;
using Vuforia;

public class virtualButtonScript : MonoBehaviour, IVirtualButtonEventHandler
{
	public int testInt;
	void Start () {
		VirtualButtonBehaviour[] vbs = transform.GetComponentsInChildren<VirtualButtonBehaviour> ();

		for (int i=0; i < vbs.Length; ++i) {
			vbs[i].RegisterEventHandler(this);
		}

		testInt = 0;
	}

    public void OnButtonPressed(VirtualButtonBehaviour vb){
			
		if(vb.name=="NextButton") { doNextButton();}
			
		if(vb.name=="NextButton") { doPrevButton();}
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb) { 
	
	}

    private void doNextButton(){
        testInt++;
	}
	private void doPrevButton(){
        testInt--;
	}
}