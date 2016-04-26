using UnityEngine;
using System.Collections;

/**
 * Representation of the stats for a unit, specifies current and maximum armor values.
 */
[System.Serializable]
public class Stats {
	public int armor;
	public int maxArmor;
}

/**
 * Abstract Controller for a specific unit instance that specifies
 * Heal and Damage behaviour.
 */
public abstract class UnitController : MonoBehaviour {

	/** The stats for this unit. */
	public Stats stats;

	/**
	 * Damage the unit, inflicting the specified amount of armor damage.
	 */
	public void Damage (int damageAmount) {
		if (stats.armor - damageAmount < 0) {
			stats.armor = 0;
		} else {
			stats.armor = stats.armor - damageAmount;
		}
	}

	/**
	 * Heal the unit, restoring the specified amount of armor, cannot exceed maximum armor value.
	 */
	public void Heal (int healAmount) {
		if (stats.armor + healAmount <= stats.maxArmor) {
			stats.armor += healAmount;
		} else {
			stats.armor = stats.maxArmor;
		}
	}
}
