using CrossCardViewSample.CustomCardView;
using CrossCardViewSample.iOS.CustomAppleCard;
using CrossCardViewSample.iOS.CustomCardRenderer;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCard), typeof(AppleCustomCardRenderer))]
namespace CrossCardViewSample.iOS.CustomCardRenderer
{
    public class AppleCustomCardRenderer : AppleCardView, IVisualElementRenderer
    {
        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;

        public CustomCard TheView { get { return this.Element == null ? null : (CustomCard)Element; } }

        public VisualElementTracker Tracker { get; private set; }

        public VisualElementPackager Packager { get; private set; }

        public VisualElement Element { get; private set; }

        public UIView NativeView { get { return this as UIView; } }

        public UIViewController ViewController { get { return null; } }

        public void SetElement(VisualElement element)
        {
            var oldElement = this.Element;

            if (oldElement != null)
            {
                oldElement.PropertyChanged -= this.HandlePropertyChanged;
            }

            this.Element = element;

            if (this.Element != null)
            {
                this.Element.PropertyChanged += this.HandlePropertyChanged;
            }

            this.RemoveAllSubviews();
            this.Tracker = new VisualElementTracker(this);

            this.Packager = new VisualElementPackager(this);
            this.Packager.Load();

            this.SetContentPadding((int)TheView.Padding.Left, (int)TheView.Padding.Top, (int)TheView.Padding.Right, (int)TheView.Padding.Bottom);

            this.SetCardBackgroundColor(this.TheView.BackgroundColor.ToUIColor());

            if (ElementChanged != null)
            {
                this.ElementChanged(this, new VisualElementChangedEventArgs(oldElement, this.Element));
            }
        }

        public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            var size = UIViewExtensions.GetSizeRequest(this.NativeView, widthConstraint, heightConstraint, 44.0, 44.0);

            return size;
        }

        public void SetElementSize(Size size)
        {
            this.Element.Layout(new Rectangle(this.Element.X, this.Element.Y, size.Width, size.Height));
        }

        protected override void Dispose(bool disposing){}

        private void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
            {
                //Tracker.UpdateLayout ();
            }
            else if (
              e.PropertyName == CustomCard.WidthProperty.PropertyName ||
              e.PropertyName == CustomCard.HeightProperty.PropertyName ||
              e.PropertyName == CustomCard.XProperty.PropertyName ||
              e.PropertyName == CustomCard.YProperty.PropertyName ||
              e.PropertyName == CustomCard.CornerRadiusProperty.PropertyName)
            {
                this.Element.Layout(this.Element.Bounds);

                var radius = (this.Element as CustomCard).CornerRadius;
                var bound = this.Element.Bounds;
                this.DrawBorder(new CoreGraphics.CGRect(bound.X, bound.Y, bound.Width, bound.Height), (nfloat)radius);
            }
            else if (e.PropertyName == CustomCard.PaddingProperty.PropertyName)
            {
                SetContentPadding((int)TheView.Padding.Left, (int)TheView.Padding.Top, (int)TheView.Padding.Right, (int)TheView.Padding.Bottom);
            }
            else if (e.PropertyName == CustomCard.BackgroundColorProperty.PropertyName)
            {
                if (TheView.BackgroundColor != null)
                {
                    SetCardBackgroundColor(TheView.BackgroundColor.ToUIColor());
                }
            }
        }

        private void SetCardBackgroundColor(UIColor color)
        {
            this.BackgroundColor = color;
        }

        private void SetContentPadding(int left, int top, int right, int bottom) { }
    }

    internal static class Extensions
    {
        internal static void RemoveAllSubviews(this UIView super)
        {
            if (super == null)
            {
                return;
            }
            for (int i = 0; i < super.Subviews.Length; i++)
            {
                var subview = super.Subviews[i];
                subview.RemoveFromSuperview();
            }
        }
    }
}
