using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
	[SerializeField]
	private Health health = null;
	public IHealth Health { get { return health; } }

	public string Name { get { return name; } }

	[SerializeField]
	private Player owner = null;
	public Player Owner { get { return owner; } }

	[SerializeField]
	private UnitController controller = null;
	public IUnitController Controller { get { return controller; } }

	[SerializeField]
	private List<Ability> abilities = new List<Ability>();
	public List<Ability> Abilities { get { return abilities; } }

	[SerializeField]
	private List<Item> items = new List<Item>();
	public List<Item> Items { get { return items; } }

	[SerializeField]
	private int maxItems = 4;
	public int MaxItems { get { return maxItems; } }

	[SerializeField] private Transform abilitiesParent = null;
	public Transform AbilitiesParent { get { return abilitiesParent; } }

	[SerializeField]
	private Ragdoll ragdoll = null;
	public Ragdoll Ragdoll { get { return ragdoll; } }

	[SerializeField]
	private Transform faceTransform = null;
	public Transform FaceTransform { get { return faceTransform; } }

	[SerializeField]
	private Transform chestTransform = null;
	public Transform ChestTransform { get { return chestTransform; } }

	[SerializeField]
	private float moveSpeedMod = 0;
	public float MoveSpeedMod { get { return moveSpeedMod; } set { controller.Movement.MoveSpeed -= moveSpeedMod; moveSpeedMod = value; controller.Movement.MoveSpeed += moveSpeedMod; } }

	[SerializeField]
	private float turnSpeedMod = 0;
	public float TurnSpeedMod { get { return turnSpeedMod; } set { controller.Movement.TurnSpeed -= turnSpeedMod; turnSpeedMod = value; controller.Movement.TurnSpeed += turnSpeedMod; } }

	[SerializeField]
	private float cooldownModPercent = 0;
	public float CooldownModPercent
	{
		get
		{
			return cooldownModPercent;
		}
		set
		{
			foreach (Ability ability in abilities)
			{
				StandardAbility thisAbility = ability as StandardAbility;
				if (!thisAbility)
					continue;

				thisAbility.SpellCooldown.Duration /= (1 - cooldownModPercent);
				cooldownModPercent = value;
				thisAbility.SpellCooldown.Duration *= (1 - cooldownModPercent);
			}
		}
	}

	[SerializeField]
	private float rangeModPercent = 0;
	// Need implementation !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

	[SerializeField]
	private float healthRegenMod = 0;
	public float HealthRegenMod { get { return healthRegenMod; } set { health.HealthRegen -= healthRegenMod; healthRegenMod = value; health.HealthRegen += healthRegenMod; } }

	[SerializeField]
	private float normDamageRedMod = 0;
	public float NormDamageRedMod { get { return normDamageRedMod; } set { health.NormDamageReduction -= normDamageRedMod; normDamageRedMod = value; health.NormDamageReduction += normDamageRedMod; } }

	[SerializeField]
	private float lavaDamageRedMod = 0;
	public float LavaDamageRedMod { get { return lavaDamageRedMod; } set { health.LavaDamageReduction-= lavaDamageRedMod; lavaDamageRedMod = value; health.LavaDamageReduction += lavaDamageRedMod; } }
}
