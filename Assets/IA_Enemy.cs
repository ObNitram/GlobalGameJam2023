using UnityEngine;

public class IA_Enemy
{
    private readonly int _numberOfRays = 30;
    private readonly float _rayDistance = 3f;
    private Vector2[] _rayDirections;
    private Vector2[] _directions;

    private Transform transform;


    public IA_Enemy(Transform transform)
    {
        this.transform = transform;

        _rayDirections = new Vector2[_numberOfRays];
        _directions = new Vector2[_numberOfRays];
        for (int i = 0; i < _numberOfRays; i++)
        {
            _rayDirections[i] = new Vector2(Mathf.Cos(i * Mathf.PI * 2 / _numberOfRays),
                Mathf.Sin(i * Mathf.PI * 2 / _numberOfRays)).normalized;
        }
    }


    public Vector2 CalculateDirection()
    {
        for (int i = 0; i < _numberOfRays; i++)
        {
            _directions[i] = new Vector2(Mathf.Cos(i * Mathf.PI * 2 / _numberOfRays),
                Mathf.Sin(i * Mathf.PI * 2 / _numberOfRays)).normalized;
        }

        for (int i = 0; i < _numberOfRays; i++)
        {
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, _rayDirections[i], _rayDistance,
                    LayerMask.GetMask("Obstacle", "Player"));

            if (!hit)
            {
                continue;
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                RejectDirection(_rayDirections[i], hit.distance / _rayDistance);
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("scarabScarabEnemy"))
            {
                RejectDirection(_rayDirections[i], hit.distance / _rayDistance);
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                ScalarDirection(_rayDirections[i], hit.distance / _rayDistance);
            }
        }

        
        Vector2 averageDirection = Vector2.zero;
        foreach (var dir in _directions)
        {
            // if (dir.magnitude > averageDirection.magnitude)
            //     averageDirection = dir;
            averageDirection += dir;
        }

        //DebugRays();
        //Debug.DrawRay(transform.position, averageDirection.normalized, Color.red);
        return averageDirection.normalized;
    }


    private void RejectDirection(Vector2 A_direction, float power = 0.5f)
    {
        for (int i = 0; i < _numberOfRays; i++)
        {
            float dotProduct = Vector2.Dot(A_direction, _rayDirections[i]);
            dotProduct = (dotProduct + 1f) / 2f;
            _directions[i] -= _rayDirections[i] * (dotProduct * (1f - power ));
        }
    }

    private void AtractDirection(Vector2 A_direction, float power = 0.5f)
    {
        for (int i = 0; i < _numberOfRays; i++)
        {
            float dotProduct = Vector2.Dot(A_direction, _rayDirections[i]);
            dotProduct = (dotProduct + 1f) / 2f;
            _directions[i] += _rayDirections[i] * (dotProduct * (1f - power) * 2f);
        }
    }

    private void ScalarDirection(Vector2 A_direction, float power = 0.5f)
    {
        A_direction = Vector2.Lerp(Vector2.Perpendicular(A_direction), A_direction, 0.8f); 
        
        for (int i = 0; i < _numberOfRays; i++)
        {
            float dotProduct = Vector2.Dot(A_direction, _rayDirections[i]);
            dotProduct = (dotProduct + 1f) / 2f;
            _directions[i] *= dotProduct * (1f - power);
        }
    }
    

    private void DebugRays()
    {
        for (int i = 0; i < _numberOfRays; i++)
        {
            var position = transform.position;
            //Debug.DrawRay(position, _rayDirections[i] * _rayDistance, Color.blue);
            Debug.DrawRay(position, _directions[i], Color.Lerp(Color.magenta, Color.green, _directions[i].magnitude));
        }
    }
}