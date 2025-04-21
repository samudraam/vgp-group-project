using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [Header("Enemy Container")]
    public GameObject enemyContainer;
    
    [Header("Gate Settings")]
    public GameObject gate;
    public float openingSpeed = 2.0f;
    public float openDistance = 5.0f;
    public Vector3 openingDirection = Vector3.up;
    
    [Header("Optional Settings")]
    public bool autoCheckEnemies = true;
    public float checkInterval = 1.0f;
    
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;
    
    void Start()
    {
        // Initialize variables
        if (gate == null)
        {
            gate = this.gameObject;
        }
        
        initialPosition = gate.transform.position;
        targetPosition = initialPosition + (openingDirection.normalized * openDistance);

        // Start periodic enemy check if enabled
        if (autoCheckEnemies && enemyContainer != null)
        {
            InvokeRepeating("CheckEnemies", 0f, checkInterval);
        }
    }
    
    void Update()
    {
        // Handle gate movement if it's currently opening
        if (isOpening)
        {
            gate.transform.position = Vector3.MoveTowards(
                gate.transform.position, 
                targetPosition, 
                openingSpeed * Time.deltaTime
            );
            
            // Check if gate has reached target position
            if (Vector3.Distance(gate.transform.position, targetPosition) < 0.01f)
            {
                isOpening = false;
                
                // Cancel the repeating check since gate is now open
                if (autoCheckEnemies)
                {
                    CancelInvoke("CheckEnemies");
                }
            }
        }
    }
    
    public void CheckEnemies()
    {
        if (enemyContainer == null)
        {
            Debug.LogWarning("Enemy container not assigned!");
            return;
        }
        
        // Count active enemies (direct children of the container)
        int activeEnemyCount = 0;
        
        // Loop through all child objects of the enemy container
        foreach (Transform child in enemyContainer.transform)
        {
            // Only count active GameObjects
            if (child.gameObject.activeInHierarchy)
            {
                activeEnemyCount++;
            }
        }
        
        // Debug info
        Debug.Log("Active enemies remaining: " + activeEnemyCount);
        
        // If no enemies remain, open the gate
        if (activeEnemyCount == 0)
        {
            OpenGate();
        }
    }
    
    public void OpenGate()
    {
        // Don't open again if already opening or opened
        if (isOpening || Vector3.Distance(gate.transform.position, targetPosition) < 0.01f)
        {
            return;
        }
        
        Debug.Log("Opening gate!");
        isOpening = true;

    }
    
    // Public method that can be called from other scripts
    public void ForceCheckEnemies()
    {
        CheckEnemies();
    }
    
    // Reset gate to initial position (useful for editor testing)
    public void ResetGate()
    {
        gate.transform.position = initialPosition;
        isOpening = false;
    }
}