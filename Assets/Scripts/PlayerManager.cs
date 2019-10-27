using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Character selectedCharacter=null;

    //BE VERY CAREFUL WITH THIS SO YOU DONT GET LOOPS
    public void SetSelectedCharacter(Character c)
    {
        if (selectedCharacter != null)
        {
            selectedCharacter.Deselect(true);
        }
        selectedCharacter = c;
       // if (selectedCharacter != null)
        //{
            //selectedCharacter.Select(true);
        //}
    }


    public void SendSelectedCharacter(Vector3 where)
    {
        if(selectedCharacter!=null)
            selectedCharacter.MoveTo(where);
    }
}
