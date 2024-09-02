using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventsBookShelf : MonoBehaviour
{

    public Dialogue dialogue_1;

    private DialogueManager theDm;
    private OrderManager theOrder;
    private PlayerManager thePlayer; //animator.getFloat(DirY == 1)

    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        theDm = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }

    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theDm.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDm.talking);
        theOrder.Move();

        flag = false;
    }

}
