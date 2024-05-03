using HeyRed.ImageSharp.AVCodecFormats;
using HeyRed.ImageSharp.AVCodecFormats.Mp4;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using Image = SixLabors.ImageSharp.Image;
using PointF = SixLabors.ImageSharp.PointF;
using SystemFonts = SixLabors.Fonts.SystemFonts;


namespace TestConversionApp
{
	internal class GifCreator
	{
		public void Create()
		{
			var configuration = new Configuration(new Mp4ConfigurationModule());
			var decoderOptions = new DecoderOptions
			{
				Configuration = configuration,
			};

			using var inputStream = File.OpenRead("C:\\Users\\bartd\\Videos\\Death.mp4");
			using var image = Image.Load(decoderOptions, inputStream);
			var size = image.Size;

			var resultWidth = size.Width;
			var resultHeight = size.Height;

			const int frameDelay = 1;

			var result = new Image<Rgba32>(resultWidth, resultHeight);
			var metadata = result.Frames.RootFrame.Metadata.GetGifMetadata();
			metadata.FrameDelay = frameDelay;

			for (int i = 0; i < image.Frames.Count; i++)
			{
				if (i % 2 == 0)
				{
					continue;
				}
				var frame = image.Frames.CloneFrame(i).CloneAs<Rgba32>();
				metadata = frame.Frames.RootFrame.Metadata.GetGifMetadata();
				metadata.FrameDelay = frameDelay;

				result.Frames
					.AddFrame(frame.Frames[0]);
			}

			result.SaveAsGif("C:\\Users\\bartd\\Videos\\Death.gif");
			//// Save all frames using png encoder


			//// For the frame delay, it is usual to be around 5 FPS
			//const int frameDelay = 60 / 5;


			//// We take the first image and call it our root, all other images will be appended as frames.

			//var metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();
			//metadata.FrameDelay = frameDelay;

			//var frameCount = 1;
			//for (var i = 0; i < image.Frames.Count; i++)
			//{
			//	// Set the meta data frame delay to ensure the desired FPS.
			//	metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
			//	metadata.FrameDelay = frameDelay;
			//	var text = frameCount.ToString();

			//	// Add the color image to the gif.
			//	gif.Frames.AddFrame(image.Frames.RootFrame);
			//	image.Dispose();
			//	frameCount++;
			//}


		}
	}
}
