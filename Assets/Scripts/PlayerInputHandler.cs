using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private string actionMapName;

    [Header("Actions Names")]
    [SerializeField] private string sprintActionName = "Sprint";
    [SerializeField] private string jumpActionName = "Jump";
    [SerializeField] private string slowDownActionName = "Slow Down";

    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction slowDownAction;

    public bool IsJumpTriggered { get; private set; }
    public bool IsSprintTriggered { get; private set; }
    public bool IsSlowDownTriggered { get; private set; }

    private void Awake()
    {
        FindActions();
        RegisterInputs();
    }

    private void FindActions()
    {
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jumpActionName);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprintActionName);
        slowDownAction = playerControls.FindActionMap(actionMapName).FindAction(slowDownActionName);
    }

    private void RegisterInputs()
    {
        jumpAction.performed += context => IsJumpTriggered = true;
        jumpAction.canceled += context => IsJumpTriggered = false;

        sprintAction.performed += context => IsSprintTriggered = true;
        sprintAction.canceled += context => IsSprintTriggered = false;

        slowDownAction.performed += context => IsSlowDownTriggered = true;
        slowDownAction.canceled += context => IsSlowDownTriggered = false;
    }

    private void OnEnable()
    {
        jumpAction.Enable();
        sprintAction.Enable();
        slowDownAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.Disable();
        sprintAction.Disable();
        slowDownAction.Disable();
    }
}
