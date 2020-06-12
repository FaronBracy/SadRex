namespace SadRex
{
   /// <summary>
   /// A RexPaint color.
   /// </summary>
   public struct RexColor
   {
      /// <summary>
      /// The red channel of the color.
      /// </summary>
      public byte R;

      /// <summary>
      /// The green channel of the color.
      /// </summary>
      public byte G;

      /// <summary>
      /// The blue channel of the color.
      /// </summary>
      public byte B;

      /// <summary>
      /// Creates a new RexPaint color with the specified RGB channels.
      /// </summary>
      /// <param name="r">The red channel of the color.</param>
      /// <param name="g">The green channel of the color.</param>
      /// <param name="b">The blue channel of the color.</param>
      public RexColor( byte r, byte g, byte b )
      {
         R = r;
         G = g;
         B = b;
      }

      public static bool operator ==( RexColor left, RexColor right )
      {
         return left.R == right.R && left.G == right.G && left.B == right.B;
      }

      public static bool operator !=( RexColor left, RexColor right )
      {
         return left.R != right.R || left.G != right.G || left.B != right.B;
      }

      public bool Equals( RexColor other )
      {
         return R == other.R && G == other.G && B == other.B;
      }

      public override bool Equals( object obj )
      {
         return obj is RexColor other && Equals( other );
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = R.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ G.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ B.GetHashCode();
            return hashCode;
         }
      }

      /// <summary>
      /// Returns the transparent color used by RexPaint: rgb(255, 0, 255).
      /// </summary>
      public static RexColor Transparent
      {
         get
         {
            return new RexColor( 255, 0, 255 );
         }
      }
   }
}
