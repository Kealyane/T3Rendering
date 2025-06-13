using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Portal : MonoBehaviour
    {
        public Portal linkedPortal;
        public MeshRenderer screen;
        [SerializeField]
        Camera playerCamera;
        [SerializeField]
        Camera portalCamera;
        
        RenderTexture viewTexture;
        
        void CreateViewTexture()
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }
            if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
            {
                viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
                portalCamera.targetTexture = viewTexture;
                linkedPortal.screen.material.SetTexture("_MainTex", viewTexture);
            }
        }

        private void LateUpdate()
        {
            Render();
        }
        
        public void Render()
        {
            screen.enabled = false;
            CreateViewTexture();
            var m = transform.localToWorldMatrix *
                    linkedPortal.transform.worldToLocalMatrix *
                    playerCamera.transform.localToWorldMatrix;
            portalCamera.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
            portalCamera.Render();
            screen.enabled = true;
        }
    }
}