using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject
{
    public int hp = 100;
    public float movePower = 2f;
    public float jumpPower = .001f;
    public float recoil = 0.0015f;
    public float debugSize = 1f;
    public double shootChance = 0f;
    public double shootInterval = 0.45f;
    public Image hpBar;
    public Text hpText;
    public float speedByItem;
    public ItemManager itemManager;

    private bool isDead = false;
    private bool isJump = false;
    private int maxHp = 100;
    private float horizontalMove;
    private float verticalMove;
    private Animator animator;
    private AnimatorClipInfo[] clipInfo;
    private Rigidbody2D rigidy;
    private Gun gun;


    protected override void Start()
    {
        animator = GetComponent<Animator>();
        rigidy = GetComponent<Rigidbody2D>();
        gun = transform.GetChild(0).GetComponent<Gun>();
        speedByItem = 1;
        base.Start();
    }

    private void Update()
    {
        UpdatePlayerMovement();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(shootInterval <= shootChance)
                Attack();
        }
    }

    public void PlayerDamaged(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
            hpText.text = "Dead";
            hpBar.fillAmount = 0;
            Dead();
        }
        hpText.text = hp.ToString();
        hpBar.fillAmount = Percent(hp, maxHp) / 100.0f;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    private float Percent(float a, float b)
    {
        return (a / b) * 100.0f;
    }

    private void Attack()
    {
        if (isJump)
            return;

        AttackAni(); 

        StartCoroutine("AttackRecoil");

    }

    private void AttackAni()
    {
        if (animator.GetBool("isCrouch"))
        {
            animator.SetTrigger("crouchAttack");
        }
        else
            animator.SetTrigger("attack");
    }

    IEnumerator AttackRecoil()
    {
        yield return new WaitForSeconds(0.35f);

        gun.UpdateGunShoot(this);

        if (transform.position.x > transform.GetChild(0).transform.position.x)
            recoil = Mathf.Abs(recoil);
        else
            recoil = - Mathf.Abs(recoil);


        Vector2 recoilPower = new Vector2(recoil, 0) * debugSize;
        rigidy.AddForce(recoilPower, ForceMode2D.Impulse);
    }

    private void Jump()
    {
        if(!isJump)
        {
            isJump = true;
            animator.SetBool("isJump", true);
            Vector2 jumpVelocity = new Vector2(0, jumpPower) * debugSize;
            rigidy.AddForce(jumpVelocity, ForceMode2D.Impulse);         
        }
    }

    public void UpdatePlayerMovement()
    {
        if (!isDead)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");

            shootChance += Time.deltaTime;

            if (shootChance > 2)
                shootChance = 0;

            Move();
        }
    }

    void Move()
    {
        
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo[0].clip.name == "playerShoot" || clipInfo[0].clip.name == "crouchShoot")
            return;
            

        Vector3 moveVelocity = Vector3.zero;

        if (horizontalMove < 0)
        {
            animator.SetBool("isRun", true);
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1) * debugSize;
        }
        else if (horizontalMove > 0)
        {
            animator.SetBool("isRun", true);
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1) * debugSize;
        }
        else
            animator.SetBool("isRun", false);

        if (verticalMove < 0)
        {
            animator.SetBool("isCrouch", true);
            gun.transform.position = new Vector3(gun.transform.position.x, transform.position.y, gun.transform.position.z);
        }
        else
        {
            animator.SetBool("isCrouch", false);
            gun.transform.position = new Vector3(gun.transform.position.x, transform.position.y + 0.55f, gun.transform.position.z);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime * speedByItem;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Tile")
        {
            isJump = false;
            animator.SetBool("isJump", false);
        }

        if (other.gameObject.tag == "Item")
        {
            itemManager.GetItem(other.gameObject.name);
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "DeadZone")
        {
            Debug.Log("deadzone");
            Dead();
        }
    }

}
