using System;
using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Health;
using Unity.VisualScripting;
using UnityEngine;

namespace TopDownCharacter2D.Attacks.Range
{
    /// <summary>
    ///     This script handles the logic of a single bullet
    /// </summary>
    public class RangedAttackController : MonoBehaviour
    {
        [Tooltip("The layer of the walls of the level")] [SerializeField]
        private LayerMask levelCollisionLayer;

        private RangedAttackConfig _config;
        private float _currentDuration;
        private Vector2 _direction;
        private ParticleSystem _impactParticleSystem;
        private bool _isReady;
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private ProjectileManager _projectileManager;


        private bool DestroyOnHit { get; set; } = true;

        public ref Vector2 Direction => ref _direction;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!_isReady)
            {
                return;
            }

            _currentDuration += Time.deltaTime;

            if (_currentDuration > _config.duration)
            {
                DestroyProjectile(transform.position, false);
            }

            _rb.velocity = _direction * _config.speed;
        }

        /// <summary>
        ///     Initializes the ranged attack with the given configuration
        /// </summary>
        /// <param name="direction"> The direction of the attack </param>
        /// <param name="config"> The parameters of the ranged attack</param>
        /// <param name="projectileManager"></param>
        public void InitializeAttack(Vector2 direction, RangedAttackConfig config, ProjectileManager projectileManager)
        {
            _projectileManager = projectileManager;
            _config = config;
            _direction = direction;
            UpdateProjectileSprite();
            _currentDuration = 0f;
            _spriteRenderer.color = config.projectileColor;

            _isReady = true;
        }

        /// <summary>
        ///     Changes the sprite of the projectile according to its size
        /// </summary>
        public void UpdateProjectileSprite()
        {
            transform.localScale = Vector3.one * _config.size;
        }

        /// <summary>
        ///     Destroys the projectile
        /// </summary>
        /// <param name="pos">The position where to create the particles</param>
        /// <param name="createFx">Whether to create particles or not</param>
        public void DestroyProjectile(Vector2 pos, bool createFx)
        {
            if (createFx)
            {
                _projectileManager.CreateImpactParticlesAtPosition(pos, _config);
            }
            gameObject.SetActive(false);
        }
    }
}