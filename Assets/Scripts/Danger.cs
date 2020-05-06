using UnityEngine;

public class Danger : MonoBehaviour
{
   public static readonly string DANGER_TAG = "Danger";
   private static readonly Color harmlessColor = Color.white;

   [SerializeField] private float harmlessTime = 3f;
   [SerializeField] [Tooltip("Set to -1 if it leaves indefinitely")] private float lifeTime = -1f;
   [SerializeField] private bool canBeHarmless = true;
   [SerializeField] public bool removeOutOfLimit = true;

   public void Awake()
   {
      if (lifeTime >= 0) { Destroy(gameObject, lifeTime); }
   }

   public void MakeHarmless(bool destroy)
   {
      if (canBeHarmless)
      {
         gameObject.tag = "Untagged";
         Renderer renderer = gameObject.GetComponent<Renderer>();
         if (renderer)
         {
            renderer.material.color = harmlessColor;
         }
         if (destroy)
         {
            Destroy(gameObject, harmlessTime);
         }
         else
         {
            Invoke("MakeDangerous", harmlessTime);
         }
      }
   }

   public void MakeDangerous()
   {
      gameObject.tag = DANGER_TAG;
      Renderer renderer = gameObject.GetComponent<Renderer>();
      if(renderer)
      {
         renderer.material.color = ConstantsManager.Red;
      }
   }

   public void MakeDangerousNoColor()
   {
      gameObject.tag = DANGER_TAG;
   }

   public void MakeDangerousYellow()
   {
      gameObject.tag = DANGER_TAG;
      Renderer renderer = gameObject.GetComponent<Renderer>();
      if (renderer)
      {
         renderer.material.color = ConstantsManager.Yellow;
      }
   }

   public void MakeHarmlessBorder()
   {
      gameObject.tag = "Untagged";
      Renderer renderer = gameObject.GetComponent<Renderer>();
      if (renderer)
      {
         renderer.material.color = ConstantsManager.Border;
      }
   }
}
