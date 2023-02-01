using Script.Enemy;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float fov = 90f;
    [SerializeField] int rayCount = 50;
    [SerializeField] float viewDistance = 10f;

    [SerializeField] private EnemyBase _enemyBase;

    private float angle;
    private float angleIncrease;

    private Mesh _mesh;


    private void Start()
    {
        angle = 0;
        angleIncrease = fov / rayCount;
        _mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = _mesh;
    }


    private void Update()
    {
        foreach (var enemy in _enemyBase.enemys)
        {
            SpotEnemy(enemy);
        }
    }

    private void GenerateMeshFilter()
    {
        _mesh.Clear();
        Vector3 origin = Vector3.zero;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            //Physics2D.Raycast(origin,)
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


    private void SpotEnemy(Enemy enemy)
    {
        if (enemy == null)
        {
            return;
        }

        var transform1 = transform;
        var enemyTransform = enemy.transform;

        Vector3 dirToEnemy = (enemyTransform.position - transform1.position).normalized;
        float angleBetween = Vector3.Angle(transform1.up, dirToEnemy);
        if (angleBetween < fov / 2f)
        {
            float distanceToEnemy = Vector3.Distance(transform1.position, enemyTransform.position);
            if (!Physics2D.Raycast(transform1.position, dirToEnemy, distanceToEnemy, LayerMask.GetMask("Obstacle")))
            {
                enemy.HasBeenSpoted();
            }

            //Debug.DrawRay(transform1.position, dirToEnemy * distanceToEnemy, Color.red);
        }
    }


    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}