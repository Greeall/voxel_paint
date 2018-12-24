using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_Layer : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Layer data = (Layer)obj;
		// Add your writer.Write calls here.
		writer.Write(data.layer);
		writer.Write(data.isDrawing);

	}
	
	public override object Read(ES2Reader reader)
	{
		Layer data = new Layer();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Layer data = (Layer)c;
		// Add your reader.Read calls here to read the data into the object.
		data.layer = reader.ReadList<VoxelPlatform>();
		data.isDrawing = reader.Read<System.Boolean>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_Layer():base(typeof(Layer)){}
}