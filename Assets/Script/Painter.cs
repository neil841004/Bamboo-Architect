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

	public void Clear() {
		while (m_dirtyTrack.Count != 0) {
			Destroy(m_dirtyTrack.Pop());
		}
	}

	[SerializeField] Camera m_renderCamera = null;
	[SerializeField] Collider[] m_inputReceivers = new Collider[0];
	[SerializeField] LineRenderer m_linePrefab = null;
	[SerializeField] GameObject m_dotPrefab = null;
	[SerializeField] Material m_solidLinePattern = null;
	[SerializeField] Material m_dottedLinePattern = null;

	enum BrushType {
		Dot, SolidLine, DottedLine
	}
	[SerializeField] BrushType m_currentBrush = BrushType.SolidLine;

	LineRenderer m_currentLine = null;
	Collider m_currentInputReceiver = null;
	Stack<GameObject> m_dirtyTrack = new Stack<GameObject>();

	void Update() {
		if (m_currentBrush == BrushType.Dot) {
			if (Input.GetMouseButtonDown(0)) {
				Vector3 position;
				if (ScreenPointToWorldPosition(Input.mousePosition, out position)) {
					GameObject dot = Instantiate(m_dotPrefab);
					dot.transform.position = position;
					dot.SetActive(true);
					m_dirtyTrack.Push(dot);
				}
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
				Vector3 position;
				if (m_currentInputReceiver = ScreenPointToWorldPosition(Input.mousePosition, out position)) {
					m_currentLine = Instantiate(m_linePrefab);
					m_currentLine.sharedMaterial = pattern;
					m_currentLine.gameObject.SetActive(true);
					m_currentLine.SetPositions(new Vector3[] {position, position});
				}
			}
			if (m_currentLine) {
				m_currentLine.SetPosition(1, ScreenPointToTransformPoint(Input.mousePosition, m_currentInputReceiver.transform));
				if (Input.GetMouseButtonUp(0)) {
					m_dirtyTrack.Push(m_currentLine.gameObject);
					m_currentLine = null;
					m_currentInputReceiver = null;
				}
			}
		}
	}

	Vector3 ScreenPointToTransformPoint(Vector3 screenPoint, Transform transform) {
		Ray ray = m_renderCamera.ScreenPointToRay(screenPoint);
		float dist;
		new Plane(transform.forward, transform.position).Raycast(ray, out dist);
		return ray.GetPoint(dist);
	}

	Collider ScreenPointToWorldPosition(Vector3 screenPoint, out Vector3 worldPosition) {
		Ray ray = m_renderCamera.ScreenPointToRay(screenPoint);
		for (int i = 0; i != m_inputReceivers.Length; ++i) {
			RaycastHit hitRlt;
			if (m_inputReceivers[i].Raycast(ray, out hitRlt, float.PositiveInfinity)) {
				worldPosition = hitRlt.point;
				return m_inputReceivers[i];
			}
		}
		worldPosition = default(Vector3);
		return null;
	}
}