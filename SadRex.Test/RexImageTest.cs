using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SadRex.Test
{
   [TestClass]
   public class RexImageTest
   {
      [TestMethod]
      public void Load_Valid2x2XpFile_ReturnsCorrectImage()
      {
         using ( FileStream fs = File.OpenRead( "./Images/2x2.xp" ) )
         {
            RexImage rexImage = RexImage.Load( fs );
            Assert.AreEqual( 2, rexImage.Height );
            Assert.AreEqual( 2, rexImage.Width );
         }
      }

      [TestMethod]
      public void ToolGen_Valid2x2XpFile_ReturnsCorrectImage()
      {
         using ( FileStream fs = File.OpenRead( "./Images/2x2.xp" ) )
         {
            RexImage rexImage = RexImage.Load( fs );
            ToolGen toolGen = new ToolGen( rexImage );
            string gen = toolGen.GenForUnity();
            File.WriteAllText( "C:\\Github\\SadRex\\SadRex.Test\\Generated.cs", gen );
         }
      }
   }

   public class ToolGen
   {
      private readonly RexImage _rexImage;

      public ToolGen( RexImage rexImage )
      {
         _rexImage = rexImage;
      }

      public string GenForUnity()
      {
         RexTokens tokens = Tokenize();
         StringBuilder sb = new StringBuilder();
         sb.AppendLine( "using System;" );
         sb.AppendLine( "using SadRex;" );
         sb.AppendLine( "namespace Rex.AutoGen" );
         sb.AppendLine( "{" );

         sb.AppendLine( "public static class Palette" );
         sb.AppendLine( "{" );
         for ( int i = 0; i < tokens.UniqueColors.Count; i++ )
         {
            RexColor tokenColor = tokens.UniqueColors[i];
            sb.AppendLine( $"public static RexColor Replace_{i}_Color = new RexColor( {tokenColor.R}, {tokenColor.G}, {tokenColor.B} );" );
         }
         sb.AppendLine( "}" );

         sb.AppendLine( "public class RexTile" );
         sb.AppendLine( "{" );
         for ( int i = 0; i < tokens.UniqueCells.Count; i++ )
         {
            sb.AppendLine( $"public static readonly RexTile Replace_{i} = new Replace_{i}RexTile();" );
         }

         sb.AppendLine( "public virtual string Name => throw new NotImplementedException();" );
         sb.AppendLine( "public virtual int Character => throw new NotImplementedException();" );
         sb.AppendLine( "public virtual RexColor Foreground => throw new NotImplementedException();" );
         sb.AppendLine( "public virtual RexColor Background => throw new NotImplementedException();" );

         for ( int i = 0; i < tokens.UniqueCells.Count; i++ )
         {
            RexCell tokenCell = tokens.UniqueCells[i];
            sb.AppendLine( $"private class Replace_{i}RexTile : RexTile" );
            sb.AppendLine( "{" );
            sb.AppendLine( $"public override string Name => \"Replace_{i}\";" );
            sb.AppendLine( $"public override int Character => {tokenCell.Character};" );
            sb.AppendLine( $"public override RexColor Foreground => new RexColor( {tokenCell.Foreground.R}, {tokenCell.Foreground.G}, {tokenCell.Foreground.B} );" );
            sb.AppendLine( $"public override RexColor Background => new RexColor( {tokenCell.Background.R}, {tokenCell.Background.G}, {tokenCell.Background.B} );" );
            sb.AppendLine( "}" );
         }
         sb.AppendLine( "}" );
         sb.AppendLine( "}" );
         return sb.ToString();
      }

      private RexTokens Tokenize()
      {
         RexTokens tokens = new RexTokens();
         HashSet<RexCell> uniqueCells = new HashSet<RexCell>();
         HashSet<RexColor> uniqueColors = new HashSet<RexColor>();
         HashSet<int> uniqueCharacterCodes = new HashSet<int>();

         foreach ( RexCell cell in _rexImage.Layers[0].Cells )
         {
            uniqueCells.Add( cell );
            uniqueColors.Add( cell.Background );
            uniqueColors.Add( cell.Foreground );
            uniqueCharacterCodes.Add( cell.Character );
         }

         tokens.UniqueCells = uniqueCells.ToList();
         tokens.UniqueColors = uniqueColors.ToList();
         tokens.UniqueCharacterCodes = uniqueCharacterCodes.ToList();

         return tokens;
      }
   }

   public class RexTokens
   {
      public RexTokens()
      {
         UniqueCells = new List<RexCell>();
         UniqueColors = new List<RexColor>();
         UniqueCharacterCodes = new List<int>();
      }

      public List<RexCell> UniqueCells
      {
         get;
         set;
      }

      public List<RexColor> UniqueColors
      {
         get;
         set;
      }

      public List<int> UniqueCharacterCodes
      {
         get;
         set;
      }
   }
}
