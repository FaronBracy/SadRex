using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SadRex
{
    /// <summary>
    /// A RexPaint image.
    /// </summary>
    public class Image
    {
        private List<RexLayer> layers;

        /// <summary>
        /// The version of RexPaint that created this image.
        /// </summary>
        public int Version { get; private set; } = 1;

        /// <summary>
        /// The width of the image.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of the image.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The total number of layers for this image.
        /// </summary>
        public int LayerCount { get { return layers.Count; } }

        /// <summary>
        /// A read-only collection of layers.
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<RexLayer> Layers { get { return new System.Collections.ObjectModel.ReadOnlyCollection<RexLayer>(layers); } }

        /// <summary>
        /// Creates a new RexPaint image.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        public Image(int width, int height)
        {
            Width = width;
            Height = height;
            layers = new List<RexLayer>();
            Create();
        }

        /// <summary>
        /// Creates a new layer for the image adding it to the end of the layer stack.
        /// </summary>
        /// <returns>A new layer.</returns>
        public RexLayer Create()
        {
            var layer = new RexLayer(Width, Height);
            layers.Add(layer);
            return layer;
        }

        /// <summary>
        /// Creates a new layer for the image and inserts it at the specified position (0-based).
        /// </summary>
        /// <param name="index">The position to create the new layer at.</param>
        /// <returns>A new layer.</returns>
        public RexLayer Create(int index)
        {
            var layer = new RexLayer(Width, Height);
            layers.Insert(index, layer);
            return layer;
        }

        /// <summary>
        /// Adds an existing layer (must be the same width/height) to the image.
        /// </summary>
        /// <param name="rexLayer">The layer to add.</param>
        public void Add(RexLayer rexLayer)
        {
            layers.Add(rexLayer);
        }

        /// <summary>
        /// Adds an existing layer (must be the same width/height) to the image and inserts it at the specified position (0-based).
        /// </summary>
        /// <param name="rexLayer">The layer to add.</param>
        /// <param name="index">The position to add the layer.</param>
        public void Add(RexLayer rexLayer, int index)
        {
            layers.Insert(index, rexLayer);
        }

        /// <summary>
        /// Removes the specified layer.
        /// </summary>
        /// <param name="rexLayer">The layer.</param>
        public void Remove(RexLayer rexLayer)
        {
            layers.Remove(rexLayer);
        }

        /// <summary>
        /// Loads a .xp RexPaint image from a GZip compressed stream.
        /// </summary>
        /// <param name="stream">The GZip stream to load.</param>
        /// <returns>The RexPaint image.</returns>
        public static Image Load(Stream stream)
        {
            var deflatedStream = new GZipStream(stream, CompressionMode.Decompress);

            using (var reader = new BinaryReader(deflatedStream))
            {
                var version = reader.ReadInt32();
                var layerCount = reader.ReadInt32();
                Image image = null;

                for (int currentLayer = 0; currentLayer < layerCount; currentLayer++)
                {
                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();

                    RexLayer rexLayer = null;

                    if (currentLayer == 0)
                    {
                        image = new Image(width, height);
                        rexLayer = image.layers[0];
                    }
                    else
                        rexLayer = image.Create();

                    // Process cells (could probably be streamlined into index processing instead of x,y...
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {

                            var cell = new RexCell(reader.ReadInt32(),                                                  // character
                                                new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte()),  // foreground
                                                new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte())); // background

                            rexLayer[x, y] = cell;
                        }
                    }
                }

                return image;
            }
        }
    }
}
