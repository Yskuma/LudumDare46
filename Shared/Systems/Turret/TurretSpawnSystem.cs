using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.EnemyComponents;
using LudumDare46.Shared.Components.TurretComponents;
using LudumDare46.Shared.Enums;
using LudumDare46.Shared.Helpers;
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
        private TurretHelper _turretHelper;
        private List<Rectangle> _spawnAreas;
        private Random _random;

        public TurretSpawnSystem(TextureManager textureManager, TurretHelper turretHelper) : base(new AspectBuilder())
        {
            _textureManager = textureManager;
            _turretHelper = turretHelper;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            var newTurrets = _turretHelper.TurretStats.Where(t => t.newPart);
            
            foreach (var turret in newTurrets)
            {
                switch (turret.turretPart)
                {
                    case TurretPart.Turret:
                        turret.newPart = false;
                        var e = CreateEntity();
                        e.Attach(new Sprite(_textureManager.Turret));
                        e.Attach(new Transform2(turret.x * 16, turret.y * 16, 0.0F, 1.0F, 1.0F));
                        e.Attach(new TurretComponent(400, 1.0f / 2)
                        {
                            PhysicalDamage = 5
                        });
                        break;
                }
            }
        }
    }
}
