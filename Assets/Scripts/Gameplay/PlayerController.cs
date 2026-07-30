using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions.PlayerActions playerActions;

    void Awake()
    {
        playerActions = InputManager.Instance.Controls.Player;
    }

    void OnEnable()
    {
        playerActions.Attack.performed += Attack;
    }

    void OnDisable()
    {
        playerActions.Attack.performed -= Attack;
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
