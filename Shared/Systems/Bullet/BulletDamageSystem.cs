﻿using System;
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
    internal class BulletDamageSystem : EntityUpdateSystem
    {
        private ComponentMapper<BulletComponent> _bulletMapper;
        private ComponentMapper<EnemyComponent> _enemyMapper;
        private ComponentMapper<Transform2> _transformMapper;

        public BulletDamageSystem() : base(
            Aspect.One(typeof(BulletComponent), typeof(EnemyComponent)).All(typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _bulletMapper = mapperService.GetMapper<BulletComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
            _enemyMapper = mapperService.GetMapper<EnemyComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;

            var bullets = ActiveEntities.Where(r => _bulletMapper.Has(r));
            var enemies = ActiveEntities.Where(r => _enemyMapper.Has(r));

            foreach (var b in bullets)
            {
                var bullet = _bulletMapper.Get(b);
                var bulletTransform = _transformMapper.Get(b);

                if (!bullet.AtTarget)
                {
                    continue;
                }

                foreach (var e in enemies)
                {
                    var enemyTransform = _transformMapper.Get(e);
                    var d = Vector2.DistanceSquared(bulletTransform.Position, enemyTransform.Position);

                    if (d < MathF.Pow(bullet.Radius,2))
                    { 
                        var enemy = _enemyMapper.Get(e);
                        var reduction = (1 - MathF.Max((enemy.Armour - bullet.ArmourPierce) / enemy.Armour, 0));
                        var damage = (bullet.PhysicalDamage) * reduction;
                        enemy.HP -= damage;

                        var num = CreateEntity();
                        num.Attach(new TextComponent()
                        {
                            Colour = Color.Red,
                            Text = $"{damage:F1}"
                        });
                        num.Attach(new Transform2(new Vector2(enemyTransform.Position.X, enemyTransform.Position.Y),0.0f,Vector2.One));
                        num.Attach(new MovementComponent(Vector2.UnitY * -40));
                        num.Attach(new AutoRemoveComponent(0.5f));

                        Debug.WriteLine($"Damage an enemy for {damage} (Base={bullet.PhysicalDamage}, AP={bullet.ArmourPierce}, Armor={enemy.Armour}, Reduction={reduction}) leaving it on {enemy.HP} HP");
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