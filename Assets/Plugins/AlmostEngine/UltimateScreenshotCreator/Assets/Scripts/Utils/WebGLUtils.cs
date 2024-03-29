﻿#if UNITY_WEBGL


using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace AlmostEngine.Screenshot
{
		public class WebGLUtils
		{

				[DllImport ("__Internal")]
				private static extern void _ExportImage (string data, string file, string format);


				public static void ExportImage (byte[] data, string file, string format)
				{
						// Convert the image data to 64bits string and call the native javascript plugin
						_ExportImage(System.Convert.ToBase64String(data), file, format);
				}

		}
}

#endif



