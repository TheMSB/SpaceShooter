using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats {
	public int armor;
	public int maxArmor;
}

public abstract class UnitController : MonoBehaviour {

	public Stats stats;

	public void Damage (int damageAmount) {
		if (stats.armor - damageAmount < 0) {
			stats.armor = 0;
		} else {
			stats.armor = stats.armor - damageAmount;
		}
	}

	public void Heal (int healAmount) {
		if (stats.armor + healAmount <= stats.maxArmor) {
			stats.armor += healAmount;
		} else {
			stats.armor = stats.maxArmor;
		}
	}
}
