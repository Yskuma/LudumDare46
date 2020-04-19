﻿using System.Linq;
using System.Collections.Generic;

namespace LudumDare46.Shared.Helpers
{
    public class TurretHelper
    {
        private int minX = 0;
        private int minY = 0;
        private int maxX = 20;
        private int maxY = 30;

        public List<TurretStat> TurretStats;

        public TurretHelper()
        {
            TurretStats = new List<TurretStat>();
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    TurretStats.Add(new TurretStat()
                    {
                        x = x,
                        y = y,
                        turretPart = Enums.TurretPart.Empty,
                        hasAmmo = false,
                        fireRate = 1,
                        radius = 1,
                        physicalDamage = 1,
                        range = 10
                    });
                }
            }
        }

        public void CalculateDamage()
        {
            List<TurretStat> explosiveAmmoBoxes;
            ExplosiveAmmoBox();
            FragAmmoBox();
            ApAmmoBox();
            AutoLoader();
            BeltFeeds();
            BarrelExtenders();
        }

        public void ExplosiveAmmoBox()
        {
            var ammos = TurretStats.Where(t => t.turretPart == Enums.TurretPart.ExplosiveAmmo);

            foreach (var ammo in ammos)
            {
                var loaders = TurretStats.Where(t => t.turretPart == Enums.TurretPart.AutoLoader
                                                     && (
                                                         (t.x == ammo.x - 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x + 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x && t.y == ammo.y + 1)
                                                         || (t.x == ammo.x && t.y == ammo.y - 1)
                                                     ));

                foreach (var loader in loaders)
                {
                    loader.radius =
                        loader.radius * 1.1f;
                    loader.armourPierce =
                        loader.armourPierce * 0.9f;
                    loader.physicalDamage =
                        loader.physicalDamage * 1.02f;
                    loader.hasAmmo = true;
                }
            }
        }

        public void FragAmmoBox()
        {
            var ammos = TurretStats.Where(t => t.turretPart == Enums.TurretPart.FragAmmo);

            foreach (var ammo in ammos)
            {
                var loaders = TurretStats.Where(t => t.turretPart == Enums.TurretPart.AutoLoader
                                                     && (
                                                         (t.x == ammo.x - 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x + 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x && t.y == ammo.y + 1)
                                                         || (t.x == ammo.x && t.y == ammo.y - 1)
                                                     ));

                foreach (var loader in loaders)
                {
                    loader.radius =
                        loader.radius * 1.1f;
                    loader.physicalDamage =
                        loader.physicalDamage * 0.95f;
                    loader.hasAmmo = true;
                }
            }
        }

        public void ApAmmoBox()
        {
            var ammos = TurretStats.Where(t => t.turretPart == Enums.TurretPart.APAmmo);

            foreach (var ammo in ammos)
            {
                var loaders = TurretStats.Where(t => t.turretPart == Enums.TurretPart.AutoLoader
                                                     && (
                                                         (t.x == ammo.x - 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x + 1 && t.y == ammo.y)
                                                         || (t.x == ammo.x && t.y == ammo.y + 1)
                                                         || (t.x == ammo.x && t.y == ammo.y - 1)
                                                     ));

                foreach (var loader in loaders)
                {
                    loader.physicalDamage =
                        loader.physicalDamage * 1.1f;
                    loader.armourPierce =
                        loader.armourPierce * 1.1f;
                    loader.hasAmmo = true;
                }
            }
        }

        public void AutoLoader()
        {
            var loaders = TurretStats.Where(t => t.turretPart == Enums.TurretPart.AutoLoader);

            foreach (var loader in loaders)
            {
                if (!loader.hasAmmo) continue;

                var turrets = TurretStats.Where(t =>
                    (t.turretPart == Enums.TurretPart.Turret
                     || t.turretPart == Enums.TurretPart.BarrelExtender)
                    && (
                        (t.x == loader.x - 1 && t.y == loader.y)
                        || (t.x == loader.x && t.y == loader.y + 1)
                        || (t.x == loader.x && t.y == loader.y - 1)
                    )).ToList();

                var beltFeeds = TurretStats.Where(t =>
                    (t.turretPart == Enums.TurretPart.BeltFeed)
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
                        turret.fireRate * ((loader.fireRate * 1.1f - 1) / targetCount + 1);
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
                        belt.fireRate * ((loader.fireRate * 1.1f - 1) / targetCount + 1);
                    belt.radius = belt.radius * ((loader.radius - 1) / targetCount + 1);
                    belt.range = belt.range * ((loader.range - 1) / targetCount + 1);
                    belt.hasAmmo = loader.hasAmmo;
                }
            }
        }

        public void BeltFeeds()
        {
            var beltFeeds = TurretStats.Where(t => t.turretPart == Enums.TurretPart.BeltFeed);


            foreach (var belt in beltFeeds)
            {
                if(!belt.hasAmmo) continue;
                var turrets = TurretStats.Where(t =>
                    (t.turretPart == Enums.TurretPart.BarrelExtender
                    || t.turretPart == Enums.TurretPart.Turret)
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
                        turret.fireRate * ((belt.fireRate * - 1) / targetCount + 1);
                    turret.radius = turret.radius * ((belt.radius - 1) / targetCount + 1);
                    turret.range = turret.range * ((belt.range - 1) / targetCount + 1);
                    turret.hasAmmo = belt.hasAmmo;
                }
            }
        }

        public void BarrelExtenders()
        {
            var barrelExtenders = TurretStats.Where(t => t.turretPart == Enums.TurretPart.BarrelExtender).ToList();

            barrelExtenders = barrelExtenders.OrderByDescending(b => b.x).ToList();

            foreach (var extender in barrelExtenders)
            {
                if (!extender.hasAmmo) continue;

                var turret = TurretStats.First(t =>
                    (t.turretPart == Enums.TurretPart.BarrelExtender
                     || t.turretPart == Enums.TurretPart.Turret)
                    && (
                        (t.x == extender.x - 1 && t.y == extender.y)
                    ));

                    turret.physicalDamage =
                        turret.physicalDamage * extender.physicalDamage * 1.1f;
                    turret.armourPierce =
                        turret.armourPierce *extender.armourPierce;
                    turret.fireRate =
                        turret.fireRate * extender.fireRate * 0.9f;
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
        public Enums.TurretPart turretPart;
        public bool hasAmmo;
        public float fireRate;
        public float radius;
        public float physicalDamage;
        public float range;
        public float armourPierce;
    }
}