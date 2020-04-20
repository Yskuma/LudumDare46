using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.TextureAtlases;

namespace LudumDare46.Shared
{
    public class TextureManager
    {
        private readonly ContentManager _contentManager;

        public TextureManager(ContentManager contentManager)
        {
            _contentManager = contentManager;

            Load();
        }

        public Texture2D RoguelikeSheet { get; private set; }
        public Texture2D RoguelikeCity { get; set; }

        public TextureRegion2D Dirt1 { get; private set; }
        public TextureRegion2D Dirt2 { get; private set; }

        public TextureRegion2D Path1 { get; set; }

        public TextureRegion2D Anvil { get; private set; }

        public TextureRegion2D RoofEdge { get; set; }
        public TextureRegion2D RoofTile { get; set; }

        public Texture2D SciFiUnit01 { get; set; }
        public Texture2D SciFiUnit06 { get; set; }
        public Texture2D SciFiUnit07 { get; set; }
        public Texture2D SciFiUnit09 { get; set; }
        public Texture2D SciFiUnit10 { get; set; }

        public Texture2D Turret { get; set; }

        public Texture2D Bullet { get; set; }

        public TextureRegion2D BarrelEnd { get; set; }
        public TextureRegion2D BeltFeed { get; set; }
        public TextureRegion2D BarrelExtender { get; set; }
        public TextureRegion2D AmmoExp { get; set; }
        public TextureRegion2D AmmoFrag { get; set; }
        public TextureRegion2D AmmoAP { get; set; }
        public TextureRegion2D Loader { get; set; }

        public TextureRegion2D Remove { get; set; }

        public Texture2D BulletExplosion { get; set; }
        public Texture2D EnemyExplosion { get; set; }
        public TextureRegion2D TargetingComputer { get; set; }


        private void Load()
        {
            RoguelikeSheet = _contentManager.Load<Texture2D>("roguelikeSheet_transparent");
            RoguelikeCity = _contentManager.Load<Texture2D>("roguelikeCity_transparent");

            Dirt1 = new TextureRegion2D(RoguelikeSheet, 6 * 17, 0 * 17, 16, 16);
            Dirt2 = new TextureRegion2D(RoguelikeSheet, 6 * 17, 1 * 17, 16, 16);
            Path1 = new TextureRegion2D(RoguelikeSheet, 6 * 17, 2 * 17, 16, 16);

            RoofEdge = new TextureRegion2D(RoguelikeSheet, 13 * 17, 17 * 17, 16, 16);
            RoofTile = new TextureRegion2D(RoguelikeSheet, 14 * 17, 17 * 17, 16, 16);

            SciFiUnit01 = _contentManager.Load<Texture2D>("Units/scifiUnit_01");
            SciFiUnit06 = _contentManager.Load<Texture2D>("Units/scifiUnit_06");
            SciFiUnit07 = _contentManager.Load<Texture2D>("Units/scifiUnit_07");
            SciFiUnit09 = _contentManager.Load<Texture2D>("Units/scifiUnit_09");
            SciFiUnit10 = _contentManager.Load<Texture2D>("Units/scifiUnit_10");

            Turret = _contentManager.Load<Texture2D>("turret2");
            Bullet = _contentManager.Load<Texture2D>("bullet");

            BulletExplosion = _contentManager.Load<Texture2D>("bulletExplosion");
            EnemyExplosion = _contentManager.Load<Texture2D>("enemyExplosion");

            //BarrelEnd = new TextureRegion2D(RoguelikeCity, 13 * 17, 16 * 17, 16, 16);
            BarrelEnd = new TextureRegion2D(Turret, 0, -8, 24, 32);
            BeltFeed = new TextureRegion2D(RoguelikeCity, 22 * 17, 9 * 17, 16, 16);
            BarrelExtender = new TextureRegion2D(RoguelikeCity, 10 * 17, 2 * 17, 16, 16);
            //Loader = new TextureRegion2D(RoguelikeCity, 2 * 17, 16 * 17, 16, 16);

            Loader = new TextureRegion2D(_contentManager.Load<Texture2D>("loader"),0,0,16,16);
            AmmoExp = new TextureRegion2D(RoguelikeCity, 10 * 17, 18 * 17, 16, 16);
            AmmoFrag = new TextureRegion2D(RoguelikeCity, 11 * 17, 18 * 17, 16, 16);
            AmmoAP = new TextureRegion2D(RoguelikeCity, 9 * 17, 18 * 17, 16, 16);
            TargetingComputer = new TextureRegion2D(RoguelikeCity, 25 * 17, 8 * 17, 16, 16);
            Remove = new TextureRegion2D(RoguelikeCity, 25 * 17, 5 * 17, 16, 16);

            FontArial = _contentManager.Load<SpriteFont>("Arial");
        }

        public SpriteFont FontArial { get; set; }

        private void Unload()
        {

        }


    }
}
