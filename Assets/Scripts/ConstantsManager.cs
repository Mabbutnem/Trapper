using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsManager : MonoBehaviour
{
   public static Color Border { get; private set; }
   public static Color Player { get; private set; }
   public static Color Red { get; private set; }
   public static Color Yellow { get; private set; }

   public static GameObject DangerBottomRectangle { get; private set; }
   public static GameObject ZeroGravityCube { get; private set; }

   private void Awake()
   {
      Border = Resources.Load<Material>("Materials/Border").color;
      Player = Resources.Load<Material>("Materials/Player").color;
      Red = Resources.Load<Material>("Materials/Red").color;
      Yellow = Resources.Load<Material>("Materials/Yellow").color;

      DangerBottomRectangle = Resources.Load<GameObject>("Prefabs/Danger Bottom Rectangle");
      ZeroGravityCube = Resources.Load<GameObject>("Prefabs/Zero Gravity Cube");
   }
}
