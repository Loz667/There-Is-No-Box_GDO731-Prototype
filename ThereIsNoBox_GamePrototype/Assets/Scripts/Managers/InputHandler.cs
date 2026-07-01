using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;



    public class InputHandler : MonoBehaviour
    {

        public static InputControls Controls;
        static float raycastRadius = 1f;

        void Awake()
        {
            if (Controls != null)
            {
                Destroy(gameObject);
                return;
            }

            Controls = new InputControls();
            DontDestroyOnLoad(gameObject);
        }
        private void OnEnable() => Controls.Enable(); 

        private void OnDisable() => Controls.Disable();
        
        private void OnDestroy()
        {
            if (Controls != null)
            {
                Controls.Dispose();
                Controls = null;
            }
        }

        public static Ray GetMouseRay() => Camera.main.ScreenPointToRay(MousePosition());
        public static Vector3 MousePosition() => Mouse.current.position.ReadValue();
        
        public static RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(InputHandler.GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }
        
        /*
        public static Vector3 GetMouseWorldPosition() 
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit, float.MaxValue, Layers.GridMask))
            {
                return hit.point;
            }
            return Vector3.zero; //TODO what's an appropriate return on error here?
        }
        */
        
    }
