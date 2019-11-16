using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
  [Header("General")]
  [Tooltip("In ms^-1")][SerializeField] float xSpeed = 20f;
  [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 20f;
  [SerializeField] float xLimit = 10f;
  [SerializeField] float yLimit = 7f;
  [SerializeField] GameObject[] guns;

  [Header("Screen-position Based")]
  [SerializeField] float positionPitchFactor = -4f;
  [SerializeField] float positionYawFactor = 3.5f;
  [Header("Control-throw Based")]
  [SerializeField] float controlPitchFactor = -25f;
  [SerializeField] float controlRollFactor = -40f;
  float xThrow, yThrow;
  bool isControlEnabled = true;
  AudioSource sound;
  Rigidbody rigidBody;
  void Start() 
  {
    rigidBody = gameObject.GetComponent<Rigidbody>();
  }

    // Update is called once per frame
    void Update()
  {
    if (isControlEnabled) {
      ProcessTranslation();
      ProcessRotation();
      ProcessFiring();
    }

  }

  void OnPlayerDeath() //called by string reference
  {
    isControlEnabled = false;
    rigidBody.isKinematic = false;
    rigidBody.velocity = rigidBody.velocity - new Vector3(0f, 10f, 0f);
  }

  private void ProcessRotation()
  {
    float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
    float yaw = transform.localPosition.x * positionYawFactor;
    float roll = xThrow * controlRollFactor;
    transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
  }

  private void ProcessTranslation()
  {
    xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    float xOffset = xThrow * Time.deltaTime * xSpeed;
    float rawXPos = transform.localPosition.x + xOffset;
    float clampedXPos = Mathf.Clamp(rawXPos, -xLimit, xLimit);

    yThrow = CrossPlatformInputManager.GetAxis("Vertical");
    float yOffset = yThrow * Time.deltaTime * ySpeed;
    float rawYPos = transform.localPosition.y + yOffset;
    float clampedYPos = Mathf.Clamp(rawYPos, -yLimit, yLimit);

    transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
  }

  void ProcessFiring()
  {
    if (CrossPlatformInputManager.GetButton("Fire"))
    {
      SetGunsActive(true);
    }
    else
    {
      SetGunsActive(false);
      sound = gameObject.GetComponent<AudioSource>();
      sound.Play();
    }
  }

  void SetGunsActive(bool isActive)
  {
    foreach (GameObject gun in guns)
    {
      var emissionModule = gun.GetComponent<ParticleSystem>().emission;
      emissionModule.enabled = isActive;
    }
  }
}
