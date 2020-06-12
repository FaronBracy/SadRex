using System.Collections.Generic;
using System.IO;
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
      private readonly HashSet<RexCell> _tokens;

      public ToolGen( RexImage rexImage )
      {
         _rexImage = rexImage;
         _tokens = new HashSet<RexCell>();
      }

      public string GenForUnity()
      {
         HashSet<RexCell> tokens = Tokenize();
         StringBuilder sb = new StringBuilder();
         sb.AppendLine( "using System;" );
         sb.AppendLine( "using SadRex;" );  
         sb.AppendLine( "public class RexTile" );
         sb.AppendLine( "{" );
         foreach ( RexCell token in tokens )
         {
            sb.AppendLine( $"public static readonly RexTile Replace_{token.Character} = new Replace_{token.Character}RexTile();" );
         }

         sb.AppendLine( "public virtual string Name => throw new NotImplementedException();" );
         sb.AppendLine( "public virtual int Character => throw new NotImplementedException();" );
         sb.AppendLine( "public virtual RexColor Foreground => throw new NotImplementedException();" );
         sb.AppendLine( "public virtual RexColor Background => throw new NotImplementedException();" );

         foreach ( RexCell token in tokens )
         {
            sb.AppendLine( $"private class Replace_{token.Character}RexTile : RexTile" );
            sb.AppendLine( "{" );
            sb.AppendLine( $"public override string Name => \"Replace_{token.Character}\";" );
            sb.AppendLine( $"public override int Character => {token.Character};" );
            sb.AppendLine( $"public override RexColor Foreground => new RexColor( {token.Foreground.R}, {token.Foreground.B}, {token.Foreground.G} );" );
            sb.AppendLine( $"public override RexColor Background => new RexColor( {token.Background.R}, {token.Background.B}, {token.Background.G} );" );
            sb.AppendLine( "}" );
         }
         sb.AppendLine( "}" );
         return sb.ToString();
      }

      private HashSet<RexCell> Tokenize()
      {
         foreach ( RexCell cell in _rexImage.Layers[0].Cells )
         {
            _tokens.Add( cell );
         }

         return _tokens;
      }
   }
}
