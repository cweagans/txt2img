using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace txt2img
{
	class MainClass
	{
		
		private string file_path = "";
		private Bitmap objBitmap = new Bitmap("base.png");
		private Graphics objGraphics;
		private Font objFont = new Font("Arial", 40, FontStyle.Bold, GraphicsUnit.Pixel);
		private SolidBrush objBrushForeColor = new SolidBrush(Color.FromArgb(0,0,0));
		private SolidBrush objBrushBackColor = new SolidBrush(Color.FromArgb(255,255,255));
		private string FileNameBase = "labelimage_";
		private string label1 = "";
		private string label2 = "";
		private string label3 = "";
		private string label4 = "";

		public static void Main (string[] args)
		{
			// Instanciate the class
			MainClass refToClass = new MainClass();
			
			// Read in the command line arguments (there should only be one)
			Console.WriteLine("INFO: Reading arguments");
			var i = 0;
			foreach(string arg in  args)
		    {
				// File path
				if (i == 0)
				{
					Console.WriteLine("INFO: filename = " + arg);
					refToClass.file_path = arg;
				}
				i++;
		    }
			
			Console.WriteLine ("INFO: Reading source text file");
			// Read in the valve numbers
			if (File.Exists(refToClass.file_path))
			{
				TextReader tr = new StreamReader(refToClass.file_path);
				string read;
				int slot_counter = 1;
				int file_counter = 1;
				
				// Loop through the lines in the file
				do
				{
				    read = tr.ReadLine();
				    Console.WriteLine("DEBUG: line = " + read);
					Console.WriteLine("DEBUG: File counter: " + file_counter);
					Console.WriteLine("DEBUG: Slot counter: " + slot_counter);
					
					if (slot_counter == 1) {
						refToClass.label1 = read;
						slot_counter++;
						continue;
					}

					if (slot_counter == 2) {
						refToClass.label2 = read;
						slot_counter++;
						continue;
					}

					if (slot_counter == 3) {
						refToClass.label3 = read;
						slot_counter++;
						continue;
					}
					
					if (slot_counter == 4) {
						refToClass.label4 = read;
						refToClass.writeImage(refToClass.FileNameBase + file_counter + ".png", refToClass.label1, refToClass.label2, refToClass.label3, refToClass.label4);
						slot_counter = 1;
						file_counter++;
						continue;
					}
					
					
				} while (read != null);
            	tr.Close();
			}
		}
		
		public bool writeImage (string filename, string field1, string field2, string field3, string field4)
		{
			// Instanciate the class
			MainClass refToClass = new MainClass();
			
			// Write Debug message to Console
			Console.WriteLine("DEBUG: " + filename + "," + field1 + "," + field2 + "," + field3);
			
			// Set the DPI of the bitmap
			refToClass.objBitmap.SetResolution(300, 300);
			
			// Create the graphics object
			refToClass.objGraphics = Graphics.FromImage(refToClass.objBitmap);
			refToClass.objGraphics.FillRectangle(refToClass.objBrushBackColor, 0, 0, 203, 406);
			refToClass.objGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			StringFormat strFormat = new StringFormat();
			strFormat.Alignment = StringAlignment.Center;
			strFormat.LineAlignment = StringAlignment.Center;
			refToClass.objGraphics.DrawString(field1, refToClass.objFont, refToClass.objBrushForeColor, new RectangleF(0,0,203,101), strFormat);
			refToClass.objGraphics.DrawString(field2, refToClass.objFont, refToClass.objBrushForeColor, new RectangleF(0,102,203,101), strFormat);
			refToClass.objGraphics.DrawString(field3, refToClass.objFont, refToClass.objBrushForeColor, new RectangleF(0,203,203,101), strFormat);
			refToClass.objGraphics.DrawString(field4, refToClass.objFont, refToClass.objBrushForeColor, new RectangleF(0,304,203,101), strFormat);
			refToClass.objBitmap.Save(filename, ImageFormat.Png);
			
			return true;
		}
	}
}

