using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEcreo.Behaviors;
using XamarinEcreo.Common;

namespace XamarinEcreo.Controls
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidatableEntry : ContentView
    {
        public static readonly BindableProperty LabelProperty = BindableProperty.CreateAttached
            ("Label", typeof(string), typeof(ValidatableEntry), string.Empty);

        public static readonly BindableProperty ValueProperty = BindableProperty.CreateAttached
            ("Value", typeof(string), typeof(ValidatableEntry), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.CreateAttached
            ("Placeholder", typeof(string), typeof(ValidatableEntry), string.Empty);
        public static readonly BindableProperty ValidationRuleProperty = BindableProperty.CreateAttached
            ("ValidationRule", typeof(IValidationRule<string>), typeof(ValidationBehavior), null);
        public ValidatableEntry()
        {
            InitializeComponent();
        }

        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }
        public string Value
        {
            get
            {
                return (string)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, Value);
            }
        }
        public IValidationRule<string> ValidationRule
        {
            get { return (IValidationRule<string>)GetValue(ValidationRuleProperty); }
            set { SetValue(ValidationRuleProperty, value); }
        }
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }


    }
}