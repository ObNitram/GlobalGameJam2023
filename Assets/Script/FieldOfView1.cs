using UnityEngine;

namespace Script
{
    public class FieldOfView1 : MonoBehaviour
    {
        private float _fov = 90f;
        const int rayCount = 50;
        [SerializeField] float viewDistance = 30f;
        [SerializeField] LayerMask layerMask;

        private float aimAngle;
        private float angle;
        private float angleIncrease;


        private Mesh _mesh;

        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;

        private Vector3 origin;

        private void Start()
        {
            angleIncrease = _fov / rayCount;

            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;

            vertices = new Vector3[rayCount + 2];
            uv = new Vector2[vertices.Length];
            triangles = new int[rayCount * 3];
        }


        private void LateUpdate()
        {
            GenerateMeshFilter();
        }


        private void GenerateMeshFilter()
        {
            angle = aimAngle + _fov / 2f;
            //origin = Vector3.zero;
        

            vertices[0] = origin;

            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance,
                    layerMask);
                //Debug.Log(raycastHit2D);
                //Debug.DrawRay(origin, GetVectorFromAngle(angle) * viewDistance, Color.red);
                if (!raycastHit2D)
                {
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                }
                else
                {
                    vertex = raycastHit2D.point;
                }

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrease;
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        public void SetOrigin(Vector3 origin_)
        {
            this.origin = origin_;
        }

        public void SetAimDirection(Vector3 aimDir)
        {
            aimAngle = GetAngleFromVectorFloat(aimDir);
        }

        private static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        private static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            return n;
        }
    }
}