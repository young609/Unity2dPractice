using UnityEngine;

namespace Study
{
    public class StudyPolygon : MonoBehaviour
    {
        private void Start()
        {
            var newObject = new GameObject("Polygon");
            var mesh = new Mesh();
            Vector3[] vertices = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 1, 0),
            };

            var triangles = new int[]
            {
                0, 2, 1,
                2, 3, 1,
            };
            // 3d 소프트마다 clockWise(CW), Counter-ClockWise(CCW) 중 하나를 앞면으로 취급함.
            // opengl = CCW, direct3D = CW, graphics api에 맞춰서 순서를 구성해줘야 함.

            var uv = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
            }; // 메쉬와 텍스쳐를 연결하는데 필요함. 각 정점마다 하나의 uv좌표를 가짐.
            
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            
            var meshFilter = newObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            
            var meshRenderer = newObject.AddComponent<MeshRenderer>();
            // meshRenderer.material = new Material(Shader.Find("Standard"));
            meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }
    }
}