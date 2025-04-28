using System;
using System.Linq;
using TMPro;
using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Attacks.Range;
using TopDownCharacter2D.Health;
using Unity.VisualScripting;
using UnityEngine;

namespace TopDownCharacter2D.Controllers
{
    /// <summary>
    ///     A basic controller for an enemy
    /// </summary>
    /// 

    public abstract class TopDownEnemyController : TopDownCharacterController
    {
        [Tooltip("The tag of the target of this enemy")]
        [SerializeField]
        private string targetTag = "Player";

        protected string TargetTag => targetTag;
        protected Transform ClosestTarget { get; private set; }

        public GameObject DamageNumbersPrefab;
        public GameObject statController;

        protected override void Awake()
        {
            base.Awake();
            ClosestTarget = FindClosestTarget();
        }

        protected virtual void FixedUpdate()
        {
            ClosestTarget = FindClosestTarget();
        }

        /// <summary>
        ///     Returns the closest valid target
        /// </summary>
        /// <returns> The transform of the closest target</returns>
        private Transform FindClosestTarget()
        {
            return GameObject.FindGameObjectsWithTag(targetTag)
                .OrderBy(o => Vector3.Distance(o.transform.position, transform.position))
                .First().transform;
        }

        /// <summary>
        ///     Computes and returns the distance to the closest target
        /// </summary>
        /// <returns></returns>
        protected float DistanceToTarget()
        {
            return Vector3.Distance(transform.position, ClosestTarget.transform.position);
        }

        /// <summary>
        ///     Computes and returns the direction toward the closest target
        /// </summary>
        /// <returns></returns>
        protected Vector2 DirectionToTarget()
        {
            return (ClosestTarget.transform.position - transform.position).normalized;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            HealthSystem health = gameObject.GetComponent<HealthSystem>();
            statController stats = statController.GetComponent<statController>();
            double rand = new System.Random().NextDouble();

            if (other.tag == "PlayerBullet")
            {
                other.GameObject().SetActive(false);

                //Debug.Log("enemy damage");

                float totalDamage = stats.damage;

                if (rand <= stats.critChance) { totalDamage *= stats.critMultiplier; }

                if (DamageNumbersPrefab != null) { DisplayDamageNumbers(totalDamage, gameObject); }

                health.ChangeHealth(-totalDamage);
                TopDownKnockBack knockBack = this.GetComponent<TopDownKnockBack>();
                if (knockBack != null)
                {
                    knockBack.ApplyKnockBack(transform);
                }
            }

            if (other.tag == "Player")
            {
                if (rand <= stats.evasionChance)
                {
                    if (DamageNumbersPrefab != null) { DisplayDamageNumbers("dodged", other.gameObject); }
                }
                else
                {
                    float totalDamage = stats.enemyDamage * (1f - stats.armor);
                    if (DamageNumbersPrefab != null) { DisplayDamageNumbers(totalDamage, other.gameObject); }
                }
            }
        }


        private void DisplayDamageNumbers(float totalDamage, GameObject target)
        {
            var damNum = Instantiate(DamageNumbersPrefab, target.transform.position, Quaternion.identity);
            damNum.GetComponent<TextMeshPro>().text = totalDamage.ToString();
        }
        private void DisplayDamageNumbers(string str, GameObject target)
        {
            var damNum = Instantiate(DamageNumbersPrefab, target.transform.position, Quaternion.identity);
            damNum.GetComponent<TextMeshPro>().text = str;
        }
    }
}