using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class Painter : MonoBehaviour {

	public void PaintWithDot() {
		m_currentBrush = BrushType.Dot;
	}

	public void PaintWithSolidLine() {
		m_currentBrush = BrushType.SolidLine;
	}

	public void PaintWithDottedLine() {
		m_currentBrush = BrushType.DottedLine;
	}

	public void Undo() {
		if (m_dirtyTrack.Count != 0) {
			Destroy(m_dirtyTrack.Pop());
		}
	}

	[SerializeField] Camera m_renderCamera;
	[SerializeField] LineRenderer m_linePrefab;
	[SerializeField] GameObject m_dotPrefab;
	[SerializeField] Material m_solidLinePattern;
	[SerializeField] Material m_dottedLinePattern;

	enum BrushType {
		Dot, SolidLine, DottedLine
	}
	[SerializeField] BrushType m_currentBrush = BrushType.SolidLine;

	LineRenderer m_currentLine = null;
	Stack<GameObject> m_dirtyTrack = new Stack<GameObject>();

	void Update() {
		if (m_currentBrush == BrushType.Dot) {
			if (Input.GetMouseButtonDown(0)) {
				GameObject dot = Instantiate(m_dotPrefab);
				dot.SetActive(true);
				dot.transform.position = ScreenPointToTransformPoint(Input.mousePosition);
				m_dirtyTrack.Push(dot);
			}
		} else {
			Material pattern;
			if (m_currentBrush == BrushType.SolidLine) {
				pattern = m_solidLinePattern;
			} else if (m_currentBrush == BrushType.DottedLine) {
				pattern = m_dottedLinePattern;
			} else {
				pattern = null;
			}
			if (Input.GetMouseButtonDown(0)) {
				m_currentLine = Instantiate(m_linePrefab);
				m_currentLine.sharedMaterial = pattern;
				m_currentLine.gameObject.SetActive(true);
				Vector2 startingPosition = ScreenPointToTransformPoint(Input.mousePosition);
				m_currentLine.SetPositions(new Vector3[] { startingPosition, startingPosition });
			}
			if (Input.GetMouseButtonUp(0)) {
				m_dirtyTrack.Push(m_currentLine.gameObject);
				m_currentLine = null;
			}
			if (m_currentLine) {
				m_currentLine.SetPosition(1, ScreenPointToTransformPoint(Input.mousePosition));
			}
		}
	}

	Vector3 ScreenPointToTransformPoint(Vector3 screenPoint) {
		Ray ray = m_renderCamera.ScreenPointToRay(screenPoint);
		float dist;
		new Plane(transform.forward, transform.position).Raycast(ray, out dist);
		return ray.GetPoint(dist);
	}
}