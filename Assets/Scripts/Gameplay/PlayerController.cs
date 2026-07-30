using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ClickAndSwipe swipe;

    private bool isSwiping;

    private Camera cam;
    private InputSystem_Actions.PlayerActions playerActions;

    void Awake()
    {
        playerActions = InputManager.Instance.Controls.Player;
        cam = Camera.main;
    }

    void OnEnable()
    {
        playerActions.Attack.started += BeginSwipe;
        playerActions.Attack.canceled += EndSwipe;
    }

    void OnDisable()
    {
        playerActions.Attack.started -= BeginSwipe;
        playerActions.Attack.canceled -= EndSwipe;
    }

    void Update()
    {
        if (!isSwiping)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        swipe.UpdateSwipe(worldPos);
    }

    private void BeginSwipe(InputAction.CallbackContext ctx)
    {
        isSwiping = true;
        swipe.BeginSwipe();
    }

    private void EndSwipe(InputAction.CallbackContext ctx)
    {
        isSwiping = false;
        swipe.EndSwipe();
    }
}
