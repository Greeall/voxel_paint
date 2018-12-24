using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_Datas : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Datas data = (Datas)obj;
		// Add your writer.Write calls here.
		writer.Write(data._colors);
		writer.Write(data._model);
		writer.Write(data._isBeginning);
		writer.Write(data._isFinished);
		writer.Write(data._mainPosition);
		writer.Write(data._mainRotation);
		writer.Write(data._mainZoom);
		writer.Write(data._isVip);

	}
	
	public override object Read(ES2Reader reader)
	{
		Datas data = new Datas();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Datas data = (Datas)c;
		// Add your reader.Read calls here to read the data into the object.
		data._colors = reader.ReadList<VoxelColor>();
		data._model = reader.ReadList<Layer>();
		data._isBeginning = reader.Read<System.Boolean>();
		data._isFinished = reader.Read<System.Boolean>();
		data._mainPosition = reader.Read<UnityEngine.Vector3>();
		data._mainRotation = reader.Read<UnityEngine.Vector3>();
		data._mainZoom = reader.Read<System.Single>();
		data._isVip = reader.Read<System.Boolean>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_Datas():base(typeof(Datas)){}
}