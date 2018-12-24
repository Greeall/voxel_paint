using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_VoxelPlatform : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		VoxelPlatform data = (VoxelPlatform)obj;
		// Add your writer.Write calls here.
		writer.Write(data.position);
		writer.Write(data.voxelColor);
		writer.Write(data.isDrawing);

	}
	
	public override object Read(ES2Reader reader)
	{
		VoxelPlatform data = new VoxelPlatform();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		VoxelPlatform data = (VoxelPlatform)c;
		// Add your reader.Read calls here to read the data into the object.
		data.position = reader.Read<UnityEngine.Vector2>();
		data.voxelColor = reader.Read<System.Int32>();
		data.isDrawing = reader.Read<System.Boolean>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_VoxelPlatform():base(typeof(VoxelPlatform)){}
}