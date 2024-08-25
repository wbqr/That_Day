using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerManager thePlayer; // 이벤트 도중에 키입력 방지
    private List<MovingObject> characters;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();        
    }

    public void PreLoadCharacter()
    {
        characters = ToList();

    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();

        for (int i=0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }

    public void NotMove()
    {
        thePlayer.notMove = true;
    }

    public void Move()
    {
        thePlayer.notMove = false;
    }

    public void SetTransparent(string _name)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }       
    }

    public void SetThorought(string _name)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false;
            }
        }    
    }

    public void SetUnThorought(string _name)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true;
            }
        }    
    }



    public void SetUnTransparent(string _name)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }       
    }


    public void Move(string _name, string _dir)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i=0; i<characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirY", 0);
                characters[i].animator.SetFloat("DirX", 0);
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY", 1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("Diry", -1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX", 0);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
