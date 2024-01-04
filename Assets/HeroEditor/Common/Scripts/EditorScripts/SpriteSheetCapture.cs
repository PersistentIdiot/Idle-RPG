using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Common;
using UnityEngine;

namespace Assets.HeroEditor.Common.Scripts.EditorScripts
{
    [RequireComponent(typeof(Camera))]
    public class SpriteSheetCapture : MonoBehaviour
    {
        private PawnModel m_PawnModel;

        public void Capture(List<CaptureOption> options, int frameSize, int frameCount, bool shadow)
        {
            StartCoroutine(CaptureFrames(options, frameSize, frameCount, shadow));
        }

        private IEnumerator CaptureFrames(List<CaptureOption> options, int frameSize, int frameCount, bool shadow)
        {
            m_PawnModel = FindObjectOfType<PawnModel>();
            m_PawnModel.LayerManager.Sprites[0].gameObject.SetActive(shadow);

            var clips = new Dictionary<string, List<Texture2D>>();

            foreach (var option in options)
            {
                m_PawnModel.Animator.SetInteger("State", (int) option.State);

                if (option.Action != null)
                {
                    m_PawnModel.Animator.SetTrigger(option.Action);
                }
                else
                {
                    m_PawnModel.Animator.ResetTrigger("Slash");
                    m_PawnModel.Animator.ResetTrigger("Jab");
                }

                m_PawnModel.Animator.SetBool("Action", option.Action != null);
                m_PawnModel.Animator.speed = 2;

                yield return new WaitForSeconds(0.1f);

                m_PawnModel.Animator.speed = 0;

                var upperClip = m_PawnModel.Animator.GetCurrentAnimatorClipInfo(0)[0].clip;
                var lowerClip = m_PawnModel.Animator.GetCurrentAnimatorClipInfo(1)[0].clip;
                
                for (var j = 0; j < frameCount; j++)
                {
                    var normalizedTime = (float) j / (frameCount - 1);
                    var expressionEvent = upperClip.events.Where(i => i.functionName == "SetExpression" && Mathf.Abs(i.time / upperClip.length - normalizedTime) <= 1f / (frameCount - 1))
                        .OrderBy(i => Mathf.Abs(i.time / upperClip.length - normalizedTime)).FirstOrDefault();

                    if (expressionEvent != null)
                    {
                        m_PawnModel.SetExpression(expressionEvent.stringParameter);
                    }

                    yield return ShowFrame(upperClip.name, lowerClip.name, normalizedTime);

                    var frame = CaptureFrame(frameSize, frameSize);
                    var animationName = option.Action ?? option.State.ToString();

                    if (clips.ContainsKey(animationName))
                    {
                        clips[animationName].Add(frame);
                    }
                    else
                    {
                        clips.Add(animationName, new List<Texture2D> { frame });
                    }
                }
            }

            m_PawnModel.SetState(CharacterState.Idle);
            m_PawnModel.Animator.speed = 1;

            var texture = CreateSheet(clips, frameSize, frameSize);

            yield return StandaloneFilePicker.SaveFile("Save as sprite sheet", "", "Character", "png", texture.EncodeToPNG(), (success, path) => { Debug.Log(success ? $"Saved as {path}" : "Error saving."); });
        }

        private IEnumerator ShowFrame(string upperClip, string lowerClip, float normalizedTime)
        {
            m_PawnModel.Animator.Play(upperClip, 0, normalizedTime);
            m_PawnModel.Animator.Play(lowerClip, 1, normalizedTime);

            yield return null;

            while (m_PawnModel.Animator.GetCurrentAnimatorClipInfo(0).Length == 0)
            {
                yield return null;
            }

            if (m_PawnModel.Animator.IsInTransition(1))
            {
                Debug.Log("IsInTransition");
            }
        }

        private Texture2D CaptureFrame(int width, int height)
        {
            var cam = GetComponent<Camera>();
            var renderTexture = new RenderTexture(width, height, 24);
            var texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);

            cam.targetTexture = renderTexture;
            cam.Render();
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            cam.targetTexture = null;
            RenderTexture.active = null;
            Destroy(renderTexture);

            return texture2D;
        }

        private Texture2D CreateSheet(Dictionary<string, List<Texture2D>> clips, int width, int height)
        {
            var texture = new Texture2D(clips.First().Value.Count * width, clips.Keys.Count * height);

            foreach (var clip in clips)
            {
                for (var i = 0; i < clip.Value.Count; i++)
                {
                    texture.SetPixels(i * width, clips.Keys.Reverse().ToList().IndexOf(clip.Key) * height, width, height, clip.Value[i].GetPixels());
                }
            }

            texture.Apply();

            return texture;
        }
    }

    public class CaptureOption
    {
        public CharacterState State;
        public string Action;

        public CaptureOption(CharacterState state, string action)
        {
            State = state;
            Action = action;
        }
    }
}