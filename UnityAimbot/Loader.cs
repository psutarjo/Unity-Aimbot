using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityAimbot
{
    public class Loader
    {
        public static GameObject load_object;

        public static void Load()
        {
            load_object = new GameObject();
            load_object.AddComponent<Main>();
            UnityEngine.Object.DontDestroyOnLoad(load_object);

        }
        public static void Unload()
        {
            GameObject.Destroy(load_object);
        }
    }
}