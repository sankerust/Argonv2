using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [Tooltip("in seconds")][SerializeField] float levelLoadDelay = 1f;
  [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFx;
  void OnTriggerEnter(Collider collision)
  {
    deathFx.SetActive(true);
    StartDeathSequence();
    Invoke("ReloadScene", levelLoadDelay);
  }

  private void StartDeathSequence()
  {
    SendMessage("OnPlayerDeath");
  }

  private void ReloadScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
