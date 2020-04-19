using System;
using System.Diagnostics;
using System.Linq;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.BulletComponents;
using LudumDare46.Shared.Components.EnemyComponents;
using LudumDare46.Shared.Components.TurretComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems
{
    internal class TurretAimSystem : EntityUpdateSystem
    {
        private ComponentMapper<TurretComponent> _turretMapper;
        private ComponentMapper<EnemyComponent> _enemyMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<MovementComponent> _movementMapper;

        private readonly TextureManager _textureManager;

        public TurretAimSystem(TextureManager textureManager) : base(
            Aspect.One(typeof(TurretComponent), typeof(EnemyComponent)).All(typeof(Transform2)))
        {
            _textureManager = textureManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _turretMapper = mapperService.GetMapper<TurretComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
            _enemyMapper = mapperService.GetMapper<EnemyComponent>();
            _movementMapper = mapperService.GetMapper<MovementComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;

            var turrets = ActiveEntities.Where(r => _turretMapper.Has(r));
            var enemies = ActiveEntities.Where(r => _enemyMapper.Has(r));

            foreach (var t in turrets)
            {
                //var turret = _turretMapper.Get(t);
                var turretTransform = _transformMapper.Get(t);
                var turret = _turretMapper.Get(t);

                turret.ReloadTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                int? nearestEnemy = null;
                float distance = float.MaxValue;

                foreach (var e in enemies)
                {
                    var enemyTransform = _transformMapper.Get(e);
                    var d = Vector2.DistanceSquared(turretTransform.Position, enemyTransform.Position);

                    if (d < distance)
                    {
                        distance = d;
                        nearestEnemy = e;
                    }
                }

                // Point at nearest
                if (nearestEnemy.HasValue)
                {
                    var enemyTransform = _transformMapper.Get(nearestEnemy.Value);
                    var enemyMovement = _movementMapper.Get(nearestEnemy.Value);

                    turretTransform.Rotation = MathF.Atan2(turretTransform.Position.Y - enemyTransform.Position.Y,
                        turretTransform.Position.X - enemyTransform.Position.X);

                    if (turret.ReloadTimeRemaining < 0)
                    {
                        var bulletSpeed = 400f;
                        var targetPosition = new Vector2(enemyTransform.Position.X, enemyTransform.Position.Y);
                        var targetTime = (targetPosition - turretTransform.Position).Length() / bulletSpeed;
                        targetPosition.X = targetPosition.X + (enemyMovement.Speed.X * targetTime);

                        var bullet = CreateEntity();
                        bullet.Attach(new Sprite(_textureManager.Bullet));
                        bullet.Attach(new BulletComponent()
                        {
                            TargetPosition = targetPosition,
                            Radius = turret.Radius,
                            PhysicalDamage = turret.PhysicalDamage,
                            ArmourPierce = turret.ArmourPierce,
                            Speed = bulletSpeed
                        });
                        bullet.Attach(new Transform2(turretTransform.Position.X, turretTransform.Position.Y));
                        var mov = Vector2.Normalize(targetPosition - turretTransform.Position) * bulletSpeed;
                        bullet.Attach(new MovementComponent(mov));

                        turret.ReloadTimeRemaining = 1.0 / turret.FireRate;
                    }
                }
            }
        }


        private void FaceNearestEnemy(Transform2 transform)
        {
            transform.Rotation = (float) (1.5 * Math.PI);
        }
    }
}