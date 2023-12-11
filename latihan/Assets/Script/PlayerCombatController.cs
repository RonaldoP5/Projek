using System.Collections;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    private Bergerak bergerakScript;

    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        bergerakScript = GetComponent<Bergerak>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled && bergerakScript != null && bergerakScript.injakTanah)
            {
                //Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput && bergerakScript != null && bergerakScript.injakTanah)
        {
            // Perform Attack1
            if (!isAttacking)
            {
                audioManager.AttackSound();
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
               

                // CheckAttackHitBox will be called when the attack animation connects
                Invoke("CheckAttackHitBox", 0.3f); // Adjust the delay based on your animation timing
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            // Wait for new input
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {

            // Cari komponen EnemyHealth pada objek yang terkena serangan
            EnemyHealth enemyHealth = collider.transform.GetComponent<EnemyHealth>();

            // Jika ditemukan, kirim pesan "TakeDamage"
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attack1Damage);
            }

            // Instantiate hit particle if needed
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    private void FinishAttack1()
    {
        audioManager.AttackStopSFX();
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
