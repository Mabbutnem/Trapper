﻿using System.Collections;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
   [SerializeField] private GameObject toSpawn;
   [SerializeField] private float spawnDelay = 0f;
   [SerializeField] private Vector3 initPosition = Vector3.zero;
   [SerializeField] private Vector3 initSpeed = Vector3.zero;

   protected void Spawn()
   {
      StartCoroutine(Routine());
   }

   private IEnumerator Routine()
   {
      yield return new WaitForSeconds(spawnDelay);

      GameObject toSpawnCopy = Object.Instantiate(toSpawn, initPosition, toSpawn.transform.rotation);
      Rigidbody rigidbody = toSpawnCopy.GetComponent<Rigidbody>();
      if (rigidbody)
      {
         rigidbody.velocity = initSpeed;
      }
   }
}
