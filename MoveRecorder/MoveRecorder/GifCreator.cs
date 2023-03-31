using SixLabors.ImageSharp.Formats.Gif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = SixLabors.ImageSharp.Image;

namespace MoveRecorder;
public class GifCreator
{
	public void Create(string fileName, List<Bitmap> frames, Screen screen)
	{
		// For the frame delay, it is usual to be around 5 FPS
		const int frameDelay = 60 / 5;

		var images = new List<Image>();
		foreach (var frame in frames)
		{
			using var stream = new MemoryStream();
			frame.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
			images.Add(Image.Load(stream.ToArray()));
		}

		var gif = images[0];

		var gifMetaData = gif.Metadata.GetGifMetadata();
		var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();
		metadata.FrameDelay = frameDelay;

		foreach (var image in images.Skip(1))
		{
			metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
			metadata.FrameDelay = frameDelay;

			// Add the color image to the gif.
			gif.Frames.AddFrame(image.Frames.RootFrame);
		}

		gif.SaveAsGif(fileName);
	}
}
