using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ES2UserType_SystemReflectionAmbiguousMatchException : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		System.Reflection.AmbiguousMatchException data = (System.Reflection.AmbiguousMatchException)obj;
		// Add your writer.Write calls here.

	}
	
	public override object Read(ES2Reader reader)
	{
		System.Reflection.AmbiguousMatchException data = new System.Reflection.AmbiguousMatchException();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		System.Reflection.AmbiguousMatchException data = (System.Reflection.AmbiguousMatchException)c;
		// Add your reader.Read calls here to read the data into the object.

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_SystemReflectionAmbiguousMatchException():base(typeof(System.Reflection.AmbiguousMatchException)){}
}