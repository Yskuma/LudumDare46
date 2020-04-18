using LudumDare46.Shared;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels
{
    public interface ILevel
    {
        World Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager _textureManager,
            ViewportAdapter viewportAdapter);
    }
}