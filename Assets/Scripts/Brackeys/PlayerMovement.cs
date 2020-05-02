using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private CharacterController2DBrackeys controller;

   [SerializeField] private float runSpeed = 40f;
   private float horizontal = 0f;
   private bool jump = false;

   void Update()
   {
      horizontal = runSpeed * Input.GetAxisRaw("Horizontal");

      if(Input.GetButtonDown("Jump")) {
         jump = true;
      }
   }

   void FixedUpdate()
    {
      controller.Move(horizontal * Time.fixedDeltaTime, false, jump);
      jump = false;
    }
}
