using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMGR : MonoBehaviour
{
    private Transform m_Target = null;
    private EnemyMGR m_EnemyMGR = null;
    
    [Header("Attributes")]
    public float m_fRange = 15;
    public E_TOWERTYPE m_eType;

    [Header("UseBullet(default)")]
    public GameObject m_Bullet = null;
    public float m_fFireRate = 1f;
    private float m_fFireCoundDown = 0f;

    [Header("Use Laser")]
    public bool m_bUseLaser = false;
    public int m_nDamageOverTime = 30;
    public float m_fSlowPercent = 0.5f;

    public LineRenderer m_LineRenderer = null;
    public ParticleSystem m_ImpactEffect = null;
    public Light m_ImpactLight = null; 

    [Header("Unity Setup Fields")]
    
    public string m_sEnemyTag = "Enemy";

    public Transform m_PartToRotate = null;
    public float m_fTrunSpeed = 10f;

    public Transform m_FirePoint = null;
    public GameObject m_Range;
    // Start is called before the first frame update
    void Start()
    {
        if (m_eType == E_TOWERTYPE.E_CANNON)
            m_fFireRate += PlayerPrefs.GetFloat("UpgradeCannonSpeed", 0);
        else if (m_eType == E_TOWERTYPE.E_MAGIC)
            m_fFireRate += PlayerPrefs.GetFloat("UpgradeMagicSpeed", 0);

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target == null)
        {
            if(m_bUseLaser)
            {
                if (m_LineRenderer.enabled)
                {
                    m_LineRenderer.enabled = false;
                    m_ImpactEffect.Stop();
                    m_ImpactLight.enabled = false;
                }
            }

            return;
        }

        LockOnTargert();

        if (m_bUseLaser)
        {
            Laser();
        }
        else
        {
            if (m_fFireCoundDown <= 0f)
            {
                Shoot();
                m_fFireCoundDown = 1f / m_fFireRate;
            }

            m_fFireCoundDown -= Time.deltaTime;
        }
    }
    private void LockOnTargert()
    {
        Vector3 vDir = m_Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(vDir);

        if (m_PartToRotate != null)
        {
            Vector3 vRotation = Quaternion.Lerp(m_PartToRotate.rotation, lookRotation, Time.deltaTime * m_fTrunSpeed).eulerAngles;
            m_PartToRotate.rotation = Quaternion.Euler(0f, vRotation.y, 0f);
        }

        m_EnemyMGR = m_Target.GetComponent<EnemyMGR>();
    }

    private void Laser()
    {
        int nUpgradeDamage = PlayerPrefs.GetInt("UpgradeLaserDamage", 0);

        m_EnemyMGR.TakeDamage((m_nDamageOverTime + nUpgradeDamage) * Time.deltaTime);
        m_EnemyMGR.Slow(m_fSlowPercent + PlayerPrefs.GetFloat("UpgradeLaserSlow"));
        if (!m_LineRenderer.enabled)
        {
            m_LineRenderer.enabled = true;
            m_ImpactEffect.Play();
            m_ImpactLight.enabled = true;
        }

        m_LineRenderer.SetPosition(0, m_FirePoint.position);
        m_LineRenderer.SetPosition(1, m_Target.position);

        Vector3 vVector = m_FirePoint.position - m_Target.position;
        m_ImpactEffect.transform.rotation = Quaternion.LookRotation(vVector);
        m_ImpactEffect.transform.position = m_Target.position + vVector.normalized * 0.5f;
    }
    private void Shoot()
    {
        int nUpgradeDamage = 0;

        if (m_eType == E_TOWERTYPE.E_CANNON)
            nUpgradeDamage = PlayerPrefs.GetInt("UpgradeCannonDamage", 0);
        else if (m_eType == E_TOWERTYPE.E_MAGIC)
            nUpgradeDamage = PlayerPrefs.GetInt("UpgradeMagicDamage", 0);

        GameObject BulletMGR = (GameObject)Instantiate(m_Bullet, m_FirePoint.position, m_FirePoint.rotation);
        BulletMGR bullet = BulletMGR.GetComponent<BulletMGR>();

        if (bullet != null)
        {
            bullet.AddDamage(nUpgradeDamage);
            bullet.SetTarget(m_Target);
        }
    }
    void UpdateTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(m_sEnemyTag);
        float fShortDistance = Mathf.Infinity;
        GameObject NearestEnemy = null;

        foreach (GameObject Enemy in Enemies)
        {
            float fDistanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);

            if (fDistanceToEnemy < fShortDistance)
            {
                fShortDistance = fDistanceToEnemy;
                NearestEnemy = Enemy;
            }
        }

        if (NearestEnemy != null && fShortDistance <= m_fRange)
        {
            m_Target = NearestEnemy.transform;
        }
        else
        {
            m_Target = null;
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_fRange);
    }

    public void DisplayRange(bool bCheck)
    {
        if (bCheck)
        {
            m_Range.SetActive(true);
        }
        else
        {
            m_Range.SetActive(false);
        }
    }
}
