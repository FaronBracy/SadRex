namespace SadRex
{
   /// <summary>
   /// A RexPaint layer cell.
   /// </summary>
   public struct RexCell
   {
      /// <summary>
      /// The character for the cell.
      /// </summary>
      public int Character;

      /// <summary>
      /// The foreground color of the cell.
      /// </summary>
      public Color Foreground;

      /// <summary>
      /// The background color of the cell.
      /// </summary>
      public Color Background;

      public RexCell( int character, Color foreground, Color background )
      {
         Character = character;
         Foreground = foreground;
         Background = background;
      }

      /// <summary>
      /// Returns true when the current color is considered transparent.
      /// </summary>
      /// <returns>True when transparent.</returns>
      public bool IsTransparent()
      {
         return Background == Color.Transparent;
      }

      public bool Equals( RexCell other )
      {
         return Character == other.Character && Foreground.Equals( other.Foreground ) && Background.Equals( other.Background );
      }

      public override bool Equals( object obj )
      {
         return obj is RexCell other && Equals( other );
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = Character;
            hashCode = ( hashCode * 397 ) ^ Foreground.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ Background.GetHashCode();
            return hashCode;
         }
      }
   }
}
