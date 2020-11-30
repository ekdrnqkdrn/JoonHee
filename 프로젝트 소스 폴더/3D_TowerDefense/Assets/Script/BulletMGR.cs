using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMGR : MonoBehaviour
{
    private Transform m_Target = null;
    
    public float m_fSpped = 70f;
    public float m_fExplosionRadius = 0;
    public int m_nDamage = 50;

    public GameObject m_ImpactEffectMGR = null;

    public void AddDamage(int nDamage)
    {
        m_nDamage += nDamage;
    }

    public void SetTarget (Transform Target)
    {
        m_Target = Target;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 vDir = m_Target.position - transform.position;
        float fDistanceThisFrame = m_fSpped * Time.deltaTime;
        
        if(vDir.magnitude <= fDistanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(vDir.normalized * fDistanceThisFrame, Space.World);
        transform.LookAt(m_Target);
    }

    private void HitTarget()
    {
        GameObject EffectIns =  (GameObject)Instantiate(m_ImpactEffectMGR, transform.position, transform.rotation);
        Destroy(EffectIns, 2f);

        if (m_fExplosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(m_Target);
        }

        Destroy(gameObject);
    }

    void Damage(Transform Enemy)
    {
        EnemyMGR TempObj = Enemy.GetComponent<EnemyMGR>();

        if (TempObj != null)
        {
            TempObj.TakeDamage(m_nDamage);
        }
    }

    void Explode()
    {
        //충돌시 해당 범위안에 오브젝트를 배열로 변환해줌
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fExplosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_fExplosionRadius);
    }
}
