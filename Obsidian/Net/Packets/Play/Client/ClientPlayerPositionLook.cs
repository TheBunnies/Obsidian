﻿using Obsidian.Serializer.Attributes;
using System;
using Obsidian.Serializer.Enums;
using Obsidian.API;
using System.Threading.Tasks;
using Obsidian.Entities;

namespace Obsidian.Net.Packets.Play.Client
{
    [Flags]
    public enum PositionFlags : sbyte
    {
        X = 0x01,
        Y = 0x02,
        Z = 0x04,
        Y_ROT = 0x08,
        X_ROT = 0x10,
        NONE = 0x00
    }

    public class ClientPlayerPositionLook : IPacket
    {
        [Field(0, true)]
        public Position Position { get; set; }

        [Field(1)]
        public float Yaw { get; set; }

        [Field(2)]
        public float Pitch { get; set; }

        [Field(3, Type = DataType.Byte)]
        public PositionFlags Flags { get; set; } = PositionFlags.X | PositionFlags.Y | PositionFlags.Z;

        [Field(4, Type = DataType.VarInt)]
        public int TeleportId { get; set; }

        public int Id => 0x34;

        public byte[] Data { get; }

        public ClientPlayerPositionLook() { }

        public ClientPlayerPositionLook(byte[] data)  { this.Data = data; }

        public Task WriteAsync(MinecraftStream stream) => Task.CompletedTask;

        public Task ReadAsync(MinecraftStream stream) => Task.CompletedTask;

        public Task HandleAsync(Obsidian.Server server, Player player) => Task.CompletedTask;
    }
    
}