import os
import subprocess


def create_avi_from_pngs(png_dir, output_file):
    # Check if the output file already exists and delete it if it does
    if os.path.exists(output_file):
        os.remove(output_file)

    # Run FFmpeg command to create AVI from PNGs
    subprocess.call(['ffmpeg', '-framerate', '24', '-i',
                    os.path.join(png_dir, 'frame%d.png'), output_file])


# Example usage
if __name__ == "__main__":
    png_directory = 'path_to_your_png_directory'
    output_file = 'output.avi'
    create_avi_from_pngs(png_directory, output_file)
