using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_VoxelColor : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		VoxelColor data = (VoxelColor)obj;
		// Add your writer.Write calls here.
		writer.Write(data.color);
		writer.Write(data.number);

	}
	
	public override object Read(ES2Reader reader)
	{
		VoxelColor data = new VoxelColor();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		VoxelColor data = (VoxelColor)c;
		// Add your reader.Read calls here to read the data into the object.
		data.color = reader.Read<UnityEngine.Color>();
		data.number = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_VoxelColor():base(typeof(VoxelColor)){}
}