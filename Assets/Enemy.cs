using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    private NavMeshAgent _agent;
    public Transform player;

    public int _life = 100;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(player.position);
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(player.position);
        
        
    }

    
        

    public void Damage(int damage)
    {
        _life -= damage;
        
        if(_life <= 0)Destroy(gameObject);
    }
    
    
}
