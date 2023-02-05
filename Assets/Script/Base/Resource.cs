using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Resource : MonoBehaviour
{
   private static Resource instance;
   public static Resource Instance
   {
      get
      {
         if (instance == null)
         {
            instance = FindObjectOfType<Resource>();
         }
         return instance;
      }
   }
   
   public int ressource = 0;
   
   
   public void AddRessource(int value)
   {
      ressource += value;
   }
   
   public void RemoveRessource(int value)
   {
      ressource -= value;
   }
   
   
   
    
}