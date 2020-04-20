using System.Collections.Generic;
using System.Linq;
using LudumDare46.Shared.Enums;
using Microsoft.Xna.Framework;

namespace LudumDare46.Levels
{
    public class TurretState
    {
        private int minX = 40;
        private int minY = 2;
        private int maxX = 60;
        private int maxY = 30;

        public List<TurretStat> TurretStats;

        public TurretState()
        {
            TurretStats = new List<TurretStat>();
        }

        public bool AddPart(Point location, TurretPart part)
        {
            var grid = TurretStats.Where(t => t.x == location.X && t.y == location.Y);

            if (!grid.Any())
            {
                return false;
            }

            var currentPart = grid.First().turretPart;

            if (currentPart == TurretPart.Empty)
            {
                TurretStats.Remove(grid.First());

                TurretStats.Add(new TurretStat
                {
                    x = location.X,
                    y = location.Y,
                    turretPart = part,
                    hasAmmo = false,
                    fireRate = 1,
                    radius = 1,
                    physicalDamage = 1,
                    range = 1,
                    armourPierce = 1,
                    newPart = true
                });

                return true;
            }

            if (currentPart != TurretPart.Empty && part == TurretPart.Empty)
            {
                TurretStats.Remove(grid.First());

                TurretStats.Add(new TurretStat
                {
                    x = location.X,
                    y = location.Y,
                    turretPart = part,
                    hasAmmo = false,
                    fireRate = 1,
                    radius = 1,
                    physicalDamage = 1,
                    range = 1,
                    armourPierce = 1,
                    newPart = true
                });

                return true;
            }

            return false;
        }

        public void CalculateDamage()
        {
            UnloadTurrets();
            ExplosiveAmmoBox();
            FragAmmoBox();
            ApAmmoBox();
            AutoLoader();
            BeltFeeds();
            BarrelExtenders();
            TurretsStatScaling();
        }

        public void TurretsStatScaling()
        {
            var turrets = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.Turret);

            foreach (var turret in turrets)
            {
                turret.physicalDamage = turret.physicalDamage * 15;
                turret.radius = turret.radius > 1 ? turret.radius * 25 : 10;
                turret.fireRate = turret.fireRate * 0.5f;
                turret.range = (turret.range * 100) + 300;
                turret.armourPierce = turret.armourPierce;
            }
        }

        public void UnloadTurrets()
        {
            var turrets = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.Turret);

