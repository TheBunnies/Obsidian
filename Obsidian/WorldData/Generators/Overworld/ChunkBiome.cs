﻿using Obsidian.ChunkData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Obsidian.WorldData.Generators.Overworld
{
    public enum Temp
    {
        hot,
        warm,
        cold,
        freezing
    }

    public enum Humidity
    {
        wet,
        neutral,
        dry
    }

    public static class ChunkBiome
    {
        public static Biomes GetBiome(int worldX, int worldZ, OverworldNoise noiseGen)
        {
            Temp t;
            double temperature = noiseGen.GetBiomeTemp(worldX, 0, worldZ);
            if (temperature > 0.8) { t = Temp.hot; }
            else if (temperature > 0.45) { t = Temp.warm; }
            else if (temperature > -0.2) { t = Temp.cold; }
            else { t = Temp.freezing; }

            Humidity h;
            double humidity = noiseGen.GetBiomeHumidity(worldX, 0, worldZ);
            if (humidity > 0.33) { h = Humidity.dry; }
            else if (humidity > -0.33) { h = Humidity.neutral; }
            else { h = Humidity.wet; }

            Biomes b = Biomes.Nether;
            // River
            if (noiseGen.isRiver(worldX, worldZ))
            {
                b = t switch
                {
                    Temp.hot => Biomes.River,
                    Temp.warm => Biomes.River,
                    Temp.cold => Biomes.River,
                    Temp.freezing => Biomes.FrozenRiver
                };
            }
            // Mountain
            else if (noiseGen.isMountain(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        b = h switch
                        {
                            Humidity.wet => Biomes.Mountains,
                            Humidity.neutral => Biomes.Mountains,
                            Humidity.dry => Biomes.ErodedBadlands
                        }; break;
                    case Temp.warm:
                        b = h switch
                        {
                            Humidity.wet => Biomes.TallBirchHills,
                            Humidity.neutral => Biomes.Mountains,
                            Humidity.dry => Biomes.WoodedBadlandsPlateau
                        }; break;
                    case Temp.cold:
                        b = h switch
                        {
                            Humidity.wet => Biomes.SnowyTaigaMountains,
                            Humidity.neutral => Biomes.WoodedMountains,
                            Humidity.dry => Biomes.Mountains
                        }; break;
                    case Temp.freezing:
                        b = h switch
                        {
                            Humidity.wet => Biomes.SnowyTaigaMountains,
                            Humidity.neutral => Biomes.SnowyMountains,
                            Humidity.dry => Biomes.GravellyMountains
                        }; break;
                }
            } 
            // Badlands/Foothills
            else if (noiseGen.isBadlands(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        b = h switch
                        {
                            Humidity.wet => Biomes.BambooJungle,
                            Humidity.neutral => Biomes.BadlandsPlateau,
                            Humidity.dry => Biomes.ErodedBadlands
                        }; break;
                    case Temp.warm:
                        b = h switch
                        {
                            Humidity.wet => Biomes.BirchForestHills,
                            Humidity.neutral => Biomes.DarkForestHills,
                            Humidity.dry => Biomes.GiantSpruceTaigaHills
                        }; break;
                    case Temp.cold:
                        b = h switch
                        {
                            Humidity.wet => Biomes.SnowyTaigaMountains,
                            Humidity.neutral => Biomes.DarkForestHills,
                            Humidity.dry => Biomes.WoodedHills
                        }; break;
                    case Temp.freezing:
                        b = h switch
                        {
                            Humidity.wet => Biomes.SnowyTaigaMountains,
                            Humidity.neutral => Biomes.SnowyMountains,
                            Humidity.dry => Biomes.GravellyMountains
                        }; break;
                }
            }
            // Hills
            else if (noiseGen.isHills(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        b = h switch
                        {
                            Humidity.wet => Biomes.Jungle,
                            Humidity.neutral => Biomes.BadlandsPlateau,
                            Humidity.dry => Biomes.DesertHills
                        }; break;
                    case Temp.warm:
                        b = h switch
                        {
                            Humidity.wet => Biomes.TallBirchForest,
                            Humidity.neutral => Biomes.DarkForest,
                            Humidity.dry => Biomes.GiantSpruceTaiga
                        }; break;
                    case Temp.cold:
                        b = h switch
                        {
                            Humidity.wet => Biomes.GiantSpruceTaiga,
                            Humidity.neutral => Biomes.FlowerForest,
                            Humidity.dry => Biomes.Forest
                        }; break;
                    case Temp.freezing:
                        b = h switch
                        {
                            Humidity.wet => Biomes.SnowyTaigaMountains,
                            Humidity.neutral => Biomes.SnowyMountains,
                            Humidity.dry => Biomes.GravellyMountains
                        }; break;
                }
            }
            //Plains
            else if (noiseGen.isPlains(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        b = h switch
                        {
                            Humidity.wet => Biomes.Swamp,
                            Humidity.neutral => Biomes.Badlands,
                            Humidity.dry => Biomes.Desert
                        }; break;
                    case Temp.warm:
                        b = h switch
                        {
                            Humidity.wet => Biomes.BirchForest,
                            Humidity.neutral => Biomes.Plains,
                            Humidity.dry => Biomes.Savanna
                        }; break;
                    case Temp.cold:
                        b = h switch
                        {
                            Humidity.wet => Biomes.GiantSpruceTaiga,
                            Humidity.neutral => Biomes.SnowyTaiga,
                            Humidity.dry => Biomes.Plains
                        }; break;
                    case Temp.freezing:
                        b = h switch
                        {
                            Humidity.wet => Biomes.IceSpikes,
                            Humidity.neutral => Biomes.SnowyTundra,
                            Humidity.dry => Biomes.SnowyBeach
                        }; break;
                }
            }
            // Ocean
            
            else if (noiseGen.isOcean(worldX, worldZ))
            {
                switch (t)
                {
                    case Temp.hot:
                        b = Biomes.WarmOcean;
                        break;
                    case Temp.warm:
                        b = Biomes.LukewarmOcean;
                        break;
                    case Temp.cold:
                        b = Biomes.ColdOcean;
                        break;
                    case Temp.freezing:
                        b = Biomes.FrozenOcean;
                        break;
                }
            }

            else { b = Biomes.Plains; }
            return b;
        }
    }
}
