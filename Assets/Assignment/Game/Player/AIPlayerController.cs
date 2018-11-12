using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayerController : PlayerController {

    // coming in a future assignment

    [SerializeField]
    private float islandAreaReduction = 0.9f;

    protected override void Start() {
        base.Start();
        StartCoroutine(MoveAround());
    }

    IEnumerator MoveAround() {
        Unit unit = Units[0] as Unit;

        while (true) {
            if (unit != null && 
                unit.Health.IsAlive &&
                unit.Controller.HasReachedDestination) {
                Vector3 target = game.Map.Island.GetRandomPosition();
                target.x *= islandAreaReduction;
                target.z *= islandAreaReduction;
                unit.Controller.MoveTo(target);
            }

            yield return new WaitForSeconds(5f);
        }
    }

}
