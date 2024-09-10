using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PropAnimation : MonoBehaviour
{

    [SerializeField] private AnimationClip[] _animations;
    [SerializeField] private UnityEvent<string> _onComplete;

    private int currentAnimation;
    private SpriteRenderer _spriteRenderer;
    private int _currentFrameIndex;
    private float _nextFrameTime;
    private bool _isPlaying = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartAnimation();
    }

    private void StartAnimation()
    {
        _currentFrameIndex = 0;
        _isPlaying = true;
        _nextFrameTime = Time.time + _animations[currentAnimation].SecondsPerFrame;
    }

    public void SetClip(string spriteName)
    {
        for (var i=0;i < _animations.Length;i++)
        {
            if (_animations[i].Name == spriteName)
            {
                currentAnimation = i;
                
                StartAnimation();
                return;
            }
        }
        _isPlaying = false;
    }

    private void Update()
    {
        if (_nextFrameTime > Time.time) return;
        var clip = _animations[currentAnimation];
        if (_currentFrameIndex >= clip.Sprites.Length)
        {
            if (clip.Loop)
            {
                _currentFrameIndex = 0;
            }
            else
            {
                _isPlaying = clip.AllowNextClip;
                clip.OnComplete?.Invoke();
                _onComplete?.Invoke(clip.Name);
                if (clip.AllowNextClip)
                {
                    _currentFrameIndex = 0;
                    currentAnimation = (int) Mathf.Repeat(currentAnimation,_animations.Length);
                    StartAnimation();   
                }
            }
            return;
        }
        
        _spriteRenderer.sprite = clip.Sprites[_currentFrameIndex];
        _nextFrameTime += _animations[currentAnimation].SecondsPerFrame;
        _currentFrameIndex++;
    }
}

[Serializable] public class AnimationClip
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite[] _sprites;
    [Range(1,100)] [SerializeField] private int _frameRate;
    [SerializeField] private bool _loop;
    [SerializeField] private bool _allowNextClip;
    [SerializeField] private UnityEvent _onComplete;
    public string Name => _name;
    public Sprite[] Sprites => _sprites;
    public int FrameRate => _frameRate;
    public bool Loop => _loop;
    public bool AllowNextClip => _allowNextClip;
    public UnityEvent OnComplete => _onComplete;
    public float SecondsPerFrame => 1f / _frameRate;
}
