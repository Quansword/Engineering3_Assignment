using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAction : StandardAbilityAction, IDamageSource {

    [SerializeField]
    private GameObjectPool projectilePool = null;

    [SerializeField]
    private Vector3 targetDir = Vector3.forward;
    public Vector3 TargetDir { get { return targetDir; } set { targetDir = value; } }

    protected override IEnumerator PreCast() {
        yield return StartCoroutine(TurnToDir(targetDir));
    }

    protected override IEnumerator PerformAction() {
        Projectile projectile = projectilePool.Get<Projectile>();
        projectile.GetComponent<Damage>().DamageSource = this;
        float projectileDiameter = projectile.GetComponent<SphereCollider>().radius * 2f;
        projectile.transform.position = actor.ChestTransform.position + actor.transform.forward * projectileDiameter;
        projectile.transform.rotation = actor.transform.rotation;

        yield return new WaitWhile(() => projectile.IsAlive);
        projectilePool.Return(projectile);
        yield break;
    }

    void OnCastPoint() {
        Debug.LogFormat("{0}::OnCastPoint", this);
    }

    public Unit DamagingUnit { get { return actor; } }

}
