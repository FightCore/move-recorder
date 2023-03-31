using Image = SixLabors.ImageSharp.Image;

namespace MoveRecorder;
public class GifCreator
{
	public void Create(string fileName, List<Bitmap> frames)
	{
		// For the frame delay, it is usual to be around 5 FPS
		const int frameDelay = 60 / 5;

		// ImageSharp works with their own Image format, we need to convert the used bitmaps to their format to make it work.
		// For this, we need to load the bitmap into a memory stream to get the array of data.
		// Then load that array into the image and store it.
		var images = new List<Image>();
		foreach (var frame in frames)
		{
			using var stream = new MemoryStream();
			frame.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
			images.Add(Image.Load(stream.ToArray()));
		}

		// We take the first image and call it our root, all other images will be appended as frames.
		var gif = images[0];
		var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();
		metadata.FrameDelay = frameDelay;

		foreach (var image in images.Skip(1))
		{
			// Set the meta data frame delay to ensure the desired FPS.
			metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
			metadata.FrameDelay = frameDelay;

			// Add the color image to the gif.
			gif.Frames.AddFrame(image.Frames.RootFrame);
		}

		gif.SaveAsGif(fileName);
	}
}
