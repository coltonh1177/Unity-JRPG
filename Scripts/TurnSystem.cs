using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TurnSystem : MonoBehaviour {

	private List<UnitStats> unitsStats;

	private GameObject playerParty;

	public GameObject enemyEncounter;

    public static bool tutorial = false;
    public void setTutorial()
    {
        TurnSystem.tutorial = true;
    }
    public void setNormal()
    {
        TurnSystem.tutorial = false;
    }

	[SerializeField]
	private GameObject actionsMenu, enemyUnitsMenu;

	void Start() {
		this.playerParty = GameObject.Find ("PlayerParty");

		unitsStats = new List<UnitStats> ();
		GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
		foreach (GameObject playerUnit in playerUnits) {
			UnitStats currentUnitStats = playerUnit.GetComponent<UnitStats> ();
			currentUnitStats.calculateNextActTurn (0);
			unitsStats.Add (currentUnitStats);
		}
		GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
		foreach (GameObject enemyUnit in enemyUnits) {
			UnitStats currentUnitStats = enemyUnit.GetComponent<UnitStats> ();
			currentUnitStats.calculateNextActTurn (0);
			unitsStats.Add (currentUnitStats);
		}
		unitsStats.Sort ();

		this.actionsMenu.SetActive (false);
		this.enemyUnitsMenu.SetActive (false);

		this.nextTurn ();
	}

	public void nextTurn() {
		GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag ("EnemyUnit");
        //if the both enemies have been defeated, load the scene from above
        if (remainingEnemyUnits.Length == 0)
        {
            string enemyType = this.enemyEncounter.ToString();
            this.enemyEncounter.GetComponent<CollectReward>().collectReward();
            //load the correct scene based on what enemy encounter this is
            if(enemyType.Contains("SnakeEnemyEncounter"))
            {
                if (tutorial)
                {
                    Debug.Log("got here");
                    SceneManager.LoadScene("Title");
                    
                }
                else
                {
                    Debug.Log("got overworld");
                    SceneManager.LoadScene("OverworldFinish");
                }
            }
            else
            {
                SceneManager.LoadScene("End");
            }
        }

        GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag ("PlayerUnit");
        //if the player characters has been defeated, load death screen
		if (remainingPlayerUnits.Length == 0) {
			SceneManager.LoadScene("DeathScreen");
		}

		UnitStats currentUnitStats = unitsStats [0];
		unitsStats.Remove (currentUnitStats);

		if (!currentUnitStats.isDead ()) {
			GameObject currentUnit = currentUnitStats.gameObject;

			currentUnitStats.calculateNextActTurn (currentUnitStats.nextActTurn);
			unitsStats.Add (currentUnitStats);
			unitsStats.Sort ();

			if (currentUnit.tag == "PlayerUnit") {
				this.playerParty.GetComponent<SelectUnit> ().selectCurrentUnit (currentUnit.gameObject);
			} else {
				currentUnit.GetComponent<EnemyUnitAction> ().act ();
			}
		} else {
			this.nextTurn ();
		}
	}
}