            foreach (var turret in turrets)
            {
                turret.hasAmmo = false;
            }
        }

        public void ExplosiveAmmoBox()
        {
            var ammos = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.ExplosiveAmmo);

            foreach (var ammo in ammos)
            {
                var loaders = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.AutoLoader
                                                     && (
                                                         (t.x == ammo.x - 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x + 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x && t.y == ammo.y + 1)
                                                         || (t.x == ammo.x && t.y == ammo.y - 1)
                                                     ));

                foreach (var loader in loaders)
                {
                    loader.radius =
                        loader.radius * 1.3f;
                    loader.armourPierce =
                        loader.armourPierce * 0.75f;
                    loader.hasAmmo = true;
                }
            }
        }

        public void FragAmmoBox()
        {
            var ammos = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.FragAmmo);

            foreach (var ammo in ammos)
            {
                var loaders = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.AutoLoader
                                                     && (
                                                         (t.x == ammo.x - 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x + 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x && t.y == ammo.y + 1)
                                                         || (t.x == ammo.x && t.y == ammo.y - 1)
                                                     ));

                foreach (var loader in loaders)
                {
                    loader.radius =
                        loader.radius * 1.25f;
                    loader.physicalDamage =
                        loader.physicalDamage * 0.90f;
                    loader.hasAmmo = true;
                }
            }
        }

        public void ApAmmoBox()
        {
            var ammos = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.APAmmo);

            foreach (var ammo in ammos)
            {
                var loaders = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.AutoLoader
                                                     && (
                                                         (t.x == ammo.x - 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x + 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x && t.y == ammo.y + 1)
                                                         || (t.x == ammo.x && t.y == ammo.y - 1)
                                                     ));

                foreach (var loader in loaders)
                {
                    loader.physicalDamage =
                        loader.physicalDamage * 1.2f;
                    loader.armourPierce =
                        loader.armourPierce * 1.5f;
                    loader.hasAmmo = true;
                }
            }
        }

        public void AutoLoader()
        {
            var loaders = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.AutoLoader);

            foreach (var loader in loaders)
            {
                if (!loader.hasAmmo) continue;

                var turrets = TurretStats.Where(t =>
                    (t.turretPart == Shared.Enums.TurretPart.Turret
                     || t.turretPart == Shared.Enums.TurretPart.BarrelExtender)
                    && (
                        (t.x == loader.x - 1 && t.y == loader.y)
                        || (t.x == loader.x && t.y == loader.y + 1)
                        || (t.x == loader.x && t.y == loader.y - 1)
                    )).ToList();

                var beltFeeds = TurretStats.Where(t =>
                    (t.turretPart == Shared.Enums.TurretPart.BeltFeed)
                    && (
                        (t.x == loader.x - 1 && t.y == loader.y)
                        || (t.x == loader.x + 1 && t.y == loader.y)
                        || (t.x == loader.x && t.y == loader.y + 1)
                        || (t.x == loader.x && t.y == loader.y - 1)
                    )).ToList();

                int targetCount = turrets.Count + beltFeeds.Count;

                foreach (var turret in turrets)
                {
                    turret.physicalDamage =
                        turret.physicalDamage * ((loader.physicalDamage - 1) / targetCount + 1);
                    turret.armourPierce =
                        turret.armourPierce * ((loader.armourPierce - 1) / targetCount + 1);
                    turret.fireRate =
                        turret.fireRate * ((loader.fireRate * 1.5f - 1) / targetCount + 1);
                    turret.radius = turret.radius * ((loader.radius - 1) / targetCount + 1);
                    turret.range = turret.range * ((loader.range - 1) / targetCount + 1);
                    turret.hasAmmo = loader.hasAmmo;
                }

                foreach (var belt in beltFeeds)
                {
                    belt.physicalDamage =
                        belt.physicalDamage * ((loader.physicalDamage - 1) / targetCount + 1);
                    belt.armourPierce =
                        belt.armourPierce * ((loader.armourPierce - 1) / targetCount + 1);
                    belt.fireRate =
                        belt.fireRate * ((loader.fireRate * 1.5f - 1) / targetCount + 1);
                    belt.radius = belt.radius * ((loader.radius - 1) / targetCount + 1);
                    belt.range = belt.range * ((loader.range - 1) / targetCount + 1);
                    belt.hasAmmo = loader.hasAmmo;
                }
            }
        }

        public void BeltFeeds()
        {
            var beltFeeds = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.BeltFeed);


            foreach (var belt in beltFeeds)
            {
                if (!belt.hasAmmo) continue;
                var turrets = TurretStats.Where(t =>
                    (t.turretPart == Shared.Enums.TurretPart.BarrelExtender
                     || t.turretPart == Shared.Enums.TurretPart.Turret)
                    && (
                        (t.x == belt.x - 1 && t.y == belt.y)
                        || (t.x == belt.x + 1 && t.y == belt.y)
                        || (t.x == belt.x && t.y == belt.y + 1)
                        || (t.x == belt.x && t.y == belt.y - 1)
                    )).ToList();

                int targetCount = turrets.Count;

                foreach (var turret in turrets)
                {
                    turret.physicalDamage =
                        turret.physicalDamage * ((belt.physicalDamage - 1) / targetCount + 1);
                    turret.armourPierce =
                        turret.armourPierce * ((belt.armourPierce - 1) / targetCount + 1);
                    turret.fireRate =
                        turret.fireRate * ((belt.fireRate * -1) / targetCount + 1);
                    turret.radius = turret.radius * ((belt.radius - 1) / targetCount + 1);
                    turret.range = turret.range * ((belt.range - 1) / targetCount + 1);
                    turret.hasAmmo = belt.hasAmmo;
                }
            }
        }

        public void BarrelExtenders()
        {
            var barrelExtenders = TurretStats.Where(t => t.turretPart == Shared.Enums.TurretPart.BarrelExtender).ToList();

            barrelExtenders = barrelExtenders.OrderByDescending(b => b.x).ToList();

            foreach (var extender in barrelExtenders)
            {
                if (!extender.hasAmmo) continue;

                var turret = TurretStats.First(t =>
                    (t.turretPart == Shared.Enums.TurretPart.BarrelExtender
                     || t.turretPart == Shared.Enums.TurretPart.Turret)
                    && (
                        (t.x == extender.x - 1 && t.y == extender.y)
                    ));

                turret.physicalDamage =
                    turret.physicalDamage * extender.physicalDamage * 1.5f;
                turret.armourPierce =
                    turret.armourPierce * extender.armourPierce;
                turret.fireRate =
                    turret.fireRate * extender.fireRate * 0.8f;
                turret.radius = turret.radius * extender.radius;
                turret.range = turret.range * extender.range;
                turret.hasAmmo = extender.hasAmmo;
            }
        }
    }

    public class TurretStat
    {
        public int x;
        public int y;
        public Shared.Enums.TurretPart turretPart;
        public bool hasAmmo;
        public float fireRate = 1;
        public float radius = 1;
        public float physicalDamage = 1;
        public float range = 1;
        public float armourPierce = 1;
        public bool newPart = false;

        public TurretStat()
        {
        }

        public TurretStat(int x, int y, TurretPart turretPart)
        {
            this.x = x;
            this.y = y;
            this.turretPart = turretPart;
        }
    }
}