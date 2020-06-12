using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SadRex.Test
{
   [TestClass]
   public class ImageTest
   {
      [TestMethod]
      public void Load_Valid2x2XpFile_ReturnsCorrectImage()
      {
         using ( FileStream fs = File.OpenRead( "./Images/2x2.xp" ) )
         {
            Image image = Image.Load( fs );
            Assert.AreEqual( 2, image.Height );
            Assert.AreEqual( 2, image.Width );
         }
      }
   }
}
