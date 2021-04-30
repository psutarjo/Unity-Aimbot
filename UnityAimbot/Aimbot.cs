using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Hack {

    public class Main : MonoBehaviour
    {

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public void Start()
        {

        }
        public void Update()
        {
            float minDistance = 9999f;
            Vector2 AimTarget = Vector2.zero;
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                Transform target_transform = target.transform;
                var pos = Camera.main.WorldToScreenPoint(target_transform.position);
                if (pos.z > -8)
                {
                    float dist = System.Math.Abs(Vector2.Distance(new Vector2(pos.x, Screen.height - pos.y),
                        new Vector2((Screen.width / 2), (Screen.height / 2))));
                    if (dist < 300 && dist < minDistance)
                    {
                        minDistance = dist;
                        AimTarget = new Vector2(pos.x, Screen.height - pos.y);
                    }
                }

            }

            if (AimTarget != Vector2.zero)
            {
                double DistX = AimTarget.x - Screen.width / 2.0f;
                double DistY = AimTarget.y - Screen.height / 2.0f;

                DistX /= 2;
                DistY /= 2;


                if (Input.GetButton("Fire2"))
                {
                    mouse_event(0x0001, (int)DistX, (int)DistY, 0, 0);
                }

            }

        }
        public void OnGUI()
        {
        }
    }
}


