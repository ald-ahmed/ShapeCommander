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

    private float aWeight = .6f;

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
        UnDisplayAttackRange();
        Tile myLocation = gManager.currentTile;
        List<Tile> attackTiles = gameGrid.TilesInRange(myLocation, attackRange);

        for (int i = 0; i < attackTiles.Count; i++)
        {
            if (attackTiles[i].Equals(target.moveScript.currentTile))
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
                }
                else
                {
                    target.animator.AnimateHit();
                }

                attackingCharacter.GetComponent<CapsuleCollider>().enabled = false;


                if (attackingCharacter.characterType.Equals("Mage"))
                {
                    aWeight = 0f;
                }
                //Instatiate attack effect (we'll figure out a better placement vector)
                Vector3 effectPos = aWeight * transform.position + (1 - aWeight) * target.transform.position;
                thisAttack = Instantiate(attackEffect, effectPos, transform.rotation);
                
                // Remove attack option for turn
                //attackingCharacter.ToggleAttackButton();
                coroutine = WaitToDestroy();
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(3);
        if (attackingCharacter.characterType.Equals("Mage"))
        {
            Destroy(thisAttack.gameObject);
        }
        //Destroy(thisAttack.gameObject);
        attackingCharacter.GetComponent<CapsuleCollider>().enabled = true;
        aController.AnimateIdle();
    }
}
