using System;

namespace SadRex
{
   /// <summary>
   /// A layer of a RexPaint image.
   /// </summary>
   public class RexLayer
   {
      private readonly RexCell[] _cells;

      /// <summary>
      /// The width of the layer.
      /// </summary>
      public int Width
      {
         get; private set;
      }

      /// <summary>
      /// The height of the layer.
      /// </summary>
      public int Height
      {
         get; private set;
      }

      /// <summary>
      /// Represents all cells of the layer.
      /// </summary>
      public System.Collections.ObjectModel.ReadOnlyCollection<RexCell> Cells
      {
         get
         {
            return new System.Collections.ObjectModel.ReadOnlyCollection<RexCell>( _cells );
         }
      }

      /// <summary>
      /// Gets a cell by coordinates.
      /// </summary>
      /// <param name="x">The x (0-based) position of the cell.</param>
      /// <param name="y">The y (0-based) position of the cell.</param>
      /// <returns>The cell.</returns>
      public RexCell this[int x, int y]
      {
         get
         {
            CheckForBounds( x, y );
            return _cells[( y * Width ) + x];
         }
         set
         {
            CheckForBounds( x, y );
            _cells[( y * Width ) + x] = value;
         }
      }

      /// <summary>
      /// Gets a cell by index.
      /// </summary>
      /// <param name="index">The index of the cell.</param>
      /// <returns>The cell.</returns>
      public RexCell this[int index]
      {
         get
         {
            CheckForIndexBounds( index );
            return _cells[index];
         }
         set
         {
            CheckForIndexBounds( index );
            _cells[index] = value;
         }
      }

      /// <summary>
      /// Creates a new layer with the specified width and height.
      /// </summary>
      /// <param name="width">The width of the layer.</param>
      /// <param name="height">The height of the layer.</param>
      public RexLayer( int width, int height )
      {
         Width = width;
         Height = height;
         _cells = new RexCell[width * height];
      }

      private void CheckForIndexBounds( int index )
      {
         if ( index < 0 || index >= _cells.Length )
         {
            throw new IndexOutOfRangeException();
         }
      }

      private void CheckForBounds( int x, int y )
      {
         if ( x < 0 || x >= Width )
         {
            throw new ArgumentOutOfRangeException( "x" );
         }

         if ( y < 0 || y >= Height )
         {
            throw new ArgumentOutOfRangeException( "y" );
         }
      }
   }
}
