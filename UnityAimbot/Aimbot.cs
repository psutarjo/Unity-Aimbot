using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityAimbot
{

    public class Main : MonoBehaviour
    {
        // Import this mouse event function from Windows Control API to specify mouse movement
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        // If the mouse has moved, indicated by MOUSEEVENTF_MOVE being set, dx and dy hold information about that motion.
        // The information is specified as absolute or relative integer values.
        public enum mouse_flags {
            MOUSEEVENTF_MOVE = 0x0001
        }
        public void Start()
        {

        }
        public void Update()
        {
            float minDistance = Mathf.Infinity;
            Vector2 AimTarget = Vector2.zero;
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                Transform target_transform = target.transform;
                Vector3 vec = new Vector3(0, 0.5f, 0);
                FPSControllerLPFP.FpsControllerLPFP[] controllers = FindObjectsOfType(typeof(FPSControllerLPFP.FpsControllerLPFP)) as FPSControllerLPFP.FpsControllerLPFP[];
                foreach (FPSControllerLPFP.FpsControllerLPFP controller in controllers)
                {
                    AutomaticGunScriptLPFP gun = FindObjectOfType(typeof(AutomaticGunScriptLPFP)) as AutomaticGunScriptLPFP;
                    var gun_transform = gun.gameObject.transform;
                    var player_transform = controller.gameObject.transform;

                    Vector3 viewPos = Camera.main.WorldToViewportPoint(target_transform.position);
                    // If the target is offscreen, we'll have the main camera look at it once
                    if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
                    {
                        controller.gameObject.transform.LookAt(target_transform);
                    }
                }
                // Need to apply offset, since the transform is located at the bottom 
                var pos = Camera.main.WorldToScreenPoint(target_transform.position + vec);
                if (pos.z > -8)
                {
                    float dist = System.Math.Abs(Vector2.Distance(new Vector2(Screen.width - pos.x, Screen.height - pos.y),
                        new Vector2((Screen.width / 2), (Screen.height / 2))));
                    if (dist < 10000 && dist < minDistance)
                    {
                        minDistance = dist;
                        AimTarget = new Vector2(Screen.width - pos.x, Screen.height - pos.y);
                    }
                }

            }

            if (AimTarget != Vector2.zero)
            { 
                double DistX = AimTarget.x - Screen.width / 2.0f;
                double DistY = AimTarget.y - Screen.height / 2.0f;

                DistX /= 20;
                DistY /= 20;

                if (Input.GetButton("Fire2"))
                {
                    mouse_event((int)mouse_flags.MOUSEEVENTF_MOVE, -(int)DistX, (int)DistY, 0, 0);
                }
            }
        }
        public void OnGUI()
        {
        }
    }
}