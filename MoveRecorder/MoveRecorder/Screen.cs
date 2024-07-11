using System.Drawing.Imaging;
using Serilog;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace MoveRecorder
{
	public class Frame
	{
		private readonly int _width;
		private readonly int _height;
		private readonly int _left;
		private readonly int _top;
		private readonly Size _size;

		public Frame(int width, int height, int left, int top, Size size)
		{
			_width = width;
			_height = height;
			_left = left;
			_top = top;
			_size = size;
		}

		public void Save(string fileName)
		{
			var bitmap = new Bitmap(_width, _height);
			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(new Point(_left, _top), Point.Empty,
					_size);
			}

			bitmap.Save(fileName, ImageFormat.Png);
			Log.Information("Written file to {Path}", fileName);
		}
	}
}
