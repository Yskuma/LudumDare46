using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LudumDare46.Levels;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.EnemyComponents;
using LudumDare46.Shared.Components.TurretComponents;
using LudumDare46.Shared.Enums;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using MonoGame.Extended.Sprites;

namespace LudumDare46.Shared.Systems.Turret
{
    class TurretSpawnSystem : EntityUpdateSystem
    {
        private readonly TextureManager _textureManager;
        private TurretState _turretState;
        private ComponentMapper<TurretComponent> _turretMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private List<Rectangle> _spawnAreas;
        private Random _random;

        public TurretSpawnSystem(TextureManager textureManager, TurretState turretState) 
            : base(Aspect.All( typeof(Transform2), typeof(TurretPartComponent)))
        {
            _textureManager = textureManager;
            _turretState = turretState;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _turretMapper = mapperService.GetMapper<TurretComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Update(GameTime gameTime)
        {
            var newTurrets = _turretState.TurretStats.Where(t => t.newPart);

            foreach (var turret in newTurrets)
            {
                turret.newPart = false;
                var tempx = turret.x * 16 + 8;
                var tempy = turret.y * 16 + 8;

                var e = CreateEntity();

                switch (turret.turretPart)
                {
                    case TurretPart.Turret:
                        e.Attach(new Sprite(_textureManager.Turret));
                        e.Attach(new Transform2(tempx, tempy, 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        e.Attach(new TurretComponent(turret.range, 0)
                        {
                            FireRate = turret.fireRate,
                            PhysicalDamage = turret.physicalDamage,
                            Radius = turret.radius,
                            HasAmmo = turret.hasAmmo,
                            ArmourPierce = turret.armourPierce
                        });
                        break;
                    case TurretPart.BarrelExtender:
                        e.Attach(new Sprite(_textureManager.BarrelExtender));
                        e.Attach(new Transform2(tempx , tempy , 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        break;
                    case TurretPart.BeltFeed:
                        e.Attach(new Sprite(_textureManager.BeltFeed));
                        e.Attach(new Transform2(tempx , tempy , 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        break;
                    case TurretPart.AutoLoader:
                        e.Attach(new Sprite(_textureManager.Loader));
                        e.Attach(new Transform2(tempx , tempy , 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        break;
                    case TurretPart.APAmmo:
                        e.Attach(new Sprite(_textureManager.AmmoAP));
                        e.Attach(new Transform2(tempx, tempy , 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        break;
                    case TurretPart.FragAmmo:
                        e.Attach(new Sprite(_textureManager.AmmoFrag));
                        e.Attach(new Transform2(tempx, tempy, 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        break;
                    case TurretPart.ExplosiveAmmo:
                        e.Attach(new Sprite(_textureManager.AmmoExp));
                        e.Attach(new Transform2(tempx, tempy, 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        break;
                    case TurretPart.TargetingComputer:
                        e.Attach(new Sprite(_textureManager.TargetingComputer));
                        e.Attach(new Transform2(tempx, tempy, 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretPartComponent());
                        break;
                    case TurretPart.Empty:
                        foreach (var entity in ActiveEntities)
                        {
                            var transform = _transformMapper.Get(entity);
                            
                                if (transform.Position.X > tempx - 1
                                    && transform.Position.X < tempx + 1
                                    && transform.Position.Y > tempy - 1
                                    && transform.Position.Y < tempy + 1)
                                {
                                    DestroyEntity(entity);
                                }
                            
                        }

                        break;
                }
            }
        }
    }
}