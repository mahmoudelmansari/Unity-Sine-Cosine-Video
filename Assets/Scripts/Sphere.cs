using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] GameObject particles;
    float speed;
    float time;
    bool increaseTime = true;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(8f, 15f);  
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (increaseTime == false)
            return;

        time += Time.deltaTime;
        float sinOutput = Mathf.Sin(time * speed) * 0.8f + 0.8f;
        transform.position = new Vector3(transform.position.x, sinOutput, transform.position.z);
        if (transform.position.y <= 0.05)
            SpawnParticles();
    }

    void SpawnParticles()
    {
        Instantiate(particles, transform.position - Vector3.up * 0.5f, particles.transform.rotation);
    }

    public void RemoveSphere() => StartCoroutine(DeletSphere());

    IEnumerator DeletSphere()
    {
        increaseTime = false;
        yield return new WaitForSeconds(1f);
        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce);
    }
}
