using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("PlayerMovementScript")]
    [SerializeField] private PlayerMovement PlayerMovement;
    [Space]
    [Header("Variables")]
    [SerializeField] private int hp = 5;
    [Space]
    [Header("Controller")]
    [SerializeField] GameController GameController;
    [SerializeField] UIController UIController;

    private SpriteRenderer spriteRenderer;
    private Transform ShipTransform;

    private bool Invulnerable;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        ShipTransform = GetComponentInChildren<Transform>();
        StartCoroutine(StartedInvulnerable());
    }

    public void DamageGiven(int damage)
    {
        if (Invulnerable)
        {
            return;
        }
        if (damage <= 0)
        {
            Debug.Log("damage <= 0");
            return;
        }
        PlayerMovement.ToStartPosition();
        StartCoroutine(StartedInvulnerable());
        hp -= damage;

        UIController.HealthChange(hp);

        if (hp <= 0)
        {
            GameController.GameEnded();
        }
    }

    private IEnumerator StartedInvulnerable()
    {
        Invulnerable = true;
        StartCoroutine(InvulnerableEffect());
        yield return new WaitForSeconds(3f);
        Invulnerable = false;
        spriteRenderer.enabled = true;
    }

    private IEnumerator InvulnerableEffect()
    {
        if (Invulnerable)
        {
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.enabled = false;
            StartCoroutine(ToInvulnerableEffect());
        }
    }
    private IEnumerator ToInvulnerableEffect()
    {
        if (Invulnerable)
        {
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.enabled = true;
            StartCoroutine(InvulnerableEffect());
        }
    }
}
