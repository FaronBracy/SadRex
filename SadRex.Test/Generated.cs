using System;
using SadRex;
public class RexTile
{
   public static readonly RexTile Replace_247 = new Replace_247RexTile();
   public static readonly RexTile Replace_5 = new Replace_5RexTile();
   public static readonly RexTile Replace_35 = new Replace_35RexTile();
   public static readonly RexTile Replace_249 = new Replace_249RexTile();
   public virtual string Name => throw new NotImplementedException();
   public virtual int Character => throw new NotImplementedException();
   public virtual RexColor Foreground => throw new NotImplementedException();
   public virtual RexColor Background => throw new NotImplementedException();
   private class Replace_247RexTile : RexTile
   {
      public override string Name => "Replace_247";
      public override int Character => 247;
      public override RexColor Foreground => new RexColor( 2, 202, 74 );
      public override RexColor Background => new RexColor( 0, 125, 23 );
   }
   private class Replace_5RexTile : RexTile
   {
      public override string Name => "Replace_5";
      public override int Character => 5;
      public override RexColor Foreground => new RexColor( 106, 23, 180 );
      public override RexColor Background => new RexColor( 0, 0, 0 );
   }
   private class Replace_35RexTile : RexTile
   {
      public override string Name => "Replace_35";
      public override int Character => 35;
      public override RexColor Foreground => new RexColor( 123, 123, 123 );
      public override RexColor Background => new RexColor( 52, 52, 52 );
   }
   private class Replace_249RexTile : RexTile
   {
      public override string Name => "Replace_249";
      public override int Character => 249;
      public override RexColor Foreground => new RexColor( 168, 168, 168 );
      public override RexColor Background => new RexColor( 0, 0, 0 );
   }
}
