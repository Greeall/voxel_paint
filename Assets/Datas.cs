using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas {

	public List<VoxelColor> _colors;
	public List<Layer> _model;

	public Datas(List<VoxelColor> _c, List<Layer> _m)
	{
		_model = _m;
		_colors = _c;
	}

    public bool isExist()
	{
		if(this._colors.Count >= 1 && this._model.Count >= 1)
			return true;
		return false;
		
	}

}
