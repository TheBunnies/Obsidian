﻿using Obsidian.Net;
using System.Threading.Tasks;

namespace Obsidian.Entities
{
    public class AgeableMob : PathfinderMob
    {
        public bool Baby { get; set; }

        public override async Task WriteAsync(MinecraftStream stream)
        {
            await base.WriteAsync(stream);

            await stream.WriteEntityMetdata(15, EntityMetadataType.Boolean, this.Baby);
        }
    }
}
