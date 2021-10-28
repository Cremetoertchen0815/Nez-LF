using FarseerPhysics.Common;
using Microsoft.Xna.Framework;

namespace Nez.Farseer
{
	public class FSCollisionBox : FSCollisionPolygon
	{
		float _x = 0f;
		float _y = 0f;
		float _width = 0.1f;
		float _height = 0.1f;


		public FSCollisionBox()
		{
		}


		public FSCollisionBox(float width, float height)
		{
			_width = width;
			_height = height;
			_verts = PolygonTools.CreateRectangle(FSConvert.DisplayToSim * _width / 2,
				FSConvert.DisplayToSim * _height / 2);
		}

		public FSCollisionBox(Rectangle rect)
		{
			_x = rect.X;
			_y = rect.Y;
			_width = rect.Width;
			_height = rect.Height;
			_verts = PolygonTools.CreateRectangle(FSConvert.DisplayToSim * _width / 2,
				FSConvert.DisplayToSim * _height / 2, new Vector2(FSConvert.DisplayToSim * _x, FSConvert.DisplayToSim * _y), 0F);
		}


		#region Configuration

		public FSCollisionBox SetSize(float width, float height)
		{
			_width = width;
			_height = height;
			_verts = PolygonTools.CreateRectangle(FSConvert.DisplayToSim * _width / 2,
				FSConvert.DisplayToSim * _height / 2, new Vector2(FSConvert.DisplayToSim * _x, FSConvert.DisplayToSim * _y), 0F);
			_areVertsDirty = true;
			RecreateFixture();
			return this;
		}
		public FSCollisionBox SetLocalOffset(float x, float y)
		{
			_x = x;
			_y = y;
			_verts = PolygonTools.CreateRectangle(FSConvert.DisplayToSim * _width / 2,
				FSConvert.DisplayToSim * _height / 2, new Vector2(FSConvert.DisplayToSim * _x, FSConvert.DisplayToSim * _y), 0F);
			_areVertsDirty = true;
			RecreateFixture();
			return this;
		}

		#endregion
	}
}