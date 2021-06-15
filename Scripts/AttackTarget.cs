using UnityEngine;
using System.Collections;
using System;

public class AttackTarget : MonoBehaviour {

	public GameObject owner;

	[SerializeField]
	private string attackAnimation;

	[SerializeField]
	private bool magicAttack;

	[SerializeField]
	private float manaCost;

	[SerializeField]
	private float minAttackMultiplier;

	[SerializeField]
	private float maxAttackMultiplier;

	[SerializeField]
	private float minDefenseMultiplier;

	[SerializeField]
	private float maxDefenseMultiplier;

    public static int userdiff;

    public void setAttackDmg(int dmg)
    {
        AttackTarget.userdiff = dmg;

    }
	
	public void hit(GameObject target) {
		UnitStats ownerStats = this.owner.GetComponent<UnitStats> ();
		UnitStats targetStats = target.GetComponent<UnitStats> ();
		if (ownerStats.mana >= this.manaCost) {
			float attackMultiplier = (UnityEngine.Random.value * (this.maxAttackMultiplier - this.minAttackMultiplier)) + this.minAttackMultiplier;
			float damage = (this.magicAttack) ? attackMultiplier * ownerStats.magic : attackMultiplier * ownerStats.attack;

			float defenseMultiplier = (UnityEngine.Random.value * (this.maxDefenseMultiplier - this.minDefenseMultiplier)) + this.minDefenseMultiplier;
			damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));

            damage = Math.Abs(damage - userdiff);
			this.owner.GetComponent<Animator> ().Play (this.attackAnimation);
          
			targetStats.receiveDamage (damage);

			ownerStats.mana -= this.manaCost;
		}
	}
}
