﻿using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 15f;
    public bool IsMoving { get; private set; }

    public Pin CurrentPin { get; set; }
    private Pin _targetPin;
    private MapManager _mapManager;
    Vector3 offset = new Vector3(0f, .67f, 0f); //offset the character to have their feet on the level dot


    public void Initialise(MapManager mapManager, Pin startPin)
    {
        _mapManager = mapManager;
        SetCurrentPin(startPin);
    }
    
    
    /// <summary>
    /// This runs once a frame
    /// </summary>
    private void FixedUpdate()
    {
        //Debug.Log("start of FixedUpdate");
        if (_targetPin == null) return;
        //Debug.Log("after null return");

        // Get the characters current position and the targets position
        
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _targetPin.transform.position + offset;

        // If the character isn't that close to the target move closer
        if (Vector3.Distance(currentPosition, targetPosition) > .02f)
        {
            transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition,
                Time.deltaTime * Speed
            );
        }
        else
        {
            if (_targetPin.IsAutomatic)
            {
                // Get a direction to keep moving in
                var pin = _targetPin.GetNextPin(CurrentPin);
                MoveToPin(pin);
            }
            else
            {
                SetCurrentPin(_targetPin);
            }
        }
    }

    
    /// <summary>
    /// Check the if the current pin has a reference to another in a direction
    /// If it does the move there
    /// </summary>
    /// <param name="direction"></param>
    public void TrySetDirection(Direction direction)
    {
        // Try get the next pin
        var pin = CurrentPin.GetPinInDirection(direction);
        
        // If there is a pin then move to it
        if (pin == null) return;
        MoveToPin(pin);
    }


    /// <summary>
    /// Move to a new pin
    /// </summary>
    /// <param name="pin"></param>
    private void MoveToPin(Pin pin)
    {
        _targetPin = pin;
        IsMoving = true;
    }

    
    /// <summary>
    /// Set the current pin
    /// </summary>
    /// <param name="pin"></param>
    public void SetCurrentPin(Pin pin)
    {
        CurrentPin = pin;
        _targetPin = null;
        transform.position = pin.transform.position + offset;
        IsMoving = false;
        
        // Tell the map manager that
        // the current pin has changed
        _mapManager.UpdateGui();
    }
}