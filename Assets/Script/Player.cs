using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 사격 시간 조정해서 반동인한 슬라이드 없애기
public class Player : MovingObject
{
    public float movePower = 2f;
    public float jumpPower = .001f;
    public float recoil = 0.0015f;
    public float debugSize = 1f;
    public double shootChance = 0f;
    public double shootInterval = 0.45f;
    private bool isDead = false;
    private bool isJump = false;
    private int hp = 100;
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
            Attack();
        }
    }

    private void Attack()
    {
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

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Tile")
        {
            isJump = false;
            animator.SetBool("isJump", false);
        }
    }
}
