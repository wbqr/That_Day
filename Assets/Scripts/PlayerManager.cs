using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{

    static public PlayerManager instance;


    public string currentMapName; // transferMap 스크립트에 있는 transferMapName의 변수의 값을 저장


    private AudioSource audioSource; // 사운드 플레이어


  


    public float runSpeed;
    private float applyRunSpeed;

    private int currentWalkCount = 0;


    private bool canMove = true;
    public bool transferMap = true;

    public bool notMove = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0  || Input.GetAxisRaw("Horizontal") != 0 && !notMove)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
            }
            else
            {
                applyRunSpeed = 0;
            }
                

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            
            if (vector.x !=0)
            {
                vector.y = 0;
            }

            // vector.x = 1 -> vector.y = 0

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag)
            {
                break;
            }

            animator.SetBool("Walking", true);


            
            while(currentWalkCount < walkCount)
            {
                if(vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed) * walkCount, 0, 0);
                }
                else if(vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed) * walkCount, 0);
                }

                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);

            }
            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;

    }



    // Update is called once per frame
    void Update()
    {
        if (canMove && !notMove)
        {
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 )
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }

        }
    }
}

