using UnityEngine;
using UnityEngine.Tilemaps;

namespace Codomaster.Extensions
{
    public static class TilemapExtensions
    {
        /// <summary>
        /// Copy tiles to another tilemap.
        /// </summary>
        /// <param name="sourceTilemap">Where to copy tiles.</param>
        /// <param name="targetTilemap">Where to paste tiles.</param>
        /// <returns>The target tilemap with copied tiles.</returns>
        public static Tilemap CopyTilesToTilemap(this Tilemap sourceTilemap, Tilemap targetTilemap)
        {
            foreach (var position in sourceTilemap.cellBounds.allPositionsWithin)
            {
                if (sourceTilemap.HasTile(position))
                {
                    var tile = sourceTilemap.GetTile(position);
                    targetTilemap.SetTile(position, tile);

                    // Tiles can have different rotations, so we apply the right matrix.
                    Matrix4x4 tileMatrix = sourceTilemap.GetTransformMatrix(position);
                    targetTilemap.SetTransformMatrix(position, tileMatrix);

                    Color tileColor = sourceTilemap.GetColor(position);
                    targetTilemap.SetColor(position, tileColor);
                }
            }

            return targetTilemap;
        }

        /// <summary>
        /// Flips the tilemap to the chosen direction..
        /// <br></br>
        /// Be careful with your properties as it changes 
        /// the <see cref="Tilemap.Orientation.Custom"/> value.
        /// </summary>
        /// <param name="tilemap">The tilemap to flip.</param>
        /// <param name="flipValue">Value used to rotate the tilemap.</param>
        public static void CustomFlip(this Tilemap tilemap, Vector3 flipValue)
        {
            tilemap.orientation = Tilemap.Orientation.Custom;
            tilemap.orientationMatrix = Matrix4x4.Scale(flipValue);
        }

        /// <summary>
        /// Flips the tilemap horizontally.
        /// </summary>
        /// <param name="tilemap">The tilemap to flip.</param>
        public static void FlipX(this Tilemap tilemap)
        {
            tilemap.CustomFlip(new Vector3(-1.0f, 1.0f, 1.0f));
        }

        /// <summary>
        /// Flips the tilemap vertically.
        /// </summary>
        /// <param name="tilemap">The tilemap to flip.</param>
        public static void FlipY(this Tilemap tilemap)
        {
            tilemap.CustomFlip(new Vector3(1.0f, -1.0f, 1.0f));
        }
    }
}
