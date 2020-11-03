﻿using Obsidian.Entities;
using Obsidian.Nbt;
using Obsidian.Nbt.Tags;
using Obsidian.Util.Collection;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Obsidian.WorldData
{
    public class Region
    {
        private bool cancel = false;
        public int X { get; }
        public int Z { get; }

        public ConcurrentDictionary<int, Entity> Entities { get; private set; } = new ConcurrentDictionary<int, Entity>();

        public DenseCollection<Chunk> LoadedChunks { get; private set; } = new DenseCollection<Chunk>(32, 32);

        internal Region(int x, int z)
        {
            this.X = x;
            this.Z = z;
        }

        internal async Task BeginTickAsync(CancellationToken cts)
        {
            while (!cts.IsCancellationRequested || cancel)
            {
                await Task.Delay(20);

                foreach (var (_, entity) in this.Entities)
                    await entity.TickAsync();
            }
        }

        internal void Cancel() => this.cancel = true;

        internal void Serialize(int serverId)
        {
            var serverDir = $"Server-{serverId}";
            var fileName = $"r.{X}.{Z}.mca";
            var regionPath = Path.Combine(serverDir, fileName);

            if (File.Exists(regionPath))
            {
                // Do amazing things to update the current region.
                SerializeToDisk(regionPath);
            }
            else
            {
                SerializeToDisk(regionPath);
            }
        }

        private void SerializeToDisk(string regionPath)
        {
            var nbtFile = new NbtFile(NbtCompound.GetCanonicalTagName(NbtTagType.Byte));
        }
    }
}
