using Xamarin.Forms;

namespace CrossCardViewSample.CustomCardView
{
    public class CustomCard : ContentView
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create<CustomCard, float>(p => p.CornerRadius, 3.0F);
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create<CustomCard, Color>(p => p.BackgroundColor, Color.White);

        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            if (Content == null)
                return new SizeRequest(new Size(100, 100));

            return Content.GetSizeRequest(widthConstraint, heightConstraint);
        }
    }
}
