using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace LudumDare46.Shared
{
    public class SoundManager
    {
        private readonly ContentManager _contentManager;

        public SoundManager(ContentManager contentManager)
        {
            _contentManager = contentManager;

            Load();
        }


        private void Load()
        {
            Explosion1 = _contentManager.Load<SoundEffect>("Sounds/explosion1");
            Explosion2 = _contentManager.Load<SoundEffect>("Sounds/explosion2");
            Explosion3 = _contentManager.Load<SoundEffect>("Sounds/explosion3");
            Explosion4 = _contentManager.Load<SoundEffect>("Sounds/explosion4");
            Explosion5 = _contentManager.Load<SoundEffect>("Sounds/explosion5");

            Hit1 = _contentManager.Load<SoundEffect>("Sounds/hit1");
            Hit2 = _contentManager.Load<SoundEffect>("Sounds/hit2");
            Hit3 = _contentManager.Load<SoundEffect>("Sounds/hit3");
            Hit4 = _contentManager.Load<SoundEffect>("Sounds/hit4");
            Hit5 = _contentManager.Load<SoundEffect>("Sounds/hit5");

            FlowingRocks = _contentManager.Load<Song>("Music/Flowing Rocks");
            MissionPlausible = _contentManager.Load<Song>("Music/Mission Plausible");
            TimeDriving = _contentManager.Load<Song>("Music/Time Driving");
        }

        public Song TimeDriving { get; set; }

        public Song MissionPlausible { get; set; }

        public Song FlowingRocks { get; set; }

        public SoundEffect Hit5 { get; set; }

        public SoundEffect Hit4 { get; set; }

        public SoundEffect Hit3 { get; set; }

        public SoundEffect Hit2 { get; set; }

        public SoundEffect Hit1 { get; set; }

        public SoundEffect Explosion5
        { get; set; }

        public SoundEffect Explosion4 { get; set; }

        public SoundEffect Explosion3 { get; set; }

        public SoundEffect Explosion2 { get; set; }

        public SoundEffect Explosion1 { get; set; }

        private void Unload()
        {

        }


    }
}
