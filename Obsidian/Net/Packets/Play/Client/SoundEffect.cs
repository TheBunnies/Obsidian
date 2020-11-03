﻿using Obsidian.Serializer.Attributes;
using Obsidian.Serializer.Enums;
using Obsidian.API;
using Obsidian.API;

namespace Obsidian.Net.Packets.Play.Client
{
    public class SoundEffect : Packet
    {
        [Field(0, Type = DataType.VarInt)]
        public int SoundId { get; set; }

        [Field(1, Type = DataType.VarInt)]
        public SoundCategory Category { get; set; }

        [Field(2)]
        public SoundPosition Position { get; set; }

        [Field(3)]
        public float Volume { get; set; }

        [Field(4)]
        public float Pitch { get; set; }

        public SoundEffect(int soundId, SoundPosition position, SoundCategory category = SoundCategory.Master, float pitch = 1.0f, float volume = 1f) : base(0x51)
        {
            this.SoundId = soundId;
            this.Position = position;
            this.Category = category;
            this.Pitch = pitch;
            this.Volume = volume;
        }
    }
}