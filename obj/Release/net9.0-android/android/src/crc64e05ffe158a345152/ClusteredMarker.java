package crc64e05ffe158a345152;


public class ClusteredMarker
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.clustering.ClusterItem
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getPosition:()Lcom/google/android/gms/maps/model/LatLng;:GetGetPositionHandler:Android.Gms.Maps.Utils.Clustering.IClusterItemInvoker, Google.Maps.Utils.Android\n" +
			"n_getSnippet:()Ljava/lang/String;:GetGetSnippetHandler:Android.Gms.Maps.Utils.Clustering.IClusterItemInvoker, Google.Maps.Utils.Android\n" +
			"n_getTitle:()Ljava/lang/String;:GetGetTitleHandler:Android.Gms.Maps.Utils.Clustering.IClusterItemInvoker, Google.Maps.Utils.Android\n" +
			"n_getZIndex:()Ljava/lang/Float;:GetGetZIndexHandler:Android.Gms.Maps.Utils.Clustering.IClusterItemInvoker, Google.Maps.Utils.Android\n" +
			"";
		mono.android.Runtime.register ("Maui.GoogleMaps.Clustering.Platforms.Android.ClusteredMarker, Maui.GoogleMaps.Clustering", ClusteredMarker.class, __md_methods);
	}

	public ClusteredMarker ()
	{
		super ();
		if (getClass () == ClusteredMarker.class) {
			mono.android.TypeManager.Activate ("Maui.GoogleMaps.Clustering.Platforms.Android.ClusteredMarker, Maui.GoogleMaps.Clustering", "", this, new java.lang.Object[] {  });
		}
	}

	public com.google.android.gms.maps.model.LatLng getPosition ()
	{
		return n_getPosition ();
	}

	private native com.google.android.gms.maps.model.LatLng n_getPosition ();

	public java.lang.String getSnippet ()
	{
		return n_getSnippet ();
	}

	private native java.lang.String n_getSnippet ();

	public java.lang.String getTitle ()
	{
		return n_getTitle ();
	}

	private native java.lang.String n_getTitle ();

	public java.lang.Float getZIndex ()
	{
		return n_getZIndex ();
	}

	private native java.lang.Float n_getZIndex ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
