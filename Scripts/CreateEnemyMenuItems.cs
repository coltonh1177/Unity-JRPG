using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateEnemyMenuItems : MonoBehaviour {

	[SerializeField]
	private GameObject targetEnemyUnitPrefab;

	[SerializeField]
	private Sprite menuItemSprite;

	[SerializeField]
	private Vector2 initialPosition, itemDimensions;

	[SerializeField]
	private KillEnemy killEnemyScript;


	// Use this for initialization
	void Awake () {
		GameObject enemyUnitsMenu = GameObject.Find ("EnemyUnitsMenu");

		GameObject[] existingItems = GameObject.FindGameObjectsWithTag ("TargetEnemyUnit");
		Vector2 nextPosition = new Vector2 (this.initialPosition.x + (existingItems.Length * this.itemDimensions.x), this.initialPosition.y);
        
		GameObject targetEnemyUnit = Instantiate (this.targetEnemyUnitPrefab, enemyUnitsMenu.transform) as GameObject;
		targetEnemyUnit.name = "Target" + this.gameObject.name;
		targetEnemyUnit.transform.localPosition = nextPosition;
		targetEnemyUnit.transform.localScale = new Vector2 (0.7f, 0.7f);
		targetEnemyUnit.GetComponent<Button> ().onClick.AddListener (() => 
			selectEnemyTarget());
		targetEnemyUnit.GetComponent<Image> ().sprite = this.menuItemSprite;

		killEnemyScript.menuItem = targetEnemyUnit;
	}

    void Update() {

        //shortcut to attack the enemies
        //prssing 4 automatically attacks the first enemy
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject partyData = GameObject.Find("PlayerParty");

            //checks if the snake1 is active
            GameObject ene = GameObject.Find("Snake1");
            if (ene == null) {
                ene = GameObject.Find("Bat1");
            }
            //attacks if only the object exists, else do nothing
            if(ene != null)
                partyData.GetComponent<SelectUnit>().attackEnemyTarget(ene);
        }

        //pressing 5 automatically attacks the 2nd enemy
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {


            //same functionality as Alpha4
            GameObject partyData = GameObject.Find("PlayerParty");
            GameObject ene = GameObject.Find("Snake2");
            if (ene == null) {
                ene = GameObject.Find("Bat2");
            }

            if(ene != null)
                partyData.GetComponent<SelectUnit>().attackEnemyTarget(ene);

        }
    }


    //attacks the enemy selected on the screen
	public void selectEnemyTarget() {
		GameObject partyData = GameObject.Find ("PlayerParty");
        Debug.Log(this.gameObject.name);
        partyData.GetComponent<SelectUnit> ().attackEnemyTarget (this.gameObject);


    }

}
