using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas {


	public List<VoxelColor> _colors;
	public List<Layer> _model;

	public bool _isBeginning;
	public bool _isFinished;

	public Vector3 _mainPosition;

	public Vector3 _mainRotation;

	public float _mainZoom;

	

	public Datas(List<VoxelColor> _c, List<Layer> _m, bool _isB, bool _isF, Vector3 _mPos, Vector3 _mRot, float zoom)
	{
		_model = _m;
		_colors = _c;
		_isBeginning = _isB;
		_isFinished = _isF;
		_mainPosition = _mPos;
		_mainRotation = _mRot;
		_mainZoom = zoom;
	}

    public bool isExist()
	{
		if(this._colors.Count >= 1 && this._model.Count >= 1)
			return true;
		return false;
	}

}
