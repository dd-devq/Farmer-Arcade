using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Collider _collider;
    private int _counter;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _counter = Random.Range(1, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Scythe")
        {
            if (_counter == 0)
            {
                other.gameObject.GetComponent<Scythe>().ScytheCallback(SpawnTree(5));
                ResourceManager.Instance.AddWood(3);
                DespawnTree();
            }
            else
            {
                _counter--;
            }
        }
    }

    void DespawnTree()
    {
        transform.gameObject.SetActive(false);
        _collider.enabled = false;
    }

    IEnumerator SpawnTree(int second)
    {
        yield return new WaitForSeconds(second);
        _counter = Random.Range(3, 6);
        transform.gameObject.SetActive(true);
        _collider.enabled = true;
    }
}
