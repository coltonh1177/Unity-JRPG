using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AddButtonCallback : MonoBehaviour {


	[SerializeField]
	private bool physical;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Button> ().onClick.AddListener (() => addCallback());
	}


    public void Update() {


        //this is for the macro system
        //pressing 1 chooses physcial attack
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject playerParty = GameObject.Find("PlayerParty");
            playerParty.GetComponent<SelectUnit>().selectAttack(true);
        }

        //pressing 2 chooses magic attack
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GameObject playerParty = GameObject.Find("PlayerParty");
            playerParty.GetComponent<SelectUnit>().selectAttack(false);
        }

        

    }

    //choosing the attack using the buttons on the screen
	private void addCallback() {
		GameObject playerParty = GameObject.Find ("PlayerParty");
		playerParty.GetComponent<SelectUnit> ().selectAttack (this.physical);
	}


}
