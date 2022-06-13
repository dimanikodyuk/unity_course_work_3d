using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public int healthCount;
    public float jumpForce;
    public float speedForce;

    public Enemy(int health, float jump, float speed)
    {
        healthCount = health;
        jumpForce = jump;
        speedForce = speed;
    }
}
