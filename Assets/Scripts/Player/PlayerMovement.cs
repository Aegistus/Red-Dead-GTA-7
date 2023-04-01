using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    CharacterController charController;

    Dictionary<Type, MovementState> availableStates;
    MovementState currentState;
    Type defaultStateType;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        availableStates = new Dictionary<Type, MovementState>()
        {
            { typeof(Standing), new Standing(gameObject) },
        };
        defaultStateType = typeof(Standing);
    }

    void Update()
    {
        if (currentState == null)
        {
            currentState = availableStates[defaultStateType];
            currentState.BeforeExecution();
        }
        Type nextStateType = currentState.CheckForTransition();
        if (nextStateType != null)
        {
            currentState.AfterExecution();
            currentState = availableStates[nextStateType];
            currentState.BeforeExecution();
        }
        else
        {
            currentState.DuringUpdate();
        }
    }

    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.DuringFixedUpdate();
        }
    }

    abstract class MovementState 
    {
        protected GameObject gameObject;
        protected Transform transform;

        public MovementState(GameObject gameObject)
        {
            this.gameObject = gameObject;
            transform = gameObject.transform;
        }

        public abstract void BeforeExecution();
        public abstract void AfterExecution();
        public abstract void DuringUpdate();
        public abstract void DuringFixedUpdate();
        public abstract Type CheckForTransition();

    }

    class Standing : MovementState
    {
        public Standing(GameObject gameObject) : base(gameObject)
        {

        }

        public override void BeforeExecution()
        {
            print("Standing");
        }

        public override void AfterExecution()
        {
            
        }

        public override void DuringUpdate()
        {
            
        }

        public override void DuringFixedUpdate()
        {
            
        }

        public override Type CheckForTransition()
        {
            return null;
        }
    }

}
