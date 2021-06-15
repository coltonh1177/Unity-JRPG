using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UnitStats : MonoBehaviour, IComparable {

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private GameObject damageTextPrefab;
	[SerializeField]
	private Vector2 damageTextPosition;

    public char difficulty;

	public float health;
	public float mana;
	public float attack;
	public float magic;
	public float defense;
	public float speed;

	public int nextActTurn;

	private bool dead = false;

	private float currentExperience;

	void Start() {

        //Easy difficulty
        if (this.difficulty == 'E')
        {
            easyDiff();
        }
        //medium difficulty
        else if (this.difficulty == 'M')
        {
            mediumDiff();
        }
        //dark souls difficulty
        else if (this.difficulty == 'D') {
            darkSoulsDiff();
        }
		
	}

	public void receiveDamage(float damage) {
		this.health -= damage;
		animator.Play ("Hit");

		GameObject HUDCanvas = GameObject.Find ("HUDCanvas");
		GameObject damageText = Instantiate (this.damageTextPrefab, HUDCanvas.transform) as GameObject;
		damageText.GetComponent<Text> ().text = "" + damage;
		damageText.transform.localPosition = this.damageTextPosition;
		damageText.transform.localScale = new Vector2 (1.0f, 1.0f);

		if (this.health <= 0) {
			this.dead = true;
			this.gameObject.tag = "DeadUnit";
			Destroy (this.gameObject);
		}
	}

	public void calculateNextActTurn(int currentTurn) {
		this.nextActTurn = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
	}

	public int CompareTo(object otherStats) {
		return nextActTurn.CompareTo (((UnitStats)otherStats).nextActTurn);
	}

	public bool isDead() {
		return this.dead;
	}

	public void receiveExperience(float experience) {
		this.currentExperience += experience;
	}

    public void easyDiff()
    {
        this.speed = 10;
        this.attack = 100;
        this.defense = 10;

        Debug.Log(this.speed);
        Debug.Log(this.attack);
        Debug.Log(this.defense);
    }

    public void mediumDiff()
    {
        this.speed = 5;
        this.attack = 50;
        this.defense = 5;

        Debug.Log(this.speed);
        Debug.Log(this.attack);
        Debug.Log(this.defense);
    }

    public void darkSoulsDiff()
    {
        this.speed = 5;
        this.attack = 15;
        this.defense = 0;
       
        Debug.Log(this.speed);
        Debug.Log(this.attack);
        Debug.Log(this.defense);
    }
}
