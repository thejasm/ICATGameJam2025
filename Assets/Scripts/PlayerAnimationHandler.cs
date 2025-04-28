using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationHandler: MonoBehaviour {
    Animator anim;
    private Vector2 move;
    private Transform spriteTransform;
    private Vector2 mouseDirection;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction; // New: Action for mouse look

    void Awake() {
        anim = GetComponent<Animator>();
        spriteTransform = anim.gameObject.transform;

        playerInput = GetComponentInParent<PlayerInput>();
        if(playerInput == null) {
            Debug.LogError("PlayerInput component not found in parent");
            return;
        }

        moveAction = playerInput.actions["Move"];
        if(moveAction == null) {
            Debug.LogError("Move action not found in the Input Action Asset.");
            return;
        }

        // Get the "Look" action (for mouse position).  Make sure you have a "Look" action of type "Value" -> "Vector 2" in your Input Action Asset.
        lookAction = playerInput.actions["Look"];  // Correct way to access action
        if(lookAction == null) {
            Debug.LogError("Look action not found in the Input Action Asset. Check Action Map and Action name.");
            return; // Exit early if action is not found.
        }
    }

    void OnEnable() {
        if(moveAction != null) {
            moveAction.Enable();
            moveAction.performed += OnMovePerformed;
            moveAction.canceled += OnMoveCanceled;
        }
        if(lookAction != null) {
            lookAction.Enable();
        }
    }

    void OnDisable() {
        if(moveAction != null) {
            moveAction.Disable();
            moveAction.performed -= OnMovePerformed;
            moveAction.canceled -= OnMoveCanceled;
        }
        if(lookAction != null) {
            lookAction.Disable();
        }
    }

    void OnMovePerformed(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext context) {
        move = Vector2.zero;
    }


    void Update() {
        UpdateMouseDirection();
        animate();
        flip();
    }

    private void UpdateMouseDirection() {
        // Use the "Look" action to get the mouse position delta:
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

        // Convert mouseDelta to world space direction.  This assumes your camera is orthographic.  If perspective, you'll need a different conversion.
        mouseDirection = Camera.main.ScreenToWorldPoint(mouseDelta) - transform.position;
        mouseDirection.Normalize();


    }

    private void animate() {
        anim.SetFloat("MoveX", mouseDirection.x);
        anim.SetFloat("MoveY", mouseDirection.y);
        anim.SetFloat("MoveMagnitude", move.magnitude); // Keep move magnitude for other animations if needed
        anim.SetFloat("LastMoveX", mouseDirection.x);
        anim.SetFloat("LastMoveY", mouseDirection.y);
    }

    private void flip() {
        if(mouseDirection.x > 0 && faceLeft) {
            spriteTransform.localScale = new Vector3(-1, 1, 1);
            faceLeft = false;
        } else if(mouseDirection.x < 0 && !faceLeft) {
            spriteTransform.localScale = new Vector3(1, 1, 1);
            faceLeft = true;
        }
    }

    private bool faceLeft = true;  // Moved this declaration here

}