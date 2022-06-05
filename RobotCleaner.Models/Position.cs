namespace RobotCleaner.Models
{
    /// <summary>
    /// A Value Object to represent coordinates of a position
    /// </summary>
    public class Position : IEquatable<Position>
    {
        /// <summary>
        /// Create a position object with its required attributes: x, y
        /// </summary>
        /// <param name="x">x coordinate of position object</param>
        /// <param name="y">y coordinate of position object</param>
        public Position(in int x, in int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X coordinate of position object
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y coordinate of position object
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Get the Neighbor position of this point depend on direction
        /// </summary>
        /// <param name="direction">The direction of the neighbor we are looking for</param>
        /// <returns>Neighbor's location as a new position'</returns>
        public Position GetNeighborLocation(Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return new Position(X + 1, Y);
                case Direction.West:
                    return new Position(X - 1, Y);
                case Direction.North:
                    return new Position(X, Y + 1);
                case Direction.South:
                    return new Position(X, Y - 1);
                default:
                    return new Position(X, Y);
            }
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position)obj);
        }
        /// <summary>
        /// Generate a unique hashcode of this point
        /// </summary>
        /// <returns>The unique hashcode of this point</returns>
        public override int GetHashCode()
        {
            return $"{X}-{Y}".GetHashCode();
        }
    }
}
