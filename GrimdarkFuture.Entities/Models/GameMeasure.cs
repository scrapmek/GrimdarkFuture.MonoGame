using System;

namespace GrimdarkFuture.Mechanics.Models
{
	public class GameMeasure
	{
		public const float GameDistanceMultiplier = 1;
		public const float MillimetersToInch = 25.4f;
		public float GameDistance { get; private set; }
		public float Millimeters
		{
			get => this.GameDistance / GameDistanceMultiplier;
			private set => this.GameDistance = value * GameDistanceMultiplier;
		}

		public float Inches
		{
			get => this.Millimeters / MillimetersToInch;
			private set => this.Millimeters = value * MillimetersToInch;
		}

		public static GameMeasure FromInches(float inches) => new GameMeasure() { Inches = inches };
		public static GameMeasure FromMillimeters(float mm) => new GameMeasure() { Millimeters = mm };
		public static GameMeasure FromGameDisance(float gameDistance) => new GameMeasure() { GameDistance = gameDistance };

		public static GameMeasure operator +(GameMeasure left, GameMeasure right) => FromGameDisance(left.GameDistance + right.GameDistance);
		public static GameMeasure operator -(GameMeasure left, GameMeasure right) => FromGameDisance(left.GameDistance - right.GameDistance);
		public static GameMeasure operator /(GameMeasure left, float right) => FromGameDisance(left.GameDistance / right);
		public static GameMeasure operator *(GameMeasure left, float right) => FromGameDisance(left.GameDistance * right);
		public static GameMeasure operator *(float left, GameMeasure right) => FromGameDisance(left * right.GameDistance);
	}
}