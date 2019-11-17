using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackEffect;
    [SerializeField] int attackRange;

    [SerializeField] AnimationController aController;
    [SerializeField] CharacterGridMovement gManager;
    [SerializeField] Grid gameGrid;

    private float aWeight = .5f;

    private GameObject thisAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayAttackRange()
    {
        Tile myLocation = gManager.currentTile;
        List<Tile> attackTiles = gameGrid.TilesInRange(myLocation, attackRange);
        int attackSize = attackTiles.Count;
        for (int i = 0; i < attackSize; i++)
        {
            attackTiles[i].Highlight("ATTACK");
        }
    }

    void UnDisplayAttackRange()
    {
        Tile myLocation = gManager.currentTile;
        List<Tile> attackTiles = gameGrid.TilesInRange(myLocation, attackRange);
        int attackSize = attackTiles.Count;
        for (int i = 0; i < attackSize; i++)
        {
            attackTiles[i].Highlight("DEFAULT");
        }
    }

    void Attack(Character target)
    {
        //Face your opponent
        transform.LookAt(target.transform.position);
        target.transform.LookAt(transform.position);
        //Animate attack
        aController.AnimateAttack();
        target.animator.AnimateHit();
        //Instatiate attack effect (we'll figure out a better placement vector)
        Vector3 effectPos = aWeight * transform.position + (1 - aWeight) * target.transform.position;
        thisAttack = Instantiate(attackEffect, effectPos, transform.rotation);
        UnDisplayAttackRange();
        waitToDestroy();
    }

    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(thisAttack);
        aController.AnimateIdle();
    }
}
