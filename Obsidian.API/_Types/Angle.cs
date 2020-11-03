﻿namespace Obsidian.API
{
    /// <summary>
    /// A class that represents an angle from 0° to 360° degrees.
    /// </summary>
    public struct Angle
    {
        public byte Value { get; set; }
        public float Degrees
        {
            get => Value / 255f * 360f;
            set => Value = (byte)(NormalizeDegree(value) / 360f * 255f);
        }

        public Angle(byte value)
        {
            this.Value = value;
        }

        public static implicit operator Angle(float degree)
        {
            var angle = default(Angle);
            angle.Degrees = degree;
            return angle;
        }

        public static implicit operator float(Angle angle) => angle.Degrees;

        internal static float NormalizeDegree(float degree)
        {
            degree %= 360;
            if (degree < 0)
                degree += 360;

            return degree;
        }
    }
}

