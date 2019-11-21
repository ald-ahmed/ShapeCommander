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
    private Character attackingCharacter;
    [SerializeField] int attackPower;

    private float aWeight = .5f;

    private GameObject thisAttack;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        attackingCharacter = this.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayAttackRange()
    {
        Tile myLocation = gManager.currentTile;
        List<Tile> attackTiles = gameGrid.TilesInRange(myLocation, attackRange);
        int attackSize = attackTiles.Count;
        for (int i = 0; i < attackSize; i++)
        {
            attackTiles[i].Highlight("ATTACK");
        }
    }

    public void UnDisplayAttackRange()
    {
        Tile myLocation = gManager.currentTile;
        List<Tile> attackTiles = gameGrid.TilesInRange(myLocation, attackRange);
        int attackSize = attackTiles.Count;
        for (int i = 0; i < attackSize; i++)
        {
            attackTiles[i].Highlight("DEFAULT");
        }
    }

    public void Attack(Character target)
    {
        //Face your opponent
        transform.LookAt(target.transform.position);
        target.transform.LookAt(transform.position);
        //Animate attack
        aController.AnimateAttack();
        target.current_health -= attackPower;
        if (target.current_health <= 0)
        {
            target.animator.AnimateDeath();
        } else
        {
            target.animator.AnimateHit();
        }
        
        //Instatiate attack effect (we'll figure out a better placement vector)
        Vector3 effectPos = aWeight * transform.position + (1 - aWeight) * target.transform.position;
        thisAttack = Instantiate(attackEffect, effectPos, transform.rotation);
        UnDisplayAttackRange();
        // Remove attack option for turn
        attackingCharacter.ToggleAttackButton();
        coroutine = WaitToDestroy();
        StartCoroutine(coroutine);
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(thisAttack.gameObject);
        aController.AnimateIdle();
    }
}
