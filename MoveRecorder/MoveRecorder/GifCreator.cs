using SixLabors.ImageSharp.Drawing.Processing;
using static System.Windows.Forms.Design.AxImporter;
using Brushes = System.Drawing.Brushes;
using Color = SixLabors.ImageSharp.Color;
using Image = SixLabors.ImageSharp.Image;
using Pens = SixLabors.ImageSharp.Drawing.Processing.Pens;
using PointF = SixLabors.ImageSharp.PointF;
using SystemFonts = SixLabors.Fonts.SystemFonts;

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
		using var gif = images[0];
		var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();
		metadata.FrameDelay = frameDelay;

		var frameCount = 1;
		foreach (var image in images.Skip(1))
		{
			// Set the meta data frame delay to ensure the desired FPS.
			metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
			metadata.FrameDelay = frameDelay;
			var text = frameCount.ToString();

			// Draws the text with horizontal red and blue hatching with a dash dot pattern outline.
			image.Mutate(x => x.DrawText(text, SystemFonts.Get("Arial").CreateFont(50), Color.Green, new PointF()
			{
				Y = 10,
				X = 10
			}));

			// Add the color image to the gif.
			gif.Frames.AddFrame(image.Frames.RootFrame);
			image.Dispose();
			frameCount++;
		}

		gif.SaveAsGif(fileName);
	}
}
