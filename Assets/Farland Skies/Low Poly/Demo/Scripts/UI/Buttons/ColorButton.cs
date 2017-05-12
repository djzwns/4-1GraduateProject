﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Borodar.FarlandSkies.LowPoly
{
    public class ColorButton : MonoBehaviour
    {
        public ColorPicker ColorPicker;
        public ColorType SkyColorType;

        private Image _image;

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        public void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Start()
        {
            switch (SkyColorType)
            {
                case ColorType.Top:
                    _image.color = SkyboxController.Instance.TopColor;
                    break;
                case ColorType.Middle:
                    _image.color = SkyboxController.Instance.MiddleColor;
                    break;
                case ColorType.Bottom:
                    _image.color = SkyboxController.Instance.BottomColor;
                    break;
                case ColorType.StarsTint:
                    _image.color = SkyboxController.Instance.StarsTint;
                    break;
                case ColorType.SunTint:
                    _image.color = SkyboxController.Instance.SunTint;
                    break;
                case ColorType.MoonTint:
                    _image.color = SkyboxController.Instance.MoonTint;
                    break;
                case ColorType.CloudTint:
                    _image.color = SkyboxController.Instance.CloudsTint;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public void OnClick()
        {
            ColorPicker.ColorButton = this;
            ColorPicker.gameObject.SetActive(true);
        }

        public void ChangeColor(Color color)
        {
            _image.color = color;

            switch (SkyColorType)
            {
                case ColorType.Top:
                    SkyboxController.Instance.TopColor = color;
                    break;
                case ColorType.Middle:
                    SkyboxController.Instance.MiddleColor = color;
                    break;
                case ColorType.Bottom:
                    SkyboxController.Instance.BottomColor = color;
                    break;
                case ColorType.StarsTint:
                    SkyboxController.Instance.StarsTint = color;
                    break;
                case ColorType.SunTint:
                    color.a = SkyboxController.Instance.SunTint.a;
                    SkyboxController.Instance.SunTint = color;
                    break;
                case ColorType.MoonTint:
                    color.a = SkyboxController.Instance.MoonTint.a;
                    SkyboxController.Instance.MoonTint = color;
                    break;
                case ColorType.CloudTint:
                    SkyboxController.Instance.CloudsTint = color;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //---------------------------------------------------------------------
        // Nested
        //---------------------------------------------------------------------

        public enum ColorType
        {
            Top,
            Middle,
            Bottom,
            StarsTint,
            SunTint,
            MoonTint,
            CloudTint
        }
    }
}