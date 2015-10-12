using Android.App;
using Android.Content.PM;
using Android.OS;

namespace CrossCardViewSample.Droid
{
	[Activity (Label = "CrossCardViewSample", Icon = "@drawable/icon", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new CrossCardViewSample.App ());
		}
	}
}

