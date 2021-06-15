using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RunFromBattle : MonoBehaviour {

	[SerializeField]
	private float runnningChance;

	public void tryRunning() {
		float randomNumber = Random.value;
		if (randomNumber < this.runnningChance) {

            string enemyType = GameObject.Find("TurnSystem").GetComponent<TurnSystem> ().enemyEncounter.ToString();
            //load the correct scene based on what enemy encounter this is
            if (enemyType.Contains("SnakeEnemyEncounter"))
            {
                SceneManager.LoadScene("Overworld");
            }
            else
            {
                SceneManager.LoadScene("Overworld2");
            }

		} else {
			GameObject.Find("TurnSystem").GetComponent<TurnSystem> ().nextTurn ();
		}
	}

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            tryRunning();
        }
    }
}
