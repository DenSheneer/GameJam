﻿using UnityEngine;
using System.Linq;

public class PlayerController : PhysicsObject
{
    [Header("Player Controller")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] Transform characterMesh;
    
    [SerializeField] Animator animator;
    public ActivePlayer activePlayer = ActivePlayer.PLAYER1;
    public Team team;
    private Vector3 moveVelocity;


    private float currentMovementSpeed = 5f;
    private float xAxis, yAxis;
    private bool movingDisabled = false;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        currentMovementSpeed = movementSpeed;
    }

    public void SetMovementSpeed(float ms) {
        this.currentMovementSpeed = ms;
    }

    public void ResetMovementSpeed() {
        this.currentMovementSpeed = movementSpeed;
    }

    public float GetCurrentMovementSpeed() {
        return currentMovementSpeed;
    }

    public float GetDefaultSpeed() {
        return movementSpeed;
    }

    public void DisableMovement() {
        this.movingDisabled = true;
    }

    public void EnableMovement() {
        this.movingDisabled = false;
    }

    public bool isMovementDisabled() {
        return this.movingDisabled;
    }

    // Update is called once per frame
    public override void Update()
    {

        var xAxis = movingDisabled ? 0 : Input.GetAxisRaw(ActivePlayerData.Horizontal(activePlayer));
        var yAxis = movingDisabled ? 0 : Input.GetAxisRaw(ActivePlayerData.Vertical(activePlayer));

        animator.SetBool("moving", Mathf.Abs(xAxis) + Mathf.Abs(yAxis) > 0);

        characterMesh.LookAt(transform.localPosition + new Vector3(xAxis, 0f, yAxis).normalized);

        SetVelocity(new Vector3(xAxis, 0f, yAxis).normalized * currentMovementSpeed);
        
        UpdateGravity();
        MoveCharacter();
    }
}