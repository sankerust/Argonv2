﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] GameObject deathFx;
  [SerializeField] GameObject hitFx;
  [SerializeField] Transform parent;
  [SerializeField] int scorePerHit = 10;
  [SerializeField] int hits = 4;

  ScoreBoard scoreBoard;
    // Start is called before the first frame update
    void Start()
  {
    AddBoxCollider();
    scoreBoard = FindObjectOfType<ScoreBoard>();
  }

  private void AddBoxCollider()
  {
    Collider boxCollider = gameObject.AddComponent<BoxCollider>();
    boxCollider.isTrigger = false;
  }

  void OnParticleCollision(GameObject other)
  {
    GameObject enemyHitFx = Instantiate(hitFx, transform.position, Quaternion.identity);
    enemyHitFx.transform.parent = parent;
    scoreBoard.ScoreHit(scorePerHit);
    hits--;
    if (hits < 1 )
    {
      KillEnemy();
    }
    
  }

  private void KillEnemy()
  {
    GameObject fx = Instantiate(deathFx, transform.position, Quaternion.identity);
    fx.transform.parent = parent;
    Destroy(gameObject);
  }
}
