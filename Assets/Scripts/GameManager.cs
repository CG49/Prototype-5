using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private Transform respawnParent;

    private const float spawnRate = 1.0f;

    private InputSystem_Actions controls;
    private InputSystem_Actions.PlayerActions playerActions;

    void Awake()
    {
        controls = new InputSystem_Actions();
        playerActions = controls.Player;
    }

    void OnEnable()
    {
        playerActions.Enable();

        playerActions.Attack.performed += OnAttack;
    }

    void OnDisable()
    {
        playerActions.Disable();

        playerActions.Attack.performed -= OnAttack;
    }

    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            if (hit.collider.TryGetComponent<Target>(out var target))
                target.Hit();
        }
    }

    IEnumerator SpawnTarget()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);

            int randomIndex = Random.Range(0, 1);

            Instantiate(targets[randomIndex], respawnParent);
        }
    }
}
