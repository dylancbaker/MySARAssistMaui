namespace MySARAssist.Models.ValidationTools
{
    public class NonBlankValidationBehavior : BaseValidationBehaviour
    {


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = !string.IsNullOrEmpty(e.NewTextValue);
            //((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            ((Entry)sender).Style = IsValid ? ValidStyle : InvalidStyle;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
