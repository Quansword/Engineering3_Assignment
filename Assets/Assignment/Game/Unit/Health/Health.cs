using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth
{

	public UnityEvent OnDeath;

	[SerializeField]
	private float normDamageReduction = 0;
	public float NormDamageReduction { get { return normDamageReduction; } set { normDamageReduction = value; } }

	[SerializeField]
	private float lavaDamageReduction = 0;
	public float LavaDamageReduction { get { return lavaDamageReduction; } set { lavaDamageReduction = value; } }

	#region IDamageable
	[SerializeField]
	private float current = 100f;
	public float Current
	{
		get { return current; }
	}

	[SerializeField]
	private float max = 100f;
	public float Max { get { return max; } set { max = value; } }

	public float Percent { get { return current / max; } }

	public bool IsAlive { get { return current > 0f; } }
	public bool IsDead { get { return current <= 0f; } }


	public void Damage(IDamageInfo damageInfo)
	{
		if (IsDead)
			return;

		float modDamage = damageInfo.Amount;
		if (damageInfo.DamageType == DamageType.Normal)
		{
			modDamage = damageInfo.Amount * (1 - normDamageReduction);
		}
		else if (damageInfo.DamageType == DamageType.Lava)
		{
			modDamage = damageInfo.Amount * (1 - lavaDamageReduction);
		}

		float actualDamage = Mathf.Min(current, modDamage);
		float endHealth = current - actualDamage;

		DamageEvent damageEvent = new DamageEvent(this, damageInfo);

		if (Equals(actualDamage, 0f))
		{
			return;
		}

		damageEvent.HealthChange = -actualDamage;
		damageEvent.EndHealth = endHealth;

		ApplyHealthChange(damageEvent);

		if (IsDead && OnDeath != null)
		{
			OnDeath.Invoke();
		}
	}

	public void SetStats(IHealthStatsInfo stats)
	{

		HealthStatsEvent healthStatsEvent = new HealthStatsEvent(this, stats);
		healthStatsEvent.EndHealth = Mathf.Min(stats.Current, stats.Max);   // enforce that health cannot be greater than max health

		ApplyHealthChange(healthStatsEvent);

	}
	#endregion

	[System.Serializable]
	public class HealthEventCallback : UnityEvent<IHealthEvent> { }

	[SerializeField]
	private HealthEventCallback onHealthEvent = null;
	public HealthEventCallback OnHealthEvent { get { return onHealthEvent; } }

	[SerializeField]
	private float healthRegen = 0;
	public float HealthRegen { get { return healthRegen; } set { healthRegen = value; } }

	private void Update()
	{
		Heal(healthRegen * Time.deltaTime);
	}

	private void Heal(float amount)
	{
		if (IsDead || amount == 0)
			return;

		if (amount > 0 && current >= max)
			return;

		current += amount;
		current = Mathf.Clamp(current, 0f, max);
	}

	#region Helpers

	private void ApplyHealthChange(IHealthEvent healthEvent)
	{

		max = healthEvent.EndMaxHealth;
		current = healthEvent.EndHealth;

		if (onHealthEvent != null)
		{
			onHealthEvent.Invoke(healthEvent);
		}
	}
	#endregion
}
