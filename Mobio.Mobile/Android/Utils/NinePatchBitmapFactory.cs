using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Linq.Expressions;
using System.Collections;
using System.Globalization;
using System.Reflection;
using Java.Nio;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace OneBuilder.Mobile.Droid.Utils
{
	public static class NinePatchBitmapFactory
	{
		// The 9 patch segment is not a solid color.
		private const int NO_COLOR = 0x00000001;

		// The 9 patch segment is completely transparent.
		private const int TRANSPARENT_COLOR = 0x00000000;

		public static NinePatchDrawable createNinePathWithCapInsets(Resources res, Bitmap bitmap, int top, int left, int bottom, int right, String srcName)
		{
			ByteBuffer buffer = GetByteBuffer(top, left, bottom, right);
			buffer.Rewind();
			var bytes = new byte[56];
			buffer.Get(bytes);
			NinePatchDrawable drawable = new NinePatchDrawable(res, bitmap, bytes, new Rect(), srcName);
			return drawable;
		}

		//public static NinePatch CreateNinePatch(Resources res, Bitmap bitmap, int top, int left, int bottom, int right, String srcName)
		//{
		//	ByteBuffer buffer = GetByteBuffer(top, left, bottom, right);
		//	NinePatch patch = new NinePatch(bitmap, buffer.ToArray<byte>(), srcName);
		//	return patch;
		//}


		private static ByteBuffer GetByteBuffer(int top, int left, int bottom, int right)
		{
			//Docs check the NinePatchChunkFile
			ByteBuffer buffer = ByteBuffer.Allocate(56).Order(ByteOrder.NativeOrder());
			//was translated
			buffer.Put((sbyte)0x01);
			//divx size
			buffer.Put((sbyte)0x02);
			//divy size
			buffer.Put((sbyte)0x02);
			//color size
			buffer.Put((sbyte)0x02);

			//skip
			buffer.PutInt(0);
			buffer.PutInt(0);

			//padding
			buffer.PutInt(0);
			buffer.PutInt(0);
			buffer.PutInt(0);
			buffer.PutInt(0);

			//skip 4 bytes
			buffer.PutInt(0);

			buffer.PutInt(left);
			buffer.PutInt(right);
			buffer.PutInt(top);
			buffer.PutInt(bottom);
			buffer.PutInt(NO_COLOR);
			buffer.PutInt(NO_COLOR);

			return buffer;
		}
	}
}