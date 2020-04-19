using System;
using System.Collections.Generic;
using System.Text;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.EnemyComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace LudumDare46.Shared.Systems
{
    class EnemySpawnSystem : EntityUpdateSystem
    {

        private readonly TextureManager _textureManager;
        

        private double TimeUntilNextSpawn = 0;
        private List<Rectangle> _spawnAreas;
        //private EnemyFactory _enemyFactory;
        private Random _random;

        public EnemySpawnSystem(TextureManager textureManager, List<Rectangle> spawnAreas) : base(new AspectBuilder())
        {
            _textureManager = textureManager;
            _spawnAreas = spawnAreas;
            //_enemyFactory = new EnemyFactory();
            _random = new Random(1983);
        }
        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds > TimeUntilNextSpawn)
            {
                var spawnIndex = _random.Next(_spawnAreas.Count);
                var spawnX = (float)(_random.NextDouble() * _spawnAreas[spawnIndex].Width) + _spawnAreas[spawnIndex].X;
                var spawnY = (float)(_random.NextDouble() * _spawnAreas[spawnIndex].Height) + _spawnAreas[spawnIndex].Y;

                var e = CreateEntity();
                e.Attach(new Sprite(_textureManager.SciFiUnit06));
                e.Attach(new Transform2(spawnX, spawnY,0.0F, 1.0F, 1.0F));
                e.Attach(new MovementComponent(Vector2.UnitX * 40));
                e.Attach(new EnemyComponent());

                TimeUntilNextSpawn = TimeUntilNextSpawn + 5;
            }


        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            
        }
    }
}
