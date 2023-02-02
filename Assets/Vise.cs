using UnityEngine;

public class Vise : MonoBehaviour
{

    private Mesh _mesh;
    private const int _arcLevelOfDetail = 10;
    private const float viewDistance = 3f;

    [SerializeField] private float _currentAngle;
    
    private void Awake()
    {
        _mesh = new Mesh();        
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private void Start()
    {
        _mesh = new Mesh();        
        GetComponent<MeshFilter>().mesh = _mesh;
    }
    
    
    public void UpdateTargetAngleWithFloat(float angle)
    {
        _currentAngle = angle*360;
        UpdateVise();
    }

    public void UpdateTargetAngleWithDeg(float angle)
    {
        _currentAngle = 360f / angle;
        UpdateVise();
    }

    public void UpdateVise()
    {       
        float angleIncrease =  _currentAngle / _arcLevelOfDetail;
        _currentAngle -= _currentAngle / 2;
        Vector3 origin = Vector3.zero;
        
        Vector3[] vertices = new Vector3[_arcLevelOfDetail + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[_arcLevelOfDetail * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= _arcLevelOfDetail; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(_currentAngle) * viewDistance;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            _currentAngle -= angleIncrease;
        }
        
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
    }
    
    private static Vector3 GetVectorFromAngle(float a)
    {
        float angleRad = a * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
