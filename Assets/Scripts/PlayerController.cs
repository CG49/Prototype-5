using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
        playerActions.Attack.performed += Attack;
    }

    void OnDisable()
    {
        playerActions.Attack.performed -= Attack;
        playerActions.Disable();
    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Target target))
                target.Hit();
        }
    }
}
