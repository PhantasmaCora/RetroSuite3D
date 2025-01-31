using UnityEngine;

namespace Assets.Scripts.Cam.Effects {
	[ExecuteInEditMode]
	[RequireComponent(typeof(UnityEngine.Camera))]
	[AddComponentMenu("Image Effects/Custom/Retro Pixel From Image")]
	public class RetroPixelFromImg : MonoBehaviour {
		[Header("Palette Source")]
		public Texture2D palSource;

		private Color[] colors;

		private Material m_material;
		private Shader shader;

		private Material material {
			get {
				if (m_material == null) {
					shader = Shader.Find("Oxysoft/RetroPixelMax");
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
		}

		private void Start() {
			colors = palSource.GetPixels();
		}

		public void OnRenderImage(RenderTexture src, RenderTexture dest) {
			if (material && colors.Length > 0) {
				material.SetInt("_ColorCount", colors.Length);
				material.SetColorArray("_Colors", colors);

				Graphics.Blit(src, dest, material);
			} else {
				Graphics.Blit(src, dest);
			}
		}

		private void OnDisable() {
			if (m_material)
				DestroyImmediate(m_material);
		}
	}
}