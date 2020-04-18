using LudumDare46.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels
{
    public interface ILevel
    {
        World Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager _textureManager,
            ViewportAdapter viewportAdapter, ContentManager contentManager);
    }
}