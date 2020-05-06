using UnityEngine;

public class SpawnStones : MonoBehaviour
{
   [SerializeField] private int nbStonesByPosition = 5;
   [SerializeField] private float minRadius = 1f;
   [SerializeField] private float maxRadius = 2f;
   [SerializeField] private float spawnVelocity = 10f;
   [SerializeField] private float xMaxSpawnVariation = 0.5f;
   [SerializeField] private Vector3[] positions;

   public void Spawn()
   {
      GameObject prefab = ConstantsManager.Stone;

      foreach(Vector3 position in positions)
      {
         Vector3 currentPosition = position;
         for(int i = 0; i < nbStonesByPosition; i++)
         {
            float radius = Random.Range(minRadius, maxRadius);
            Vector3 spawnPosition = currentPosition + new Vector3(Random.Range(-xMaxSpawnVariation, xMaxSpawnVariation), 0f, 0f);
            GameObject stone = GameObject.Instantiate(prefab, spawnPosition, prefab.transform.rotation);
            stone.transform.localScale = new Vector3(radius * stone.transform.localScale.x, stone.transform.localScale.y, radius * stone.transform.localScale.z);
            stone.GetComponent<Rigidbody>().velocity = new Vector3(0f, -spawnVelocity, 0f);
            
            currentPosition += new Vector3(0f, radius, 0f);
         }
      }
   }
}
