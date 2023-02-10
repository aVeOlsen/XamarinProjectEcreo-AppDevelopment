﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Google.Android.Material.TextField;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinEcreo.Controls;
using XamarinEcreo.Droid.Renderers;

[assembly: ExportRenderer(typeof(FloatingLabelEntry), typeof(FloatingLabelEntryRenderer))]
namespace XamarinEcreo.Droid.Renderers
{
    public class FloatingLabelEntryRenderer : ViewRenderer<FloatingLabelEntry, TextInputLayout>, ITextWatcher
    {
        public FloatingLabelEntryRenderer(Context context) : base(context)
        {
        }

        private EditText EditText => Control.EditText;

        protected override TextInputLayout CreateNativeControl()
        {
            var textInputLayout = new TextInputLayout(Context);
            var editText = new EditText(Context);
            editText.SetTextSize(ComplexUnitType.Sp, (float)Element.FontSize);
            textInputLayout.AddView(editText);
            return textInputLayout;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
            {
                Control.Hint = Element.Placeholder;
            }
            else if (e.PropertyName == FloatingLabelEntry.HasErrorProperty.PropertyName)
            {
                if (!Element.HasError || string.IsNullOrEmpty(Element.ErrorMessage))
                {
                    EditText.Error = null;
                    Control.ErrorEnabled = false;
                }
                else
                {
                    Control.ErrorEnabled = true;
                    EditText.Error = Element.ErrorMessage;
                }
            }

        }


        protected override void OnElementChanged(ElementChangedEventArgs<FloatingLabelEntry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var textView = CreateNativeControl();
                textView.EditText.AddTextChangedListener(this);
                //textView.Focusable = true;
                //textView.HintEnabled = true;
                //textView.HintAnimationEnabled = true;
                //textView.EditText.ShowSoftInputOnFocus = true;

                SetNativeControl(textView);
            }

            Control.Hint = Element.Placeholder;
            EditText.Text = Element.Text;
        }

        void ITextWatcher.AfterTextChanged(IEditable @string)
        {
        }

        void ITextWatcher.BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
        }

        void ITextWatcher.OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if (string.IsNullOrEmpty(Element.Text) && s.Length() == 0)
            {
                return;
            }

           ((IElementController)Element)
             .SetValueFromRenderer(Entry.TextProperty, s.ToString());
        }


    }
}