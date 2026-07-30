using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public InputSystem_Actions Controls { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Controls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        if (Controls == null)
            return;

        EnableGameplay();
        EnableUI();

        Controls.UI.Pause.performed += OnPause;
    }

    void OnDisable()
    {
        if (Controls == null)
            return;

        Controls.UI.Pause.performed -= OnPause;

        DisableGameplay();
        DisableUI();
    }

    public void EnableGameplay()
    {
        Controls.Player.Enable();
    }

    public void DisableGameplay()
    {
        Controls.Player.Disable();
    }

    public void EnableUI()
    {
        Controls.UI.Enable();
    }

    public void DisableUI()
    {
        Controls.UI.Disable();
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        GameEvents.Raise(new GameEvent(GameEventType.Pause));
    }
}
