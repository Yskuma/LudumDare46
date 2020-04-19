﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public Texture2D SciFiUnit06 { get; set; }

        private void Load()
        {
            RoguelikeSheet = _contentManager.Load<Texture2D>("roguelikeSheet_transparent");
            RoguelikeCity = _contentManager.Load<Texture2D>("roguelikeCity_transparent");

            Dirt1 = new TextureRegion2D(RoguelikeSheet, 6 * 17, 0 * 17, 16, 16);
            Dirt2 = new TextureRegion2D(RoguelikeSheet, 6 * 17, 1 * 17, 16, 16);
            Path1 = new TextureRegion2D(RoguelikeSheet, 6 * 17, 2 * 17, 16, 16);

            RoofEdge = new TextureRegion2D(RoguelikeSheet, 13 * 17, 17 * 17, 16, 16);
            RoofTile = new TextureRegion2D(RoguelikeSheet, 14 * 17, 17 * 17, 16, 16);

            SciFiUnit06 = _contentManager.Load<Texture2D>("Units/scifiUnit_06");
        }

        

        private void Unload()
        {

        }


    }
}
