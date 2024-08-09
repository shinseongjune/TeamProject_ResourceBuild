using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MosterSpawner : MonoBehaviour
{

    public List<NpcController> mosters;
    public LayerMask playerLayer;

    public float radius;
    void Start()
    {
        mosters.Clear();
        // 부모 오브젝트의 자식 개수만큼 반복
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            mosters.Add(child.GetComponentInChildren<NpcController>());
        }
    }
    private void Update()
    {
        
    }
    public bool PlayerDetection()
    {
        Collider[] playersInViewRadius = Physics.OverlapSphere(transform.position, radius, playerLayer);
        foreach (Collider player in playersInViewRadius)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta; 
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
