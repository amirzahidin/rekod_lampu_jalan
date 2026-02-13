package crc64e05ffe158a345152;


public class ClusterRenderer
	extends com.google.maps.android.clustering.view.DefaultClusterRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBeforeClusterRendered:(Lcom/google/maps/android/clustering/Cluster;Lcom/google/android/gms/maps/model/MarkerOptions;)V:GetOnBeforeClusterRendered_Lcom_google_maps_android_clustering_Cluster_Lcom_google_android_gms_maps_model_MarkerOptions_Handler\n" +
			"n_onBeforeClusterItemRendered:(Lcom/google/maps/android/clustering/ClusterItem;Lcom/google/android/gms/maps/model/MarkerOptions;)V:GetOnBeforeClusterItemRendered_Lcom_google_maps_android_clustering_ClusterItem_Lcom_google_android_gms_maps_model_MarkerOptions_Handler\n" +
			"n_getBucket:(Lcom/google/maps/android/clustering/Cluster;)I:GetGetBucket_Lcom_google_maps_android_clustering_Cluster_Handler\n" +
			"n_getColor:(I)I:GetGetColor_IHandler\n" +
			"n_getClusterText:(I)Ljava/lang/String;:GetGetClusterText_IHandler\n" +
			"";
		mono.android.Runtime.register ("Maui.GoogleMaps.Clustering.Platforms.Android.ClusterRenderer, Maui.GoogleMaps.Clustering", ClusterRenderer.class, __md_methods);
	}

	public ClusterRenderer (android.content.Context p0, com.google.android.gms.maps.GoogleMap p1, com.google.maps.android.clustering.ClusterManager p2)
	{
		super (p0, p1, p2);
		if (getClass () == ClusterRenderer.class) {
			mono.android.TypeManager.Activate ("Maui.GoogleMaps.Clustering.Platforms.Android.ClusterRenderer, Maui.GoogleMaps.Clustering", "Android.Content.Context, Mono.Android:Android.Gms.Maps.GoogleMap, Xamarin.GooglePlayServices.Maps:Android.Gms.Maps.Utils.Clustering.ClusterManager, Google.Maps.Utils.Android", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}

	public void onBeforeClusterRendered (com.google.maps.android.clustering.Cluster p0, com.google.android.gms.maps.model.MarkerOptions p1)
	{
		n_onBeforeClusterRendered (p0, p1);
	}

	private native void n_onBeforeClusterRendered (com.google.maps.android.clustering.Cluster p0, com.google.android.gms.maps.model.MarkerOptions p1);

	public void onBeforeClusterItemRendered (com.google.maps.android.clustering.ClusterItem p0, com.google.android.gms.maps.model.MarkerOptions p1)
	{
		n_onBeforeClusterItemRendered (p0, p1);
	}

	private native void n_onBeforeClusterItemRendered (com.google.maps.android.clustering.ClusterItem p0, com.google.android.gms.maps.model.MarkerOptions p1);

	public int getBucket (com.google.maps.android.clustering.Cluster p0)
	{
		return n_getBucket (p0);
	}

	private native int n_getBucket (com.google.maps.android.clustering.Cluster p0);

	public int getColor (int p0)
	{
		return n_getColor (p0);
	}

	private native int n_getColor (int p0);

	public java.lang.String getClusterText (int p0)
	{
		return n_getClusterText (p0);
	}

	private native java.lang.String n_getClusterText (int p0);

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
