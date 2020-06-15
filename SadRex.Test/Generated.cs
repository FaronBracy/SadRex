using System;
using SadRex;
namespace Rex.AutoGen
{
   public static class Palette
   {
      public static RexColor Replace_0_Color = new RexColor( 0, 23, 125 );
      public static RexColor Replace_1_Color = new RexColor( 2, 74, 202 );
      public static RexColor Replace_2_Color = new RexColor( 0, 0, 0 );
      public static RexColor Replace_3_Color = new RexColor( 106, 180, 23 );
      public static RexColor Replace_4_Color = new RexColor( 52, 52, 52 );
      public static RexColor Replace_5_Color = new RexColor( 123, 123, 123 );
      public static RexColor Replace_6_Color = new RexColor( 168, 168, 168 );
   }
   public class RexTile
   {
      public static readonly RexTile Replace_0 = new Replace_0RexTile();
      public static readonly RexTile Replace_1 = new Replace_1RexTile();
      public static readonly RexTile Replace_2 = new Replace_2RexTile();
      public static readonly RexTile Replace_3 = new Replace_3RexTile();
      public virtual string Name => throw new NotImplementedException();
      public virtual int Character => throw new NotImplementedException();
      public virtual RexColor Foreground => throw new NotImplementedException();
      public virtual RexColor Background => throw new NotImplementedException();
      private class Replace_0RexTile : RexTile
      {
         public override string Name => "Replace_0";
         public override int Character => 247;
         public override RexColor Foreground => Palette.Replace_1_Color;
         public override RexColor Background => Palette.Replace_0_Color;
      }
      private class Replace_1RexTile : RexTile
      {
         public override string Name => "Replace_1";
         public override int Character => 5;
         public override RexColor Foreground => Palette.Replace_3_Color;
         public override RexColor Background => Palette.Replace_2_Color;
      }
      private class Replace_2RexTile : RexTile
      {
         public override string Name => "Replace_2";
         public override int Character => 35;
         public override RexColor Foreground => Palette.Replace_5_Color;
         public override RexColor Background => Palette.Replace_4_Color;
      }
      private class Replace_3RexTile : RexTile
      {
         public override string Name => "Replace_3";
         public override int Character => 249;
         public override RexColor Foreground => Palette.Replace_6_Color;
         public override RexColor Background => Palette.Replace_2_Color;
      }
   }
}
