using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace  UC
{
    public class UC_Mesh : MonoBehaviour
    {
        [SerializeField] private GameObject mesh_Face;
        [SerializeField] private GameObject mesh_Body;



        public void MeshControl(string _Mode)
        {
            switch (_Mode)
            {
                case "First": 
                    mesh_Face.SetActive(false);
                    mesh_Body.SetActive(false);
                    break;
                case"Third":
                    mesh_Face.SetActive(true);
                    mesh_Body.SetActive(true);
                    break;
            }
        }

        private bool _isActive;
        public bool BodyMesh
        {
            get
            {
                return _isActive;
            }
            set
            {
                mesh_Body.SetActive(value);
            }
        }
    }
    
}
