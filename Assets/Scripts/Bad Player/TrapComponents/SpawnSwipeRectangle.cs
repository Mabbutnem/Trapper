using System.Collections;
using UnityEngine;

public class SpawnSwipeRectangle : MonoBehaviour
{
   [SerializeField] private float spawnDelay = 0f;
   [SerializeField] private Vector3 initPosition1 = Vector3.zero;
   [SerializeField] private Vector3 initPosition2 = Vector3.zero;
   [SerializeField] private Vector3 initSpeed = Vector3.zero;

   public void Spawn()
   {
      StartCoroutine(Routine());
   }

   private IEnumerator Routine()
   {
      yield return new WaitForSeconds(spawnDelay);

      int rd = Random.Range(0, 2);
      Vector3 initPosition = rd == 0 ? initPosition1 : initPosition2;
      Vector3 signedInitSpeed = (rd == 0 ? 1 : -1) * initSpeed;

      GameObject toSpawn = ConstantsManager.DangerBottomRectangle;
      GameObject toSpawnCopy = Object.Instantiate(toSpawn, initPosition, toSpawn.transform.rotation);
      Rigidbody rigidbody = toSpawnCopy.GetComponent<Rigidbody>();
      if (rigidbody)
      {
         rigidbody.velocity = signedInitSpeed;
      }
   }
}
